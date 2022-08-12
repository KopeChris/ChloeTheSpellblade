using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage;
    public int pushForce;

    private string detectionTag = "Player";
    
    public Rigidbody2D rb;

     AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage, pushForce * rb.gameObject.GetComponent<EnemyBasic>().playerDirectionX);
        }
    }


}
