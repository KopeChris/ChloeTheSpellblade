using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Vector2 newVelocity;
    Vector2 newForce;
    Rigidbody2D rb;
    Vector3 position;
    SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        
    }
    void OnEnable()
    {

        transform.position = position;
        newVelocity.Set(0.0f, 0.0f);
        rb.velocity = newVelocity;

        sprite.enabled = true;

        newForce.Set(30 * PlayerBasic.facingDirection, 20);
        rb.AddForce(newForce, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        sprite.enabled = false;
    }
}
