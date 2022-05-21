using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisableEnableComponentBasedOnAnotherComponent : MonoBehaviour
{

    public GameObject text;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(text.GetComponent<TextMeshProUGUI>().isActiveAndEnabled ==true)
        {
            spriteRenderer.enabled= true;
        }
        else
        {
            spriteRenderer.enabled= false;
        }    
    }
}
