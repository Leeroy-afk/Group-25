using UnityEngine;

public class SafeZoneStay : MonoBehaviour
{
    public GameObject patient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (patient != null)
            {
             
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(patient != null)
            {
               
            }
        }
    }
}
