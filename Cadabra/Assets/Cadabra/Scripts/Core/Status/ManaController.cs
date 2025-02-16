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

        // Inherited
        void Start()
        {
            currentMana = maxMana;
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