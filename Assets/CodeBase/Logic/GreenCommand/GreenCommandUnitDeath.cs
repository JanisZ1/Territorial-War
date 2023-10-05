using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandUnitDeath : MonoBehaviour
    {
        [SerializeField] private int _unitHealth = 5;

        public event Action<GreenCommandUnitDeath> OnUnitDied;
        public event Action OnTakeDamage;

        public void Die()
        {
            Destroy(gameObject);
            OnUnitDied?.Invoke(this);
        }
    }
}