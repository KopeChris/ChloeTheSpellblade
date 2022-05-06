using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : StateMachineBehaviour
{


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        PlayerBasic.isRolling = false;
        PlayerBasic.canMove = true;
        PlayerBasic.chloeAttack1Hit = false;
        //PlayerBasic.chloeAttack1 = false;
        PlayerBasic.isInvincible = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       /* if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerBasic.chloeAttack1 = true;
        }
       */
        PlayerBasic.isInvincible = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // PlayerBasic.chloeAttack1 = false;

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