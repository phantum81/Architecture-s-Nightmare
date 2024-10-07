using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {
        if(_send is PlayerController playerCtr)
        {
            playerCtr.InitRigidbodyVelocity();
        }

    }

    public void OperateUpdate(T _send)
    {
        if (_send is PlayerController playerCtr)
        {
            playerCtr.Rotate();
        }
       
    }

    public void OperateExit(T _send)
    {

    }
}
