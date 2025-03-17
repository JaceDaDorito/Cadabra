using System.Collections;
using System.Collections.Generic;
using Cadabra.Scripts.Core.Demo;
using UnityEngine;
using UnityEngine.Events;

namespace Cadabra.Core
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterBody))]
    public class HealthController : MonoBehaviour
    {
        public float maxHealth;
        public UnityEvent<CharacterBody> bodyDeathBehavior;
        [HideInInspector]
        public float currentHealth;
        [HideInInspector]
        public CharacterBody body;
        [HideInInspector]
        public bool invincible = false;

        public bool isMaxHealth => currentHealth == maxHealth;


        private void Start()
        {
            currentHealth = maxHealth;
        }

        private void Awake()
        {
            body = this.GetComponent<CharacterBody>();
        }

        public void RequestDamage(DamageInfo damageInfo)
        {
            if (invincible) return;

            //If the damage doesn't ignore team and the team of the attacker and victim are the same, discard damage
            if(damageInfo.attacker != null)
            {
                if (!damageInfo.ignoreTeam && (damageInfo.attacker._team == body._team)) return;
            }

            if (damageInfo.force.sqrMagnitude > 0)
                gameObject.GetComponent<CharacterBody>()._characterMotor.RequestImpulseForce(damageInfo.force);

            
            currentHealth -= (damageInfo.damage * (damageInfo.crit ? damageInfo.critDamageMultiplier : 1));
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            DemoHandler.GetCurrentDemoRound().IncrementDamageTaken(damageInfo.damage);
            if (currentHealth == 0) RequestDeath();
        }

        public void Suicide()
        {
            currentHealth = 0;
            RequestDeath();
        }
        //this is fine for now

        public void RequestDeath()
        {
            if(bodyDeathBehavior != null)
                bodyDeathBehavior.Invoke(body);
        }

        public void Heal(float healAmount)
        {
            float metricHealAmount = healAmount;
            if (currentHealth + healAmount > maxHealth)
            {
                metricHealAmount = maxHealth - currentHealth;
            }
            
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            DemoHandler.GetCurrentDemoRound().IncrementHealthGained(metricHealAmount);
        }
    }
}