using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadabra.Core;
using UnityEngine;

namespace Cadabra.Projectile
{


    //Object created by damager to send to the victim's HealthController via Request Damage
    public class ImpactInfo
    {
        public CharacterBody owner;

        public bool collided;

        public LayerMask collisionObjectLayer;

        public Vector3 impactPoint;

        public Vector3 normal;
    }
}