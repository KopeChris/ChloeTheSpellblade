using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    Vector2 newVelocity;
    [SerializeField]
    [Range(0, 2f)]
    float velocityMultiplier;
    
    void OnEnable()
    {
        newVelocity.Set(PlayerBasic.facingDirection * PlayerBasic.movementSpeed * velocityMultiplier, 0.0f);
        PlayerBasic.rb.velocity = newVelocity;
    }

    void OnDisable()
    {
        
    }
}
