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
    public class WeaponDef : ScriptableObject, ISerializationCallbackReceiver
    {
        public string weaponName;
        public int inventorySlot;
        public float fireRate;
        public GameObject weaponPrefab;

        public UnityEvent<PlayerBody, WeaponStateMachine> primaryFire;
        public UnityEvent<PlayerBody, WeaponStateMachine> secondaryFire;

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
