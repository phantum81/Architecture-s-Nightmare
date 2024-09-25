using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopChecker : MonoBehaviour
{
    [Header("레이길이"), SerializeField]
    private float rayDistance;
    [Header("최대 슬로프"), SerializeField]
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
            
            if(angle != 0)
                Debug.Log(angle);

            return angle > 0 && angle < maxSlope;
        
        }

        return false;
    }

    public Vector3 AdjustDirectionToSlope(Vector3 _inputDir)
    {
        Vector3 adjustedDir = Vector3.ProjectOnPlane(_inputDir, slopeHit.normal).normalized;
        Debug.Log("Slope Normal: " + slopeHit.normal + " | Adjusted Dir: " + adjustedDir);
        return adjustedDir;
    }

    public void OnDrawGizmos()
    {
        
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);
    }

}
