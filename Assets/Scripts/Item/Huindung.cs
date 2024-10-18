using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huindung : Item
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
        eObjectType = EObjectType.HuinDung;
    }
    public override void OnInteraction()
    {

        EventBus.TriggerEventAction(EEventType.TutorialCinema, eObjectType);
        EventBus.TriggerEventAction(EEventType.TutorialCinema);

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Pick;
    }
}
