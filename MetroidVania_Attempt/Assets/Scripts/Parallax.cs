using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos, ypos;
    float camStartPosX;
    float camStartPosY;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        ypos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        camStartPosX = cam.transform.position.x;
        camStartPosY = cam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = ((cam.transform.position.x- camStartPosX) * parallaxEffect);
        float ydist = ((cam.transform.position.y - camStartPosY) * parallaxEffect);

        transform.position = new Vector3(startpos + dist, ypos + ydist, transform.position.z);
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}