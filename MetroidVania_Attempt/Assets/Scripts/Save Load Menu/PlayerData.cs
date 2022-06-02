using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData  
{
    public float[] position;
    public int currentHealth;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public int coin;
    public static int sceneIndex;
    public static string sceneIndexString;

    public PlayerData(PlayerBasic player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        currentHealth = player.currentHealth;
        maxHealth = player.maxHealth;
        mana = player.mana;
        maxMana = player.maxMana;
        coin = player.playerCoin;

        //sceneIndex=SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(sceneIndexString, SceneManager.GetActiveScene().buildIndex);

        //bosses that died;

    }
}
