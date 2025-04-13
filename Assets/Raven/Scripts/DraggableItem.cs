using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Raven
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image image;
        [HideInInspector] public Transform parentAfterDrag;
        private Transform dragLayer;
        public int currentSlotIndex = -1;

        private void Awake()
        {
            // Assign to drag layer
            dragLayer = GameObject.Find("DragLayer")?.transform;

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            InventorySlot slot = GetComponentInParent<InventorySlot>();
            if (slot != null)
            {
                currentSlotIndex = slot.slotIndex;
            }

            // Debug.Log("Begin drag");
            parentAfterDrag = transform.parent;
            transform.SetParent(dragLayer != null ? dragLayer : transform.parent);
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
