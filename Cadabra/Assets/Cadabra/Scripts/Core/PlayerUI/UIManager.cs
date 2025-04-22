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
        [SerializeField]
        private SyphonController _syphonController;
        [SerializeField]
        private UISyphonController _uiSyphonController;

        private bool isPaused;
        GameObject optionsPanel;

        // Inherited
        private void Start()
        {
            _uiHealthController.UpdateHealth(_healthController.maxHealth, _healthController.maxHealth);
            _uiManaController.UpdateMana(_manaController.maxMana, _manaController.maxMana);
            _uiSyphonController.UpdateSyphonCooldown(_syphonController.timer, _syphonController.syphonCooldownAmount);
            optionsPanel = GameObject.Find("OptionsMenu");
            optionsPanel.SetActive(false);
            isPaused = false;
        }

        // Inherited
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
                Time.timeScale = 0;
                isPaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                optionsPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
                Time.timeScale = 1;
                isPaused = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                optionsPanel.SetActive(false);
            } 
            _uiHealthController.UpdateHealth(_healthController.currentHealth, _healthController.maxHealth);
            _uiManaController.UpdateMana(_manaController.currentMana, _manaController.maxMana);
            _uiSyphonController.UpdateSyphonCooldown(_syphonController.timer, _syphonController.syphonCooldownAmount);
        }
    }
}