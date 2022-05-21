using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    Vector2 newVelocity;
    [SerializeField]
    [Range(0, 2f)]
    float velocityMultiplier;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void OnEnable()
    {
        newVelocity.Set(PlayerBasic.facingDirection * PlayerBasic.movementSpeed * velocityMultiplier, 0.0f);
        rb.velocity = newVelocity;
    }

    void OnDisable()
    {
        
    }
}
