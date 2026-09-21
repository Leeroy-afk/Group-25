using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PatientFragmentDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    

    public float wordSpeed;
    public bool patientIsClose;

    // Update is called once per frame
    void Update()
    {
        if(patientIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patient"))
        {
            patientIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Patient"))
        {
            patientIsClose = false;
            zeroText();
        }
    }
}
