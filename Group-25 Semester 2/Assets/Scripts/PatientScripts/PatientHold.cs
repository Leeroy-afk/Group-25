using Unity.VisualScripting;
using UnityEngine;

public class PatientSit : PatientAI
{
    public GameObject patientStanding, patientSitting, intText, standText;
    public bool interactable, sitting;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                intText.SetActive(false) ;
                standText.SetActive(true);
                patientSitting.SetActive(true);
                sitting = true;
                patientStanding.SetActive(false);
                interactable = false;
            }
        }
        
        if (sitting == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                standText.SetActive(false);
                patientSitting.SetActive(false);
                sitting = false;
                patientStanding.SetActive(true);
                
            }
        }

     

    }
 }
