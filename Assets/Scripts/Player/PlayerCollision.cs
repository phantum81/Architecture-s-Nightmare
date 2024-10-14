using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerCtr;
    Vector3 dir = Vector3.zero;
    private InputManager inputMgr;
    private Coroutine curCoroutine;
    private void Start()
    {
        inputMgr = GameManager.Instance.InputMgr;
    }


    private void OnCollisionStay(Collision collision)
    {
        //if(collision.collider.gameObject.layer != 7)
        //{
        //    // 충돌한 벽의 표면 노말을 가져옵니다.
        //    Vector3 wallNormal = collision.contacts[0].normal;
        //    dir = wallNormal;
        //}
        //else
        //    dir = Vector3.zero;
        if (collision.collider.gameObject.layer == 0)
        {
            // 충돌한 벽의 표면 노말을 가져옵니다.
            Vector3 wallNormal = collision.contacts[0].normal;
            Vector3 projectedDir = Vector3.ProjectOnPlane(playerCtr.InputDir, wallNormal);
            playerCtr.SetInputDir(projectedDir);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ConstBundle.INTERACTION_TAG)
        {
            if (curCoroutine == null)
            {
                curCoroutine = StartCoroutine(Interacting(other));
            }
           
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.tag == ConstBundle.INTERACTION_TAG)
        {
            if (curCoroutine != null) // null이 아닐 경우에만 중지
            {
                StopCoroutine(curCoroutine);
                EventBus.TriggerEventAction(EEventType.OffInteraction);
                curCoroutine = null; // 중지 후 null로 초기화
            }
        }
    }



    private IEnumerator Interacting(Collider _col)
    {
        IInteraction obj = _col.transform.parent.GetComponent<IInteraction>();
        bool isInteracting = false;
        
        if (obj != null)
        {
            
            while (true)
            {
                float angle = Vector3.Angle(transform.forward, _col.transform.position - transform.position);

                if (angle < 40f)
                {
                    
                    if (!isInteracting)
                    {
                        EventBus.TriggerEventAction(EEventType.OnInteraction, obj.GetInteractionType());
                        EventBus.TriggerEventAction(EEventType.OnInteraction);
                        isInteracting = true;

                    }
                    if (inputMgr.InputDic[EUserAction.Interaction])
                    {
                        obj.OnInteraction();
                    }
                }
                else
                {
                    if (isInteracting)
                    {
                        EventBus.TriggerEventAction(EEventType.OffInteraction);
                        isInteracting = false;
                    }
                        
                }


                yield return null;
            }
        }



    }


    public Vector3 GetCollisionNormal()
    {
        return dir;
    }


    
}
