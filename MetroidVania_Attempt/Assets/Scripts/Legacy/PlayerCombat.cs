/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Animator combatAnimator;
    public int MaxHealth = 100;
    public int currentHealth;

    public Transform attack1point;
    public float attack1Range = 0.5f;
    public LayerMask enemyLayers;
    public bool grounded;
    public int attackDamage1 = 30;

    private int attack1Counter = 0;

    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;

    public static bool isAttacking = false;
    public static bool isStunned = false;



    [SerializeField] private Transform groundCheck;
    [SerializeField] private float rad0Circle;
    [SerializeField] private LayerMask whatIsGround;


    private void Start()
    {
        currentHealth = MaxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, rad0Circle, whatIsGround);

        if (Time.time >= nextAttackTime)

        {
            if (Input.GetKeyDown(KeyCode.I) && grounded)
            {
                isAttacking = true;
                Invoke("StopAttacking", 0.7f);

                Atk1();
                Invoke("Atk1Hit", 0.40f);
                nextAttackTime = Time.time + attackRate;

                PlayerController.speed = 0;
                Invoke("SetSpeedToTwo", 0.7f);

               


            }
        }
    }
    

    void Atk1()
    {
        
        //animation
        combatAnimator.SetTrigger("Atk1");


    }
    void Atk1Hit()
    {
        attack1Counter += 1;
        Debug.Log("attack " + attack1Counter);
        //detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack1point.position, attack1Range, enemyLayers);
        //damage enemies effect of hit
          foreach (Collider2D enemy in hitEnemies)
          {
               enemy.GetComponent<AiMeleeAttackDetector>().TakeDamage(attackDamage1);
          }
       
    }
private void OnDrawGizmosSelected() // allows to draw on the editor when object is selected
    {
        if (attack1point == null)
        {
            return;
        }
            Gizmos.DrawWireSphere(attack1point.position, attack1Range);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //BossAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            //Die();
            Debug.Log("Player dead");
        }
    }

    public void SetSpeedToTwo()
    {
        PlayerController.speed = 2.0f;
    }
    public void StopAttacking()
    {
        isAttacking = false;
    }

    
}
*/