using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;

namespace Cadabra.PlayerInteractables
{
    public class PickupMana : MonoBehaviour
    {
        [SerializeField]
        public float syphonAmount;
        [SerializeField]
        public bool destroyAfterUse = true;
        public void Syphon(PlayerBody body)
        {
            if (body._manaController.isMaxMana) return;
            body?._manaController.Syphon(syphonAmount);
            if (destroyAfterUse) Destroy(this.gameObject);
        }
    }
}
