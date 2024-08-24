using System;
using UnityEngine;

public class GameFlag<T> : GameFlag
{
    public T Value { get; protected set; }

    void OnDisable() => Value = default;

    void OnEnable() => Value = default;
}