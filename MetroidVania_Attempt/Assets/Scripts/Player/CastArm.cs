using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastArm : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireBallPrefab;

    void OnEnable()
    {
        Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }
}
