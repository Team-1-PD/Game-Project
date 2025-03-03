using UnityEngine;

namespace Raven
{

    [System.Serializable]
    public class Inventory
    {

        [SerializeField] private Item[] itemSlots;
        [SerializeField] private int[] itemCounts;

        [SerializeField] private int maxSlots = 0;

        public Inventory(int size)
        {
            maxSlots = size;
            itemSlots = new Item[size];
            itemCounts = new int[size];
        }

        // Add item to hotbar, stack if existing
        public int AddItem(string itemID, int amount)
        {
            int emptySlot = -1; // Invalid slot

            // Check if item already on hotbar
            for (int i = 0; i < maxSlots; i++)
            {
                if (itemSlots[i] != null && itemSlots[i].ID.Equals(itemID))
                {
                    //itemSlots[i].amount += itemID.amount;
                    itemCounts[i]+= amount;
                    return i;
                }
                else if (itemSlots[i] == null && emptySlot < 0)
                {
                    emptySlot = i;
                }
            }

            if (emptySlot >= 0)
            {
                itemSlots[emptySlot] = ItemDatabase.instance.Items[itemID];
                itemCounts[emptySlot] = amount;
                return emptySlot;
            }

            // No slots available
            Debug.Log("Inventory full!");
            return -1;
        }

        // Remove item from hotbar, returns amount of items removed from inventory
        public int RemoveItem(string itemID, int amount)
        {
            if (amount <= 0)
            {
                return 0;
            }

            for (int i = 0; i < maxSlots; i++)
            {
                // Filter ignored items
                if (itemSlots[i] == null || !itemID.Equals(itemSlots[i].ID))
                {
                    continue;
                }


                // Get the amount we are removing
                int itemCount = itemCounts[i];
                if (amount > itemCount)
                {
                    itemSlots[i] = Item.None();
                    itemCounts[i] = 0;
                    return itemCount;
                }

                itemCounts[i] -= amount;
                return amount;

            }

            return 0;
        }
        public Item GetItemAt(int index)
        {
            return itemSlots[index];
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
            int itemCount = itemCounts[fromIndex];
            if (itemSlots[toIndex] == null)
            {
                // Remove and replace
                itemSlots[fromIndex] = null;
                itemCounts[fromIndex] = 0;

                itemSlots[toIndex] = item;
                itemCounts[toIndex] = itemCount;
                return true;
            }

            // Stack items
            if (itemSlots[toIndex].ID.Equals(item.ID))
            {
                itemCounts[toIndex] += itemCount;

                itemSlots[fromIndex] = null;
                itemCounts[fromIndex] = 0;
                return true;
            }

            // Swap items
            itemSlots[fromIndex] = itemSlots[toIndex];
            itemCounts[fromIndex] = itemCounts[toIndex];

            itemSlots[toIndex] = item;
            itemCounts[toIndex] = itemCount;

            return true;

        }

    }
}
