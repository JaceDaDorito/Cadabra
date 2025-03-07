using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;
using Cadabra.Attacks;
using Cadabra.Util;

namespace Cadabra.Hazards
{
    [RequireComponent(typeof(ChildLocator))]
    public class LaserTurretBehavior : MonoBehaviour
    {
        public float damage = 1f;
        public float force = 1f;
        public bool ignoreTeam = false;
        public float maxDistance = 500f;
        public float tickRate = 5;
        public float offset = 0;
        public GameObject tracer;

        private BulletAttack bulletAttack;

        private float time;
        private void Awake()
        {
            ChildLocator cd = gameObject.GetComponent<ChildLocator>();

            bulletAttack = new BulletAttack();
            bulletAttack.damage = damage;
            bulletAttack.force = force;
            bulletAttack.ignoreTeam = ignoreTeam;
            bulletAttack.maxDistance = maxDistance;
            bulletAttack.critsOnWeakPoints = false;
            bulletAttack.tracerPrefab = tracer;

            Transform muzzleTransform = cd.FindTransform(0).transform;
            bulletAttack.origin = muzzleTransform.position;
            bulletAttack.aimVec = muzzleTransform.forward;
            bulletAttack.muzzle = muzzleTransform;

            time = tickRate + offset;
        }

        private void FixedUpdate()
        {
            time -= Time.deltaTime;

            if (time > 0) return;

            time = tickRate;

            bulletAttack.Fire();
        }
    }
}

