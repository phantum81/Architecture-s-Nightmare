using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNone<T> : IState<T> where T : class
{
    public void OperateEnter(T _send)
    {


    }

    public void OperateUpdate(T _send)
    {

    }

    public void OperateExit(T _send)
    {

    }
}
