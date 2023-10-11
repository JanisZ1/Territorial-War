using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandUnitHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _unitHealth = 5;

        [SerializeField] private GreenCommandUnitDeath _greenCommandUnitDeath;
        public event Action OnTakeDamage;

        public int UnitHealth => _unitHealth;

        public void TakeDamage(int damage)
        {
            _unitHealth -= damage;
            OnTakeDamage?.Invoke();

            if (_unitHealth <= 0)
                _greenCommandUnitDeath.Die();
        }
    }
}