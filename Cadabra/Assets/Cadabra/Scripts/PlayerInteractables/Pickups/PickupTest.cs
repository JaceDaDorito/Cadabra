using System;
using System.Collections;
using System.Collections.Generic;
using Cadabra.Core;
using UnityEngine;

namespace Cadabra.PlayerInteractables
{
    public class PickupTest : MonoBehaviour
    {
        [SerializeField]
        public String testString;
        
        
        public void Test(PlayerBody body)
        {
            PlayerInputs inputs = new PlayerInputs();
            inputs.JumpPressed = true;
            body._characterMotor.SetInputs(ref inputs);
            DemoHandler.incrementJumpCount();

            Debug.Log(testString);
        }
        
    }
}


