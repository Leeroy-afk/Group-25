using TMPro;
using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum MemoryAlignment
        {
            None,
            Good,
            Bad
        }

        [Header("Item Identification")]
        public string itemID;

        [Header("Item Information")]
        public string title;
        public string description;
        public Sprite icon;

        [Header("Memory Settings")]
        public MemoryAlignment alignment = MemoryAlignment.None;

        [Header("Placement")]
        public bool isPlaceable = false;
    }
}
