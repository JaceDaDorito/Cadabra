using System.Collections;
using System.Collections.Generic;
using Cadabra.Scripts.Core.Demo;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cadabra.Core;
public class EndDemoPickup : MonoBehaviour
{

    public static void EndDemoSession()
    {
        Debug.Log("Ending demo session");
        DemoHandler.EndDemo();
        DemoHandler.SetCurrentCheckpoint(null);
        SceneManager.LoadScene("BootScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.instance.currentCheckpoint = null;   
    }
    
}
