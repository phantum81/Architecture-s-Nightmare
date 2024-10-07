using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBusParamsWrapper
{
    public string _string { get; private set; }
    public int _int { get; private set; }
    public bool _bool { get; private set; }

    public EventBusParamsWrapper(string _string = "", int _int = 0, bool _bool = false)
    {
        this._string = _string;
        this._int = _int;
        this._bool = _bool;
    }

}
