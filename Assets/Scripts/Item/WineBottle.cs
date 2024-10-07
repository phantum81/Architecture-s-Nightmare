using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBottle : Item
{

    public override EObjectType GetObjectType()
    {
        return base.GetObjectType();
    }

    public override void Init()
    {
        base.Init();
        eObjectType = EObjectType.WineBottle;
    }
    public override void OnInteraction()
    {
        
        Debug.Log("¾å»ß");
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.Pick;
    }
}
