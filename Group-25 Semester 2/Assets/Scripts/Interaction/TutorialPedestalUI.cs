using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Interaction
{
    public class TutorialPedestalUI : MonoBehaviour
    {
        public static TutorialPedestalUI Instance { get; private set; }


        [SerializeField] private GameObject pedestalPanel;
        [SerializeField] private Transform optionsParent;
        [SerializeField] private GameObject optionButtonPrefab;
        [SerializeField] private TextMeshProUGUI titleText;

        [Header("Customize Menu Text")]
        [SerializeField] private string chooseMedicineTitle = "Choose medicine to administer";
        [SerializeField] private string noMedicineText = "You have no medicine to administer.";

        private TutPedestalInteract currentPedestal;
        public bool IsOpen { get; private set; } = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            if (pedestalPanel != null) pedestalPanel.SetActive(false);
        }

       
        public void OpenPedestalMenu(TutPedestalInteract pedestal)
        {
            // Toggle closed if already open for this pedestal
            if (IsOpen && currentPedestal == pedestal)
            {
                ClosePedestalMenu();
                return;
            }

            currentPedestal = pedestal;
            IsOpen = true;

            foreach (Transform child in optionsParent)
            {
                Destroy(child.gameObject);
            }

            bool foundAny = false;

            if (InventoryManager.Instance != null)
            {
                List<ItemData> items = InventoryManager.Instance.GetItems();

                foreach (ItemData item in items)
                {
                    if (item.isPlaceable)
                    {
                        foundAny = true;
                        GameObject buttObj = Instantiate(optionButtonPrefab, optionsParent);

                        TextMeshProUGUI buttText = buttObj.GetComponentInChildren<TextMeshProUGUI>();
                        if (buttText != null) buttText.text = item.title;

                        Button butt = buttObj.GetComponent<Button>();
                        ItemData selectedItem = item;
                        butt.onClick.AddListener(() => OnItemSelected(selectedItem));
                    }
                }
            }

            // Dynamically set the title text depending on inventory contents
            if (titleText != null)
            {
                if (foundAny)
                {
                    titleText.text = chooseMedicineTitle;
                }
                else
                {
                    titleText.text = noMedicineText;
                }
            }

            pedestalPanel.SetActive(true);

            // Unlock mouse for UI interaction
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Safely pause player look input without touching FPController
            var playerController = FindAnyObjectByType<FPController>();
            if (playerController != null) playerController.enabled = false;
        }

        private void OnItemSelected(ItemData item)
        {
            if (currentPedestal != null)
            {
                currentPedestal.TryPlaceMemory(item);
            }
            ClosePedestalMenu();
        }

        public void ClosePedestalMenu()
        {
            if (!IsOpen) return;

            IsOpen = false;
            if (pedestalPanel != null) pedestalPanel.SetActive(false);

            // Re-lock mouse and restore player controls
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            var playerController = FindAnyObjectByType<FPController>();
            if (playerController != null) playerController.enabled = true;
        }
    }
}