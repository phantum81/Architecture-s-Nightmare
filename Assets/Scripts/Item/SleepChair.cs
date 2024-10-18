using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepChair : Item
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
        eObjectType = EObjectType.SleepChair;
    }
    public override void OnInteraction()
    {
        EventBus.TriggerEventAction(EEventType.StudioToAnotherScene, eObjectType);
        interactionCollider.enabled= false;
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Sit;
    }
}
