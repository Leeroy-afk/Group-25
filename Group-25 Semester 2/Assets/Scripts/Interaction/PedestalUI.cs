using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class PedestalUI : MonoBehaviour
    {
        public static PedestalUI Instance { get; private set; }
        [SerializeField] private GameObject pedestalPanel;
        [SerializeField] private Transform optionsParent;
        [SerializeField] private GameObject optionButtonPrefab;
        [SerializeField] private TextMeshProUGUI titleText;

        private PedestalInteract currentPedestal;
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
        public void OpenPedestalMenu(PedestalInteract pedestal)
        {
            currentPedestal = pedestal;
            IsOpen = true;

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

                    if (!foundAny && titleText != null)
                    {
                        titleText.text = "You have no memories to place here.";
                    }
                }
            }

            pedestalPanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        }
    }
}
