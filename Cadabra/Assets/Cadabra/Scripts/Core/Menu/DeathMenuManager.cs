using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("BootScene");
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
