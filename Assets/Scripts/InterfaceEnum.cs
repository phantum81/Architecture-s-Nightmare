using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IObject
{
    EObjectType eObjectType { get; set; } 
}
public interface IState<T>
{
    void OperateEnter(T _obj);

    void OperateUpdate(T _obj);

    void OperateExit(T _obj);


}

public interface IInteraction
{

    void OnInteraction();

    EInteractionType GetInteractionType();

    
}


public enum EObjectType
{
    WineBottle,
}

public enum EUserAction
{
    MoveForward,
    MoveBackward,
    MoveRight,
    MoveLeft,
    Run,
    Jump,
    Interaction,


}

public enum ECameraType
{
    Fps,

}

public enum TState
{

}

public enum EPlayerState
{
    None,
    Idle,
    Walk,
    Run,
    Jump,


}
public enum EPlayerGroundState
{
    Ground,
    Slope,
    Air,

}

public enum EItemType
{
    WineBottle,

}

public enum EInteractionType
{
    Pick,
    Push,
    Destroy,
    Look,

}


