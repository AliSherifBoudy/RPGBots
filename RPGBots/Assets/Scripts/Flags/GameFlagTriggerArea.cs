using UnityEngine;

public class GameFlagTriggerArea : MonoBehaviour
{
    [SerializeField] BoolGameFlag _boolGameFlag;
    private void OnTriggerEnter(Collider other)
    {
        _boolGameFlag.Set(true);
    }
}
