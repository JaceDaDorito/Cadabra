using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cadabra.Core;
using Cadabra.Util;
using Cadabra.VFX;
using KinematicCharacterController;

namespace Cadabra.Attacks
{

    public enum AOEFalloff { 
        None, 
        Linear
    }
    public class AOEAttack
    {
        private static LayerMask hitboxMask = LayerMask.GetMask("HurtBox") | LayerMask.GetMask("EnemyHurtBox");
        private static LayerMask worldMask = LayerMask.GetMask("World");

        public CharacterBody owner;
        public float damage = 1f;
        public bool crit = false;
        public float critDamageMultiplier = 2f;
        public float force = 1f;
        public float additiveUpwardForce = 0f;
        public bool ignoreTeam = false;
        public bool checkLOS = true;
        public Vector3 origin = new Vector3(0,0,0);
        public float radius = 10f;
        public GameObject vfxPrefab;
        public AOEFalloff falloffModel;

        private Collider[] collisions = Array.Empty<Collider>();
        private HashSet<HealthController> hitHealthControllers = new HashSet<HealthController>();
        public bool hitSomething => collisions.Length > 0;

        public void Fire()
        {
            InstantiateVfx();

            collisions = Physics.OverlapSphere(origin, radius, hitboxMask);
            if (!hitSomething) return;

            collisions = collisions.OrderBy(c => (origin - c.transform.position).sqrMagnitude).ToArray();

            QueryCollisionResults();
        }

        private void InstantiateVfx()
        {
            if(vfxPrefab)
                GameObject.Instantiate(vfxPrefab, origin, Quaternion.identity);
        }

        private void QueryCollisionResults()
        {
            foreach(Collider c in collisions)
            {
                if (!BitwiseUtils.Contains(hitboxMask, c.gameObject.layer)) return;

                CharacterBody cb = c.gameObject.GetComponent<HurtBox>().healthController.body;
                if (!cb)
                {
                    Debug.LogErrorFormat("GameObject {0} is on Player layer but has no CharacterBody component", new object[]
                    {
                        c.gameObject
                    });
                    continue;
                }


                Vector3 displacement = new Vector3(0f,0f,0f);
                float coeff = 1;
                Vector3 direction = EvaluateAOEFalloff(c, ref displacement, ref coeff);
                float instancedForceMagnitude = force * coeff;
                float instancedUpwardForceMagnitude = additiveUpwardForce * coeff;
                Vector3 forceVector = new Vector3(0, 0, 0);

                if (checkLOS && Physics.Raycast(origin, displacement.normalized, displacement.magnitude, worldMask))
                {
                    return; //Fail LOS
                }

                if (hitHealthControllers.Contains(cb._healthController)) continue; //This attack already hit this particular body.
                hitHealthControllers.Add(cb._healthController);

                if (force != 0)
                {
                    forceVector = direction * instancedForceMagnitude;
                }

                DamageInfo damageInfo = new DamageInfo();
                damageInfo.attacker = owner;
                damageInfo.damage = damage;
                damageInfo.crit = crit;
                damageInfo.critDamageMultiplier = critDamageMultiplier;
                damageInfo.force = forceVector + (instancedUpwardForceMagnitude * Vector3.up);
                damageInfo.ignoreTeam = ignoreTeam;

                cb._healthController.RequestDamage(damageInfo);
            }
        }

        private Vector3 EvaluateAOEFalloff(Collider hb, ref Vector3 displacement, ref float coeff)
        {
            displacement = hb.bounds.center - origin;
            Vector3 direction = displacement.normalized;
            float t = displacement.magnitude / radius;

            Debug.Log("Displacement: " + displacement + " Magnitude: " + displacement.magnitude + " Value: " + t);


            switch(falloffModel){
                case AOEFalloff.Linear:
                    coeff = Mathf.Lerp(1, 0, t);
                    break;
            }

            return direction;

        }
    }
}
