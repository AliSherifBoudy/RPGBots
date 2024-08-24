using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bool Game Flag")]
public class BoolGameFlag : GameFlag<bool>
{
    public void Set(bool value)
    {
        Value = value;
        SendChanged();
    }
}
