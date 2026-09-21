using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class SlotClickTest : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("<color=yellow>POINTER DOWN DETECTED ON SLOT!</color>");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("<color=green>POINTER CLICK DETECTED ON SLOT!</color>");
        }
    }
}