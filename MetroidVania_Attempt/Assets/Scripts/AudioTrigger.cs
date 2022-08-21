using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool on;
    public string audioName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (on)
            FindObjectOfType<AudioManager>().Play(audioName); 
        else
            FindObjectOfType<AudioManager>().Stop(audioName); 

    }
}
