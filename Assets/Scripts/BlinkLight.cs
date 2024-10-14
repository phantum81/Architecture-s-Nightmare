using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;
public class BlinkLight : MonoBehaviour
{
    [Header("IsBlink"),SerializeField]
    private bool isBlink = false;
    [Header("미니멈 랜덤 값"), SerializeField]
    private float randomMim = 0.4f;
    [Header("맥시멈 랜덤 값"), SerializeField]
    private float randomMax = 1f;
    private Light selfLight;
    private bool isTimeLine = false;
    private CancellationTokenSource cancellationTokenSource; // 취소 토큰 소스
    void Start()
    {
        selfLight = transform.GetComponent <Light>();

    }

    // Update is called once per frame
    void Update()
    {
        if(cancellationTokenSource == null)
        {
            StartBlinking();
        }

        if (!isBlink)
        {
            StopBlinking();
            if(!isTimeLine)
                selfLight.enabled = true;
        }


    }
          
    public void SetLight(bool _set)
    {
        isTimeLine = true;
        selfLight.enabled = _set;
    }


    private async UniTask Blink(CancellationToken _token)
    {
        float random = Random.Range(randomMim, randomMax);
        if(randomMim < 0)
        {
            Debug.LogWarning("미니멈값이 0이하입니다.");
            return;
        }
        while (isBlink)
        {
            random = Random.Range(randomMim, randomMax);

            selfLight.enabled = !selfLight.enabled;

            await UniTask.Delay(TimeSpan.FromSeconds(random), cancellationToken: _token);
        }
    }

    public void SetBlink(bool _blink)
    {
        isBlink = _blink;
    }



    private void StartBlinking()
    {
        StopBlinking(); // 기존 코루틴이 있다면 중지

        cancellationTokenSource = new CancellationTokenSource(); // 새로운 취소 토큰 소스 생성
        Blink(cancellationTokenSource.Token).Forget(); // Blink 메서드 실행
    }

    private void StopBlinking()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel(); // 실행 중인 블링크 취소
            cancellationTokenSource.Dispose(); // 리소스 해제
            cancellationTokenSource = null; // 참조 해제
        }
    }
}
