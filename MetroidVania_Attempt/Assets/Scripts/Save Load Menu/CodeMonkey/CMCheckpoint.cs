using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMCheckPoint : Indicator
{
    //public GameObject menu;
    public GameObject interactionIndication;

    public PlayerBasic playerBasic;


    //states
    // bool closeAction = true;
    //bool menuOpen;

    public override void Interact()
    {
        playerBasic.currentHealth = playerBasic.maxHealth;
        playerBasic.berries = playerBasic.maxBerries;
        playerBasic.mana = playerBasic.maxMana;
        interactionIndication.SetActive(false); //close the indication forever
        playerBasic.animator.Play("Pray");


        //playerBasic.Save();
    }

}
