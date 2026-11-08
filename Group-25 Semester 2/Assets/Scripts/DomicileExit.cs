using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomicileExit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogueTrigger dialogueTrigger;

    [Header("Settings")]
    [SerializeField] private int conversationCount= 1;
    [SerializeField] private float disappearDelay = 1f;

    private bool hasDisappeared = false;

    private void Update()
    {
        if (dialogueTrigger == null)
            return;

        if (hasDisappeared)
            return;

        Debug.Log("Current conversation count: " +
                  dialogueTrigger.conversationCount);

        if (dialogueTrigger.conversationCount >= conversationCount)
        {
            Debug.Log("Correct dialogue finished! Character will disappear.");

            hasDisappeared = true;
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearDelay);
        Debug.Log("Domicile has left");

        gameObject.SetActive(false);
    }
}
