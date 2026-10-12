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
        public string title;

        public string description;

        public Sprite icon;

        public MemoryAlignment alignment = MemoryAlignment.None;

        public bool isPlaceable = false; 
    }

}
