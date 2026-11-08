using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Interaction
{
    public class TutorialPedestalInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private string pedestalName = "tutorial Pedestale";
        [SerializeField] private List<PedestalParts> memAndBodies; // this refers to the item information and the physical item gameobject together as one element in the list. This looks at which object is occupying the pedastle

        [SerializeField] private Material highlightMaterial;
        [SerializeField] private TextMeshProUGUI promptText;

        private Renderer objectRenderer;

        private Material originalMaterial;

        private ItemData currentlyPlacedItem;
        public ItemData CurrentlyPlacedItem => currentlyPlacedItem;
        public bool IsOccupied => currentlyPlacedItem != null;
        public string DisplayName => IsOccupied ? pedestalName + " (Occupied)" : pedestalName;

        public bool CanInteract() => !IsOccupied;

        [System.Serializable]
        public struct PedestalParts // basiaclly connects these two pieces of information (the memory fragment and the item data it has attached)
        {
            public ItemData itemData;
            public GameObject memoryObject;
        }

        private void Awake()
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


        private void Start()
        {
            foreach (var Parts in memAndBodies)
            {
                if (Parts.memoryObject != null)
                {
                    Parts.memoryObject.SetActive(false);
                }

            }
        }

        public void OnFocusGained() // this is the function that is called when the player looks at the pedestal. It will highlight the pedestal and show the prompt text
        {
            if (IsOccupied)
            {
                return;
            }
            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = " press [E]/ Button South to interact ";
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


        public void Interact()
        {
            if (IsOccupied)
            {
                return;
            }
            if (TutorialPedestalUI.Instance != null)
            {
                TutorialPedestalUI.Instance.OpenPedestalMenu(this);
            }
        }

        public bool TryPlaceMemory(ItemData itemData)
        {
            if (IsOccupied || itemData == null) return false;

            foreach (var Parts in memAndBodies)
            {
                if (Parts.itemData == itemData)
                {
                    currentlyPlacedItem = itemData;

                    if (Parts.memoryObject != null) // makes the memory object on the pedestal visible
                    {
                        Parts.memoryObject.SetActive(true);
                    }
                    if (InventoryManager.Instance != null) // removes memory from inventory 
                    {
                        InventoryManager.Instance.RemoveItem(itemData);
                    }

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;


                    return true;

                }

            }

            Debug.LogWarning("This item cannot be placed on this pedestal.");

            return false;
        }
    }
}

