using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorStartCinematic : MonoBehaviour
{
    private BoxCollider2D doorCollider;
    private SpriteRenderer doorSprite;
    public GameObject bossHB;
    public EnemyBasic bossCurrentHealth;

    private string detectionTag = "Player";
    private bool doorInAction;
    public Animator camAnim;
    public float sceneDuration;

    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.enabled = false;
        doorSprite = GetComponent<SpriteRenderer>();
        doorSprite.enabled = false;

        bossHB.SetActive(false);
    }

    
    private void Update()
    {
        if(bossCurrentHealth.currentHealth==0)
        {
            Invoke("DestroyBossHBdoorSprite", 3);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag) && !doorInAction)
        {
            doorCollider.enabled = true;
            doorSprite.enabled = true;
            doorInAction = true;


            camAnim.SetBool("cinematic1", true);
            PlayerBasic.cinematicState = true;
            Invoke("StopCutScene", sceneDuration);
        }
    }
    void StopCutScene()
    {

        bossHB.SetActive(true);
        camAnim.SetBool("cinematic1", false);
        PlayerBasic.cinematicState = false;
    }

    //after boss dies
    void DestroyBossHBdoorSprite()
    {
        Destroy(bossHB);
        Destroy(doorCollider);
        Destroy(doorSprite);
    }

}
