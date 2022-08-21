using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using EZCameraShake;

public class BossDoorStartCinematic : MonoBehaviour
{
    BoxCollider2D doorCollider;
    CircleCollider2D trigger;
    SpriteRenderer doorSprite;
    public GameObject bossHB;
    public GameObject Queen;
    public GameObject Driver;

    public EnemyBasic boss;
    public Light2D doorLight;

    private string detectionTag = "Player";
    private bool doorInAction;
    public Animator camAnim;
    public float sceneDuration;

    public float destroyDelay = 3f;

    public AudioSource bossSource;
    public AudioClip bossMusic;
    public AudioClip mainTheme;

    void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.enabled = false;
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.enabled = false;

        trigger = GetComponent<CircleCollider2D>();

        bossHB.SetActive(false);
        doorLight.enabled = false;

        

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag) && !doorInAction)    //when triggered activate door collider,sprite , activate cinematic
        {
            doorCollider.enabled = true;
            doorSprite.enabled = true;
            doorInAction = true;
            doorLight.enabled = true;
            trigger.enabled = false;

            FindObjectOfType<AudioManager>().Play("Gate");
            PlayerBasic.cinematicState = true;      //stops the player movement
            CameraShaker.Instance.ShakeOnce(100f, 4f, 0.15f, 1.5f);
            Invoke("StartCutScene", 3f);            //moves the camaera to boss

            bossSource.Stop();
        }
    }

    void StartCutScene()
    {
        camAnim.SetBool("cinematic1", true);
        Invoke("StopCutScene", sceneDuration);
        bossSource.clip = bossMusic;
        bossSource.Play();
    }

    void StopCutScene()
    {
        bossHB.SetActive(true);
        camAnim.SetBool("cinematic1", false);
        PlayerBasic.cinematicState = false;
        boss.animator.SetBool("isFollowing", true);
    }

    private void Update()
    {
        if(boss.currentHealth==0)  //after boss dies
        {
            bossSource.Stop();
            Destroy(bossHB, destroyDelay);      //remove boss healthbar
            Destroy(doorCollider, destroyDelay);// destroy boss door collider
            Destroy(doorSprite, destroyDelay);  //Destroy boss door sprite
            Destroy(this.gameObject, destroyDelay);
            Destroy(Queen);
            Destroy(Driver);
        }

    }

    public void LoadGameFreeDoor()
    {
        doorCollider.enabled = false;
        doorSprite.enabled = false;
        bossHB.SetActive(false);
        doorInAction = false;
        trigger.enabled = true;
        doorLight.enabled = false;
        bossSource.Stop();
        bossSource.clip = mainTheme;
        bossSource.Play();
}
}
