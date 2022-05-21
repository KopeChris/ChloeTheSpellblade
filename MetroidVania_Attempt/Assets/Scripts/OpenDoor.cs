using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Indicator
{
    public SpriteRenderer closed;
    public SpriteRenderer open;
    BoxCollider2D collider;
    
    private void Start()
    {
        open.enabled = false;
        collider = GetComponent<BoxCollider2D>();
    }/*
    private void Update()
    {
        
    }*/
    public override void Interact() //open door
    {
        base.Interact();
        open.enabled = true;
        Destroy(this.gameObject);
        
        //AudioManager.instance.PlayDoorOpen();
    }
}
