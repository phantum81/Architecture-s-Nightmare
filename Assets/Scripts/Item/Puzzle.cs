using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Item
{
    public override void Init()
    {
        eObjectType = EObjectType.Puzzle;
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
