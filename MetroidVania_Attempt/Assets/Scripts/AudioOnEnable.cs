using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnEnable : MonoBehaviour
{
    public string audioName;

    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play(audioName);
    }
}
