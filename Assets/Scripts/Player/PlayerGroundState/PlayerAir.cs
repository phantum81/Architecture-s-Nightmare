using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {


    }

    public void OperateUpdate(T _send)
    {
        if (_send is PlayerController playerCtr)
        {
            playerCtr.AirGravity();
        }
    }

    public void OperateExit(T _send)
    {

    }
}
