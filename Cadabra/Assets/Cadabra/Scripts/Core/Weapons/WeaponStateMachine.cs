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
        public GameObject syphonTracer;
        
        [HideInInspector]
        public float shotStopwatch = 0f;
        //[HideInInspector]
        //public float secondaryStopwatch = 0f;
        [HideInInspector]
        public float swapStopwatch = 0f;

        private bool primaryBuffer = false;
        private bool secondaryBuffer = false;

        public const float SWAP_PERIOD = 0.25f;

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
        private float syphonCooldown = 5f;
        private float syphonStopwatch = 0f;
        public struct WeaponInputs
        {
            public bool PrimaryPressed;
            public bool SecondaryPressed;
            public bool WeaponKeyPressed;
            public int WeaponIndexPressed;
            public bool SyphonPressed;
        }
        void Start()
        {
            hitMask = LayerMask.GetMask("World") | LayerMask.GetMask("HurtBox");
            weaponInventory = gameObject.AddComponent<WeaponInventory>();
            
        }
        void Update()
        {
            if (swapStopwatch > 0) swapStopwatch -= Time.deltaTime;
            if (shotStopwatch > 0) shotStopwatch -= Time.deltaTime;
            //if (secondaryStopwatch > 0) secondaryStopwatch -= Time.deltaTime;
            if (syphonStopwatch > 0) syphonStopwatch -= Time.deltaTime;
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

            if (swapStopwatch <= 0)
            {
                //If the weapon has a primary attack, your current mana is above the mana cost, and the primary cooldown is over
                bool canFirePrimary = currentWeapon.hasPrimary &&
                    body._manaController.currentMana >= currentWeapon.manaCost &&
                    shotStopwatch <= 0;
                //If you press the button or buffer an input
                bool primaryInput = primaryBuffer || inputs.PrimaryPressed;

                //If the weapon has a secondary attack, your current mana is above the mana cost, and the secondary cooldown is over
                bool canFireSecondary = currentWeapon.hasSecondary &&
                    body._manaController.currentMana >= currentWeapon.secondaryCost &&
                    //secondaryStopwatch <= 0 &&
                    shotStopwatch <= 0;
                //If you press the button or buffer an input
                bool secondaryInput = secondaryBuffer || inputs.SecondaryPressed;

                if (primaryInput && canFirePrimary)
                {
                    primaryBuffer = false;
                    shotStopwatch = currentWeapon.primaryCooldown;
                    currentWeapon.IShootWandAssociation.ShootPrimary(this);
                    body._manaController.UseMana(currentWeapon.manaCost);
                }
                else if (secondaryInput && canFireSecondary)
                {
                    secondaryBuffer = false;
                    shotStopwatch = currentWeapon.secondaryCooldown;
                    //secondaryStopwatch = currentWeapon.secondaryCooldown;
                    currentWeapon.IShootWandAssociation.ShootSecondary(this);
                    body._manaController.UseMana(currentWeapon.secondaryCost);
                }
            }
            else
            {
                //Buffer inputs during weapon swap window
                if(!primaryBuffer && !secondaryBuffer)
                {
                    if (inputs.PrimaryPressed) primaryBuffer = true;
                    else if (inputs.SecondaryPressed) secondaryBuffer = true;
                }
            }
            if (inputs.SyphonPressed && syphonStopwatch <= 0) ShootSyphon();
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
            shotStopwatch = 0f;
            //secondaryStopwatch = 0f;
            swapStopwatch = SWAP_PERIOD;
        }

        private void ShootSyphon()
        {
            syphonStopwatch = syphonCooldown;

            BulletAttack syphonAttack = new BulletAttack();
            syphonAttack.damage = 100f;
            syphonAttack.force = 0f;
            syphonAttack.ignoreTeam = true;
            syphonAttack.maxDistance = 50f;
            syphonAttack.critsOnWeakPoints = false;
            syphonAttack.tracerPrefab = syphonTracer;
            syphonAttack.origin = _cameraController.transform.position;
            syphonAttack.aimVec = _cameraController.transform.forward;
            syphonAttack.overrideMuzzle = true;
            syphonAttack.muzzleOverride = new Vector3(_cameraController.transform.position.x, _cameraController.transform.position.y - 0.8f, _cameraController.transform.position.z);
            syphonAttack.manaController = body._manaController;
            syphonAttack.isSyphon = true;
            syphonAttack.syphonAmount = 50f;
            syphonAttack.Fire();

            body._syphonController.UseSyphon();
        }
    }

    public interface IShootWand
    {
        public WeaponDef weaponDef { get; set; }
        public void ShootPrimary(WeaponStateMachine wsm);

        public void ShootSecondary(WeaponStateMachine wsm);
    }
}
