using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMapBuilding : Item
{
    public override void Init()
    {
        eObjectType = EObjectType.BuildingMap;
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
