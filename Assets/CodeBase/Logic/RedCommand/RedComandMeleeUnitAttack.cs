using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedComandMeleeUnitAttack : MonoBehaviour
    {
        private const string GreenCommandLayer = "GreenCommand";
        [SerializeField] private RedCommandAnimator _redCommandAnimator;
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
            _layerMask = 1 << LayerMask.NameToLayer(GreenCommandLayer);

            _redCommandAnimator.AttackEventFired += OnAttack;
            _redCommandAnimator.OnStateExited += OnAttackEnded;
        }

        private void OnAttackEnded(RedCommandAnimationState state)
        {
            if (state == RedCommandAnimationState.Attack)
            {
                _cooldown = _attackCooldown;
                _isAttacking = false;
            }
        }

        private void OnDestroy()
        {
            _redCommandAnimator.AttackEventFired -= OnAttack;
            _redCommandAnimator.OnStateExited -= OnAttackEnded;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CooldownIsUp() && !_isAttacking && _attackEnabled)
                StartAttack();
        }

        public void EnableAttack() =>
            _attackEnabled = true;

        public void DisableAttack()
        {
            _redCommandAnimator.PlayIdle();
            _attackEnabled = false;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
                hit.GetComponent<IDamageable>().TakeDamage(_damage);
        }

        private void StartAttack()
        {
            _redCommandAnimator.PlayAttack();
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
            int hitsCount = Physics.OverlapSphereNonAlloc(transform.position - transform.right, _fightRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }
    }
}