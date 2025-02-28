using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cadabra/WeaponDef")]
    public class WeaponDef : ScriptableObject, ISerializationCallbackReceiver
    {
        public string weaponName;
        public int inventorySlot;
        public float fireRate;
        public GameObject weaponPrefab;

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
