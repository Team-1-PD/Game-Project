using UnityEngine;
using UnityEngine.EventSystems;

namespace Raven
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] Transform itemContainer;
        public void OnDrop(PointerEventData eventData)
        {
            // Item being dropped
            DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            // Item currently in slot
            DraggableItem itemInSlot = GetComponentInChildren<DraggableItem>();

            // Check if null
            if (droppedItem == null)
            {
                return;
            }

            // If slot is empty, drop item into slot
            if (itemInSlot == null)
            {
                droppedItem.parentAfterDrag = transform;
            }

            // If slot is not empty, swap items
            else if (itemInSlot != droppedItem)
            {
                {
                    Transform originalParent = droppedItem.parentAfterDrag;
                    itemInSlot.transform.SetParent(originalParent);
                    droppedItem.parentAfterDrag = transform;
                }
            }

        }
    }
}

