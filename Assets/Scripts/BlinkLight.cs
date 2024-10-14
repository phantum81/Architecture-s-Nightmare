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
    [Header("�̴ϸ� ���� ��"), SerializeField]
    private float randomMim = 0.4f;
    [Header("�ƽø� ���� ��"), SerializeField]
    private float randomMax = 1f;
    private Light selfLight;
    private bool isTimeLine = false;
    private CancellationTokenSource cancellationTokenSource; // ��� ��ū �ҽ�
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
            Debug.LogWarning("�̴ϸذ��� 0�����Դϴ�.");
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
        StopBlinking(); // ���� �ڷ�ƾ�� �ִٸ� ����

        cancellationTokenSource = new CancellationTokenSource(); // ���ο� ��� ��ū �ҽ� ����
        Blink(cancellationTokenSource.Token).Forget(); // Blink �޼��� ����
    }

    private void StopBlinking()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel(); // ���� ���� ��ũ ���
            cancellationTokenSource.Dispose(); // ���ҽ� ����
            cancellationTokenSource = null; // ���� ����
        }
    }
}
