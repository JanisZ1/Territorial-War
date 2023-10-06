using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandBullet : MonoBehaviour
    {
        private const int Damage = 1;

        private bool _triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;

            RedCommandUnitHealth redCommandUnitHealth = other.gameObject.GetComponent<RedCommandUnitHealth>();
            if (redCommandUnitHealth)
            {
                _triggered = true;
                redCommandUnitHealth.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}