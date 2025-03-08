using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Cadabra.Core;

namespace Cadabra.Projectile
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GenericProjectile : MonoBehaviour
    {
        public GameObject projectileGhost;
        public Transform projectileGhostNest;
        public float acceleration;
        public float initialSpeed;
        public float lifetime;
        public bool triggerOnImpact;

        [HideInInspector]
        public CharacterBody owner;
        [HideInInspector]
        public Vector3 aimDir;
        [HideInInspector]
        public float time;
        [HideInInspector]
        public GameObject projectileGhostInstance;

        public virtual void Start()
        {
            time = Math.Abs(lifetime);
            if (projectileGhost)
            {
                projectileGhostInstance = Instantiate(projectileGhost, projectileGhostNest ? projectileGhostNest : transform);
            }
        }

        public virtual ImpactInfo ConstructImpactInfo(Collision collision)
        {
            ImpactInfo impactInfo = new ImpactInfo();
            impactInfo.owner = owner;
            if (collision != null)
            {
                impactInfo.collided = true;
                impactInfo.collisionObjectLayer = collision.gameObject.layer;
                impactInfo.impactPoint = collision.contacts.Length == 0 ? collision.collider.transform.position : collision.contacts[0].point;
                impactInfo.normal = collision.contacts.Length == 0 ? Vector3.zero : collision.contacts[0].normal;
            }
            else
            {
                impactInfo.collided = false;
                impactInfo.impactPoint = gameObject.transform.position;
                impactInfo.normal = Vector3.zero;
            }
            return impactInfo;
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
            Trigger(collision);
        }

        public virtual void FixedUpdate()
        {
            time -= Time.fixedDeltaTime;
            if (time >= 0) Trigger(null);
        }

        public virtual void Trigger(Collision collision)
        {
            ImpactInfo impactInfo = ConstructImpactInfo(collision);
        }
    }
}
