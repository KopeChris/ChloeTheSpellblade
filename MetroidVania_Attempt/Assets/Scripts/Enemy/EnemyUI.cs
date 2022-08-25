using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [HideInInspector]
    public EnemyBasic enemy;

    public float damage;
    public float duration = 3f;
    [HideInInspector]
    public float countdown;

    private void Start()
    {
        textComponent.color = new Color(1, 0.8f, 0.8f, 0.9f);
        textComponent.enabled = false;
    }

    void Update()
    {
        //textComponent.text = enemy.playerDirectionX.ToString();   //to show playerdirection
        //textComponent.text = " ";    // to show nothing

        if(countdown > 0)
        {
            textComponent.enabled = true;
            textComponent.text = damage.ToString();
            countdown -= Time.deltaTime + 0.001f;
        }
        
        if(countdown <0)
        {
            textComponent.enabled = false;
            damage = 0;
            countdown = 0;
        }
    }
}
