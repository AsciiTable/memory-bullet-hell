using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject lore;
    /*
     * This script depends on the scenes being in a specific order:
     * 0: Main Menu
     * 1: Game Scene
     */
    public void GoToGame()
    {

        if (1 < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Going to Game Scene");
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Game Scene not Found");
        }
    }

    public void GoToTitle()
    {
        if (0 < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Going to Title Scene");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Title Scene not Found");
        }
    }

    public void ExitApplication()
    {
        Debug.Log("Stopping Application");
        Application.Quit();
    }

    public void GoToCredits() {
        credits.SetActive(true);
        main.SetActive(false);
    }

    public void GoToLore() {
        lore.SetActive(true);
        main.SetActive(false);
    }

    public void GoBackFromCredits() {
        main.SetActive(true);
        credits.SetActive(false);
    }

    public void GoBackFromLore()
    {
        main.SetActive(true);
        lore.SetActive(false);
    }


}
