using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileForce : MonoBehaviour
{
    Vector2 newVelocity;
    Vector2 newForce;
    public Rigidbody2D rb;
    public int forceX;
    public int forceY;
    int direction;
    

    void OnEnable()
    {
        if(PlayerBasic.positionX>transform.position.x)
        {
            direction=1;
        }
        else { direction = -1; }

        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        newForce.Set(forceX * direction, forceY);
        rb.AddForce(newForce, ForceMode2D.Impulse);

    }
    private void OnDisable()
    {
        Destroy(this.gameObject);

    }


}
