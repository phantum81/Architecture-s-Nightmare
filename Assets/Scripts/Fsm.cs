using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm<TState, T> where TState : System.Enum where T : MonoBehaviour
{
    private static Fsm<TState, T> instance;
    public static Fsm<TState, T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Fsm<TState, T>();
            }
            return instance;
        }
    }

    private TState curState;
    public TState CurState => curState;
    private Dictionary<TState, IState<T>> stateDic;

    public Fsm()
    {
        curState = default(TState);
        stateDic = new Dictionary<TState, IState<T>>();
        Init();
    }

    public void Init()
    {
        foreach (TState state in Enum.GetValues(typeof(TState)))
        {
            stateDic[state] = CreateStateInstance(state);
        }
    }
    private IState<T> CreateStateInstance(TState _state)
    {        
        switch (_state)
        {
            case EPlayerState.None:
                return new PlayerNone<T>();
            case EPlayerState.Idle:
                return new PlayerIdle<T>();
            case EPlayerState.Walk:
                return new PlayerWalk<T>();
            case EPlayerState.Run:
                return new PlayerRun<T>();
            case EPlayerState.Jump:
                return new PlayerJump<T>();

            case EPlayerGroundState.Ground:
                return new PlayerGround<T>();
            case EPlayerGroundState.Slope:
                return new PlayerSlope<T>();
            case EPlayerGroundState.Air:
                return new PlayerAir<T>();


            default:
                return null;
        }
    }


    public void ChangeState(TState _newState, T _obj)
    {
        if (EqualityComparer<TState>.Default.Equals(curState, _newState))
            return;

        //if (!CanChangeState(curState, newState))
        //    return;

        ExitState(curState, _obj);
        curState = _newState;
        EnterState(curState, _obj);
    }



    //private bool CanChangeState(TState curState, TState newState)
    //{
    //    switch (curState)
    //    {
    //        case EPlayerState.Die:
    //            if (newState.Equals(EPlayerState.None))
    //            {
    //                return true;
    //            }
    //            return false;

    //        default:
    //            return true;
    //    }

    //}



    private void EnterState(TState _state, T _obj)
    {
        stateDic[_state].OperateEnter(_obj);
    }
    private void ExitState(TState _state, T _obj)
    {
        stateDic[_state].OperateExit(_obj);
    }

    public void Update(T _obj)
    {
        stateDic[curState].OperateUpdate(_obj);
    }

    public void Update(T _obj, TState _state)
    {
        stateDic[_state].OperateUpdate(_obj);
    }
}
