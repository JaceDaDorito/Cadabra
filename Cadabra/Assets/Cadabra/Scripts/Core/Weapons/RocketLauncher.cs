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
        private AudioSource sound;
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
                sound = primaryProjectile.GetComponent<AudioSource>();
            }

            Vector3 muzzle = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.2f, wsm._cameraController.transform.position.z) + (wsm._cameraController.transform.forward * 1.3f);
            GameObject instance = GameObject.Instantiate(primaryProjectile, muzzle, wsm._cameraController.transform.rotation);
            GenericProjectile gpInstance = instance.GetComponent<GenericProjectile>();
            gpInstance.aimDir = wsm._cameraController.transform.forward;
            gpInstance.owner = wsm.body;
            AudioSource.PlayClipAtPoint(sound.clip, muzzle);
        }

        public void ShootSecondary(WeaponStateMachine wsm)
        {
            return;
        }
    }
}
