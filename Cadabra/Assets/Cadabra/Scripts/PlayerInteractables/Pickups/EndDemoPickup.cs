using System.Collections;
using System.Collections.Generic;
using Cadabra.Scripts.Core.Demo;
using UnityEngine;

public class EndDemoPickup : MonoBehaviour
{

    public static void EndDemoSession()
    {
        Debug.Log("Ending demo session");
        DemoHandler.EndDemo();
    }
    
}
