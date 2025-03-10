using System.Linq;
using Cadabra.Core;
using UnityEngine;
using UnityEngine.UIElements;
using KinematicCharacterController;
namespace Cadabra.Scripts.Core.Demo
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        public int checkpointID;
        
        [SerializeField]
        public Transform checkpointTransform;
        
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

            player.GetComponent<KinematicCharacterMotor>().SetPosition(checkpointTransform.position);

            //Debug.Log($"Teleporting {player} to checkpoint: {checkpointID}");
        }



    }
}