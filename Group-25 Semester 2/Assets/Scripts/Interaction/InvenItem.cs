using UnityEngine;

namespace Inventory
{
    public class InvenItem // the data the inventory will display for each item
    {
        public string Title { get; }
        public string Description { get; }

        public Sprite Image { get; }

        public InvenItem(string title, string description, Sprite image)
        {
            Title = title;
            Description = description;
            Image = image;
        }
    }

}
