using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private List<InventoryItem> items = new List<InventoryItem>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
            Debug.Log("Inventory manager Initialized");
        }

        public void AddItem(string title, string description, Sprite icon)
        {
            InventoryItem newItem = new InventoryItem(title, description, icon);
            items.Add(newItem);
            Debug.Log($"Added {title} to inventory. Total items: {items.Count}");
        }

        public List<InventoryItem> GetItems()
        {
            return items;
        }
    }

}
