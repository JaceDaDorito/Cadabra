using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cadabra.Core
{
    public class UIWeaponManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject rifle;
        [SerializeField]
        private GameObject shotgun;
        [SerializeField]
        private GameObject launcher;

        private Button rifleButton, shotgunButton, launcherButton;
        private RectTransform rifleTransform, shotgunTransform, launcherTransform;

        private int currentWeapon = -1;

        private void Start()
        {
            rifleButton = rifle.GetComponent<Button>();
            shotgunButton = shotgun.GetComponent<Button>();
            launcherButton = launcher.GetComponent<Button>();
            rifleTransform = rifle.GetComponent<RectTransform>();
            shotgunTransform = shotgun.GetComponent<RectTransform>();
            launcherTransform = launcher.GetComponent<RectTransform>();

            rifleButton.interactable = false;
            shotgunButton.interactable = false;
            launcherButton.interactable = false;
        }

        public void EnableWeaponUI(int index)
        {
            if (index == 0) {
                rifleButton.interactable = true;
            } else if (index == 1) {
                shotgunButton.interactable = true;
            } else if (index == 2) {
                launcherButton.interactable = true;
            }
        }

        public void SwitchWeaponUI(int index)
        {
            if (index == 0) {
                SwitchOutWeapon();
                currentWeapon = 0;
                rifleTransform.localPosition = new Vector3(-340, -90, 0);
            } else if (index == 1) {
                SwitchOutWeapon();
                currentWeapon = 1;
                shotgunTransform.localPosition = new Vector3(-340, -125, 0);
            } else if (index == 2) {
                SwitchOutWeapon();
                currentWeapon = 2;
                launcherTransform.localPosition = new Vector3(-340, -160, 0);
            }
        }

        private void SwitchOutWeapon()
        {
            if (currentWeapon == 0) {
                rifleTransform.localPosition = new Vector3(-450, -90, 0);
            } else if (currentWeapon == 1) {
                shotgunTransform.localPosition = new Vector3(-450, -125, 0);
            } else if (currentWeapon == 2) {
                launcherTransform.localPosition = new Vector3(-450, -160, 0);
            }
        }
    }
}