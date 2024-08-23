using UnityEngine;

public class GameFlagTriggerAreaForIntFlags : MonoBehaviour
{
    [SerializeField] int _amount;
    [SerializeField] IntGameFlag _intGameFlag;
    void OnTriggerEnter(Collider other) => _intGameFlag.Modify(_amount);
}
