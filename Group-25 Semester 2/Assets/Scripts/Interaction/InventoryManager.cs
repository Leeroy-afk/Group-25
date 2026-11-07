using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private readonly List<ItemData> items = new List<ItemData>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("InventoryManager initialized!");
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddItem(ItemData item)
        {
            if (item != null && !items.Contains(item))
            {
                items.Add(item);
                Debug.Log("Item added: " + item.title);
            }
        }

        public void RemoveItem(ItemData item)
        {
            if (item != null && items.Contains(item))
            {
                items.Remove(item);
                Debug.Log("Item removed from inventory: " + item.title);
            }
        }

        public List<ItemData> GetItems()
        {
            return items;
        }

        // Checks whether the inventory contains an item with this ID.
        public bool HasItem(string itemID)
        {
            foreach (ItemData item in items)
            {
                if (item != null && item.itemID == itemID)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
