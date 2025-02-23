using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KinematicCharacterController;
using System.Collections;

public struct PlayerInputs
{
    public float MoveAxisForward;
    public float MoveAxisRight;
    public Quaternion CameraRotation;
    public bool JumpPressed;
}

namespace Cadabra.Core
{
    public class CharacterMotor : MonoBehaviour, ICharacterController
    {
        [SerializeField]
        private KinematicCharacterMotor _motor;

        [SerializeField]
        private Vector3 _gravity = new Vector3(0f, -30f, 0f);

        [SerializeField]
        private float _maxStableMoveSpeed = 10f, _stableMovementSharpness = 15f, _airMovementSharpness = 5f, _noInputAirMovementSharpness = 1f, _orientationSharpness = 10f, _jumpSpeed = 10f;

        [SerializeField]
        private int maxJumps = 1;

        [SerializeField]
        public bool _dontTriggerJumpVolumes;

        private Vector3 _moveInputVector, _lookInputVector, _impulseForceRequested;
        private bool _jumpRequested;
        private bool _forceUngroundRequested;
        public bool _forceNoInput;
        private int currentJumpCount;
        


        private void Start()
        {
            _impulseForceRequested = new Vector3(0, 0, 0);
            _motor.CharacterController = this;
            currentJumpCount = maxJumps;
        }

        public void SetInputs(ref PlayerInputs inputs)
        {
            if (_forceNoInput)
            {
                _moveInputVector = new Vector3(0, 0, 0);
            }
            else
            {
                Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);
                Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, _motor.CharacterUp).normalized;

                if (cameraPlanarDirection.sqrMagnitude == 0f)
                {
                    cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, _motor.CharacterUp).normalized;
                }

                Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, _motor.CharacterUp);

                _moveInputVector = cameraPlanarRotation * moveInputVector;
            }
            _lookInputVector = _moveInputVector.normalized;

            if (inputs.JumpPressed)
            {
                StartCoroutine(RequestJump());
            }
        }

        IEnumerator RequestJump() {
            _jumpRequested = true;
            yield return new WaitForSeconds(.1f);
            _jumpRequested = false;
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
        }

        public void BeforeCharacterUpdate(float deltaTime)
        {
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            currentJumpCount = maxJumps; //reset jumps
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void PostGroundingUpdate(float deltaTime)
        {
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            if(_lookInputVector.sqrMagnitude > 0f && _orientationSharpness > 0f)
            {
                Vector3 smoothedLookInputDirection = Vector3.Slerp(_motor.CharacterForward, _lookInputVector, 1-Mathf.Exp(-_orientationSharpness * deltaTime)).normalized;

                currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, _motor.CharacterUp);
            }
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            float currentVelocityMagnitude = currentVelocity.magnitude;
            Vector3 effectiveGroundNormal = _motor.GroundingStatus.GroundNormal;
            Vector3 inputRight = Vector3.Cross(_moveInputVector, _motor.CharacterUp);
            Vector3 reorientInput;

            if (_motor.GroundingStatus.IsStableOnGround)
            {
                currentVelocity = _motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;
                reorientInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * _moveInputVector.magnitude;
            }
            else
            {
                reorientInput = _moveInputVector.normalized;
            }

            if (_jumpRequested && currentJumpCount > 0 && _motor.GroundingStatus.IsStableOnGround)
            {
                RequestImpulseForce(new Vector3(0, 1, 0), _jumpSpeed);
                _jumpRequested = false;
                currentJumpCount--;
            }

            if (_impulseForceRequested.sqrMagnitude != 0)
            {
                HandleImpulseForce(ref currentVelocity);
            }

            Vector3 targetMovementVelocity = reorientInput * _maxStableMoveSpeed;
            Vector2 currentVelocityXZ = new Vector2(currentVelocity.x, currentVelocity.z);
            Vector2 targetMovementVelocityXZ = new Vector2(targetMovementVelocity.x, targetMovementVelocity.z);
            float movementSharpness = _stableMovementSharpness;
            if (!_motor.GroundingStatus.IsStableOnGround)
            {
                movementSharpness = new Vector2(_lookInputVector.x, _lookInputVector.z).magnitude > 0 ? _airMovementSharpness : _noInputAirMovementSharpness;
            }

            currentVelocityXZ = Vector2.Lerp(currentVelocityXZ, targetMovementVelocityXZ, 1f - Mathf.Exp(-movementSharpness * deltaTime));
            currentVelocity.x = currentVelocityXZ.x;
            currentVelocity.z = currentVelocityXZ.y; //this is z

            if (!_motor.GroundingStatus.IsStableOnGround)
            {
                currentVelocity += _gravity * deltaTime;
            }
        }

        private void HandleImpulseForce(ref Vector3 currentVelocity)
        {
            currentVelocity += (_impulseForceRequested) - Vector3.Project(currentVelocity, _impulseForceRequested.normalized);
            if (_forceUngroundRequested) _motor.ForceUnground();
            _impulseForceRequested = new Vector3(0, 0, 0);
            _forceUngroundRequested = false;
        }

        public void RequestImpulseForce(Vector3 dir, float magnitude)
        {
            if (dir.sqrMagnitude == 0) return;

            dir = dir.normalized;
            if (dir.y > 0) _forceUngroundRequested = true;
            _impulseForceRequested += dir * magnitude;
        }
    }
}
