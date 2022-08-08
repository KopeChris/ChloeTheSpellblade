using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;

    public  AudioClip enemyHurtClip;
    public  AudioClip swingClip;
    public  AudioClip playerHurt;
    public  AudioClip footsteps;
    public  AudioClip jump;
    public  AudioClip save;
    public  AudioClip berryMunch;
    public  AudioClip gateClose;
    public  AudioClip speech;

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
    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
    /*
    public void PlayHurt()
    {
        audioSource.PlayOneShot(enemyHurtClip);
    }
    public void PlaySwing()
    {
        audioSource.PlayOneShot(swingClip);
    }
    public void PlayPlayerHurt()
    {
        audioSource.PlayOneShot(playerHurt);
    }
    */
}
