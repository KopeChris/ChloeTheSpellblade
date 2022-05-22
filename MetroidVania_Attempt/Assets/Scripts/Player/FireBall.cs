using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Vector2 newVelocity;
    Vector2 newForce;
    public Rigidbody2D rb;

    

    void OnEnable()
    {
        

        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        newForce.Set(20* PlayerBasic.facingDirection, 1);
        rb.AddForce(newForce, ForceMode2D.Impulse);

    }

    
}
