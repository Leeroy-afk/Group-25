using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace Interaction
{
    public class InventoryManager : MonoBehaviour
    {
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

        public void AddItem(ItemData item)
        {
            if (item != null && !items.Contains(item)) 
            {
                items.Add(item);
                Debug.Log ("Item added: " + item.title);
            }
        }

        public List<ItemData> GetItems() //reads the list of items in the inventory and returns it
        {
            return items;
        }
        

       


    }

}
