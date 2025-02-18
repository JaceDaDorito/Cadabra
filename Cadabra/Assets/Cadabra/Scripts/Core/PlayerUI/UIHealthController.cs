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
            maxPosX = _healthForeground.localPosition.x;
            maxPosY = _healthForeground.localPosition.y;
            maxPosZ = _healthForeground.localPosition.z;
            maxScaleX = _healthForeground.localScale.x;
            maxScaleY = _healthForeground.localScale.y;
            maxScaleZ = _healthForeground.localScale.z;
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _healthText.text = "Health: " + currentHealth + "/" + maxHealth;
            _healthForeground.localScale = new Vector3(maxScaleX, maxScaleY*(currentHealth/maxHealth), maxScaleZ);
            _healthForeground.localPosition = new Vector3(maxPosX, maxPosY - (0.5f*(maxScaleY*((maxHealth-currentHealth)/maxHealth))), maxScaleZ);
        }
    }
}