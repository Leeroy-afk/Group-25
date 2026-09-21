using UnityEngine;
using Interaction;

namespace Interaction
{
    public class MemoryFragment : MonoBehaviour
    {

        [SerializeField] private string titleText = "";
        [SerializeField] private string descriptionText = "";
        [SerializeField] private Sprite itemImage;

        private bool isCollected = false;

        public void OnInteract()
        {
            Debug.Log("Interacting with Memory Fragment");
            if (isCollected) return;

            if(InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(titleText, descriptionText, itemImage);
                
            }

            if (InteractInspectUI.Instance != null)
            {
                isCollected = true;
                Debug.Log("Memory Fragment collected");
                InteractInspectUI.Instance.ShowInspection(titleText, descriptionText, itemImage, gameObject);
            }
           
        }

    }

}
