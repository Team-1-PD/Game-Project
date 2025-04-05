using kristina;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Raven
{
    public class ShopUpdate : MonoBehaviour
    {
        [Header("Shop Slots Settings")]
        [SerializeField] Transform scrollArea;
        [SerializeField] GameObject shopSlot;
        [SerializeField] ItemDatabase itemDatabase;
        [SerializeField] int maxSlots = 8;

        [Header("Shop Item Details Settings")]
        [SerializeField] TMP_Text shopDetails;
        [SerializeField] TMP_Text shopPrice;
        [SerializeField] Button buyButton;
        [SerializeField] TMP_Text bankAmount;

        private Item currentItem;
        [SerializeField] Player player;

        private UI_Hotbar hotbar;

        // Force bank to be amount for **TESTING**
        private void Awake()
        {
            player.getBank = 1000;
        }
        void Start()
        {
            hotbar = FindFirstObjectByType<UI_Hotbar>();

            buyButton.gameObject.SetActive(false);
            UpdateShop();
            bankAmount.text = player.getBank.ToString();
        }

        void UpdateShop()
        {
            // Get dictionary items
            var shopList = new List<Item>(itemDatabase.Items.Values);

            // Create shop slots 
            for (int i = 0; i < maxSlots; i++)
            {
                Item item = shopList[i];

                if (item.ID == "none")
                {
                    continue;
                }

                // Only spawn specific item types in shop
                if (item.itemType != Item.ItemType.Seed && item.itemType != Item.ItemType.Placeable)
                {
                    continue;
                }

                // Instantiate slot from template
                GameObject shopSlotObject = Instantiate(shopSlot, scrollArea);

                ShopSlot slotUI = shopSlotObject.GetComponent<ShopSlot>();

                // Pull sprite from database
                Sprite itemSprite = null;
                if (itemDatabase.ItemSprites.TryGetValue(item.ID, out Sprite sprite))
                {
                    itemSprite = sprite;

                }

                // Set slot item sprite and name
                slotUI.SetItem(itemSprite, item.NAME);

                // Update details window
                Button slotButton = shopSlotObject.GetComponent<Button>();
                slotButton.onClick.AddListener(() => UpdateDetails(item));


            }

        }

        void UpdateDetails(Item item)
        {
            currentItem = item;

            shopDetails.text = item.DESCRIPTION;
            shopPrice.text = item.COST.ToString();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => { BuyItem(item, currentItem.COST); });

            buyButton.gameObject.SetActive(true);

        }

        void BuyItem(Item item, int price)
        {
            if (player.getBank >= price)
            {
                player.getBank -= price;
                bankAmount.text = player.getBank.ToString();
                Debug.Log("Purchased: " + item.ID);

                hotbar.AddItem(Database.ITEMS.Items[item.ID], 1);
                Debug.Log("Added: " + item.ID + " to inventory");

            }
            else
            {
                Debug.Log("Not enough funds!");
            }

        }
    }
}
