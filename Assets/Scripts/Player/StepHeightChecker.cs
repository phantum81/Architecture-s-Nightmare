using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class StepHeightChecker : MonoBehaviour
{
    [SerializeField]
    private float stepHeight = 0.1f;

    [Header("올라갈 수 있는 높이"), SerializeField]
    private float limitHeightMultiply = 0.3f;

    [Header("낮은레이 길이"), SerializeField]
    private float lowRayDistance = 0.3f;

    [Header("높은레이 길이"), SerializeField]
    private float highRayDistance = 0.4f;


    private LayerMask expectLayer = (1 << 6);
    private RaycastHit hitLower;
    private RaycastHit preHit;
    [Header("보정타임"), SerializeField]
    private float time = 0.3f;

    
    
    private bool isTask = false;
    public bool IsTask => isTask;

    CancellationTokenSource cancelToken = new CancellationTokenSource() ;

    public async void StepHeightMove(Rigidbody _rigd,  Vector3 _inputdir)
    {
        if (_inputdir == Vector3.zero) return;

        Vector3 limitHeight = transform.position + Vector3.up * limitHeightMultiply;
        float angle = 0f;
        
        

        if (Physics.Raycast(transform.position, _inputdir, out hitLower, lowRayDistance, ~expectLayer) )
        {
            
            angle = Vector3.Angle(Vector3.up, hitLower.normal);
            
            if (!Physics.Raycast(limitHeight, _inputdir, highRayDistance, ~expectLayer) && angle >=80f)
            {
                
                if (hitLower.collider != preHit.collider)
                {
                    Debug.Log($"새로운 시작{hitLower.collider} {preHit.collider}");
                    preHit = hitLower;

                    cancelToken.Cancel();
                    cancelToken.Dispose();
                    cancelToken = new CancellationTokenSource();

                    if (!isTask)
                        stepHeight = await FindStairHeight(() => _inputdir, cancelToken);
                }



                Debug.Log($"높이 {stepHeight}");
                Vector3 targetPosition = new Vector3(_rigd.position.x, Mathf.Lerp(_rigd.position.y, _rigd.position.y + stepHeight, Time.deltaTime * time), _rigd.position.z);

                //_rigd.MovePosition(new Vector3(_rigd.position.x, _rigd.position.y + stepHeight, _rigd.position.z));
                _rigd.MovePosition(targetPosition);


            }

        }






    }



    async UniTask<float> FindStairHeight(Func<Vector3> _inputDir, CancellationTokenSource _token)
    {

        Vector3 inputDir = _inputDir();
        float checkHeight = 0.00f;        
        isTask = true;

        try
        {
            while (true)
            {


                Vector3 rayPos = new Vector3(transform.position.x, transform.position.y + checkHeight, transform.position.z);

                if (Physics.Raycast(rayPos, inputDir, lowRayDistance , ~expectLayer))
                {
                    checkHeight += 0.02f;

                }
                else
                {
                    isTask = false;

                    return checkHeight;



                }
                await UniTask.Yield(cancellationToken: _token.Token);

            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("작업이 취소되었습니다.");
            isTask = false;
            cancelToken = new CancellationTokenSource();
            return checkHeight; // 작업이 취소된 경우에도 현재 높이 반환
        }

        
        
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawRay(transform.position, transform.forward * lowRayDistance);

        Gizmos.color = Color.red;


        Gizmos.DrawRay(transform.position + Vector3.up * limitHeightMultiply, transform.forward * highRayDistance);

        //Ray가 충돌한 위치에 구를 그림
        if (hitLower.collider != null)
        {
            Gizmos.color = Color.green;  // 충돌된 위치는 녹색으로 표시
            Gizmos.DrawSphere(hitLower.point, 0.05f);  // 충돌된 지점에 작은 구 그리기
        }
    }


}
