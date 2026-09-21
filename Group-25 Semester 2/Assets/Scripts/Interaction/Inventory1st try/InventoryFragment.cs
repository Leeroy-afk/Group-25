using UnityEngine;

namespace Interaction
{
    [System.Serializable] // basically an empty script to hold teh information that we want the inventory to display when called to do so.
    public class InventoryItem
    {
        public string title;
        public string description;
        public Sprite icon;

        public InventoryItem(string title, string description, Sprite icon)
        {
            this.title = title;
            this.description = description;
            this.icon = icon;
        }
    }

}
