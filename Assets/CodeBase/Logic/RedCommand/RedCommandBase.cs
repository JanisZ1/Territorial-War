using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandBase : MonoBehaviour
    {
        private int _health = 10;
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}