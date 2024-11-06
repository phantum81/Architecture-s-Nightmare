using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollObject : Item
{
    private Rigidbody rigd;

    [Header("∏ÿ√„ ∆«¥‹"), SerializeField]
    private float stopThreshold = 0.01f;
    private Vector3 originPos;
    private Quaternion originRotate;
    private bool isInteraction = false;
    private void Update()
    {
        if (!rigd.isKinematic && !isInteraction)
        {
            if (transform.localPosition.z < 0.8 || rigd.velocity.magnitude < stopThreshold)
            {
                ResetRollObject();

            }
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
        if(!isInteraction)
        {
            rigd.isKinematic = false;
            isInteraction = true;
            Invoke("EndInteraction", 0.5f);
        }

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Roll;
    }


    private void ResetRollObject()
    {
        rigd.velocity = Vector3.zero;
        transform.position = originPos;
        transform.rotation = originRotate;
        rigd.isKinematic = true;
        isInteraction = false;
    }


    private void EndInteraction()
    {
        isInteraction = false;
    }
}
