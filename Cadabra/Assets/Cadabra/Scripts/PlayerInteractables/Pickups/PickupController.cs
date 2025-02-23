using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;
using UnityEngine.Events;

namespace Cadabra.PlayerInteractables
{
    //temporary prototype
    public class PickupController : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent<PlayerBody> onPickupTriggered;

        //need to look into unity logs
        private void OnValidate()
        {
            Collider comp = gameObject.GetComponent<Collider>();
            if (!comp || !comp.isTrigger) gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerBody comp = other.gameObject.GetComponent<PlayerBody>();

            if (!comp) return;

            onPickupTriggered.Invoke(comp);
        }
    }
}
