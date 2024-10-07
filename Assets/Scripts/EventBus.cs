using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;


public static class EventBus
{   
    private static Dictionary<EEventType, List<Action>> eventsAction = new Dictionary<EEventType, List<Action>>();

    private static Dictionary<EEventType, List<Delegate>> eventsActionWithParam = new Dictionary<EEventType, List<Delegate>>();

    public static void TriggerEventAction(EEventType _event)
    {
        if (eventsAction.TryGetValue(_event, out var actions))
        {
            
            actions.ForEach(action => action?.Invoke());
        }
    }
    public static void TriggerEventAction<T>(EEventType eventType, params T[] _param)
    {
        if (eventsActionWithParam.TryGetValue(eventType, out var actions))
        {
            actions.ForEach(action =>
            {
                if (action is Action<T[]> typedAction) // 타입 검사 및 캐스팅
                {
                    typedAction.Invoke(_param);
                }
            });
        }
    }

    public static void SubscribeAction(EEventType _event, Action _handler)
    {
        if (!eventsAction.ContainsKey(_event))
        {
            eventsAction[_event] = new List<Action> { _handler };

        }
        else
        {
            if (!eventsAction[_event].Contains(_handler))
            {
                eventsAction[_event].Add(_handler);
            }
        }
    }
    /// <summary>
    /// 박싱언박싱 문제로 참조형은 피할것. (클래스로 만들어서하든지)
    /// </summary>
    /// <param name="_event"></param>
    /// <param name="_handler"></param>
    public static void SubscribeAction<T>(EEventType _event, Action<T[]> _handler)
    {
        if (!eventsActionWithParam.ContainsKey(_event))
        {
            eventsActionWithParam[_event] = new List<Delegate> {_handler };
        }
        else
        {
            if (!eventsActionWithParam[_event].Contains(_handler))
            {
                eventsActionWithParam[_event].Add(_handler);
            }
            
        }
            
    }




    public static void UnsubscribeAction(EEventType _event, Action _handler)
    {
        if (eventsAction.TryGetValue(_event, out var actions))
        {
            actions.Remove(_handler);

            
            if (actions.Count == 0)
            {
                eventsAction.Remove(_event);
            }
        }
    }
    public static void UnsubscribeAction<T>(EEventType _event, Action<T[]> _handler)
    {
        if (eventsActionWithParam.TryGetValue(_event, out var actions))
        {
            actions.Remove(_handler);


            if (actions.Count == 0)
            {
                eventsActionWithParam.Remove(_event);
            }
        }

    }
}
