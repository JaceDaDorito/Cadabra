using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    //simple camera setup for now
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _defaultDistance = 0f,
            _minDistance = 0f,
            _maxDistance = 0f,
            _distanceMovementSpeed = 0f,
            _distanceMovementSharpness = 0f,
            _rotationSpeed = 10f,
            _rotationSharpness = 10000f,
            _rollSharpness = 15f,
            _followSharpness = 10000f,
            _minVerticalAngle = -90f,
            _maxVerticalAngle = 90f,
            _defaultVerticalAngle = 20f,
            _maxRollAngle = 15f;

        private Transform _followTransform;
        private Vector3 _currentFollowPosition, _planarDirection;

        private float _targetVerticalAngle, _targetRollAngle, _currentDistance, _targetDistance, _rollRotationInput;

        private void Awake()
        {
            _currentDistance = _defaultDistance;
            _targetDistance = _currentDistance;
            _targetVerticalAngle = 0f;
            _targetRollAngle = 0f;
            _rollRotationInput = 0f;
            _planarDirection = Vector3.forward;
        }
        public void SetFollowTransform(Transform t)
        {
            _followTransform = t;
            _currentFollowPosition = t.position;
            _planarDirection = t.forward;
        }

        public void SetRollRotation(float rollRotationInput)
        {
            _rollRotationInput = rollRotationInput;
        }
        private void OnValidate()
        {
            _defaultDistance = Mathf.Clamp(_defaultDistance, _minDistance, _maxDistance);
            _defaultVerticalAngle = Mathf.Clamp(_defaultVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void HandleRotationInput(float deltaTime, Vector3 rotationInput, out Quaternion targetRotation)
        {
            Quaternion rotationFromInput = Quaternion.Euler(_followTransform.up * (rotationInput.x * _rotationSpeed));
            _planarDirection = rotationFromInput * _planarDirection;
            Quaternion planarRot = Quaternion.LookRotation(_planarDirection, _followTransform.up);

            _targetVerticalAngle -= (rotationInput.y * _rotationSpeed);
            _targetVerticalAngle = Mathf.Clamp(_targetVerticalAngle, _minVerticalAngle, _maxVerticalAngle);
            Quaternion verticalRot = Quaternion.Euler(_targetVerticalAngle, 0, 0);

            _targetRollAngle = Mathf.Lerp(_targetRollAngle, (_rollRotationInput * _maxRollAngle), _rollSharpness * deltaTime);
            Quaternion rollRot = Quaternion.Euler(0, 0, _targetRollAngle);

            targetRotation = Quaternion.Slerp(transform.rotation, planarRot * verticalRot * rollRot, _rotationSharpness * deltaTime);
            transform.rotation = targetRotation;
        }

        private void HandlePosition(float deltaTime, float zoomInput, Quaternion targetRotation)
        {
            _targetDistance += zoomInput * _distanceMovementSpeed;
            _targetDistance = Mathf.Clamp(_targetDistance, _minDistance, _maxDistance);

            _currentFollowPosition = Vector3.Lerp(_currentFollowPosition, _followTransform.position, 1f - Mathf.Exp(-_followSharpness + deltaTime));
            Vector3 targetPosition = _currentFollowPosition - ((targetRotation * Vector3.forward) * _currentDistance);

            _currentDistance = Mathf.Lerp(_currentDistance, _targetDistance, 1 - Mathf.Exp(-_distanceMovementSharpness * deltaTime));
            transform.position = targetPosition;
        }

        public void UpdateWithInput(float deltaTime, float zoomInput, Vector3 rotationInput)
        {
            if (_followTransform)
            {
                HandleRotationInput(deltaTime, rotationInput, out Quaternion targetRotation);
                HandlePosition(deltaTime, zoomInput, targetRotation);
            }
        }
    }
}

