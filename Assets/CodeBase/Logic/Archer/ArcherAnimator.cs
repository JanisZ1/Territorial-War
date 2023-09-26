﻿using UnityEngine;

namespace Assets.CodeBase.Logic.Archer
{
    public class ArcherAnimator : MonoBehaviour
    {
        private IDamageable _damageable;
        private readonly int _damage = 1;

        public void MakeDamageFromAnimation() =>
            _damageable.TakeDamage(_damage);

        public void InitializeTarget(Collider other) =>
            _damageable = other.GetComponent<IDamageable>();
    }
}