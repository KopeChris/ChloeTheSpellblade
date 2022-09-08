using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProjectileDamage : MonoBehaviour
{
    public int attackDamage;
    public int force;
    private string detectionTag = "Player";
    private string collisionTag = "CollisionBlocker";
    Animator animator;
    Rigidbody2D rb;


    public AudioClip explosion;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage,force*(PlayerBasic.positionX-transform.position.x)/Mathf.Abs(PlayerBasic.positionX - transform.position.x));
        }

        if(!collision.CompareTag(collisionTag))     //igore the outside collision blocker of player
        {
            animator.Play("Explosion");
            audioSource.PlayOneShot(explosion);

            rb.velocity = Vector2.zero;
        }
    }
}
