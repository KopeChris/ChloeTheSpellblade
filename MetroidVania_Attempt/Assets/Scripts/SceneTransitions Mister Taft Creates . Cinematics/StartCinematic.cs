using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    public Animator camAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            camAnim.SetBool("cinematic1", true);
            Invoke("StopCutScene", 3f);
        }
    }
    void StopCutScene()
    {
        camAnim.SetBool("cinematic1", false);
    }
}
