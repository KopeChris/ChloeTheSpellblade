using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogue;

    public Transform player;
    public float radius = 3f;

    
    private void Update()
    {

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= radius && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Y)) ) //interact Button Y
        {
            //Debug.Log("Interact");
            dialogue.SetActive(true);
        }
    }
    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
