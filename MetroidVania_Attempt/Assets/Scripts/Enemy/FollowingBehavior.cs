using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBehavior : StateMachineBehaviour
{
    private Transform playerPos;
    public bool facingRight;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        animator.gameObject.GetComponent<EnemyBasic>().canAttack = true;
        
    }

    // Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move towards player
        // animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, animator.gameObject.GetComponent<EnemyBasic>().speed * Time.deltaTime);
        animator.GetComponent<EnemyBasic>().newVelocity.Set(animator.GetComponent<EnemyBasic>().speed * animator.GetComponent<EnemyBasic>().playerDirection, animator.GetComponent<EnemyBasic>().rb.velocity.y); //y = 0 in the original slope code
        animator.GetComponent<EnemyBasic>().rb.velocity = animator.GetComponent<EnemyBasic>().newVelocity;

        //Debug.Log(Time.deltaTime);

        //Flip towards player
        if ((playerPos.position.x < animator.transform.position.x) && facingRight)

        {
            facingRight = !facingRight;
            animator.transform.Rotate(0.0f, 180.0f, 0.0f);

        }
        if ((playerPos.position.x > animator.transform.position.x) && !facingRight)
        {
            facingRight = !facingRight;
            animator.transform.Rotate(0.0f, 180.0f, 0.0f);
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
