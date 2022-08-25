using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerryUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public PlayerBasic player;

    void Update()
    {
        textComponent.text = player.berries.ToString() + "/" + player.maxBerries.ToString();
    }
}
