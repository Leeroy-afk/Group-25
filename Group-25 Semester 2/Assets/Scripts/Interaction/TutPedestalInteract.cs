using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Interaction
{
    public class TutPedestalInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private string pedestalName = "Tutorial Pedestal";
        [SerializeField] private List<PedestalParts> memAndBodies;

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
        public struct PedestalParts
        {
            public ItemData itemData;
            public GameObject memoryObject;
        }

        private void Awake()
        {
            objectRenderer = GetComponent<Renderer>();
            if (objectRenderer != null) originalMaterial = objectRenderer.material;
            if (promptText != null) promptText.gameObject.SetActive(false);
        }

        private void Start()
        {
            foreach (var parts in memAndBodies)
            {
                if (parts.memoryObject != null) parts.memoryObject.SetActive(false);
            }
        }

        public void OnFocusGained()
        {
            if (IsOccupied) return;

            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = " press [E] to interact ";
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
            if (IsOccupied) return;

            if (TutorialPedestalUI.Instance != null)
            {
                TutorialPedestalUI.Instance.OpenPedestalMenu(this);
            }
        }

        public bool TryPlaceMemory(ItemData itemData)
        {
            if (IsOccupied || itemData == null) return false;

            foreach (var parts in memAndBodies)
            {
                if (parts.itemData == itemData)
                {
                    currentlyPlacedItem = itemData;

                    if (parts.memoryObject != null)
                    {
                        parts.memoryObject.SetActive(true); // Makes the 3D model appear
                    }
                    if (InventoryManager.Instance != null)
                    {
                        InventoryManager.Instance.RemoveItem(itemData); // Removes item from inventory
                    }

                    return true;
                }
            }

            Debug.LogWarning("This item cannot be placed on this tutorial pedestal.");
            return false;
        }
    }
}