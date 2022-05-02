using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHbBillboard : MonoBehaviour
{
    public Animator animator;
    bool flipToLeft;
    bool flipToRigt;

    private void Start()
    {
        if(animator.GetBehaviour<FollowingBehavior>().facingRight == true)
        {
            flipToLeft = true;
        }

        if(animator.GetBehaviour<FollowingBehavior>().facingRight == false)
        {
            flipToRigt = true;
        }

    }
    void LateUpdate()
    {
        if(animator.GetBehaviour<FollowingBehavior>().facingRight == false && flipToLeft)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToLeft=false;
            flipToRigt = true;
        }
        if (animator.GetBehaviour<FollowingBehavior>().facingRight == true && flipToRigt)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToRigt = false;
            flipToLeft = true;
        }

        if (animator.GetBehaviour<FollowingBehavior>().facingRight == true )
        {
            
        }
    }
}
