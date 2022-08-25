using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, height, startPosX, startPosY;
    float camStartPosX;
    float camStartPosY;
    public GameObject cam;
    public float parallaxEffect;

    float extraXDistance = 0;

    public float smoothingX = 1f;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        camStartPosX = cam.transform.position.x;
        camStartPosY = cam.transform.position.y;
    }
    
    void Update()
    {
        
        float temp = ((cam.transform.position.x- camStartPosX) * (1 - parallaxEffect));
        float xDist = ((cam.transform.position.x - camStartPosX) * parallaxEffect) + extraXDistance;
        float yDist = ((cam.transform.position.y - camStartPosY) * parallaxEffect)/50;      //0;

        #region legacy
        /*
        if(cam.transform.position.x== startPosX)//only activate if camera is inside // maybe not actually i dont know i'll see tomorow how other games are doing it, plus will i change scenes or not?
        {

        }
        */
        /*
        if(PlayerBasic.positionY> startPosY + height * parallaxEffect) 
        {
            yDist = height * parallaxEffect;
        }
        else if(PlayerBasic.positionY < startPosY - height * parallaxEffect) 
        {
            yDist = -height * parallaxEffect;

        }
        else 
        {
            yDist = ((cam.transform.position.y - camStartPosY) * parallaxEffect);

        }
        */
        #endregion
        
        if (temp < (length + extraXDistance) && temp > (-length + extraXDistance))  //move the backgrounds a little
        {
            Vector3 backgroundTargetPosX = new Vector3(startPosX + xDist, startPosY + yDist, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosX, smoothingX * Time.deltaTime);

        }


        //move the backgrounds a lot displace them equal to their lenth as to not have to make many backgrounds
        if (temp >= (length + extraXDistance))
        {
            extraXDistance += length;
        }
        else if (temp <= (-length + extraXDistance))
        {
            extraXDistance -= length;
        }


    }
}