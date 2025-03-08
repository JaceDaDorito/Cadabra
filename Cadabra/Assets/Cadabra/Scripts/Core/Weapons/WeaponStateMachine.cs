using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class WeaponStateMachine : MonoBehaviour
    {
        [SerializeField]
        private CameraController _cameraController;

        [SerializeField]
        public LayerMask layerMask;
        public struct WeaponInputs
        {
            public bool PrimaryPressed;
        }
        void Start()
        {

        }
        void Update()
        {

        }

        //very temporary

        public void SetInputs(ref WeaponInputs inputs)
        {
            if (inputs.PrimaryPressed) Shoot();
        }

        private void Shoot()
        {
            RaycastHit hit;
            Physics.Raycast(_cameraController.transform.position, _cameraController.transform.forward, out hit, 100);
            if(hit.collider)Debug.Log($"{hit.collider.gameObject.layer}");

        }
    }

}
