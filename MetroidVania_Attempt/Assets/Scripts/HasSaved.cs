using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class HasSaved : MonoBehaviour
{
    public static HasSaved instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        hasSaved = SaveGame.Load<bool>("hasSaved");
    }

    public static bool hasSaved;
}
