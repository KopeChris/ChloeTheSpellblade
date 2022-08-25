using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class MainMenu : MonoBehaviour
{

    public void Continue()
    {
        //SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerData.sceneIndexString));
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);

        //SceneManager.LoadScene(1);
    }
    public void DeleteSave()
    {
        SaveGame.Clear();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}