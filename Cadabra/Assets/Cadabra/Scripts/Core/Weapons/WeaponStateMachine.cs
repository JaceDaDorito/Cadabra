using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class WeaponStateMachine : MonoBehaviour
    {
        [SerializeField]
        private CameraController _cameraController;

        LayerMask layerMask = LayerMask.GetMask("World");
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
            Debug.Log($"{hit.collider.gameObject.layer}");
        }
    }

}
