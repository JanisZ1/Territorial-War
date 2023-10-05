using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

public class GreenCommandRangeUnitAttack : MonoBehaviour
{
    [SerializeField] private GreenCommandAnimator _greenCommandAnimator;
    [SerializeField] private Transform _spawn;
    [SerializeField] private GreenCommandBullet _bullet;
    [SerializeField] private float _attackCooldown;

    private float _cooldown;
    private bool _attackEnabled;
    private bool _isAttacking;

    private void Start()
    {
        _greenCommandAnimator.RangeAttackEventFired += SpawnBullet;
        _greenCommandAnimator.OnStateExited += OnAttackEnded;
    }

    private void OnDestroy()
    {
        _greenCommandAnimator.RangeAttackEventFired -= SpawnBullet;
        _greenCommandAnimator.OnStateExited -= OnAttackEnded;
    }

    private void Update()
    {
        UpdateCooldown();

        if (CooldownIsUp() && _attackEnabled && !_isAttacking)
            StartAttack();
    }

    public void EnableAttack() =>
        _attackEnabled = true;

    public void DisableAttack()
    {
        _greenCommandAnimator.PlayIdle();
        _attackEnabled = false;
    }

    private void SpawnBullet() =>
        Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);

    private void StartAttack()
    {
        _greenCommandAnimator.PlayRangeAttack();
        _isAttacking = true;
    }

    private void OnAttackEnded(GreenCommandAnimationState greenCommandAnimationState)
    {
        if (greenCommandAnimationState == GreenCommandAnimationState.RangeAttack)
            _isAttacking = false;
    }

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _cooldown -= Time.deltaTime;
    }

    private bool CooldownIsUp() =>
        _cooldown <= 0;
}
