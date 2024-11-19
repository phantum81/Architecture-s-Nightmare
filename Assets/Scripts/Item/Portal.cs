using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Item
{
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
        eObjectType = EObjectType.Portal;

    }
    public override void OnInteraction()
    {
        

        interactionCollider.enabled = false;
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Move;
    }
}
