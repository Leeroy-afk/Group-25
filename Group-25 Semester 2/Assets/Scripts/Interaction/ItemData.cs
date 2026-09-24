using TMPro;
using UnityEngine;

namespace Interaction
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string title;

        public string description;

        public Sprite icon;



    }

}
