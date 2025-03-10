using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cadabra.Core;

public class DeathMenuManager : MonoBehaviour
{
    public void Respawn()
    {
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
