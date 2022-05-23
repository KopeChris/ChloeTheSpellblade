using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public int attackDamage;
    public int force;
    private string detectionTag = "Player";




    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage,force);
        }
    }
}
