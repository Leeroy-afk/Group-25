using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionRange = 3f;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, interactionRange);

            foreach (Collider col in nearbyObjects)
            {
                ItemInteractable item = col.GetComponent<ItemInteractable>();
                if (item != null)
                {
                    item.Collect();
                    break; // Exit the loop after collecting the first item
                }
            }
        }
    }
}
