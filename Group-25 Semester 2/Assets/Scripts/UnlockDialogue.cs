using Interaction;
using UnityEngine;

public class UnlockDialogueLater : MonoBehaviour
{
    [Header("Pedestals")]
    public TutorialPedestalInteract[] pedestals;

    [Header("Dialogue")]
    public GameObject dialogueTrigger;

    private bool dialogueUnlocked = false;

    private void Start()
    {
        if (dialogueTrigger != null)
        {
            dialogueTrigger.SetActive(false);
        }
    }

    private void Update()
    {
        if (dialogueUnlocked)
            return;

        foreach (TutorialPedestalInteract pedestal in pedestals)
        {
            if (pedestal == null || !pedestal.IsOccupied)
            {
                return;
            }
        }

        UnlockDialogue();
    }

    private void UnlockDialogue()
    {
        dialogueUnlocked = true;

        if (dialogueTrigger != null)
        {
            dialogueTrigger.SetActive(true);
        }

        Debug.Log("Last Medicine placed. Dialogue unlocked.");
    }

}
