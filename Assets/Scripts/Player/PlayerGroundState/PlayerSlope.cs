using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlope<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {
        if (_send is PlayerController playerCtr)
        {
            playerCtr.SetGravity(false);
        }

    }

    public void OperateUpdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {
        if (_send is PlayerController playerCtr)
        {
            playerCtr.SetGravity(true);
        }
    }
}
