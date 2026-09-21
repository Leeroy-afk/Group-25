using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
