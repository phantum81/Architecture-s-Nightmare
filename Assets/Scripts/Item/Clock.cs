using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Item
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
        eObjectType = EObjectType.Clock;
    }
    public override void OnInteraction()
    {

        EventBus.TriggerEventAction(EEventType.MimMapCinema);
        interactionCollider.enabled= false;

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Use;
    }
}
