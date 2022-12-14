using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileDamage : MonoBehaviour
{

    public int attackDamage;
    private string detectionTag = "Enemies";
    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Legacy
        /*
        if(target== LayerMask.NameToLayer("Enemies"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
                Destroy(this.gameObject);
            }
        }

        if(target== LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage, 10 * this.gameObject.GetComponent<EnemyBasic>().facingDirection);
                Destroy(this.gameObject);
            }
        }*/
        #endregion

        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
            attackDamage = 0;
        }
        animator.SetTrigger("Explode");
        FindObjectOfType<AudioManager>().Play("FireHurt");

        rb.velocity = Vector2.zero;
    }
}
