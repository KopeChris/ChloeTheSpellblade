using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHbBillboard : MonoBehaviour
{
    public EnemyBasic enemy;
    bool flipToLeft;
    bool flipToRight;

    private void Start()
    {

        if (enemy.facingRight)
        {
            flipToLeft = true;
        }

        if(!enemy.facingRight)
        {
            flipToRight = true;
        }

    }
    void Update()
    {
        
        if (!enemy.facingRight && flipToLeft)
        { 
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToLeft = false;
            flipToRight = true;
        }
        if (enemy.facingRight && flipToRight)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            flipToRight = false;
            flipToLeft = true;
        }
        
    }
        
}
