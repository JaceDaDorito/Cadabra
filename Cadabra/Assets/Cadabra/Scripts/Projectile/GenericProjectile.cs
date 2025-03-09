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
    //I can abstract this further but this will do for now

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GenericProjectile : MonoBehaviour
    {
        public GameObject projectileGhost;
        public Transform projectileGhostNest;
        [Min (0.01f)]
        public float speedCoefficient = 1f;
        public bool enabledSpeedOverTime;
        public AnimationCurve speedOverTime;
        [Min(0f)]
        public float lifetime = 7f;
        public bool triggerOnImpact = true;

        public UnityEvent<ImpactInfo> onTrigger;

        [HideInInspector]
        public CharacterBody owner;
        [HideInInspector]
        public Vector3 aimDir;
        [HideInInspector]
        public float time;
        [HideInInspector]
        public GameObject projectileGhostInstance;
        [HideInInspector]
        public Collider collider;
        [HideInInspector]
        public Rigidbody rigidbody;

        public virtual void Start()
        {
            Debug.Log(aimDir);
            collider = gameObject.GetComponent<Collider>();
            rigidbody = gameObject.GetComponent<Rigidbody>();
            time = 0;
            if (projectileGhost)
            {
                projectileGhostInstance = Instantiate(projectileGhost, projectileGhostNest ? projectileGhostNest : transform);
            }
            if (aimDir.sqrMagnitude != 0) {
                aimDir.Normalize();
                transform.LookAt(aimDir);
                SetVelocity(speedCoefficient, aimDir);
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
            if (triggerOnImpact) Trigger(collision);
        }

        public virtual void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > lifetime) Trigger(null);

            if (enabledSpeedOverTime) SetVelocity(speedCoefficient, aimDir);
        }

        protected void SetVelocity(float speed, Vector3 dir)
        {
            
            if (!rigidbody) return;

            if (enabledSpeedOverTime)
                rigidbody.velocity = speed * speedOverTime.Evaluate(time / lifetime) * dir;
            else
                rigidbody.velocity = speed * dir;
        }

        protected virtual void Trigger(Collision collision)
        {
            
            ImpactInfo impactInfo = ConstructImpactInfo(collision);
            onTrigger.Invoke(impactInfo);
            Destroy(this.gameObject);
        }
        public virtual void Destroy(ImpactInfo impactInfo)
        {
            Destroy(gameObject);
        }
    }
}
