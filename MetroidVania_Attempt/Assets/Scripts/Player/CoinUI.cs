using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public static int coins;
    public PlayerBasic player;

    void Update()
    {
        coins = player.playerCoin;
        textComponent.text = coins.ToString();
    }
}
