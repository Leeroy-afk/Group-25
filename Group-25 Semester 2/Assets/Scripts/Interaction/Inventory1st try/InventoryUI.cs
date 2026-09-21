using Intercation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Transform gridParent; 
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private TextMeshProUGUI inventoryTitle;

        private string inventoryTitleText = "Inventory";

        public bool IsOpen { get; private set; } = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            if (inventoryPanel != null) inventoryPanel.SetActive(false);
        }

        public void OnToggleInventory(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            // Don't open inventory if inspecting an item popup
            if (InteractInspectUI.Instance != null && InteractInspectUI.Instance.IsInspecting)
                return;

            ToggleInventory();
        }

        public void ToggleInventory()
        {
            IsOpen = !IsOpen;

            if (IsOpen)
            {
                if (inventoryTitle != null)
                {
                    inventoryTitle.text = inventoryTitleText;
                }
                RefreshGrid();
                inventoryPanel.SetActive(true);
                Time.timeScale = 0f;

                Cursor.lockState = CursorLockMode.None; // Unlock the cursor when inventory is open
                Cursor.visible = true;
                Debug.Log("inventory cursor activated");
            }
            else
            {
                inventoryPanel.SetActive(false);
                Time.timeScale = 1f;

                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when inventory is closed
                Cursor.visible = false;
            }
        }

        private void RefreshGrid()
        {
            // Clear existing slots
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }

            GameObject firstSlot = null;

            // Populate slots from InventoryManager
            if (InventoryManager.Instance != null)
            {
                List<InventoryItem> items = InventoryManager.Instance.GetItems();
                foreach (InventoryItem item in items)
                {
                    GameObject slotObj = Instantiate(slotPrefab, gridParent);
                    InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
                    if (slotUI != null)
                    {
                        slotUI.Setup(item);
                    }
                    if (firstSlot == null) firstSlot = slotObj;
                }
            }

            if (firstSlot != null && EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSlot);
            }
        }
    }
}