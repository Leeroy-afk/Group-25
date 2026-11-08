using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private int maxCapacity = 5; 
        public static InventoryManager Instance { get; private set; }

        private readonly List<ItemData> items = new List<ItemData>(); //making a list containing all the items in the inventory

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
        public bool AddItem(ItemData item)
        {
            if (item == null)
            {
                return false;
            }
            if (items.Contains(item)) 
            {
                Debug.Log ("Item added: " + item.title);
                return false;
            }
            if(items.Count >= maxCapacity)
            {
                Debug.LogWarning("Inventory is full. Cannot add item: " + item.title);
                return false;
            }
            items.Add(item);
            Debug.Log("Item added: " + item.title + " (Total: " + "/" + maxCapacity + ")");
            return true;


        }
        public void RemoveItem(ItemData item)
        {
            if (item != null && items.Contains(item))
            {
                items.Remove(item);
                Debug.Log("Item removed from inventory: " + item.title);
            }
        }
        public List<ItemData> GetItems() //reads the list of items in the inventory and returns it
        {
            return items;
        }
        public bool IsFull() => items.Count >= maxCapacity; 
    }

}
