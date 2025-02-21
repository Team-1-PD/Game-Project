using UnityEngine;

namespace Raven
{

    public class Inventory : MonoBehaviour
    {
        public const int HOTBAR_SIZE = 9;
        private Item[] itemSlots = new Item[HOTBAR_SIZE];

        [SerializeField] UI_Hotbar hotbar;


        private void Start()
        {
            if (hotbar != null)
            {
                hotbar.SetInventory(this);
            }

            // Manually add items to hotbar ** FOR TESTING **
            AddItem(new Item { itemType = Item.ItemType.Plant1, amount = 1 });
            AddItem(new Item { itemType = Item.ItemType.Plant2, amount = 2 });
            AddItem(new Item { itemType = Item.ItemType.Plant3, amount = 1 });
        }

        // Add item to hotbar, stack if existing ** TO DO: implement stacking
        public bool AddItem(Item newItem)
        {
            // Check if item already on hotbar
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                if (itemSlots[i] != null && itemSlots[i].itemType == newItem.itemType)
                {
                    itemSlots[i].amount += newItem.amount;
                    hotbar?.RefreshHotbar();
                    return true;
                }
            }

            // Put item in next empty slot
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                if (itemSlots[i] == null)
                {
                    itemSlots[i] = new Item { itemType = newItem.itemType, amount = newItem.amount };
                    hotbar?.RefreshHotbar();
                    return true;
                }
            }

            // No slots available
            Debug.Log("Inventory full!");
            return false;
        }

        // Remove item from hotbar
        public bool RemoveItem(Item.ItemType itemType, int amount)
        {
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                if (itemSlots[i] != null && itemSlots[i].itemType == itemType)
                {
                    if (itemSlots[i].amount >= amount)
                    {
                        itemSlots[i].amount -= amount;
                        if (itemSlots[i].amount <= 0)
                        {
                            itemSlots[i].amount = 0;
                        }
                        hotbar?.RefreshHotbar();
                    }
                }
            }
            return false;
        }

        public Item[] GetItemSlots()
        {
            return itemSlots;
        }

    }
}
