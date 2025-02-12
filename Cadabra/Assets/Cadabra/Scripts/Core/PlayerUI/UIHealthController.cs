using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cadabra.Core
{
    public class UIHealthController : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _healthText;

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _healthText.text = "Health" + currentHealth + "/" + maxHealth;
        }
    }
}