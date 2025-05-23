using AYellowpaper.SerializedCollections;
using Raven;
using System.Collections.Generic;
using UnityEngine;

namespace kristina
{
    [CreateAssetMenu(menuName = "Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [field: SerializeField, SerializedDictionary("ID", "Items")]
        private SerializedDictionary<string, Item> items;
        public Dictionary<string, Item> Items => items;

        [field: SerializeField, SerializedDictionary("ID", "Sprites")]
        private SerializedDictionary<string, Sprite> itemSprites;
        public Dictionary<string, Sprite> ItemSprites => itemSprites;


    }
}