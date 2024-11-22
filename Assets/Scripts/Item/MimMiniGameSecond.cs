using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimMiniGameSecond : Item
{
    public override EObjectType GetObjectType()
    {
        return eObjectType;
    }
    public override Transform GetTransform()
    {
        return transform;
    }
    public override void Init()
    {
        base.Init();
        eObjectType = EObjectType.MimMiniGameSecond;
    }
    public override void OnInteraction()
    {
        GameManager.Instance.SetGameState(EGameState.MiniGameMimMapSecond);
    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.MiniGame;
    }
}
