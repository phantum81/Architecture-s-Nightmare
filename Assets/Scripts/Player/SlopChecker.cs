using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SlopChecker : MonoBehaviour
{
    [Header("���̱���"), SerializeField]
    private float rayDistance;
    [Header("�ִ� ������"), SerializeField]
    private float maxSlope;

    private RaycastHit slopeHit;
    private LayerMask expectLayer = 1<<6;

    public PlayerController pc;

    public bool CheckSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit,
                            rayDistance, ~expectLayer))
        {
            
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            
            //if(angle != 0)
            //    Debug.Log(angle);

            return angle > 5f && angle < maxSlope;
        
        }

        return false;
    }

    public Vector3 AdjustDirectionToSlope(Vector3 _inputDir)
    {

        Vector3 adjustedDir = Vector3.ProjectOnPlane(_inputDir, slopeHit.normal).normalized;
       
        return adjustedDir;
    }

    public float CaculateNextFrameGroundAngle(Transform _player, Vector3 _inputDir, float _moveSpeed)
    {
        if (_inputDir == Vector3.zero)
        {
            _inputDir = _player.transform.forward;
        }

        Collider col = _player.GetComponent<Collider>();
        if (col is CapsuleCollider _playerCol)
        {
            
            Vector3 rayPos = _player.position + _playerCol.radius * _inputDir * _moveSpeed * 5f * Time.deltaTime;

            
            if (Physics.Raycast(rayPos, Vector3.down, out RaycastHit hit, rayDistance * 5f, ~expectLayer))
            {
                if(Vector3.Angle(Vector3.up, hit.normal) != 0f)
                    Debug.Log(Vector3.Angle(Vector3.up, hit.normal));
                return Vector3.Angle(Vector3.up, hit.normal);
            }
            else
            {
                
                return 0;
            }
        }
        else
        {
            Debug.LogWarning("�ش� �ݶ��̴��� ĸ���̾ƴ�");
            return 0;
        }
            

        
    }




    private void OnDrawGizmos()
    {
       // Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);

        // �÷��̾� ��ġ�� �Է� ������ ����Ͽ� ������ ������ ���
        if (transform != null)
        {
            Vector3 inputDir = pc.Player.transform.forward;


            float moveSpeed = 3f; // ���� �̵� �ӵ� (������ ����)

            // �ݶ��̴� ��������
            Collider col = GetComponent<Collider>();
            if (col is CapsuleCollider _playerCol)
            {
                Vector3 rayPos = pc.Player.position + (_playerCol.radius * inputDir.normalized) * moveSpeed ;
                
                // Gizmos�� ���� �׸���
                Gizmos.color = Color.red; // ���� ���� ����
                Gizmos.DrawLine(rayPos, rayPos + Vector3.down*200f * rayDistance); // �Ʒ� �������� ���� �׸���
            }
        }
    }
}
