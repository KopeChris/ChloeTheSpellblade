using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileForce : MonoBehaviour
{
    Vector2 newVelocity;
    Vector2 newForce;
    public Rigidbody2D rb;

    public float forceX;
    public float forceY;


    

    void OnEnable()
    {
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        newForce.Set(forceX * PlayerBasic.facingDirection, forceY+3*forceY * UnityEngine.Input.GetAxis("Vertical"));
        rb.AddForce(newForce, ForceMode2D.Impulse);

    }
    private void Update()
    {
        if (rb.velocity.y > 2)           
            rb.gravityScale = 1;
        else
            rb.gravityScale = 4;
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

}
