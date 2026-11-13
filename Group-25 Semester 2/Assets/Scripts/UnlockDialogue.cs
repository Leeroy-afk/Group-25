using Interaction;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDialogueLater : MonoBehaviour
{
    [Header("Pedestals")]
    public List<TutPedestalInteract> pedestals;

    [Header("Dialogue")]
    public GameObject dialogueTriggerObject;

    private bool dialogueUnlocked = false;

    private void Start()
    {
        // Keep the dialogue trigger hidden until all items are placed
        if (dialogueTriggerObject != null)
        {
            dialogueTriggerObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (dialogueUnlocked)
            return;

        CheckAllPedestals();
    }

    private void CheckAllPedestals()
    {
        if (pedestals.Count == 0)
            return;

        foreach (TutPedestalInteract pedestal in pedestals)
        {
            if (pedestal == null || !pedestal.IsOccupied)
                return;
        }

        // All items have been placed
        UnlockDialogue();
    }

    private void UnlockDialogue()
    {
        dialogueUnlocked = true;

        if (dialogueTriggerObject != null)
        {
            dialogueTriggerObject.SetActive(true);
        }

        Debug.Log("All memories placed! Dialogue unlocked.");
    }
}
