using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;

    public  AudioClip enemyHurtClip;
    public  AudioClip fireHurt;
    public  AudioClip swingClip;
    public  AudioClip playerHurt;
    public  AudioClip death;
    public  AudioClip jump;
    public  AudioClip save;
    public  AudioClip berryMunch;
    public  AudioClip heal;
    public  AudioClip gateClose;
    public  AudioClip speech;
    public  AudioClip roll;

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
    public void LoopSound()
    {
        audioSource.loop=true;
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
