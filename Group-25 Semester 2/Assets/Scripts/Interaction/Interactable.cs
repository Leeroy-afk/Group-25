using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class Interactable : MonoBehaviour, IInteractable
    {
       // for general inetraction
        [SerializeField] private string displayName = "Interact";
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private UnityEvent onInteract;

      // for visual feedback
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private TextMeshProUGUI promptText;

      // to dictate what happens on interaction.
        [SerializeField] private ItemData itemData;
        [SerializeField] private bool showInspectionScreen = true;
        [SerializeField] private bool addToInventory = true;
        [SerializeField] private bool destroyOnClose = true;
        

        private Renderer objectRenderer;

        private Material originalMaterial;

        private bool isCollected = false;
        public string DisplayName => displayName;
        public bool CanInteract() => isEnabled && (!addToInventory || !isCollected);

        public void Awake()
        {
            objectRenderer = GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                originalMaterial = objectRenderer.material;
            }

            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }

        public void Interact() // encumpases all the logic for every interactable item this time
        {
            onInteract?.Invoke();

            if (itemData != null && showInspectionScreen)
            {
                if (addToInventory) // adds item to inventory
                {
                    isCollected = true;
                    if (InventoryManager.Instance != null)
                    {
                        InventoryManager.Instance.AddItem(itemData); 
                    }
                }
                
                if (InteractInspectUI.Instance != null) // destroys game objects (like for collectable items)
                { 
                    GameObject targetToDestroy = destroyOnClose ? gameObject : null;

                    InteractInspectUI.Instance.ShowInspection(itemData.title, itemData.description, itemData.icon, targetToDestroy); 
                }
            }
        }

        public void OnFocusGained()
        {
            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }

            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = (" press [E] / button south to interact ");
            }
        }

        public void OnFocusLost()
        {
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