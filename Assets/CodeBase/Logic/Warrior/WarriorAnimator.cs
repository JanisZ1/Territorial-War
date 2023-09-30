namespace Assets.CodeBase.Logic.Warrior
{
    public class WarriorAnimator : GreenCommandAnimator
    {
        private IDamageable _damageable;
        private readonly int _damage = 1;

        public override void MakeDamageFromAnimation() =>
            _damageable.TakeDamage(_damage);

        public override void InitializeTarget(IDamageable damageable) =>
            _damageable = damageable;
    }
}