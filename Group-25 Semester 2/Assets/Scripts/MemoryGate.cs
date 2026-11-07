using UnityEngine;

namespace Interaction
{
    public class MemoryGate : MonoBehaviour
    {
        [Header("Required Memory")]
        public string requiredItemID;

        [Header("Gate Settings")]
        public Animator gateAnimator;
        public string openAnimationTrigger = "Open";

        private bool isUnlocked = false;

        public void TryUnlock()
        {
            if (isUnlocked)
                return;

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager not found!");
                return;
            }

            if (InventoryManager.Instance.HasItem(requiredItemID))
            {
                isUnlocked = true;

                if (gateAnimator != null)
                {
                    gateAnimator.SetTrigger(openAnimationTrigger);
                }

                Debug.Log("Gate unlocked!");
            }
            else
            {
                Debug.Log("You need memory fragment: " + requiredItemID);
            }
        }
    }
}
