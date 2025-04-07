using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;
using Cadabra.Projectile;
using Cadabra.Scripts.Core.Demo;
using Cadabra.ScriptableObjects;

namespace Cadabra.Core
{
    public class WeaponStateMachine : MonoBehaviour
    {
        private static LayerMask hitMask;

        public CameraController _cameraController;
        public PlayerBody body;
        public WeaponInventory weaponInventory;

        public LayerMask layerMask;
        public GameObject tracer;
        public GameObject projectile;
        
        [HideInInspector]
        public float primaryStopwatch = 0f;
        [HideInInspector]
        public float secondaryStopwatch = 0f;

        [SerializeField]
        private int currentWeaponIndex = -1;
        public WeaponDef currentWeapon
        {
            get
            {
                if (WeaponInventory.WeaponIndexInBounds(currentWeaponIndex))
                    return weaponInventory.inventory[currentWeaponIndex].weaponDef;
                return null;
            }
        }
        public struct WeaponInputs
        {
            public bool PrimaryPressed;
            public bool SecondaryPressed;
            public bool WeaponKeyPressed;
            public int WeaponIndexPressed;
        }
        void Start()
        {
            hitMask = LayerMask.GetMask("World") | LayerMask.GetMask("HurtBox");
            weaponInventory = gameObject.AddComponent<WeaponInventory>();
        }
        void Update()
        {
            if (primaryStopwatch > 0) primaryStopwatch -= Time.deltaTime;
            if (secondaryStopwatch > 0) secondaryStopwatch -= Time.deltaTime;
        }

        public void SetInputs(ref WeaponInputs inputs)
        {
            if (inputs.WeaponKeyPressed)
            {
                if (weaponInventory.HasWeapon(inputs.WeaponIndexPressed))
                {
                    SwapWeapon(inputs.WeaponIndexPressed);
                }
            }

            if (!currentWeapon) return;

            if (inputs.PrimaryPressed && primaryStopwatch <= 0) currentWeapon.primaryFire?.Invoke(body, this);
            if (inputs.SecondaryPressed && secondaryStopwatch <= 0) currentWeapon.secondaryFire?.Invoke(body, this);
        }

        public void GrantAndSwapToWeapon(WeaponDef weaponDef)
        {
            weaponInventory.GrantWeapon(weaponDef);
            SwapWeapon(weaponDef.inventorySlot);
        }

        //Probably set a buffer + cache for swapping weapons

        private void SwapWeapon(int weaponIndex)
        {
            currentWeaponIndex = weaponIndex;
            primaryStopwatch = 0f;
            secondaryStopwatch = -0f;
        }


        /*private void ShootPrimary()
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
            bulletAttack.muzzleOverride = new Vector3(_cameraController.transform.position.x, _cameraController.transform.position.y - 0.8f, _cameraController.transform.position.z);
            bulletAttack.Fire();

            body._manaController.UseMana(5f);
        }

        private void ShootSecondary()
        {
            secondaryStopwatch = secondaryCooldown;

            Vector3 muzzle = new Vector3(_cameraController.transform.position.x, _cameraController.transform.position.y - 0.2f, _cameraController.transform.position.z) + (_cameraController.transform.forward * 1.3f);
            GameObject instance = GameObject.Instantiate(projectile, muzzle, _cameraController.transform.rotation);
            instance.GetComponent<GenericProjectile>().aimDir = _cameraController.transform.forward;

            body._manaController.UseMana(10f);

            
        }*/
    }

}
