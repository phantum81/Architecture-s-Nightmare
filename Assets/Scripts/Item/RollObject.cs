using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollObject : Item
{
    private Rigidbody rigd;

    private Vector3 originPos;
    private Quaternion originRotate;

    private void Update()
    {
        if(transform.localPosition.z < 0.8)
        {
            rigd.isKinematic = true;
            rigd.velocity = Vector3.zero;
            transform.position = originPos;
            transform.rotation = originRotate;
            
        }
    }


    public override EObjectType GetObjectType()
    {
        return base.GetObjectType();
    }
    public override Transform GetTransform()
    {
        return base.GetTransform();
    }
    public override void Init()
    {
        base.Init();
        eObjectType = EObjectType.RollObject;
        rigd = GetComponent<Rigidbody>();
        originPos = transform.position;
        originRotate = transform.rotation;

    }
    public override void OnInteraction()
    {
        
        rigd.isKinematic = false;
        
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Roll;
    }





}
