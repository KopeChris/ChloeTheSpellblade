using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : MonoBehaviour
{
    EnemyBasic enemy;
    int flipSpeed;
    Rigidbody2D rb;
    
    private void Start()
    {
        enemy = GetComponent<EnemyBasic>();
        flipSpeed = 1;
        rb = GetComponent<Rigidbody2D>();
        

    }
    void FixedUpdate()
    {
        if(!enemy.attackPlayer)
        enemy.rb.velocity = new Vector2(flipSpeed*enemy.speed * 50 * Time.fixedDeltaTime, enemy.rb.velocity.y);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(!enemy.attackPlayer)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            enemy.facingRight = !enemy.facingRight;
            flipSpeed *= -1;
        }
        
    }
    private void OnDisable()
    {
        rb.gravityScale = 1;
    }

}
