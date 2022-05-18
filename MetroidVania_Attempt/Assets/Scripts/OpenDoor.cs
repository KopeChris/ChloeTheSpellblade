using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Indicator
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        base.Interact();
        animator.Play("Open");
    }
}
