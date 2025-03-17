using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cadabra.Core;
using Cadabra.Scripts.Core.Demo;

public class DeathMenuManager : MonoBehaviour
{
    public void Respawn()
    {
        DemoHandler.FailDemo();
        SceneManager.LoadScene("DemoPlaytest");
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("BootScene");
        Cursor.visible = true;
        GameManager.instance.currentCheckpoint = null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
