using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckOnPlatform : MonoBehaviour
{
    public PlayerBasic player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MovingPlatform") && player.isJumping)
        {
            player.isJumping = false;
            player.transform.parent= collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MovingPlatform"))
        {
            player.transform.parent = null;
        }
    }
}
