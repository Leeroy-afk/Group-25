using UnityEngine;
using UnityEngine.SceneManagement;
using static Interaction.ItemData;

namespace Interaction
{
    public class EndingManager: MonoBehaviour
    {
        public static EndingManager Instance { get; private set; }

        [SerializeField] private PedestalInteract pedestalA;
        [SerializeField] private PedestalInteract pedestalB;

        [SerializeField] private string goodEndingScene = "GoodEnding";
        [SerializeField] private string neutralEndingScene = "NeutralEnding";
        [SerializeField] private string badEndingScene = "BadEnding";

        private void Awake()
        {
            if (Instance == this)
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
            if (!pedestalA.IsOccupied || !pedestalB.IsOccupied) return; // checks if both pedestals have an object

            MemoryAlignment alignA = pedestalA.CurrentlyPlacedItem.alignment;
            MemoryAlignment alignB = pedestalB.CurrentlyPlacedItem.alignment;

            EvaluateAndLoadEnding(alignA, alignB);
        }

        private void EvaluateAndLoadEnding(MemoryAlignment a, MemoryAlignment b) //calculates and takes you to the corrosponding ending 
        {
            Time.timeScale = 1f;

            if (a == MemoryAlignment.Good && b == MemoryAlignment.Good)
            {
                Debug.Log("Good ending earned!");
                SceneManager.LoadScene(goodEndingScene);
            }
            else if (a == MemoryAlignment.Bad && b == MemoryAlignment.Bad)
            {
                Debug.Log("Bad ending earned!");
                SceneManager.LoadScene(badEndingScene);
            }
            else
            {
                Debug.Log("Mixed items -> Neutral Ending!");
                SceneManager.LoadScene(neutralEndingScene);
            }
        }
    }

}
