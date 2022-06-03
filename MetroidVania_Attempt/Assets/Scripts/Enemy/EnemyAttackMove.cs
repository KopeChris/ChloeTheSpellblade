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

    public enum pushDirection // your custom enumeration
    {
        Facing,
        Player,
    };
    public pushDirection enemyPushDirection = pushDirection.Facing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyBasic>(); 
    }
    void OnEnable()
    {
        if (enemyPushDirection == pushDirection.Facing)
        {
            newVelocity.Set(enemy.facingDirection * enemy.speed * velocityMultiplier, 0);
            rb.velocity = newVelocity;

        }
        //enemy.Push(enemy.facingDirection * enemy.speed * velocityMultiplier);


        if (enemyPushDirection == pushDirection.Player)
        {
            newVelocity.Set(enemy.playerDirectionX * enemy.speed * velocityMultiplier, 0);   //* (PlayerBasic.positionX - rb.transform.position.x)
            rb.AddForce(newVelocity, ForceMode2D.Impulse);
        }

    }

    void OnDisable()
    {
        
    }
}
