using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiPatrol : MonoBehaviour
{
    EnemyBasic enemy;
    int flipSpeed;
    Rigidbody2D rb;

    NavMeshAgent agent;
    
    private void Start()
    {
        enemy = GetComponent<EnemyBasic>();
        flipSpeed = 1;
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if(!enemy.playerDetected)
        enemy.rb.velocity = new Vector2(flipSpeed* agent.speed * 50 * Time.fixedDeltaTime, enemy.rb.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(!enemy.playerDetected)       //and maybe add collision.CompareTag(detectionTag)
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
