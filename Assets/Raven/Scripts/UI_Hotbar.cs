using kristina;
using System.Collections;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Raven
{

    public class UI_Hotbar : MonoBehaviour
    {
        [SerializeField] Transform slotContainer;
        [SerializeField] GameObject slotTemplate;

        [SerializeField] private Sprite plant1_sprite;
        [SerializeField] private Sprite plant2_sprite;
        [SerializeField] private Sprite plant3_sprite;
        [SerializeField] private Sprite plant4_sprite;

        [SerializeField] private Inventory inventory;
        private GameObject[] hotbar;


        public const int HOTBAR_SIZE = 9;


        private void Awake()
        {
            inventory = new Inventory(HOTBAR_SIZE);

            // Create hotbar
            hotbar = new GameObject[HOTBAR_SIZE];
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                GameObject slotObject = Instantiate(slotTemplate, slotContainer);
                slotObject.SetActive(true);

                // Assign indexes
                InventorySlot slot = slotObject.GetComponent<InventorySlot>();
                if (slot != null)
                {
                    slot.slotIndex = i;
                }

                hotbar[i] = slotObject;
            }

            StartCoroutine(CreateItems());

            // Manually add items to hotbar ** FOR TESTING **
            /*AddItem(new Item(Item.ItemType.Plant1, 1));
            AddItem(new Item(Item.ItemType.Plant2, 2));
            AddItem(new Item(Item.ItemType.Plant3, 1));
            AddItem(new Item(Item.ItemType.Plant4, 2));*/

        }

        private IEnumerator CreateItems()
        {
            yield return new WaitForEndOfFrame();
            AddItem(ItemDatabase.instance.Items["leaf"]);
            AddItem(ItemDatabase.instance.Items["egg"]);
            AddItem(ItemDatabase.instance.Items["flower"]);
            AddItem(ItemDatabase.instance.Items["mushroom"]);
            AddItem(ItemDatabase.instance.Items["butterfly"]);
        }

        // Stacks items otherwise places in first empty slot
        public int AddItem(Item item)
        {
            int position = inventory.AddItem(item.ID, 1); //TODO: amount adjustments
            if (position >= 0)
            {
                RefreshHotbar();
                return position;
            }

            return -1;
        }


        // Returns how many items were removed from the hotbar
        public int RemoveItem(Item item)
        {
            int amountRemoved = inventory.RemoveItem(item.ID, 1); //TODO:: amount adjustments
            if (amountRemoved > 0)
            {
                RefreshHotbar();
            }

            return amountRemoved;
        }

        // Swaps, moves, and stacks items
        public bool MoveItem(int fromIndex, int toIndex)
        {
            return inventory.MoveItem(fromIndex, toIndex);
        }

        public Item GetItemAt(int index)
        {
            return inventory.GetItemAt(index);
        }
        public GameObject GetItemSlotAt(int index)
        {
            return hotbar[index];
        }

        public Item[] GetItems()
        {
            return inventory.GetItems();
        }


        public void RefreshHotbar()
        {
            // Update hotbar UI 
            Item[] slots = inventory.GetItems();
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                Item item = slots[i];
                Image itemImage = hotbar[i].transform.Find("ItemImage")?.GetComponent<Image>();
                if (itemImage == null)
                {
                    continue;
                }

                if (item == null || item.itemType == Item.ItemType.None)
                {
                    itemImage.sprite = null;
                    itemImage.enabled = false;
                    continue;
                }

                itemImage.sprite = AssignSprite(slots[i]);
                itemImage.enabled = true;

            }


        }

        // Assign sprite image
        private Sprite AssignSprite(Item item)
        {
            return ItemDatabase.instance.ItemSprites[item.ID];
            /*switch (itemType)
            {
                case Item.ItemType.Plant1:
                    return plant1_sprite;
                case Item.ItemType.Plant2:
                    return plant2_sprite;
                case Item.ItemType.Plant3:
                    return plant3_sprite;
                case Item.ItemType.Plant4:
                    return plant4_sprite;
                default:
                    return null;
            }*/
        }
    }
}
