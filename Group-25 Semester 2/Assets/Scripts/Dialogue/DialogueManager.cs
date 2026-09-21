using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueTrigger currentTrigger;
    public GameObject dialoguePanel;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public Image portraitImage;

    public InputActionReference dialogueAdvance;

    private Conversation currentConversation;
    private int lineIndex;

    private void OnEnable()
    {
        dialogueAdvance.action.Enable();
    }

    private void OnDisable()
    {
        dialogueAdvance.action.Disable();
    }
    public void StartDialogue(Conversation conversation, DialogueTrigger trigger)
    {
        currentTrigger = trigger;

        currentConversation = conversation;
        lineIndex = 0;

        dialoguePanel.SetActive(true);

        ShowLine();
    }

    void ShowLine()
    {
        DialogueLine line = currentConversation.lines[lineIndex];

        speakerText.text = line.speaker;
        dialogueText.text = line.text;
        portraitImage.sprite = line.portrait;
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && dialogueAdvance.action.WasPressedThisFrame())
        {
            NextLine();
        }
    }

    void NextLine()
    {
        lineIndex++;

        if (lineIndex >= currentConversation.lines.Count)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        currentTrigger.ConversationFinished();
        currentTrigger = null;
    }
}
