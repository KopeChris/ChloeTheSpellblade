using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosX, startPosY;
    float camStartPosX;
    float camStartPosY;
    public GameObject cam;
    public float parallaxEffect;

    float extraXDistance = 0;

    void Awake()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        camStartPosX = cam.transform.position.x;
        camStartPosY = cam.transform.position.y;
    }

    void Update()
    {
        /*
        if(cam.transform.position.x== startPosX)//only activate if camera is inside // maybe not actually i dont know i'll see tomorow how other games are doing it, plus will i change scenes or not?
        {

        }
        */
        float temp = ((cam.transform.position.x- camStartPosX) * (1 - parallaxEffect));
        float xDist = ((cam.transform.position.x - camStartPosX) * parallaxEffect) + extraXDistance; 
        float yDist = ((cam.transform.position.y - camStartPosY) * parallaxEffect); 

        transform.position = new Vector3(startPosX + xDist, startPosY + yDist, transform.position.z);


        if (temp > (length + extraXDistance))
        {
            extraXDistance += length;
        }
        else if (temp < (-length + extraXDistance))
        {
            extraXDistance -= length;
        }
        

    }
}