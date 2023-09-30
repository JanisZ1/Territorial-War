using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandUnitHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _unitHealth = 5;

        public event Action OnUnitDied;
        public event Action OnTakeDamage;

        public void TakeDamage(int damage)
        {
            _unitHealth -= damage;
            OnTakeDamage.Invoke();

            if (_unitHealth <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
            OnUnitDied.Invoke();
        }
    }
}