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

        private Inventory inventory;
        private GameObject[] slotUI;

        private void Awake()
        {
            // Create hotbar
            slotUI = new GameObject[Inventory.HOTBAR_SIZE];
            for (int i = 0; i < Inventory.HOTBAR_SIZE; i++)
            {
                GameObject slotObject = Instantiate(slotTemplate, slotContainer);
                slotObject.SetActive(true);
                slotUI[i] = slotObject;
            }
        }

        public void RefreshHotbar()
        {
            // Check if null
            if (inventory == null || slotUI == null)
            {
                return;
            }

            // Update hotbar UI 
            Item[] slots = inventory.GetItemSlots();
            for (int i = 0; i < Inventory.HOTBAR_SIZE; i++)
            {
                GameObject slotObject = slotUI[i];

                Image itemImage = slotObject.transform.Find("ItemImage")?.GetComponent<Image>();

                if (slots[i] != null)
                {
                    if (itemImage != null)
                    {
                        itemImage.sprite = AssignSprite(slots[i].itemType);
                        itemImage.enabled = true;
                    }

                }
                else
                {
                    if (itemImage != null)
                    {
                        itemImage.sprite = null;
                        itemImage.enabled = false;
                    }
                }
            }

        }

        // Assign sprite image
        private Sprite AssignSprite(Item.ItemType itemType)
        {
            switch (itemType)
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
            }
        }

        public void SetInventory(Inventory inventory)
        {
            this.inventory = inventory;
        }
    }


}
