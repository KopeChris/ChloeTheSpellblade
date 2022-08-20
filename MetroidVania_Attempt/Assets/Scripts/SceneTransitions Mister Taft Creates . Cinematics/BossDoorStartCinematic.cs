using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossDoorStartCinematic : MonoBehaviour
{
    private BoxCollider2D doorCollider;
    private CircleCollider2D trigger;
    private SpriteRenderer doorSprite;
    public GameObject bossHB;
    public EnemyBasic boss;
    public Light2D doorLight;

    private string detectionTag = "Player";
    private bool doorInAction;
    public Animator camAnim;
    public float sceneDuration;

    public float destroyDelay = 3f;


    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.enabled = false;
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.enabled = false;

        trigger = GetComponent<CircleCollider2D>();

        bossHB.SetActive(false);
        doorLight.enabled = false;

        

    }

    
    private void Update()
    {
        if(boss.currentHealth==0)  //after boss dies
        {
            Destroy(bossHB, destroyDelay);      //remove boss healthbar
            Destroy(doorCollider, destroyDelay);// destroy boss door collider
            Destroy(doorSprite, destroyDelay);  //Destroy boss door sprite
            Destroy(this.gameObject, destroyDelay);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag) && !doorInAction)    //when triggered activate door collider,sprite , activate cinematic
        {
            doorCollider.enabled = true;
            doorSprite.enabled = true;
            doorInAction = true;
            doorLight.enabled = true;

            camAnim.SetBool("cinematic1", true);
            PlayerBasic.cinematicState = true;
            Invoke("StopCutScene", sceneDuration);  //stop scene and activate Boss
            trigger.enabled = false;
            //AudioManager.instance.PlaySound(AudioManager.instance.gateClose);
            FindObjectOfType<AudioManager>().Play("Gate");

        }
    }
    void StopCutScene()
    {
        bossHB.SetActive(true);
        camAnim.SetBool("cinematic1", false);
        PlayerBasic.cinematicState = false;
        boss.animator.SetBool("isFollowing", true);
    }

    public void LoadGameFreeDoor()
    {
        doorCollider.enabled = false;
        doorSprite.enabled = false;
        bossHB.SetActive(false);
        doorInAction = false;
        trigger.enabled = true;
        doorLight.enabled = false;
    }
}
