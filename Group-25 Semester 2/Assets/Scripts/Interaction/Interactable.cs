using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Interaction
{

    public class Interactable : MonoBehaviour, IInteractable //placed on every interactable so the player can interact with it.
    {
        [SerializeField] private string displayName = "Interact";

        [SerializeField] private bool isEnabled = true;

        [SerializeField] private UnityEvent onInteract; // the action we want to perform when we interact with this object 

        [SerializeField] private Material highlightMaterial;

        [SerializeField] private TextMeshProUGUI promptText;

        private Renderer objectRenderer;

        private Material originalMaterial;

        private bool isHighlighted = false; 

        public string DisplayName => displayName;

        public bool CanInteract() => isEnabled;

        public void Awake()
        {
            objectRenderer = GetComponent<Renderer>();
            {
                if(objectRenderer != null)
                {
                    originalMaterial = objectRenderer.material;
                }
                else
                {
                    Debug.LogError(" No renderer component found on object " + gameObject.name);
                }
            }

            if(promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }

        }

        public void Interact()
        {
            onInteract?.Invoke();
        }
        public void OnFocusGained()
        {
            isHighlighted = true;

            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }

            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = " press [E] / button south to interact with " + displayName;
            }
        }

        public void OnFocusLost()
        {
            isHighlighted = false;

            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }

            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

}