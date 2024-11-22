using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBed : Item
{
    BoxCollider interactionCol;
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
        //base.Init();
        eObjectType = EObjectType.SleepBed;

    }
    public override void OnInteraction()
    {
        interactionCol = transform.GetChild(0).GetComponent<BoxCollider>();

        EventBus.TriggerEventAction(EEventType.StudioToAnotherScene, eObjectType);

        interactionCol.enabled = false;
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Lean;
    }
}
