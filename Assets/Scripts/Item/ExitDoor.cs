using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Item
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
        eObjectType = EObjectType.ExitDoor;
    }
    public override void OnInteraction()
    {

        EventBus.TriggerEventAction(EEventType.FirstMapCinema);
        interactionCollider.enabled = false;

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Open;
    }
}
