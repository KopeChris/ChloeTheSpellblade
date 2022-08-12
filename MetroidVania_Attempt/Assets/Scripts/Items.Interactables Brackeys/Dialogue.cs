using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject gameobject;
    public bool repeatedSpeech;

    public string[] lines;
    [Range(0.0f, 1f)]
    public float textSpeed;
    public int index;

    bool asleep;
    /*
    private void Awake()
    {
        if(!asleep)
        {
            gameobject.SetActive(false);
            asleep=true;
        }
        index = 0;
    }
    */
    private void OnEnable()
    {//if i had these things in start it wouldnt work
        textComponent.text = string.Empty;
        StartDialogue();
        Debug.Log("onEnable");
    }

    
    void StartDialogue()
    {
        StartCoroutine(TypeCharacter());
    }

    IEnumerator TypeCharacter()
    {
        foreach(char c  in lines[index].ToCharArray())
        {
            textComponent.text += c;
            //AudioManager.instance.PlaySound(AudioManager.instance.speech);
            FindObjectOfType<AudioManager>().Play("Speech");
            yield return new WaitForSeconds((0.27f-0.27f *textSpeed));
            // AudioManager.instance.PlayPlayerHurt();      Play sound for each character typed
        }
        //Line Ends
        yield return new WaitForSeconds(2*textSpeed);
        NextLine();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Interact"))     //change line
        {
            
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();                    
                textComponent.text = lines[index];
                
            }
        }
    }
    void NextLine()
    {
        if(index < lines.Length-1)
        {
            index++; 
            textComponent.text=string.Empty;
            StartCoroutine(TypeCharacter());
        }
        else //endDialogue
        {
            if (repeatedSpeech) { index = 0; }  //start from line 0 else it will repeat last line
            gameObject.SetActive(false);

        }
    }
}
