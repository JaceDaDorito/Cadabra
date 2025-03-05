using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (currentHealth == 0) { 
                SceneManager.LoadScene("DeathScene");
                currentHealth = maxHealth;
            }
        }

        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}