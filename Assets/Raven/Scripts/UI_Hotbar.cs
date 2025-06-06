using kristina;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Raven
{

    public class UI_Hotbar : MonoBehaviour
    {
        [SerializeField] Transform slotContainer;
        [SerializeField] GameObject slotTemplate;
        [SerializeField] TMP_Text amountText;

        [SerializeField] private Sprite plant1_sprite;
        [SerializeField] private Sprite plant2_sprite;
        [SerializeField] private Sprite plant3_sprite;
        [SerializeField] private Sprite plant4_sprite;

        [SerializeField] private Inventory inventory;

        [SerializeField] private HotbarSelector hotbarSelector;

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
        }

        // Stacks items otherwise places in first empty slot
        public int AddItem(Item item, int amount)
        {
            int position = inventory.AddItem(item.ID, amount);
            if (position >= 0)
            {
                RefreshHotbar();

                hotbarSelector.SelectSlot(hotbarSelector.currentSlotIndex);

                return position;
            }

            return -1;
        }


        // Returns how many items were removed from the hotbar
        public int RemoveItem(Item item, int amount)
        {
            int amountRemoved = inventory.RemoveItem(item.ID, amount); //TODO:: amount adjustments
            if (amountRemoved >= 0)
            {
                RefreshHotbar();
            }
            hotbarSelector.SelectSlot(hotbarSelector.currentSlotIndex);

            return amountRemoved;
        }

        // Swaps, moves, and stacks items
        public bool MoveItem(int fromIndex, int toIndex)
        {
            bool moved = inventory.MoveItem(fromIndex, toIndex);
            if (moved)
            {
                RefreshHotbar();
            }
            hotbarSelector.SelectSlot(hotbarSelector.currentSlotIndex);
            return moved;
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
            int[] counts = inventory.GetAmounts();
            for (int i = 0; i < HOTBAR_SIZE; i++)
            {
                Item item = slots[i];
                int count = counts[i];

                Image itemImage = hotbar[i].transform.Find("ItemImage")?.GetComponent<Image>();
                TMP_Text amountText = hotbar[i].transform.Find("ItemCounter/ItemCount")?.GetComponent<TMP_Text>();
                if (itemImage == null)
                {
                    continue;
                }

                if (item == null || item.TYPE == Item.ItemType.None)
                {
                    itemImage.sprite = null;
                    itemImage.enabled = false;
                    amountText.text = "";
                    continue;
                }

                itemImage.sprite = AssignSprite(slots[i]);
                itemImage.enabled = true;

                // Display count of items on hotbar
                if (count > 1)
                {
                    amountText.text = count.ToString();
                }
                else
                {
                    amountText.text = "";
                }

            }


        }

        // Assign sprite image
        private Sprite AssignSprite(Item item)
        {
            return Database.ITEMS.ItemSprites[item.ID];
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
