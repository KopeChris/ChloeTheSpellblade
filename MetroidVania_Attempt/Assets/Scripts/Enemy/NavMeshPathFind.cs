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
        InvokeRepeating("Follow", 0, 0.1f);
    }

    void Follow()
    {
        if(enemyBasic.playerDetected && !enemyBasic.isDead)
            agent.SetDestination(target.position);
    }
}
