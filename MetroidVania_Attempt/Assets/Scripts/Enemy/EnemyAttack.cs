using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage;
    public int pushForce;

    private string detectionTag = "Player";
    
    public Rigidbody2D rb;
    int pushDirection;


    private void Update()
    {
        if (PlayerBasic.positionX < rb.transform.position.x)    
        {pushDirection = -1;}        
        else { pushDirection = 1; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage, pushForce * pushDirection);
        }
    }


}
