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

        private float maxPosY, maxScaleY;

        private void Start()
        {
            maxPosY = _healthForeground.localPosition.y;
            maxScaleY = _healthForeground.localScale.y;
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _healthText.text = "Health" + currentHealth + "/" + maxHealth;
            _healthForeground.localScale = new Vector3(100f, maxScaleY*(currentHealth/maxHealth), 1f);
            _healthForeground.localPosition = new Vector3(100f, maxPosY - (0.5f*(maxScaleY*((maxHealth-currentHealth)/maxHealth))), 0f);
        }
    }
}