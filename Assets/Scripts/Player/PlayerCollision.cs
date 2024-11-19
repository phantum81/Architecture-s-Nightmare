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
    private CameraManager cameraMgr;
    private void Start()
    {
        inputMgr = GameManager.Instance.InputMgr;
        cameraMgr = GameManager.Instance.CameraMgr;
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
            if (curCoroutine != null) 
            {
                StopCoroutine(curCoroutine);
                EventBus.TriggerEventAction(EEventType.OffInteraction);
                curCoroutine = null; 
            }
        }
    }



    private IEnumerator Interacting(Collider _col)
    {
        IInteraction obj = _col.transform.parent.GetComponent<IInteraction>();
        bool isInteracting = false;
        Camera curCam = cameraMgr.CinemachineBrain.OutputCamera;
        if (obj != null)
        {
            
            while (true)
            {
                
                float angle = Vector3.Angle(transform.forward, _col.transform.parent.position - transform.position);
                Vector3 screenPoint = curCam.WorldToViewportPoint(_col.transform.parent.position);
                bool isIn = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && _col.enabled;

                if (isIn)
                {
                    
                    if (!isInteracting)
                    {
                        EventBus.TriggerEventAction(EEventType.OnInteraction, obj.GetInteractionType());
                        EventBus.TriggerEventAction(EEventType.OnInteraction);
                        isInteracting = true;
                        Debug.Log(obj);

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

                        if (curCoroutine != null)
                        {
                            EventBus.TriggerEventAction(EEventType.OffInteraction);
                            isInteracting = false;
                            StopCoroutine(curCoroutine);                            
                            curCoroutine = null;
                        }
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
