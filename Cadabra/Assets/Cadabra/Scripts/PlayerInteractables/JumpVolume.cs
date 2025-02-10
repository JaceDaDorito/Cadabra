using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;

namespace Cadabra.PlayerInteractables
{
    public class JumpVolume : MonoBehaviour
    {
        [SerializeField]
        public Vector3 _direction;
        [SerializeField]
        public float _magnitude;

        //need to look into unity logs
        private void OnValidate()
        {
            Collider comp = gameObject.GetComponent<Collider>();
            if (!comp || !comp.isTrigger) gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterMotor comp = other.gameObject.GetComponent<CharacterMotor>();
            Debug.Log("gaming");

            if (comp)
            {
                comp.RequestImpulseForce(_direction.normalized, _magnitude);
            }
        }
    }
}
