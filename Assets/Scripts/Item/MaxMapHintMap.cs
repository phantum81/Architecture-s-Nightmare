using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMapHintMap : Item
{
    public override void Init()
    {
        eObjectType = EObjectType.BuildingHintMap;
        transform.parent = GameManager.Instance.transform;

    }


    public override EObjectType GetObjectType()
    {
        return eObjectType;
    }


    public override Transform GetTransform()
    {
        return transform;
    }
}
