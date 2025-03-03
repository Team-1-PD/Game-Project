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

        public Item(ItemType itemType, int amount)
        {
            this.itemType = itemType;
            //this.amount = amount;
        }

        public static Item None()
        {
            return new Item(ItemType.None, 0);
        }
    }

}
