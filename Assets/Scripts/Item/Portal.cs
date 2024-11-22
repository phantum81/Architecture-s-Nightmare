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
        if(interactionCollider == null)
            interactionCollider = transform.GetChild(0).GetComponent<SphereCollider>();

        EventBus.TriggerEventAction(EEventType.MaxMapCinema);

        interactionCollider.enabled = false;
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Move;
    }
}
