using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class HealthController : MonoBehaviour
    {
        public float maxHealth;
        [HideInInspector]
        public float currentHealth;

        public bool isMaxHealth => currentHealth == maxHealth;

        // Inherited
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}