using UnityEngine;

public class PlayerUnitMover : MonoBehaviour, IMovable
{
    private float _xVector = 1f;
    public Vector3 _playerUnitVector;

    public void Move()
    {
        _playerUnitVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
        transform.Translate(_playerUnitVector);
    }

    public void StopMove() =>
        _playerUnitVector = Vector3.zero;
}
