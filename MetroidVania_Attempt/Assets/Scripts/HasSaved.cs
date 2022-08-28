using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class HasSaved : MonoBehaviour
{
    public static HasSaved instance;
    public static bool hasSaved;

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

        
    }
    private void Update()
    {
        if (SaveGame.Exists("hasSaved"))
        {
            hasSaved = SaveGame.Load<bool>("hasSaved");
        }
        else { hasSaved = false; }
    }
}
