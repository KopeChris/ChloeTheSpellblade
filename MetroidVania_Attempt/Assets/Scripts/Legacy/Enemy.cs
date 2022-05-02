using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
     * 
    //set Variables
    public Animator EnemyAnimator;
    public int MaxHealth = 100;
    public int currentHealth;

    public LayerMask playerLayer;
    public Transform attack1Point;
    public float attack1Range = 0.5f;
    public int attack1Damage = 50;
    public float attack1HitDelay = 0.2f;

    public float attack1Rate = 3.0f;
    private float nextAttackTime = 0f;

    public bool CanGetStunned = false;
    public static bool isStunned = false;

 

    int attack1Counter = 0;



    

    void Start()
    {
        currentHealth = MaxHealth;
        
    }

    private void Update()
    {
       

        if (isStunned == false  && Time.time >= nextAttackTime)
        {
            Atk1();
            Invoke("Atk1Hit", attack1HitDelay);
            nextAttackTime = Time.time + attack1Rate;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
        if(CanGetStunned==true)
        {
            Stunned();
        }
    }

    void Die()
    {
        EnemyAnimator.SetBool("isDead", true);
        Debug.Log("die enemy lol");
        this.enabled = false;

    }

    void Atk1()
    {
        //animation
        //EnemyAnimator.SetTrigger("Atk1");


    }
    void Atk1Hit()
    {
        attack1Counter += 1;
        Debug.Log("GoblinAttack " + attack1Counter);
        //detect enemies
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack1Point.position, attack1Range, playerLayer);
        //damage enemies effect of hit
        foreach (Collider2D Player in hitPlayer)
        {
            Player.GetComponent<PlayerCombat>().TakeDamage(attack1Damage);
        }
        
    }
    void Stunned()
        {
            EnemyAnimator.SetTrigger("Stunned");
            isStunned = true;
            Invoke("unStun",0.2f);
        
        }
    void unStun()
    {
        isStunned = false;
    }

    private void OnDrawGizmosSelected() // allows to draw on the editor when object is selected
    {
        
        Gizmos.DrawWireSphere(attack1Point.position, attack1Range);

        Gizmos.color = Color.yellow;
    }

    */
}
