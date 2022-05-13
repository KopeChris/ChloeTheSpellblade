using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;

    public  AudioClip hurtClip;
    public  AudioClip swingClip;
    public  AudioClip playerHurt;

    private void Awake()
    {
        if(instance != null && instance !=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    public void PlayHurt()
    {
        audioSource.PlayOneShot(hurtClip);
    }
    public void PlaySwing()
    {
        audioSource.PlayOneShot(swingClip);
    }
    public void PlayPlayerHurt()
    {
        audioSource.PlayOneShot(playerHurt);
    }

}
