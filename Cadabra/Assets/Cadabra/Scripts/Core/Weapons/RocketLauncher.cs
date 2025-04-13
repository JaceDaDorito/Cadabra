using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadabra.Core;
using Cadabra.Projectile;
using Cadabra.ScriptableObjects;
using UnityEngine;

namespace Cadabra.Core
{
    public class RocketLauncher : IShootWand
    {
        private GameObject primaryProjectile;
        public WeaponDef weaponDef;

        WeaponDef IShootWand.weaponDef
        {
            get
            {
                return weaponDef;
            }
            set
            {
                weaponDef = value;
            }
        }

        public void ShootPrimary(WeaponStateMachine wsm)
        {
            if (!primaryProjectile)
            {
                primaryProjectile = weaponDef.FindGameObject("Rocket");
            }

            Vector3 muzzle = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.2f, wsm._cameraController.transform.position.z) + (wsm._cameraController.transform.forward * 1.3f);
            GameObject instance = GameObject.Instantiate(primaryProjectile, muzzle, wsm._cameraController.transform.rotation);
            instance.GetComponent<GenericProjectile>().aimDir = wsm._cameraController.transform.forward;
        }

        public void ShootSecondary(WeaponStateMachine wsm)
        {
            return;
        }
    }
}
