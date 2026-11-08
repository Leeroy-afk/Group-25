using UnityEngine;
using System.Collections;

namespace Interaction
{
    public class MemoryGate : MonoBehaviour
    {
        [Header("Required Memory")]
        public string requiredItemID;

        [Header("Gate Movement")]
        public Transform gatePivot;
        public float openAngle = 90f;
        public float openDuration = 1.5f;

        private bool isUnlocked = false;
        private Quaternion closedRotation;

        private void Awake()
        {
            if (gatePivot == null)
                gatePivot = transform;

            closedRotation = gatePivot.localRotation;
        }

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

                Debug.Log("Memory gate unlocked!");

                StartCoroutine(OpenGate());
            }
            else
            {
                Debug.Log("You need memory fragment: " + requiredItemID);
            }
        }

        private IEnumerator OpenGate()
        {
            Quaternion targetRotation =
                closedRotation * Quaternion.Euler(0f, openAngle, 0f);

            float elapsed = 0f;

            while (elapsed < openDuration)
            {
                elapsed += Time.deltaTime;

                float progress = Mathf.Clamp01(elapsed / openDuration);

                gatePivot.localRotation = Quaternion.Slerp(
                    closedRotation,
                    targetRotation,
                    progress
                );

                yield return null;
            }

            gatePivot.localRotation = targetRotation;
        }
    }
}