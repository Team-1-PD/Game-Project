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
        [SerializeField] PlantDatabase plantDatabase;
        //[SerializeField] int maxSlots = 8;

        [Header("Shop Item Details Settings")]
        [SerializeField] TMP_Text shopDetails;
        [SerializeField] TMP_Text shopPrice;
        [SerializeField] Button buyButton;
        [SerializeField] TMP_Text bankAmount;
        [SerializeField] TMP_Text oxygenAmount;
        [SerializeField] TMP_Text sellAmount;

        private Item currentItem;

        Player player;

        private UI_Hotbar hotbar;

        // Force bank to be amount for **TESTING**
        /*private void Awake()
        {
            player.getBank = 1000;
        }*/
        void Start()
        {
            hotbar = FindFirstObjectByType<UI_Hotbar>();
            player = FindFirstObjectByType<Player>();

            buyButton.gameObject.SetActive(false);
            UpdateShop();
            bankAmount.text = player.getBank.ToString();
        }
        private void OnEnable()
        {
            PlayerInput.Input.Player.Disable();
        }
        private void OnDisable()
        {
            PlayerInput.Input.Player.Enable();
        }

        void UpdateShop()
        {
            // Get dictionary items
            var shopList = new List<Item>(itemDatabase.Items.Values);

            // Create shop slots 
            //int count = 0;
            foreach (Item item in itemDatabase.Items.Values)
            {
                if (item.ID == "none")
                {
                    continue;
                }

                // Only spawn specific item types in shop
                if (item.TYPE != Item.ItemType.Seed && item.TYPE != Item.ItemType.Placeable)
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

                //if we wanted limited slots:
                /*count++;
                if (count >= maxSlots)
                {
                    break;
                }*/
            }
        }

        void UpdateDetails(Item item)
        {

            currentItem = item;


            shopDetails.text = item.DESCRIPTION;

            shopPrice.text = "$ " + item.COST.ToString();

            if (item.TYPE == Item.ItemType.Placeable)
            {
                oxygenAmount.text = "";
                sellAmount.text = "";
                return;
            }
            string produce_id = Database.PLANTS.Plants[item.ID].Produce_ID;
            int produce_value = Database.ITEMS.Items[produce_id].COST;

            int produce_oxygen = Database.ITEMS.Items[produce_id].OXYGEN_VALUE;

            oxygenAmount.text = "Oxygen Amount: " + produce_oxygen;
            sellAmount.text = "Sell Value: " + produce_value;


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
