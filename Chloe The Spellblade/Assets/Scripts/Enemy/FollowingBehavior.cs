using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    public EnemyBasic enemy;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyBasic>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        enemy.canAttack = true;


        enemy.runSource.clip = enemy.run;
        enemy.runSource.Play();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move towards player
        // animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, animator.gameObject.GetComponent<EnemyBasic>().speed * Time.deltaTime);
        
        enemy.newVelocity.Set(enemy.speed * enemy.playerDirectionX * 50 * Time.fixedDeltaTime, enemy.rb.velocity.y);//y = 0 in the original slope code
        enemy.rb.velocity = enemy.newVelocity;

        //Debug.Log(Time.deltaTime);
        //Flip towards player
        if ((playerPos.position.x < animator.transform.position.x) && enemy.facingRight)

        {
            enemy.facingRight = !enemy.facingRight;
            animator.transform.Rotate(0.0f, 180.0f, 0.0f);

        }
        if ((playerPos.position.x > animator.transform.position.x) && !enemy.facingRight)
        {
            enemy.facingRight = !enemy.facingRight;
            animator.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.runSource.Stop();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}




}
