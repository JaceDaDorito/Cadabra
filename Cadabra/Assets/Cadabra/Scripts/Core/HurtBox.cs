using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.Core
{
    [RequireComponent(typeof(Collider))]
    public class HurtBox : MonoBehaviour
    {
        public Collider collider { get; private set; }
        public bool isBulletWeakPoint = false;

        [HideInInspector]
        public HealthController healthController;
        private void Awake()
        {
            collider = gameObject.GetComponent<Collider>();
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody.isKinematic = true;
        }
    }
}
