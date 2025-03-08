using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.Core
{
    

    //Object created by damager to send to the victim's HealthController via Request Damage
    public class DamageInfo
    {
        public float damage;

        public bool crit;

        public float critDamageMultiplier;

        public CharacterBody attacker;

        public Vector3 force;

        public bool ignoreTeam;
    }
}
