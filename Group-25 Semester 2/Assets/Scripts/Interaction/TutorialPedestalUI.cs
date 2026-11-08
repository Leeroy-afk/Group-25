using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

        [Header("Tutorial Customization")]
        [SerializeField] private string tutorialMenuTitile = "ADMINISTER MEDICINE"; //these ones are just place holders, you can customize it to say whatever you want
        [SerializeField] private string noItemsText = "You have no medicine to administer."; 
        private TutorialPedestalInteract currentPedestal;
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
            if (pedestalPanel != null)
            {
                pedestalPanel.SetActive(false);
            }
        }

        private void Update()
        {
            // Allows the player to press Escape or Q to close the menu if they get stuck
            if (IsOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)))
            {
                ClosePedestalMenu();
            }
        }
        public void OpenPedestalMenu(TutorialPedestalInteract pedestal)
        {
            currentPedestal = pedestal;
            IsOpen = true;

            if (titleText != null) // thsi sets the customer title text for the tutorial 
            {
                titleText.text = tutorialMenuTitile;
            }

            foreach (Transform child in optionsParent) //clears old buttons
            {
                Destroy(child.gameObject);
            }

            if (InventoryManager.Instance != null)
            {
                List<ItemData> items = InventoryManager.Instance.GetItems();

                bool foundAny = false;

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
                if (!foundAny && titleText != null)
                {
                    titleText.text = noItemsText;
                }
            }

            pedestalPanel.SetActive(true);
            

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            var playerController = FindObjectOfType<FPController>();
            if (playerController != null)
            {
                playerController.enabled = false; // Disables the controller script entirely while menu is open
            }
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
            IsOpen = false;
            if (pedestalPanel != null)
            {
                pedestalPanel.SetActive(false);
            }

            Time.timeScale = 1f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable the player controller script
            var playerController = FindObjectOfType<FPController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }
        }
    }
}
