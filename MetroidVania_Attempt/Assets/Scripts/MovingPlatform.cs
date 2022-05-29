using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1,pos2;
    public float speed=2;
    public Transform startPos;

    Vector3 nextPos;

    void Start()
    {
        nextPos = pos1.position;
        transform.position = nextPos;
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, pos1.position)<1)
        {
            nextPos = pos2.position;
        }
                
        if (Vector2.Distance(transform.position, pos2.position) < 1)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * 2 * Time.deltaTime);
        //transform.position = Vector2.Lerp(transform.position, nextPos, speed * 2 * Time.deltaTime);       //great for elavator
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position,pos2.position);
    }

}
