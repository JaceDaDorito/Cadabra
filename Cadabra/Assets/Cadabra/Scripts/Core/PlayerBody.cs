using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class PlayerBody : MonoBehaviour
    {
        [SerializeField]
        private CameraController _cameraController;
        [SerializeField]
        private CharacterMotor _characterMotor;
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private ManaController _manaController;
        [SerializeField]
        private Transform _cameraFollowPoint;

        private Vector3 _lookInputVector;

        // Update is called once per frame
        private void Start()
        {
            _cameraController.SetFollowTransform(_cameraFollowPoint);
        }
        private void HandleMouseInput()
        {
            float mouseY = Input.GetAxisRaw("Mouse Y");
            float mouseX = Input.GetAxisRaw("Mouse X");

            _lookInputVector = new Vector3(mouseX, mouseY, 0f);

            float scrollInput = -Input.GetAxis("Mouse ScrollWheel");
            _cameraController.UpdateWithInput(Time.deltaTime, scrollInput, _lookInputVector);
        }

        private void HandleCharacterInputs()
        {
            PlayerInputs inputs = new PlayerInputs();
            inputs.MoveAxisForward = Input.GetAxisRaw("Vertical");
            inputs.MoveAxisRight = Input.GetAxisRaw("Horizontal");
            inputs.JumpPressed = Input.GetKeyDown(KeyCode.Space);
            inputs.CameraRotation = _cameraController.transform.rotation;

            _characterMotor.SetInputs(ref inputs);
            _cameraController.SetRollRotation(Mathf.Clamp(inputs.MoveAxisRight, -1, 1));
        }

        private void Update()
        {
            HandleCharacterInputs();

            // Temp Damage Trigger
            if (Input.GetKeyDown(KeyCode.J)) _healthController.TakeDamage(25f);

            // Temp Health Trigger
            if (Input.GetKeyDown(KeyCode.K)) _healthController.Heal(50f);

            // Temp Mana Loss Trigger
            if (Input.GetKeyDown(KeyCode.Mouse0)) _manaController.UseMana(25f);

            // Temp Mana Gain Trigger
            if (Input.GetKeyDown(KeyCode.E)) _manaController.Syphon(50f);
        }

        private void LateUpdate()
        {
            HandleMouseInput();
        }
    }
}

