using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cadabra.Core;
using Cadabra.Util;

namespace Cadabra.Attacks
{
    public class BulletAttack
    {
        private static LayerMask hurtBoxMask = LayerMask.GetMask("HurtBox");
        private static LayerMask hitMask = LayerMask.GetMask("World") | LayerMask.GetMask("HurtBox");

        public CharacterBody owner;
        public float damage = 1f;
        public bool critsOnWeakPoints = true; //overrides inputted crit
        public bool crit = false;
        public float critDamageMultiplier = 2f;
        public float force = 1f;
        public bool ignoreTeam = false;
        public Vector3 origin;
        public Vector3 aimVec;
        public Transform muzzle; //The bullet comes from the camera, the visual comes from the muzzle
        public float maxDistance = 500f;

        private Vector3 forceVector = new Vector3(0, 0, 0);

        public void Fire()
        {
            
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(origin, aimVec, out hit, maxDistance, hitMask);
            //If the raycast hits
            if (hitSomething)
            {
                if (BitwiseUtils.Contains(hurtBoxMask, hit.collider.gameObject.layer))
                {
                    HurtBox hb = hit.collider.gameObject.GetComponent<HurtBox>();
                    if (!hb)
                    {
                        Debug.LogErrorFormat("GameObject {0} is on HurtBox layer but has no HurtBox component", new object[]
                        {
                            hit.collider.gameObject
                        });
                    }

                    if(force != 0)
                    {
                        forceVector = aimVec.normalized * force;
                    }

                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.attacker = owner;
                    damageInfo.damage = damage;
                    damageInfo.crit = critsOnWeakPoints ? hb.isBulletWeakPoint : crit;
                    damageInfo.critDamageMultiplier = critDamageMultiplier;
                    damageInfo.force = forceVector;
                    damageInfo.ignoreTeam = ignoreTeam;

                    hb.healthController.RequestDamage(damageInfo);
                }
            }
        }

    }
}
