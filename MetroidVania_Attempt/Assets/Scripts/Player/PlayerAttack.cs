using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;
    private string detectionTag = "Enemies";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
        }
        */


        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<EnemyBasic>().TakeDamage(attackDamage);
        }
        
    }


}
