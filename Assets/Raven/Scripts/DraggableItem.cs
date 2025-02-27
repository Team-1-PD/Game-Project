using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Raven
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image image;
        [HideInInspector] public Transform parentAfterDrag;
        public int currentSlotIndex = -1;

        public void OnBeginDrag(PointerEventData eventData)
        {
            InventorySlot slot = GetComponentInParent<InventorySlot>();
            if (slot != null)
            {
                currentSlotIndex = slot.slotIndex;
            }

            // Debug.Log("Begin drag");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

        }
        public void OnDrag(PointerEventData eventData)
        {
            // Debug.Log("Dragging");
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Debug.Log("End dragging");
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
    }
}
