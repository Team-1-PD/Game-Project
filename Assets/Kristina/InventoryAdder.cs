using UnityEngine;
using Raven;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

namespace kristina
{
    public class InventoryAdder : MonoBehaviour
    {
        UI_Hotbar hotbar;

        [field: SerializeField, SerializedDictionary("Item ID", "Item Count"), Header("Items To Add")]
        private SerializedDictionary<string, int> AddToInventory;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            hotbar = FindFirstObjectByType<UI_Hotbar>();

            foreach (string key in AddToInventory.Keys)
            {
                int count = AddToInventory[key];

                hotbar.AddItem(Database.ITEMS.Items[key], count);
            }
        }
    }
}