using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    public float speed = 2;
    public bool facingRight;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyBasic.canAttack = true;
    }

    // Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move towards player
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);

        //Flip towards player
        if ((playerPos.position.x < animator.transform.position.x) && facingRight)

        {
            facingRight = !facingRight;
            Vector3 tmpScale = animator.transform.localScale;
            tmpScale.x *= -1;
            animator.transform.localScale = tmpScale;

        }
        if ((playerPos.position.x > animator.transform.position.x) && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 tmpScale = animator.transform.localScale;
            tmpScale.x *= -1;
            animator.transform.localScale = tmpScale;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
