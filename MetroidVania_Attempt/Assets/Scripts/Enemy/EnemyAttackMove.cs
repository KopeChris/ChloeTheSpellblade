using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMove : MonoBehaviour
{
    Vector2 newVelocity;
    [SerializeField]
    [Range(0, 20f)]
    float velocityXMultiplier;
    [SerializeField]
    [Range(0, 20f)]
    float velocityYMultiplier;
    Rigidbody2D rb;
    EnemyBasic enemy;

    public enum leapDirection // your custom enumeration
    {
        Facing,
        Player,
    };
    public leapDirection enemyleapDirection = leapDirection.Facing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyBasic>(); 
    }

  

    void OnEnable()
    {
        if (enemyleapDirection == leapDirection.Facing)
        {
            newVelocity.Set(enemy.facingDirection * enemy.speed * velocityXMultiplier, enemy.speed * velocityYMultiplier);
            rb.velocity = newVelocity;

        }


        if (enemyleapDirection == leapDirection.Player)
        {
            newVelocity.Set(enemy.playerDirectionX * enemy.speed * velocityXMultiplier, enemy.speed * velocityYMultiplier);   //* (PlayerBasic.positionX - rb.transform.position.x)
            rb.AddForce(newVelocity, ForceMode2D.Impulse);
        }

    }
}
