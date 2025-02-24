using UnityEngine;

namespace Raven
{

    [System.Serializable]
    public class Inventory
    {

        [SerializeField] private Item[] itemSlots;
        [SerializeField] private int maxSlots = 0;

        public Inventory(int size)
        {
            maxSlots = size;
            itemSlots = new Item[size];
        }

        // Add item to hotbar, stack if existing
        public int AddItem(Item newItem)
        {
            int emptySlot = -1; // Invalid slot

            // Check if item already on hotbar
            for (int i = 0; i < maxSlots; i++)
            {
                if (itemSlots[i] != null && itemSlots[i].itemType == newItem.itemType)
                {
                    itemSlots[i].amount += newItem.amount;
                    return i;
                }
                else if (itemSlots[i] == null && emptySlot < 0)
                {
                    emptySlot = i;
                }
            }

            if (emptySlot >= 0)
            {
                itemSlots[emptySlot] = newItem;
                return emptySlot;
            }

            // No slots available
            Debug.Log("Inventory full!");
            return -1;
        }

        // Remove item from hotbar, returns amount of items removed from inventory
        public int RemoveItem(Item.ItemType itemType, int amount)
        {
            if (amount <= 0)
            {
                return 0;
            }

            for (int i = 0; i < maxSlots; i++)
            {
                // Filter ignored items
                if (itemSlots[i] == null || itemType != itemSlots[i].itemType)
                {
                    continue;
                }


                // Get the amount we are removing
                int itemCount = itemSlots[i].amount;
                if (amount > itemCount)
                {
                    itemSlots[i] = Item.None();
                    return itemCount;
                }

                itemSlots[i].amount -= amount;
                return amount;

            }

            return 0;
        }

        public Item[] GetItems()
        {
            return itemSlots;
        }

        public bool MoveItem(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex >= maxSlots || toIndex >= maxSlots)
            {
                return false;
            }
            else if (itemSlots[fromIndex] == null)
            {
                return false;
            }

            Item item = itemSlots[fromIndex];
            if (itemSlots[toIndex] == null)
            {
                // Remove and replace
                itemSlots[fromIndex] = null;
                itemSlots[toIndex] = item;
                return true;
            }

            // Stack items
            if (itemSlots[toIndex].itemType == item.itemType)
            {
                itemSlots[toIndex].amount += item.amount;
                itemSlots[fromIndex] = null;
                return true;
            }

            // Swap items
            itemSlots[fromIndex] = itemSlots[toIndex];
            itemSlots[toIndex] = item;

            return true;

        }

    }
}
