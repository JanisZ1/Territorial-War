using UnityEngine;

namespace Assets.CodeBase.Logic.Archer
{
    public class ArcherAnimator : MonoBehaviour
    {
        private EnemyHealth _enemyHealth;
        private readonly int _damage = 1;

        public void MakeDamageFromAnimation() =>
            _enemyHealth.TakeDamage(_damage);

        public void InitializeEnemyHealthVariable(Collider other) =>
            _enemyHealth = other.GetComponent<EnemyHealth>();
    }
}