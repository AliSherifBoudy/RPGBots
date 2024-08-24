using System;
using UnityEngine;

public class GameFlag: ScriptableObject
{
    public event Action Changed;

    protected void SendChanged() => Changed?.Invoke();
}