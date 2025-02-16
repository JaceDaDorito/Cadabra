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
        [SerializeField]
        private ManaController _manaController;
        [SerializeField]
        private UIManaController _uiManaController;

        // Inherited
        private void Start()
        {
            _uiHealthController.UpdateHealth(_healthController.maxHealth, _healthController.maxHealth);
            _uiManaController.UpdateMana(_manaController.maxMana, _manaController.maxMana);
        }

        // Inherited
        private void Update()
        {
            _uiHealthController.UpdateHealth(_healthController.currentHealth, _healthController.maxHealth);
            _uiManaController.UpdateMana(_manaController.currentMana, _manaController.maxMana);
        }
    }
}