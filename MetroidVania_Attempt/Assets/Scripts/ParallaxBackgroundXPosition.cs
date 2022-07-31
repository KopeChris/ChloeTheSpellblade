using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundXPosition : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //newPos.Set(player.transform.position.x, transform.position.y);
        transform.position  = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

}
