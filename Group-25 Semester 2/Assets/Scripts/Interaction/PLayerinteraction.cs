using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevelPhysics2D;

namespace Interaction
{
    public class PLayerinteraction : MonoBehaviour // this is attached to the player onject so that it can link to the input system 
    {
       [SerializeField] private float interactionRange = 3.5f;

        [SerializeField] private LayerMask interactionLayerMask;

        private Collider[] buffer = new Collider[32]; //contains all the colliders that are in range of the player

        private IInteractable focused;

        private void Update()
        {
            IInteractable nearest = FindNearestInteractable();
            UpdateFocus(nearest); 
        }

        private IInteractable FindNearestInteractable()
        {
            int count = Physics.OverlapSphereNonAlloc(transform.position, interactionRange, buffer, interactionLayerMask, QueryTriggerInteraction.Collide);
            IInteractable nearest = null;
            float bestDistSq = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                Collider col = buffer[i];
                if (col == null) continue;
                IInteractable interactable = col.GetComponentInParent<IInteractable>();
                if (interactable == null) continue;
                if (!interactable.CanInteract()) continue;
                float distSq = (col.transform.position - transform.position).sqrMagnitude;
                if (distSq < bestDistSq)
                {
                    bestDistSq = distSq;
                    nearest = interactable;
                }

            }
            return nearest;

        }

        private void UpdateFocus(IInteractable nearest) //compares if the new one is not equall to the current, and then changes the focus 
        {
            if (ReferenceEquals(focused, nearest)) return;
            focused?.OnFocusLost();
            focused = nearest;
            focused?.OnFocusGained();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log("interaction button pressed"); 

            if (!context.performed) return;
           

            if(InteractInspectUI.Instance != null && InteractInspectUI.Instance.IsInspecting) // when the inspectionUI panel is up, presing the interact button during the popup screen will close the popup. 
            {
                InteractInspectUI.Instance.CloseInspection();
                return;
            }

            if (focused != null && focused.CanInteract()) // this is the standard interaction code for if there is no pop up. 
            {
                Debug.Log("interaction button pressed and focused is not null");
                focused.Interact();
            }
        }


    }
}



