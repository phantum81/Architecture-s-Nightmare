using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopChecker : MonoBehaviour
{
    [Header("레이길이"), SerializeField]
    private float rayDistance;

    private bool isSlope = false;
    public bool IsSlope => isSlope;

    private LayerMask expectLayer = 1<<6;


    public float CalculateGroundAngle(Vector3 _inputDir)
    {
        if (_inputDir == Vector3.zero)
            _inputDir = transform.forward;
        if (Physics.Raycast(transform.position, _inputDir, out RaycastHit hitInfo,
                            rayDistance, ~expectLayer))
        {

            return Vector3.Angle(Vector3.up, hitInfo.normal);
        }

        return 0f;
    }
}
