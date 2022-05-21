using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    PlayerBasic player;
    public int healAmount;

    private void Awake()
    {
        player = GetComponent<PlayerBasic>();
    }
    private void OnEnable()
    {
        player.Cast();
    }
}
