using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;
using Cadabra.ScriptableObjects;

namespace Cadabra.PlayerInteractables
{
    public class PickupWeapon : MonoBehaviour
    {
        [SerializeField]
        public WeaponDef grantedWeapon;
        [SerializeField]
        public bool destroyAfterUse = true;
        public void GrantWeapon(PlayerBody body)
        {
            body._weaponStateMachine.GrantAndSwapToWeapon(grantedWeapon);
            if (destroyAfterUse) Destroy(this.gameObject);
        }
    }
}
