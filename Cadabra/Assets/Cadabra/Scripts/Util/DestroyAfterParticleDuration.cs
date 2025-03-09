using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.Util
{
    public class DestroyAfterParticleDuration : MonoBehaviour
    {
        public ParticleSystem trackedSystem;
        public GameObject[] gameObjectsToDestroy = Array.Empty<GameObject>();

        private float stopwatch = 0;

        public void Start()
        {
            if(trackedSystem == null)
            {
                enabled = false;
                return;
            }
        }

        public void Update()
        {
            stopwatch += Time.deltaTime;
            if(stopwatch > trackedSystem.main.duration)
                DestroyGameObjects();
        }

        private void DestroyGameObjects()
        {
            foreach(GameObject go in gameObjectsToDestroy)
            {
                Destroy(go);
            }
        }
    }
}
