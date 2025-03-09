using System.Linq;
using Cadabra.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cadabra.Scripts.Core.Demo
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private int checkpointID;
        
        [SerializeField]
        private Transform checkpointTransform;
        
        public CheckPoint(int id)
        {
            checkpointID = id;
        }

        public void SetDemoCheckpoint()
        {
            Debug.Log("Setting checkpoint: " + checkpointID);
            DemoHandler.SetCurrentCheckpoint(this);
        }
        
        public void TeleportToCheckpoint(PlayerBody player)
        {
            if (checkpointTransform == null)
            {
                Debug.LogError($"Checkpoint {checkpointID} has no transform assigned!");
                return;
            }

            // Use KinematicCharacterController to teleport the player

            Debug.Log($"Teleporting to checkpoint: {checkpointID}");
        }



    }
}