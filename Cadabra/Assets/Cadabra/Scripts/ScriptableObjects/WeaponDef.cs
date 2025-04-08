using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Cadabra.Core;
using Cadabra.Util;

namespace Cadabra.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Cadabra/WeaponDef")]
    public class WeaponDef : ScriptableObject
    {
        //Dont really like this very much

        [SerializeField]
        private NameGameObjectPair[] providedObjects = Array.Empty<NameGameObjectPair>();

        [Serializable]
        private struct NameGameObjectPair
        {
            public string name;
            public GameObject gameObject;
        }
        public int Count
        {
            get
            {
                return providedObjects.Length;
            }
        }

        public GameObject FindGameObject(int index)
        {
            return providedObjects[index].gameObject;
        }

        public GameObject FindGameObject(string name)
        {
            foreach (NameGameObjectPair ntp in providedObjects)
            {
                if (ntp.name.Equals(name)) return ntp.gameObject;
            }
            return null;
        }

        public string weaponName;
        public int inventorySlot;
        public GameObject weaponPrefab;
        public bool hasPrimary = true;
        public bool hasSecondary = false;
        public float primaryCooldown = .25f;
        public float secondaryCooldown = 1f;

        private IShootWand IShootWand;
        public IShootWand IShootWandAssociation
        {
            get
            {
                return IShootWand;
            }
            set
            {
                IShootWand = value;
                value.weaponDef = this;
            }
        }
    }
}
