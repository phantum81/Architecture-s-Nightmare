using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerCtr;
    Vector3 dir = Vector3.zero;
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
        if (collision.collider.gameObject.layer != 7)
        {
            // 충돌한 벽의 표면 노말을 가져옵니다.
            Vector3 wallNormal = collision.contacts[0].normal;
            Vector3 projectedDir = Vector3.ProjectOnPlane(playerCtr.InputDir, wallNormal);
            playerCtr.SetInputDir(projectedDir);
        }

    }

    public Vector3 GetCollisionNormal()
    {
        return dir;
    }

}
