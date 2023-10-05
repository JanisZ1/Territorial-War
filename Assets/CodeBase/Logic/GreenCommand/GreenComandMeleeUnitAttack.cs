using Assets.CodeBase.Logic.GreenCommand;
using System.Linq;
using UnityEngine;
using GreenCommandAnimationState = Assets.CodeBase.Logic.GreenCommand.GreenCommandAnimationState;

[RequireComponent(typeof(GreenCommandMeleeUnitMove))]
public class GreenComandMeleeUnitAttack : MonoBehaviour
{
    private const string RedCommandLayer = "RedCommand";
    [SerializeField] private GreenCommandAnimator _greenCommandAnimator;
    [SerializeField] private float _fightRadius = 0.5f;

    private Collider[] _hits = new Collider[1];

    private int _layerMask;
    [SerializeField] private float _attackCooldown = 2;
    private float _cooldown;

    private bool _isAttacking;
    private bool _attackEnabled;
    private readonly int _damage = 1;

    private void Start()
    {
        _layerMask = 1 << LayerMask.NameToLayer(RedCommandLayer);

        _greenCommandAnimator.AttackEventFired += OnAttack;
        _greenCommandAnimator.OnStateExited += StateExited;
    }

    private void OnDestroy()
    {
        _greenCommandAnimator.AttackEventFired -= OnAttack;
        _greenCommandAnimator.OnStateExited -= StateExited;
    }

    private void Update()
    {
        UpdateCooldown();

        if (CooldownIsUp() && !_isAttacking && _attackEnabled)
            StartAttack();
    }

    private void StateExited(GreenCommandAnimationState state)
    {
        if (state == GreenCommandAnimationState.Attack)
        {
            _cooldown = _attackCooldown;
            _isAttacking = false;
        }
    }

    public void EnableAttack() =>
        _attackEnabled = true;

    public void DisableAttack()
    {
        _greenCommandAnimator.PlayIdle();
        _attackEnabled = false;
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
            hit.GetComponent<IDamageable>().TakeDamage(_damage);
    }

    private void StartAttack()
    {
        _greenCommandAnimator.PlayAttack();
        _isAttacking = true;
    }

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _cooldown -= Time.deltaTime;
    }

    private bool CooldownIsUp() =>
        _cooldown <= 0;

    private bool Hit(out Collider hit)
    {
        int hitsCount = Physics.OverlapSphereNonAlloc(transform.position + transform.right, _fightRadius, _hits, _layerMask);

        hit = _hits.FirstOrDefault();

        return hitsCount > 0;
    }
}
