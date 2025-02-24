using UnityEngine;
using UnityEngine.EventSystems;

namespace Raven
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] Transform itemContainer;

        public int slotIndex;

        public void OnDrop(PointerEventData eventData)
        {

            UI_Hotbar hotbar = GetComponentInParent<UI_Hotbar>();
            if (hotbar == null)
            {
                Debug.Log("Could not find hotbar when dragging item.");
                return;
            }

            // Item being dropped
            DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

            // Item currently in slot
            DraggableItem itemInSlot = GetComponentInChildren<DraggableItem>();

            // Check if null
            if (droppedItem == null)
            {
                Debug.Log("Dropped item is null");
                return;
            }

            int fromIndex = droppedItem.currentSlotIndex;
            int toIndex = slotIndex;

            // Move the item
            if (!hotbar.MoveItem(fromIndex, toIndex))
            {
                Debug.Log("Unable to move the item from: " + fromIndex + " to: " + toIndex);
                return;
            }





            // If slot is not empty, swap items
            if (itemInSlot != null)
            {
                itemInSlot.currentSlotIndex = fromIndex;
                itemInSlot.transform.SetParent(droppedItem.parentAfterDrag);

            }

            droppedItem.currentSlotIndex = toIndex;
            droppedItem.parentAfterDrag = transform;

            hotbar.RefreshHotbar();

        }
    }
}

