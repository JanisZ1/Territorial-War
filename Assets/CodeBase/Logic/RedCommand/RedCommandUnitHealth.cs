using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandUnitHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _unitHealth = 5;
        [SerializeField] private RedCommandUnitDeath _redCommandUnitDeath;

        public event Action OnTakeDamage;

        public int UnitHealth => _unitHealth;

        public void TakeDamage(int damage)
        {
            _unitHealth -= damage;
            OnTakeDamage?.Invoke();

            if (_unitHealth <= 0)
                _redCommandUnitDeath.Die();
        }
    }
}