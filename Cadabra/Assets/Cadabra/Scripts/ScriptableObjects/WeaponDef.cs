using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Cadabra.Core;

namespace Cadabra.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cadabra/WeaponDef")]
    public class WeaponDef : ScriptableObject
    {
        public string weaponName;
        public int inventorySlot;
        public GameObject weaponPrefab;
        public float primaryCooldown = .25f;
        public float secondaryCooldown = 1f;
        public GameObject providedBulletTracer; //this sucks but we are running out of time
        public GameObject providedProjectile;

        public UnityEvent<PlayerBody, WeaponStateMachine> primaryFire;
        public UnityEvent<PlayerBody, WeaponStateMachine> secondaryFire;
    }

    public interface IWeaponAction
    {
        public void ShootPrimary(PlayerBody playerBody, WeaponStateMachine wsm);

        public void ShootSecondary(PlayerBody playerBody, WeaponStateMachine wsm);
    }
}
