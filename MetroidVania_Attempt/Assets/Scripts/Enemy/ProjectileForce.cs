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

        newForce.Set(direction*(forceX/2f + forceX/2f  * 0.1f * Mathf.Abs((PlayerBasic.positionX - rb.transform.position.x))), forceY+ forceY/2 * 0.3f * (PlayerBasic.positionY - rb.transform.position.y));
        rb.AddForce(newForce, ForceMode2D.Impulse);

    }
    private void OnDisable()
    {
        Destroy(this.gameObject);

    }


}
