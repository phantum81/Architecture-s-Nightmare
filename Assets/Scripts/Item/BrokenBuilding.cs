using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBuilding : Item
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
        eObjectType = EObjectType.BrokenBuilding;

    }
    public override void OnInteraction()
    {
        interactionCollider = transform.GetChild(0).GetComponent<SphereCollider>();
        EventBus.TriggerEventAction(EEventType.BrokeBuilding, transform.position);
        interactionCollider.enabled = false;
        gameObject.SetActive(false);

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Destroy;
    }
}
