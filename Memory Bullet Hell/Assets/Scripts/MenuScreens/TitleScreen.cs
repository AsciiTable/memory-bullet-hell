using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    

    public void PlayGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        if (index < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Going to Game Scene");
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.Log("Game Scene not Found");
        }
    }

    public void ExitApplication()
    {
        Debug.Log("Stopping Application");
        Application.Quit();
    }
}
