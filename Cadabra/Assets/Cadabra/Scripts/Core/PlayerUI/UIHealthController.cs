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
        [SerializeField]
        private RectTransform _healthForeground;

        private float maxPosX, maxPosY, maxPosZ, maxScaleX, maxScaleY, maxScaleZ;

        // Inherited
        private void Start()
        {
            maxPosX = 50;
            maxPosY = -150;
            maxPosZ = 0;
            maxScaleX = 70;
            maxScaleY = 70;
            maxScaleZ = 1;
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _healthText.text = "Health: " + currentHealth + "/" + maxHealth;
            _healthForeground.localScale = new Vector3(maxScaleX, maxScaleY*(currentHealth/maxHealth), maxScaleZ);
            _healthForeground.localPosition = new Vector3(maxPosX, maxPosY - (0.5f*(maxScaleY*((maxHealth-currentHealth)/maxHealth))), maxPosZ);
        }
    }
}