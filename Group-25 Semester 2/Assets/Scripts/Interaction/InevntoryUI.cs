using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interaction
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance { get; private set; }

        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private string invenTitle = "INVENTORY";
   

        public bool IsOpen { get; private set; } = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (inventoryPanel != null)
            {
                inventoryPanel.SetActive(false);
            }
        }
        public void OnToggleInventory(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            if (InteractInspectUI.Instance != null && InteractInspectUI.Instance.IsInspecting) // can't toggle inventory when inspecting an object
            {
                return;
            }
            ToggleInventory();
        }

        public void ToggleInventory()
        {
            IsOpen = !IsOpen;

            if (IsOpen)
            {
                if (titleText != null)
                {
                    titleText.text = invenTitle;
                    RefreshGrid();
                }
                if (inventoryPanel != null)
                {
                    inventoryPanel.SetActive(true);
                }

                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                if (inventoryPanel != null)
                {
                    inventoryPanel.SetActive(false);
                }

                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void RefreshGrid()
        {
            if (gridParent == null || slotPrefab == null)
            {
                return;
            }
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }

            if (InventoryManager.Instance != null)
            {
                List<ItemData> items = InventoryManager.Instance.GetItems();

                foreach (ItemData item in items)
                {
                    GameObject slotObj = Instantiate(slotPrefab, gridParent);

                    InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();

                    if (slotUI != null)
                    {
                        slotUI.Setup(item);
                    }
                }
            }
        }
    }
}
    



