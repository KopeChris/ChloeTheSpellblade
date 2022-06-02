using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerData.sceneIndexString));

        //SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);

        //SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}