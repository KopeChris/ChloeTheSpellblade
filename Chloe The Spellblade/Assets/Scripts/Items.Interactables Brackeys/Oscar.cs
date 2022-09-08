using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Oscar : NpcDialogue
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        base.Interact();

        player.gameObject.GetComponent<PlayerBasic>().maxBerries += 1;
        player.gameObject.GetComponent<PlayerBasic>().berries = player.gameObject.GetComponent<PlayerBasic>().maxBerries;

        animator.Play("Oscar Hand Raised");

        Destroy(this);
    }

}
