using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IScriptView
{
    void WriteScript(string _script);

    void SetScriptPanel(bool _bol);

    void InvisibleScriptsPanel(float _time, float _wait);

    bool GetScriptPanelActiveSelf();

}



public interface IObject
{
    void Init();
    EObjectType GetObjectType();

    Transform GetTransform();
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
    DecoWinebBottle,
    DecoHuinDung,
    HuinDung,
    Saboa,
    ExitDoor,
    SleepChair,
    SleepLapTop,

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
    TutorialCam,

}

public enum EEventType
{
    SceneStart,
    OnInteraction,
    OffInteraction,
    StudioEnter,
    TutorialCinema,
    FirstMapCinema,
    StudioDecoInteraction,
    StudioToAnotherScene,

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
    Open,
    Destroy,
    Look,
    Sit,

}

public enum EGameStage
{
    Tutorial,
    FirstMap,
    MinMap,
    MaxMap,

}



/// <summary>
/// 작명법 장소+상황+대상
/// </summary>
public enum EPlayerScriptsType
{
    StudioInteractionWineBottle,
    StudioInteractionHuindung,
    StudioInteractionSaboa,
    StudioToFirstMap,
    StudioToMimMap,
}

