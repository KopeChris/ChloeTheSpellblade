using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogue : Indicator
{
    public GameObject dialogue;

    private void Awake()
    {
        dialogue.SetActive(false);
    }
    
    public override void Interact()
    {
        // this method is meant to be overwritten
        base.Interact();
        dialogue.SetActive(true);
    }

}
