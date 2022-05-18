using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject gameobject;

    public string[] lines;
    public float textSpeed;
    public int index;
    public bool repeatedSpeech=true;

    bool asleep;

    private void Awake()
    {
        if(!asleep)
        {
            gameobject.SetActive(false);
            asleep=true;
        }
        index = 0;
    }
    
    private void OnEnable()
    {//if i had these things in start it wouldnt work
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Y))
        {
            if(textComponent.text == lines[index])
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
    void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c  in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }    

    void NextLine()
    {
        if(index < lines.Length-1)
        {
            index++; 
            textComponent.text=string.Empty;
            StartCoroutine(TypeLine());
        }
        else //endDialogue
        {
            if (repeatedSpeech) { index = 0; }  //start from line 0 else it will repeat last line
            gameObject.SetActive(false);

        }
    }
}