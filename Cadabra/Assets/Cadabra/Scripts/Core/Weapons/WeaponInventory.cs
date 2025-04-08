using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;
using Cadabra.Projectile;
using Cadabra.Scripts.Core.Demo;
using Cadabra.ScriptableObjects;

namespace Cadabra.Core
{
    
    public class WeaponInventory : MonoBehaviour
    {
        const int MAXIMUM_WEAPONS = 9;

        public WeaponDefAvailabilityPair[] inventory = new WeaponDefAvailabilityPair[MAXIMUM_WEAPONS];

        public struct WeaponDefAvailabilityPair
        {
            public WeaponDef weaponDef;
            public bool available;
        }

        public bool GrantWeapon(WeaponDef weapon)
        {
            if (weapon.inventorySlot >= MAXIMUM_WEAPONS) return false; //Inventory slot is above the maximum value, this shouldn't happen

            if (inventory[weapon.inventorySlot].available) return false; //Inventory slot is already taken, this shouldn't happen

            inventory[weapon.inventorySlot] = new WeaponDefAvailabilityPair { available = true, weaponDef = weapon };
            return true;
        }

        public bool HasWeapon(WeaponDef weapon)
        {
            return HasWeapon(weapon.inventorySlot);
        }

        public bool HasWeapon(int index)
        {
            return inventory[index].available;
        }

        public static bool WeaponIndexInBounds(int index)
        {
            return index >= 0 && index <= 9;
        }
    }
}
