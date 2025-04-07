using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cadabra.Core
{
    public class PlayerBody : CharacterBody
    {
        

        [SerializeField]
        private CameraController _cameraController;
        [SerializeField]
        private GameObject _uiCamera;
        [SerializeField]
        private Transform _cameraFollowPoint;

        private Vector3 _lookInputVector;

        // Update is called once per frame
        private void Start()
        {
            _cameraController.SetFollowTransform(_cameraFollowPoint);
            
        }
        //Refactor inputs at some point
        protected virtual void HandleMouseInput()
        {
            float mouseY = Input.GetAxisRaw("Mouse Y");
            float mouseX = Input.GetAxisRaw("Mouse X");

            _lookInputVector = new Vector3(mouseX, mouseY, 0f);

            float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
            _cameraController.UpdateWithInput(Time.deltaTime, scrollInput, _lookInputVector);
        }

        protected virtual void HandleCharacterInputs()
        {
            PlayerInputs inputs = new PlayerInputs();
            inputs.MoveAxisForward = Input.GetAxisRaw("Vertical");
            inputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
            inputs.JumpPressed = Input.GetKeyDown(KeyCode.Space);
            inputs.CameraRotation = _cameraController.transform.rotation;

            _characterMotor.SetInputs(ref inputs);
            _cameraController.SetRollRotation(Mathf.Clamp(inputs.MoveAxisRight, -1, 1));

            WeaponStateMachine.WeaponInputs wInputs = new WeaponStateMachine.WeaponInputs();
            wInputs.PrimaryPressed = Input.GetKey(KeyCode.Mouse0);
            wInputs.SecondaryPressed = Input.GetKeyDown(KeyCode.Mouse1);
            int weaponIndexPressed = -1;
            wInputs.WeaponKeyPressed = GetPressedWeaponKey( out weaponIndexPressed);
            wInputs.WeaponIndexPressed = weaponIndexPressed;
            
            _weaponStateMachine.SetInputs(ref wInputs);


            bool kill = Input.GetKey(KeyCode.K);
            if (kill) _healthController.Suicide();
        }

        

        private void Update()
        {
            HandleCharacterInputs();
        }

        //so it is easier to rebind later
        public static KeyCode[] keyCodes =
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
        };
        public static bool GetPressedWeaponKey(out int weaponIndexPressed)
        {
            weaponIndexPressed = -1;
            foreach (KeyCode kc in keyCodes)
            {
                weaponIndexPressed++;
                if (!Input.GetKeyDown(keyCodes[weaponIndexPressed])) continue;

                if (WeaponInventory.WeaponIndexInBounds(weaponIndexPressed)) continue;

                return true;
            }
            weaponIndexPressed = -1;
            return false;
        }

        private void LateUpdate()
        {
            HandleMouseInput();
        }
    }
}

