using UnityEngine;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public List<Conversation> conversations;

    private int conversationCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (conversationCount >= conversations.Count)
            return;

        dialogueManager.StartDialogue(conversations[conversationCount], this);
    }

    public void ConversationFinished()
    {
        conversationCount++;
    }
}