using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;
using Cadabra.Attacks;
using Cadabra.Util;
using Cadabra.Projectile;

namespace Cadabra.Hazards
{
    [RequireComponent(typeof(ChildLocator))]
    public class ProjectileTurretBehavior : MonoBehaviour
    {

        public float tickRate = 5;
        public float offset = 0;
        public GameObject projectile;
        private Transform muzzleTransform;

        private float time;
        private void Awake()
        {
            ChildLocator cd = gameObject.GetComponent<ChildLocator>();
            muzzleTransform = cd.FindTransform("Muzzle").transform;
            time = tickRate + offset;
        }

        private void FixedUpdate()
        {
            time -= Time.deltaTime;

            if (time > 0) return;

            time = tickRate;

            GameObject instance = GameObject.Instantiate(projectile, muzzleTransform.position, muzzleTransform.rotation);
            instance.GetComponent<GenericProjectile>().aimDir = muzzleTransform.forward;
        }
    }
}

