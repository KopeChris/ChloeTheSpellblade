using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int attackDamage;
    public LayerMask target;
    private string detectionTag = "Enemies";
    float time=1.5f;

    

    
    private void OnEnable()
    {
        time = 1.5f;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(this.gameObject);
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

        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
            Destroy(this.gameObject);
        }
    }
}
