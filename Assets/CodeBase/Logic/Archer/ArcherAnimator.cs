namespace Assets.CodeBase.Logic.Archer
{
    public class ArcherAnimator : GreenCommandAnimator
    {
        private IDamageable _damageable;
        private readonly int _damage = 1;

        public override void MakeDamageFromAnimation() => 
            _damageable.TakeDamage(_damage);

        public override void InitializeTarget(IDamageable damageable) =>
            _damageable = damageable;
    }
}