using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage;
    public int pushForce;

    private string detectionTag = "Player";
    
    public Rigidbody2D rb;


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage, pushForce * rb.gameObject.GetComponent<EnemyBasic>().playerDirectionX);
        }
    }


}
