using Assets.CodeBase.Logic.GreenCommand;
using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandUnitHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private uint _unitHealth = 5;
        public event Action OnUnitDied;
        public event Action OnTakeDamage;
        private GreenCommandUnitHealth _greenCommandUnitHealth;

        public void TakeDamage(int damage)
        {
            _unitHealth -= (uint)damage;
            OnTakeDamage?.Invoke();
            if (_unitHealth <= 0)
            {
                Die();
            }
        }

        public void MakeDamage(int damage) =>
            _greenCommandUnitHealth.TakeDamage(damage);

        private void Die()
        {
            if (transform.parent.gameObject != null)
            {
                Destroy(transform.parent.gameObject);
                OnUnitDied?.Invoke();
            }
        }

        public void FullGreenCommandHealthVariable(Collider other) =>
        _greenCommandUnitHealth = other.GetComponentInParent<GreenCommandUnitHealth>();
    }
}