using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepHeightChecker : MonoBehaviour
{
    [Header("���� ���� ����"), SerializeField]
    private float stepHeight = 0.1f;

    [Header("�ö� �� �ִ� ����"), SerializeField]
    private float limitHeightMultiply = 0.3f;

    private LayerMask expectLayer = (1 << 6);
    private RaycastHit hitLower;
    [Header("����Ÿ��"), SerializeField]
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

        ////Ray�� �浹�� ��ġ�� ���� �׸�
        //if (hitLower.collider != null)
        //{
        //    Gizmos.color = Color.green;  // �浹�� ��ġ�� ������� ǥ��
        //    Gizmos.DrawSphere(hitLower.point, 0.05f);  // �浹�� ������ ���� �� �׸���
        //}
    }

}
