using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoWineBottle : Item
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
        eObjectType = EObjectType.DecoWinebBottle;
    }
    public override void OnInteraction()
    {
        EventBus.TriggerEventAction(EEventType.StudioDecoInteraction, eObjectType);
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Look;
    }
}
