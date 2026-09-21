using UnityEngine;


namespace Interaction
{
    public interface IInteractable
    {
        Transform transform { get; }

        string DisplayName { get; }

        bool CanInteract();

        void Interact();

        void OnFocusGained();

        void OnFocusLost();
        public void OnInteract()
        {
            Debug.Log(" interacted with ");
        }
    }

}
