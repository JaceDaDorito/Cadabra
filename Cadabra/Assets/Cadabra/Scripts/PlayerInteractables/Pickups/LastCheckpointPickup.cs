using Cadabra.Core;
using Cadabra.Scripts.Core.Demo;
using UnityEngine;

namespace Cadabra.PlayerInteractables
{
    public class LastCheckpointPickup : MonoBehaviour
    {
        public static void ReturnToLastCheckpoint(PlayerBody body)
        {
            DemoHandler.ReturnToLastCheckpoint(body);
        }
    }
}