using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject menu;
    public GameObject interactionIndication;


    public PlayerBasic player;
    public float radius = 3f;

    bool closeAction = true;
    //states
    bool menuOpen;

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= radius && Input.GetKeyDown(KeyCode.Y))
        {
            player.currentHealth = player.maxHealth;
            PlayerBasic.cinematicState = true;

            menu.SetActive(true);
            interactionIndication.SetActive(false);

            menuOpen = true;

        }
        if (distance <= radius && !menuOpen)
        {
            interactionIndication.SetActive(true);
            closeAction = true;
        }
        if(distance > 2*radius && closeAction)
        {     
            closeAction = false; 
            interactionIndication.SetActive(false); 
            menu.SetActive(false); }
        if (UnityEngine.Input.GetKeyDown(KeyCode.O))
        {
            menu.SetActive(false);
            PlayerBasic.cinematicState = false;
            menuOpen = false;

        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
