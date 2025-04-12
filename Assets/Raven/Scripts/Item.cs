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

        [field: SerializeField] public int OXYGEN_VALUE { get; private set; }

        public Item(ItemType _itemType, string _id, string _name, string _desc, int _cost, int _oxygenValue)
        {
            TYPE = _itemType;
            ID = _id;
            NAME = _name;
            DESCRIPTION = _desc;
            COST = _cost;
            OXYGEN_VALUE = _oxygenValue;
        }

        public static Item None()
        {
            return new Item(ItemType.None, "none", "", "none", 0, 0);
        }
    }

}
