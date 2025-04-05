using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("DemoPlaytest");
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
