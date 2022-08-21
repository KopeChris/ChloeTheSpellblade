using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Queen : NpcDialogue
{
    public override void Interact()
    {
        base.Interact();

        if (player.gameObject.GetComponent<PlayerBasic>().maxBerries < 2)
        {
            player.gameObject.GetComponent<PlayerBasic>().maxBerries = 2;
            player.gameObject.GetComponent<PlayerBasic>().berries = 2;
        }
    }

}
