using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private UIHealthController _uiHealthController;

        // Inherited
        private void Start()
        {
            _uiHealthController.UpdateHealth(_healthController.maxHealth, _healthController.maxHealth);
        }

        // Inherited
        private void Update()
        {
            _uiHealthController.UpdateHealth(_healthController.currentHealth, _healthController.maxHealth);
        }
    }
}