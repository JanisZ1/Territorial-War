using UnityEngine;

public class GreenCommandRangeUnitAttack : MonoBehaviour
{
    private bool _attackEnabled;

    public void EnableAttack() =>
        _attackEnabled = true;

    public void DisableAttack() => 
        _attackEnabled = false;
}
