using UnityEngine;
using UnityEngine.UI;

namespace Interaction
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        [SerializeField] private Button slotButton;

        private ItemData currentItem;

        public void Setup(ItemData item)
        {
            currentItem = item;

            if (itemImage != null && item != null && item.icon != null)
            {
                itemImage.sprite = item.icon;
                itemImage.color = Color.white;
                itemImage.gameObject.SetActive(true);
            }

            if (slotButton != null) // makes sure to resent every time slot is clicked or inventory is opened 
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(OnSlotClicked);
            }
        }

        private void OnSlotClicked()
        {
            if (currentItem != null && InteractInspectUI.Instance != null)
            {
                // Inspect item from inventory without passing a world GameObject to destroy
                InteractInspectUI.Instance.ShowInspection(currentItem.title, currentItem.description, currentItem.icon, null);
            }
        }
    }
}
        
    
