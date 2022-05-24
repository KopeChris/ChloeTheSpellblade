using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    /*
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    */

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.I))
        {
            PlayerBasic.nextAttack = true;

        }
    }
}
