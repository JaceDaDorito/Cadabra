using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;

namespace Cadabra.PlayerInteractables
{
    public class PickupHeal : MonoBehaviour
    {
        [SerializeField]
        public float healAmount;
        [SerializeField]
        public bool destroyAfterUse = true;
        public void Heal(PlayerBody body)
        {
            if (body._healthController.isMaxHealth) return;
            body?._healthController.Heal(healAmount);
            if (destroyAfterUse) Destroy(this.gameObject);
        }
    }
}
