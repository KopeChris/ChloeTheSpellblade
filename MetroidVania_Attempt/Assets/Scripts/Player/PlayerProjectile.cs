using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    public int attackDamage;
    private string detectionTag = "Enemies";
    public float timeUntilDestroyed =1.2f;

    

    
    
    private void Update()
    {
        timeUntilDestroyed  -= Time.deltaTime;
        if (timeUntilDestroyed  <= 0)
        {
            Destroy(this.gameObject,0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        if (collision.CompareTag(detectionTag) || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if(collision.CompareTag(detectionTag))
                collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);

            Destroy(this.gameObject);
        }
    }
}
