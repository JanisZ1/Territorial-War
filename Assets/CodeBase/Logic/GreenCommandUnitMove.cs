using UnityEngine;

public class GreenCommandUnitMove : MonoBehaviour
{
    private float _xVector = 1f;
    public Vector3 _playerUnitVector;

    [SerializeField] private Animator _animator;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private MeleeAttack _meleeAttack;

    private IGreenCommandSpawner _greenCommandSpawner;
    private GreenCommandUnitMove _greenCommandUnitMove;

    public bool IsMoving { get; set; }

    public int Id { get; set; }

    public void Construct(IGreenCommandSpawner greenCommandSpawner) =>
        _greenCommandSpawner = greenCommandSpawner;

    public void Move()
    {
        Vector3 movingVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
        transform.Translate(movingVector);
    }
}
