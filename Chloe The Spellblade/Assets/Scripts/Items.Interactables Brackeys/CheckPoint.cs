using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Indicator
{
    //public GameObject menu;
    public GameObject interactionIndication;

    public PlayerBasic playerBasic;


    //states
    // bool closeAction = true;
    //bool menuOpen;
    
    public override void Interact()
    {
        FindObjectOfType<AudioManager>().Play("Heal");
        
        interactionIndication.SetActive(false); //close the indication forever
        playerBasic.animator.Play("Pray");

        playerBasic.SaveGameFree();
    }
    
}
