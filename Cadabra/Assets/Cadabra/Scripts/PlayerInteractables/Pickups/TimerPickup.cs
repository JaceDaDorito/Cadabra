using System;
using Cadabra.Core;
using Cadabra.Scripts.Core.Demo;
using UnityEngine;

namespace Cadabra.PlayerInteractables
{
    public class TimerPickup : MonoBehaviour
    {
        // start, end, print
        [SerializeField] private String type;  
        
        public void OnPickup(PlayerBody body)
        {
            switch (type)
            {
                case "start":
                    DemoHandler.StartParkourRun();
                    break;
                case "end":
                    ParkourRound round = DemoHandler.EndParkourRun();
                    if (round != null)
                    {
                        Debug.Log("Timer ended: " + round.GetTime());
                    }
                    else
                    {
                        Debug.Log("No timer running");
                    }
                    break;
                case "print":
                    DemoHandler.PrintParkourRunTimes();
                    break;
                default:
                    Debug.Log("Invalid type: " + type);
                    break;
            }
        }
        
    }
}


