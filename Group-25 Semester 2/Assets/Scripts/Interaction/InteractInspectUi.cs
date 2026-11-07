using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using UnityEngine.InputSystem;
using Unity.AI.Navigation.Samples;
using System.Collections;


namespace Interaction
{
    public class InteractInspectUI : MonoBehaviour
    {
        public static InteractInspectUI Instance { get; private set; }

        [SerializeField] private GameObject panel;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI exitPromptText;
        [SerializeField] private GameObject viewingPanel;
        [SerializeField] private TextMeshProUGUI notificationText; // item collected notifaction
        [SerializeField] private float notificationDuration = 2f;


        private GameObject currentTargetObject;

        private ItemData currentItemData;

        private Interactable currentInteractableComponent;

        public bool IsInspecting { get; private set; } = false;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("InteractInspectUI initialized!");
            }

            else
            {
                Destroy(this);
            }
            if (panel != null) 
            { 
                panel.SetActive(false); 
            }
            if (viewingPanel != null) 
            { 
                viewingPanel.SetActive(false); 
            }
            if (notificationText != null)
            {
                notificationText.gameObject.SetActive(false);
            }
        }

        
        

        private void OnDestroy()
        {

            if (Instance == this)
            {
                Instance = null;
            }
            Time.timeScale = 1f;
        }

        public void OnCollect(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            if(IsInspecting)
            {
                TryAction();
            }
        }


        //for showing inspection when intecating with item in world
        public void ShowInspection(ItemData itemData, GameObject targetObject, Interactable interactable)
        {
            currentTargetObject = targetObject;
            currentItemData = itemData;
            currentInteractableComponent = interactable;

            if (currentTargetObject != null && currentInteractableComponent != null)
            { 
               currentInteractableComponent.OnFocusLost();
                
            }

            if (titleText != null) 
            { 
                titleText.text = itemData.title;
            }
            if (descriptionText != null) 
            { 
                descriptionText.text = itemData.description; 
            }
            if (exitPromptText != null)
            {
                if(currentInteractableComponent != null && currentInteractableComponent.AddToInventory) // modified for collectionaction on collectable items.
                {
                    exitPromptText.text = "Press [Q] / button east to collect item | press [E] / button south to exit";
                }
                else
                {
                    exitPromptText.text = "press [E] / button south to exit";
                }
               
            }

            if (itemImage != null) 
            { 
                itemImage.sprite = itemData.icon;
                itemImage.gameObject.SetActive(itemData.icon != null);
            }

            panel.SetActive(true);
            viewingPanel.SetActive(true);
            Time.timeScale = 0f;
            IsInspecting = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        // for showing inpection in ineventory 
        public void ShowInspection(ItemData itemData)
        {
            currentTargetObject = null;
            currentItemData = itemData;
            currentInteractableComponent = null;

            if (titleText != null) 
            { 
                titleText.text = itemData.title; 
            }
            if (descriptionText != null) 
            { 
                descriptionText.text = itemData.description; 
            }
            if (exitPromptText != null) 
            { 
                exitPromptText.text = "press [Q] / button east to discard item | press [E] / button south to exit";
            }

            if (itemImage != null)
            {
                itemImage.sprite = itemData.icon;
                itemImage.gameObject.SetActive(itemData.icon != null);
            }

            panel.SetActive(true);
            viewingPanel.SetActive(true);
            Time.timeScale = 0f;
            IsInspecting = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void TryAction()
        {
            if (currentInteractableComponent != null && currentInteractableComponent.AddToInventory && currentItemData != null)
            {
                if (InventoryManager.Instance != null)
                {

                    if (InventoryManager.Instance.AddItem(currentItemData))
                    {
                        currentInteractableComponent.MarkAsCollected();

                        ShowNotification($"'{currentItemData.title}' added to inventory");

                        if (currentTargetObject != null)
                        {
                            Destroy(currentTargetObject);
                        }

                        CloseInspection();
                    }
                    else
                    {
                        if (exitPromptText != null)
                        {
                            exitPromptText.text = "Inventory is full (max 5)! press [E] / button south to exit";
                        }
                    }
                }

            }
            else if (currentInteractableComponent == null && currentItemData != null) // discarding inventory item
            {
                if (InventoryManager.Instance != null)
                {
                    InventoryManager.Instance.RemoveItem(currentItemData);
                    ShowNotification($"'{currentItemData.title}' discarded from inventory");
                }
                if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen)
                {
                    InventoryUI.Instance.RefreshGrid();
                }
                CloseInspection();
            }
        }

        public void ShowNotification(string message)
        {
            if (NotificationDisplay.Instance != null)
            {
                NotificationDisplay.Instance.ShowNotification(message);
            }
        }

        public void CloseInspection() // working for both the initial inspectiong and the inventory view.
        {
            panel.SetActive(false);
            viewingPanel.SetActive(false);
            Time.timeScale = 1f;
            IsInspecting = false;

            currentTargetObject = null;
            currentItemData = null;
            currentInteractableComponent = null;

            if (InventoryUI.Instance == null || !InventoryUI.Instance.IsOpen) // unpauses game and re-lock cursor if inventory panel is not open underneath
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

    }

}