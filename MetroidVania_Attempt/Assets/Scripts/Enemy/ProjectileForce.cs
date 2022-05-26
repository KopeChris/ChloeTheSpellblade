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
    public float forceY2;
    [Range(0f, 1f)]
    public float rotationValue;

    int direction;

    

    void OnEnable()
    {
        Debug.Log(rotationValue * 4 * (PlayerBasic.positionY - transform.position.y) - 2);
        transform.Rotate(0, 0, rotationValue * 4 * (PlayerBasic.positionY - transform.position.y) - 2f);
        
        if (PlayerBasic.positionX>transform.position.x)
        {
            direction=1;
        }
        else { direction = -1; }

        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        newForce.Set(direction*(forceX/2f + forceX/2f  * 0.1f * Mathf.Abs((PlayerBasic.positionX - rb.transform.position.x))), forceY+ forceY2 *0.1f* (PlayerBasic.positionY - rb.transform.position.y));
        rb.AddForce(newForce, ForceMode2D.Impulse);

    }
    private void OnDisable()
    {
        Destroy(this.gameObject);

    }
}
