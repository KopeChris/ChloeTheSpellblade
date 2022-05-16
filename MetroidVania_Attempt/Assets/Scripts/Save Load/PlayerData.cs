using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData  
{
    public int currentHealth;
    public int maxHealth;
    public float[] position;

    public PlayerData(PlayerBasic player)
    {
        currentHealth = player.currentHealth;
        maxHealth = player.maxHealth;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        //scene;
        //coin
        //bosses that died;

    }
}
