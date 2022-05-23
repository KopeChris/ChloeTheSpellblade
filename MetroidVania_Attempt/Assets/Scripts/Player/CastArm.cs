using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastArm : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectileBallPrefab;

    void OnEnable()
    {
        Instantiate(projectileBallPrefab, firePoint.position, firePoint.rotation);
    }
}
