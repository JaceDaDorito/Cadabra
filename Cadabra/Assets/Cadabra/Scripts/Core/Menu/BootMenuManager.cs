using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootMenuManager : MonoBehaviour
{
    private bool isPaused;
    GameObject optionsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("DemoPlaytest");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    private void Start() 
    {
        optionsPanel = GameObject.Find("OptionsMenu");
        optionsPanel.SetActive(false);
        isPaused = false;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            Time.timeScale = 0;
            isPaused = true;
            optionsPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            Time.timeScale = 1;
            isPaused = false;
            optionsPanel.SetActive(false);
        } 
    }
}
