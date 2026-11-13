using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public string scenename;
    public ItemFindManager collectionManager;
    public TMP_Text promptText;

    private bool sceneChanging = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sceneChanging)
        {
            if (collectionManager.AllItemsFound)
            {
                sceneChanging = true;
                SceneManager.LoadScene(scenename);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(DisplayMessage());
            }
        }

    }
    private System.Collections.IEnumerator DisplayMessage()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = "Not all medicine found!";

        yield return new WaitForSeconds(3f);

        promptText.gameObject.SetActive(false);
    }

}





