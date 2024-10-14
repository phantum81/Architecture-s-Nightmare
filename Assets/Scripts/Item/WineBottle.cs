using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBottle : Item
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
        eObjectType = EObjectType.WineBottle;
    }
    public override void OnInteraction()
    {
        gameObject.transform.position = new Vector3(100f, 100f, 100f);
        
        EventBus.TriggerEventAction(EEventType.TutorialCinema, eObjectType);
        EventBus.TriggerEventAction(EEventType.TutorialCinema);


    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Pick;
    }
}
