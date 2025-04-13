using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;

namespace Cadabra.Projectile
{
    //very rough implementation
    public class ExplodeOnTrigger : MonoBehaviour
    {
        public float damage = 1f;
        public float radius = 10f;
        public float force = 1f;
        public float additiveUpwardForce = 0f;
        public bool ignoreTeam = false;
        public bool crit = false;
        public bool checkLOS = true;
        public GameObject vfx;
        public AOEFalloff falloffModel;
        public void Explode(ImpactInfo impactInfo)
        {
            AOEAttack aoeAttack = new AOEAttack();
            aoeAttack.owner = impactInfo.owner;
            aoeAttack.damage = damage;
            aoeAttack.crit = crit;
            aoeAttack.force = force;
            aoeAttack.additiveUpwardForce = additiveUpwardForce;
            aoeAttack.ignoreTeam = ignoreTeam;
            aoeAttack.radius = radius;
            aoeAttack.vfxPrefab = vfx;
            aoeAttack.checkLOS = checkLOS;
            aoeAttack.falloffModel = AOEFalloff.Linear;
            aoeAttack.origin = this.gameObject.transform.position;
            aoeAttack.Fire();
        }
    }
}

