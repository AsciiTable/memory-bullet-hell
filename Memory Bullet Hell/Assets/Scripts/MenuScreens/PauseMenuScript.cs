using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MenuScript
{
    /*
    * This script depends on the scenes being in a specific order:
    * 0: Main Menu
    * 1: Game Scene
    */
    private Canvas pauseMenu = null;
    private bool paused = false;

    private void OnEnable()
    {
        UpdateHandler.UpdateOccurred += CheckPaused;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
        UpdateHandler.UpdateOccurred -= CheckPaused;
    }

    private void Start()
    {
        pauseMenu = GetComponentInChildren<Canvas>();
        pauseMenu.gameObject.SetActive(false);
    }
    public void CheckPaused()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);
        paused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
        paused = false;
    }
}
