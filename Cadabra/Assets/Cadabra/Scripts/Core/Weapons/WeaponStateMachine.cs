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

            if (currentWeapon.hasPrimary && inputs.PrimaryPressed && primaryStopwatch <= 0) {
                primaryStopwatch = currentWeapon.primaryCooldown;
                currentWeapon.IShootWandAssociation.ShootPrimary(this);
            }
            if (currentWeapon.hasSecondary && inputs.SecondaryPressed && secondaryStopwatch <= 0)
            {
                secondaryStopwatch = currentWeapon.secondaryCooldown;
                currentWeapon.IShootWandAssociation.ShootSecondary(this);
            }

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
            secondaryStopwatch = 0f;
        }
    }

    public interface IShootWand
    {
        public WeaponDef weaponDef { get; set; }
        public void ShootPrimary(WeaponStateMachine wsm);

        public void ShootSecondary(WeaponStateMachine wsm);
    }
}
