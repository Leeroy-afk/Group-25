using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Interaction
{
    public class PedestalInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private string pedestalName = "Pedestale";
        [SerializeField] private List<PedestalParts> memAndBodies; // this refers to the item information and the physical item gameobject together as one element in the list. This looks at which object is occupying the pedastle

        private ItemData currentlyPlacedItem;
        public ItemData CurrentlyPlacedItem => currentlyPlacedItem;
        public bool IsOccupied => currentlyPlacedItem != null;
        public string DisplayName => IsOccupied ? pedestalName + " (Occupied)" : pedestalName;
        public void OnFocusGained() { }
        public void OnFocusLost() { }
        public bool CanInteract() => !IsOccupied;

        [System.Serializable]
        public struct PedestalParts // basiaclly connects these two pieces of information
        {
            public ItemData itemData;
            public GameObject memoryObject;
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

        public void Interact()
        {
            if (IsOccupied)
            {
                return;
            }
            if (PedestalUI.Instance != null)
            {
                PedestalUI.Instance.OpenPedestalMenu(this);
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
                    EndingManager.Instance?.CheckEndState();

                    return true;

                }
                
            }

            Debug.LogWarning("This item cannot be placed on this pedestal.");

            return false;
        }
    }
}

