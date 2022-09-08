using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathFind : MonoBehaviour
{
    public Transform target;
    EnemyBasic enemyBasic;
    NavMeshAgent agent;

    void Start()
    {
        enemyBasic=GetComponent<EnemyBasic>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        InvokeRepeating("Follow", 0.1f, 0.1f);
    }

    void Follow()
    {
        if(enemyBasic.playerDetected && !enemyBasic.isDead && enemyBasic.canAttack)
            agent.SetDestination(target.position);
    }

    private void Update()
    {
        if(enemyBasic.isDead)
        {
            enemyBasic.rb.gravityScale = 2;

            enemyBasic.newVelocity.Set(enemyBasic.rb.velocity.x/2, enemyBasic.rb.velocity.y/2);
            enemyBasic.rb.velocity = enemyBasic.newVelocity;
        }
    }
}
