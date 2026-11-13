using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public ItemFindManager collectionManager;

    private bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;

            collectionManager.ItemFound();

            // Disable the item after it is found
            gameObject.SetActive(false);
        }
    }
}
