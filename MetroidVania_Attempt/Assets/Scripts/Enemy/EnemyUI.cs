using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public EnemyBasic enemy;

    
    void Update()
    {
        textComponent.text = enemy.playerDirectionX.ToString();
    }
}
