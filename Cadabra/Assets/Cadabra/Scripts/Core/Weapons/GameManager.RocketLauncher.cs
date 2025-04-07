using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;
using Cadabra.Projectile;
using Cadabra.Scripts.Core.Demo;
using Cadabra.ScriptableObjects;

namespace Cadabra.Core
{
    public partial class GameManager : MonoBehaviour
    {
        public static void ShootRocket(PlayerBody body, WeaponStateMachine wsm)
        {
            wsm.secondaryStopwatch = wsm.currentWeapon.secondaryCooldown;

            Vector3 muzzle = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.2f, wsm._cameraController.transform.position.z) + (wsm._cameraController.transform.forward * 1.3f);
            GameObject instance = GameObject.Instantiate(wsm.currentWeapon.providedProjectile, muzzle, wsm._cameraController.transform.rotation);
            instance.GetComponent<GenericProjectile>().aimDir = wsm._cameraController.transform.forward;

            body._manaController.UseMana(10f);
        }
    }
}
