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
            Produce,
            Oxygenable
        }

        [field: SerializeField] public ItemType TYPE { get; private set; } = ItemType.None;

        //public int amount = 0;
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public string NAME { get; private set; }
        public Item(ItemType _itemType, string _id, string _name)
        {
            TYPE = _itemType;
            ID = _id;
            NAME = _name;
        }

        public static Item None()
        {
            return new Item(ItemType.None, "none", "");
        }
    }

}
