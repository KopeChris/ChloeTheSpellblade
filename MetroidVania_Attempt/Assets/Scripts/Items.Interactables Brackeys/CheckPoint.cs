using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //public GameObject menu;
    public GameObject interactionIndication;


    public PlayerBasic player;
    public float radius;


    //states
   // bool closeAction = true;
    //bool menuOpen;
    

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= radius && Input.GetKeyDown(KeyCode.Y))      //when to activate the checkpoint
        {
            // menu.SetActive(true);
           //menuOpen = true;

            player.currentHealth = player.maxHealth;

            interactionIndication.SetActive(false); //close the indication

            player.SavePlayer();
            player.animator.Play("Pray");


        }
        if (distance <= radius)    //Show the indication       if (distance <= radius && !menuOpen) 
        {
            interactionIndication.SetActive(true);
            //closeAction = true;
        }

        if(distance>2*radius)
        {
            //menuOpen = false;
            interactionIndication.SetActive(false);
        }
        /*
        if((distance > 2*radius && closeAction) || UnityEngine.Input.GetKeyDown(KeyCode.O))      //when to close it
        {     
            // menu.SetActive(false); 
            // menuOpen = false;

            closeAction = false; 
            interactionIndication.SetActive(false);
            PlayerBasic.cinematicState = false;
        }
        */
        
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
