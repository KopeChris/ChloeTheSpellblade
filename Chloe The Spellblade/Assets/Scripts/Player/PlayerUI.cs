using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float duration = 3f;

    [HideInInspector]
    public float countdown;

    private void Start()
    {
        //textComponent.color = new Color(1, 0.8f, 0.8f, 0.9f);
        textComponent.enabled = false;
    }

    void Update()
    {
        //textComponent.text = enemy.playerDirectionX.ToString();   //to show playerdirection
        //textComponent.text = " ";    // to show nothing

        if (countdown > 0)
        {
            textComponent.enabled = true;
            countdown -= Time.deltaTime + 0.001f;
        }

        if (countdown < 0)
        {
            textComponent.enabled = false;
            countdown = 0;
        }
    }
}
