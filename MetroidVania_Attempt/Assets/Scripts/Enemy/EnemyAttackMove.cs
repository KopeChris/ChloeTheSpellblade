using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMove : MonoBehaviour
{
    Vector2 newVelocity;
    [SerializeField]
    [Range(0, 20f)]
    float velocityMultiplier;
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
            newVelocity.Set(enemy.facingDirection * enemy.speed * velocityMultiplier, 0);
            rb.velocity = newVelocity;

        }


        if (enemyleapDirection == leapDirection.Player)
        {
            newVelocity.Set(enemy.playerDirectionX * enemy.speed * velocityMultiplier, 0);   //* (PlayerBasic.positionX - rb.transform.position.x)
            rb.AddForce(newVelocity, ForceMode2D.Impulse);
        }

    }

    void OnDisable()
    {
        
    }
}
