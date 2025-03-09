using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;
using Cadabra.Projectile;

namespace Cadabra.Core
{
    //THIS SUCKS AND IS TEMPORARY
    public class WeaponStateMachine : MonoBehaviour
    {
        private static LayerMask hitMask;

        [SerializeField]
        private CameraController _cameraController;
        public CharacterBody body;

        [SerializeField]
        public LayerMask layerMask;
        public GameObject tracer;
        public GameObject projectile;
        
        private float primaryCooldown = .25f;
        private float secondaryCooldown = 1f;
        private float primaryStopwatch = 0f;
        private float secondaryStopwatch = 0f;
        public struct WeaponInputs
        {
            public bool PrimaryPressed;
            public bool SecondaryPressed;
        }
        void Start()
        {
            hitMask = LayerMask.GetMask("World") | LayerMask.GetMask("HurtBox");
        }
        void Update()
        {
            if (primaryStopwatch > 0) primaryStopwatch -= Time.deltaTime;
            if (secondaryStopwatch > 0) secondaryStopwatch -= Time.deltaTime;
        }

        public void SetInputs(ref WeaponInputs inputs)
        {
            if (inputs.PrimaryPressed && primaryStopwatch <= 0) ShootPrimary();
            if (inputs.SecondaryPressed && secondaryStopwatch <= 0) ShootSecondary();
        }

        private void ShootPrimary()
        {
            primaryStopwatch = primaryCooldown;

            BulletAttack bulletAttack = new BulletAttack();
            bulletAttack.damage = 5f;
            bulletAttack.force = 0f;
            bulletAttack.ignoreTeam = false;
            bulletAttack.maxDistance = 50f;
            bulletAttack.critsOnWeakPoints = true;
            bulletAttack.tracerPrefab = tracer;
            bulletAttack.origin = _cameraController.transform.position;
            bulletAttack.aimVec = _cameraController.transform.forward;
            bulletAttack.overrideMuzzle = true;
            bulletAttack.muzzleOverride = new Vector3(_cameraController.transform.position.x, _cameraController.transform.position.y - 2f, _cameraController.transform.position.z);
            bulletAttack.Fire();

            body._manaController.UseMana(5f);
        }

        private void ShootSecondary()
        {
            secondaryStopwatch = secondaryCooldown;

            Vector3 muzzle = new Vector3(_cameraController.transform.position.x, _cameraController.transform.position.y - 0.2f, _cameraController.transform.position.z) + _cameraController.transform.forward * 3;
            GameObject instance = GameObject.Instantiate(projectile, muzzle, _cameraController.transform.rotation);
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(_cameraController.transform.position, _cameraController.transform.forward, out hit, 20f, hitMask);
            instance.GetComponent<GenericProjectile>().aimDir = hitSomething ? Vector3.Normalize(hit.point - muzzle) : _cameraController.transform.forward;

            body._manaController.UseMana(10f);
        }
    }

}
