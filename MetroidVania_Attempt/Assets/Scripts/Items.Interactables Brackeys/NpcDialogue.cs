using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : Interactable
{
    public GameObject dialogue;

    public override void Interact()
    {
        base.Interact();
        dialogue.SetActive(true);
    }
}
