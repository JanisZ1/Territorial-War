using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandRangeUnitAttack : MonoBehaviour
    {
        [SerializeField] private GreenCommandAnimator _greenCommandAnimator;
        [SerializeField] private Transform _spawn;
        [SerializeField] private GreenCommandBullet _bullet;
        [SerializeField] private ClosestEnemyUnitCalculatorForGreenCommand _closestEnemyCalculator;
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

        private void SpawnBullet()
        {
            float velocity = CalculateBulletVelocity();
            GreenCommandBullet greenCommandBullet = Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);
            greenCommandBullet.GetComponent<Rigidbody>().velocity = velocity * transform.right;
        }

        private float CalculateBulletVelocity()
        {
            (float distance, RedCommand.RedCommandUnit unit) closestRedUnit =
                _closestEnemyCalculator.ClosestRedCommandUnit();

            Vector3 fromTo = closestRedUnit.unit.transform.position - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;
            float angleInDegrees = 45;
            float g = -9.8f;

            float angleInRadians = angleInDegrees * Mathf.PI / 180;

            float v2 = g * x * x / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(v2);
            return v;
        }

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
}