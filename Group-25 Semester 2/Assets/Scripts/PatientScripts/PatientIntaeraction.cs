using UnityEngine;
using UnityEngine.InputSystem;

public class PatientStay : MonoBehaviour
{
    [SerializeField] private PatientAI patientAI;

    [SerializeField] private float interactionRange = 3f;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["PatientCommand"].performed += OnInteract;
    }

    private void OnDisable()
    {
        playerInput.actions["PatientCommand"].performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Patient Command pressed");

        if (patientAI == null)
            return;

        float distance = Vector3.Distance(transform.position,
            patientAI.transform.position );

        if (distance <= interactionRange)
        {
            Debug.Log("Patient interaction successful");
            patientAI.ToggleFollowing();
        }
        else
        {
            Debug.Log("Player is far from patient");
        }
    }

}