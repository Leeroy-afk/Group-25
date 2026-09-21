using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PatientStay : PatientStayCommand
{
    private PlayerInput _playerInput;
  

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        Debug.Log("PatientStay Enabled");
        _playerInput.actions["PatientCommand"].performed += OnInteract;
       

    }

    private void OnDisable()
    {
        Debug.Log("PatientStay Disabled");
        _playerInput.actions["PatientCommand"].performed -= OnInteract;
       
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("I'm Working...");
        PatientCommand();
       
    }
         
}
