using UnityEngine;

namespace Raven
{
    [System.Serializable]
    public class Item
    {
        public enum ItemType
        {
            None,
            Placeable,
            Seed,
            Produce
        }

        [field: SerializeField] public ItemType itemType { get; private set; } = ItemType.None;

        //public int amount = 0;
        [field: SerializeField] public string ID { get; private set; }

        public Item(ItemType itemType, string ID)
        {
            this.itemType = itemType;
            this.ID = ID;
        }

        public static Item None()
        {
            return new Item(ItemType.None, "none");
        }
    }

}
