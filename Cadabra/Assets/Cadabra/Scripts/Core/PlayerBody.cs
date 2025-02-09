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
        private Transform _cameraFollowPoint;

        private Vector3 _lookInputVector;

        // Update is called once per frame
        private void Start()
        {
            _cameraController.SetFollowTransform(_cameraFollowPoint);
        }
        private void HandleCameraInput()
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
        }

        private void Update()
        {
            HandleCharacterInputs();
        }

        private void LateUpdate()
        {
            HandleCameraInput();
        }
    }
}

