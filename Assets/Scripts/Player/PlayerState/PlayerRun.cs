using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {


    }

    public void OperateUpdate(T _send)
    {
        if (_send is PlayerController playerCtr)
        {
            playerCtr.Move(playerCtr.InputDir, playerCtr.RunSpeed);
            playerCtr.Rotate();
            playerCtr.StepHeightMove();
        }
    }

    public void OperateExit(T _send)
    {

    }
}
