using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {
        if(_send is PlayerController playerCtr)
        {
            playerCtr.Jump();
        }

    }

    public void OperateUpdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
