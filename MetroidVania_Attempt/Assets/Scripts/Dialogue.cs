using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;

    public GameObject gameobject;

    private void Awake()
    {
        gameobject.SetActive(false);

    }
    
    private void OnEnable()
    {//if i had these things in start it wouldnt work
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.I))
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
        index = 0;
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
            index = 0;
            gameObject.SetActive(false);
        }
    }
}
