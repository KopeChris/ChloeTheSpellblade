using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class OpenDoor : Indicator
{
    public SpriteRenderer closed;
    public SpriteRenderer open;
    BoxCollider2D doorCollider;

    public static bool hasInteracted;

    
    private void Start()
    {
        open.enabled = false;
        doorCollider = GetComponent<BoxCollider2D>();

        if (SaveGame.Load<bool>("hasInteracted"))
        {
            Interact();
        }
    }/*
    private void Update()
    {
        
    }*/
    public override void Interact() //open door
    {
        base.Interact();
        open.enabled = true;
        FindObjectOfType<AudioManager>().Play("OpenDoor");

        hasInteracted = true;
        SaveGame.Save<bool>("hasInteracted", true);

        Destroy(GetComponent<BoxCollider2D>());
        Destroy(closed);
        
        //AudioManager.instance.PlayDoorOpen();
    }
}
