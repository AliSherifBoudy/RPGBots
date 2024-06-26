using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bool Game Flag")]
public class GameFlag : ScriptableObject
{
    public event Action Changed;
    public bool Value { get; private set; }

    void OnEnable() => Value = default;
    private void OnDisable() => Value = default;

    public void Set(bool value)
    {
        Value = value;
        Changed?.Invoke();
    }
}
