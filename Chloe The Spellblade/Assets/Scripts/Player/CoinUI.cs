using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public PlayerBasic player;

    void Update()
    {
        textComponent.text = player.playerCoin.ToString();
    }
}
