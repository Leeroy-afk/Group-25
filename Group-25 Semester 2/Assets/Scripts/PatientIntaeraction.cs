using UnityEngine;
using UnityEngine.InputSystem;

public class PatientStay : MonoBehaviour
{
    [SerializeField] private PatientStayCommand patientStayCommand;

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

        if (patientStayCommand != null)
        {
            patientStayCommand.PatientCommand();
        }
    }
}