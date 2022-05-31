using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int attackDamage;
    public int force;
    private string detectionTag = "Player";
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            collision.GetComponent<PlayerBasic>().TakeDamage(attackDamage,force*(PlayerBasic.positionX-transform.position.x)/Mathf.Abs(PlayerBasic.positionX - transform.position.x));
        }
        animator.Play("Explosion");
    }
}
