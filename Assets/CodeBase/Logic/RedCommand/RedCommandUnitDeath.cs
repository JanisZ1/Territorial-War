using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandUnitDeath : MonoBehaviour
    {
        public event Action<RedCommandUnitDeath> OnUnitDied;

        public void Die()
        {
            Destroy(gameObject);
            OnUnitDied?.Invoke(this);
        }
    }
}