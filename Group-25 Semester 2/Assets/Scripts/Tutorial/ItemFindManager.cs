using UnityEngine;
using TMPro;
using System.Collections;

public class ItemFindManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text promptText;

    [Header("Collection")]
    public int totalItems = 3;

    private int itemsFound = 0;
    public bool AllItemsFound => itemsFound >= totalItems;

    private void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    public void ItemFound()
    {
        itemsFound++;

        if (itemsFound < totalItems)
        {
            ShowPrompt("Medicine Found");
        }
        else
        {
            ShowPrompt("All Medicine Found");
        }
    }

    private void ShowPrompt(string message)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayPrompt(message));
    }

    private IEnumerator DisplayPrompt(string message)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = message;

        yield return new WaitForSeconds(3f);

        promptText.gameObject.SetActive(false);
    }
}
