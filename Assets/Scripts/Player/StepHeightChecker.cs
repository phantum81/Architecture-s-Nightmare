using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHeightChecker : MonoBehaviour
{
    [Header("스텝 보정 높이"), SerializeField]
    private float stepHeight = 0.1f;

    [Header("올라갈 수 있는 높이"), SerializeField]
    private float limitHeightMultiply = 0.3f;

    private LayerMask expectLayer = (1 << 6);
    private RaycastHit hitLower;
    [Header("보정타임"), SerializeField]
    private float time = 0.3f;


    public void StepHeightMove(Rigidbody _rigd,  Vector3 _inputdir)
    {

        Vector3 limitHeight = transform.position + Vector3.up * limitHeightMultiply;
        float angle = 0f;

        

        if (Physics.Raycast(transform.position, _inputdir, out hitLower, 0.6f, ~expectLayer) )
        {
            
            angle = Vector3.Angle(Vector3.up, hitLower.normal);
            
            if (!Physics.Raycast(limitHeight, _inputdir, 0.7f, ~expectLayer) && angle >=80f)
            {
                _rigd.position -= new Vector3(0f, -stepHeight, 0f);


            }

        }
        else
        {
            if (hitLower.collider != null)
            {

            }

                


        }



        
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red; 

        
        //Gizmos.DrawRay(transform.position, transform.forward * 0.3f);

        //Gizmos.color = Color.red; 

       
        //Gizmos.DrawRay(transform.position + Vector3.up * limitHeightMultiply, transform.forward * 0.4f);

        ////Ray가 충돌한 위치에 구를 그림
        //if (hitLower.collider != null)
        //{
        //    Gizmos.color = Color.green;  // 충돌된 위치는 녹색으로 표시
        //    Gizmos.DrawSphere(hitLower.point, 0.05f);  // 충돌된 지점에 작은 구 그리기
        //}
    }

}
