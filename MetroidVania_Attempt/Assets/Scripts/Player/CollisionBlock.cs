using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlock : MonoBehaviour
{
    public CapsuleCollider2D charcter;
    public CapsuleCollider2D blockCollider;
    void Start()
    {
        Physics2D.IgnoreCollision(charcter, blockCollider, true);
    }

    
}
