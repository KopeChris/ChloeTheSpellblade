using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBillboard : MonoBehaviour
{
    public PlayerBasic player;
    bool flipToLeft;
    bool flipToRight;

    private void Start()
    {

        if (player.facingRight)
        {
            flipToLeft = true;
        }

        if (!player.facingRight)
        {
            flipToRight = true;
        }

    }
    void Update()
    {

        if (!player.facingRight && flipToLeft)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToLeft = false;
            flipToRight = true;
        }
        if (player.facingRight && flipToRight)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToRight = false;
            flipToLeft = true;
        }

    }

}
