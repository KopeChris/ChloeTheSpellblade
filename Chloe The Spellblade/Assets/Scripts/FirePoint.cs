using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public EnemyBasic enemy;

    // Update is called once per frame
    void Update()
    {
        if(enemy.facingRight)
        {
            transform.Rotate(0.0f, 0, 0.0f);
        }
        else
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
