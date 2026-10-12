using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interaction
{
    public class EndingManager : MonoBehaviour
    {
        public static EndingManager Instance { get; private set; }

       
        [SerializeField] private PedestalInteract pedestalA;
        [SerializeField] private PedestalInteract pedestalB;

        [SerializeField] private string goodEndingScene = "GoodEnding";
        [SerializeField] private string neutralEndingScene = "NeutralEnding";
        [SerializeField] private string badEndingScene = "BadEnding";

        private void Awake()
        {
            // FIX: Changed from (Instance == this) to check if Instance is null
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CheckEndState()
        {
            // FIX: Safety check to make sure both individual slots are filled in the inspector
            if (pedestalA == null || pedestalB == null)
            {
                Debug.LogWarning("Please assign both Pedestal A and Pedestal B in the EndingManager inspector!");
                return;
            }

            // FIX: If either pedestal is empty, stop executing and wait for the player to place the other memory
            if (!pedestalA.IsOccupied || !pedestalB.IsOccupied)
            {
                return;
            }

            int goodCount = 0;
            int badCount = 0;

            // FIX: Check Pedestal A alignment
            if (pedestalA.CurrentlyPlacedItem.alignment == ItemData.MemoryAlignment.Good) goodCount++;
            else if (pedestalA.CurrentlyPlacedItem.alignment == ItemData.MemoryAlignment.Bad) badCount++;

            // FIX: Check Pedestal B alignment
            if (pedestalB.CurrentlyPlacedItem.alignment == ItemData.MemoryAlignment.Good) goodCount++;
            else if (pedestalB.CurrentlyPlacedItem.alignment == ItemData.MemoryAlignment.Bad) badCount++;

            // Both pedestals are verified full, proceed to choose ending
            TriggerEnding(goodCount, badCount);
        }

        private void TriggerEnding(int goodCount, int badCount)
        {
            // Clean up UI/game states before changing scenes
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Choose the ending based on the combination showing on the pedestals
            // FIX: Replaced non-existent 'pedestals.Count' with the literal number 2, and corrected scene string names
            if (goodCount == 2)
            {
                Debug.Log("Both pedestals have Good memories! Loading Good Ending.");
                SceneManager.LoadScene(goodEndingScene);
            }
            else if (badCount == 2)
            {
                Debug.Log("Both pedestals have Bad memories! Loading Bad Ending.");
                SceneManager.LoadScene(badEndingScene);
            }
            else
            {
                Debug.Log("One Good and one Bad memory placed! Loading Neutral Ending.");
                SceneManager.LoadScene(neutralEndingScene);
            }
        }
    }
}
