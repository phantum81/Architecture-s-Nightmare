using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimMapSisaw : MonoBehaviour
{
    private Rigidbody rigd;
    void Start()
    {
        rigd = GetComponent<Rigidbody>();

        rigd.centerOfMass = Vector3.zero;
    }


}
