using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _weaponStateMachine.SetInputs(ref wInputs);


        }

        private void Update()
        {
            HandleCharacterInputs();
/*
            // Temp Damage Trigger
            if (Input.GetKeyDown(KeyCode.Keypad1)) _healthController.TakeDamage(25f);

            // Temp Health Trigger
            if (Input.GetKeyDown(KeyCode.Keypad2)) _healthController.Heal(50f);

            // Temp Mana Loss Trigger
            if (Input.GetKeyDown(KeyCode.Keypad3)) _manaController.UseMana(25f);

            // Temp Mana Gain Trigger
            if (Input.GetKeyDown(KeyCode.Keypad4)) _manaController.Syphon(50f);*/
        }

        private void LateUpdate()
        {
            HandleMouseInput();
        }
    }
}

