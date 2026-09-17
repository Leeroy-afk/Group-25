using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PatientStay : PatientStayCommand
{
    private PlayerInput _playerInput;
    private Transform _transform;
    [SerializeField] private LayerMask interactableLayer;
  

    private void Awake()
    {
        _transform = transform;
        
        _playerInput = GetComponent<PlayerInput>();

    }

    private void OnEnable()
    {
        _playerInput.actions["PatientCommand"].performed += OnInteract;
       

    }

    private void OnDisable()
    {
        _playerInput.actions["PatientCommand"].performed -= OnInteract;
       
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
       
        if (Physics.Raycast(_transform.position + (Vector3.up * 0.03f) + (_transform.forward * 0.2f), _transform.forward, out var hit, 1.5f, interactableLayer)) return;
        PatientCommand();
        Debug.Log("I'm Working...");
    }
         
}
