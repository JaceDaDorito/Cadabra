using System;
using System.Collections;
using System.Collections.Generic;
using Cadabra.Core;
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
                    DemoHandler.StartTimer();
                    Debug.Log("Timer started");
                    break;
                case "end":
                    DemoHandler.EndTimer();
                    Debug.Log("It took: " + DemoHandler.GetTime() + " seconds");
                    break;
                case "print":
                    DemoHandler.PrintAllTimes();
                    break;
                default:
                    Debug.Log("Invalid type: " + type);
                    break;
            }
        }
        
    }
}


