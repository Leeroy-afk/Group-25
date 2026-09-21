using Unity.VisualScripting;
using UnityEngine.Events;
namespace Assets.Scripts
{
    public interface IInteractable1
    {
        public UnityEvent onInteract { get; protected set; }
        public void Interact();
    }
}
