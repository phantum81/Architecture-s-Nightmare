using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EObjectType
{

}
public interface IObject
{
    EObjectType eObjectType { get; set; } 
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
