using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cadabra.Scripts.Core.Demo;


namespace Cadabra.Core
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public static PlayerSpawnPoint instance;

        private void Start()
        {
            CheckPoint instance = gameObject.AddComponent<CheckPoint>();
            instance.checkpointID = 0;
            instance.checkpointTransform = this.transform;
            DemoHandler.SetCurrentCheckpoint(instance);
        }
        private void OnEnable()
        {
            if (instance)
            {
                //Only one player spawn point should be active at a time.
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void OnDisable()
        {
            if (instance = this)
                instance = null;
        }
    }
}
