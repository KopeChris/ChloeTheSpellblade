using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public int attackDamage;
    public int force;
    private string detectionTag = "Player";
    public float timeUntilDestroyed=1.5f;




    
    private void Update()
    {
        timeUntilDestroyed -= Time.deltaTime;
        if (timeUntilDestroyed <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage,force);
            Destroy(this.gameObject,0.1f);
        }
    }
}
