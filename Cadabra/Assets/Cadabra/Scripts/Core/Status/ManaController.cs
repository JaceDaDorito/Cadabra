using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class ManaController : MonoBehaviour
    {
        public float maxMana;
        [HideInInspector]
        public float currentMana;
        public float manaRegen;
        [Min(0.1f)]
        public float tickRate;

        private float timer = 0;

        public bool isMaxMana => currentMana == maxMana;

        // Inherited
        void Start()
        {
            currentMana = maxMana;
            timer = tickRate;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = tickRate;
                Syphon(manaRegen);
            }
        }

        public void UseMana(float usedAmount)
        {
            currentMana -= usedAmount;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        }

        public void Syphon(float syphonAmount)
        {
            currentMana += syphonAmount;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        }
    }
}