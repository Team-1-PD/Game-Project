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

        [field: SerializeField, TextArea(3, 10)] public string DESCRIPTION { get; private set; }
        [field: SerializeField] public int COST { get; private set; }

        public Item(ItemType _itemType, string _id, string _name, string _desc, int _cost)
        {
            TYPE = _itemType;
            ID = _id;
            NAME = _name;
            DESCRIPTION = _desc;
            COST = _cost;
        }

        public static Item None()
        {
            return new Item(ItemType.None, "none", "", "none", 0);
        }
    }

}
