using Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Intercation
{
    public class InventorySlotUI : MonoBehaviour
    {
            [SerializeField] private Image itemImage;
            [SerializeField] private Button button;

            private InventoryItem itemData;

            public void Setup(InventoryItem item)
            {
                itemData = item;
                if (itemImage != null && item.icon != null)
                {
                    itemImage.sprite = item.icon;
                    itemImage.gameObject.SetActive(true);
                }

                if (button != null)
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(OnSlotClicked);
                }
            }

            private void OnSlotClicked()
            {
                if (itemData != null && InteractInspectUI.Instance != null)
                {
                    // Opens the inspection panel for this saved item without destroying any scene object
                    InteractInspectUI.Instance.ShowInspection(itemData.title, itemData.description, itemData.icon, null);
                }
            }
        
    }
}


