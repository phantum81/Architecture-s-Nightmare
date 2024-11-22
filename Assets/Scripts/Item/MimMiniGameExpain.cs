using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimMiniGameExpain : Item
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
        eObjectType = EObjectType.MimMiniGameFirst;
    }
    public override void OnInteraction()
    {
        GameManager.Instance.SetGameState(EGameState.MiniGameMimMapFirst);

    }


    public override EInteractionType GetInteractionType()
    {
        return EInteractionType.MiniGame;
    }
}
