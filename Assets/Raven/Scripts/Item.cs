namespace Raven
{
    [System.Serializable]
    public class Item
    {
        public enum ItemType
        {
            None,
            Plant1,
            Plant2,
            Plant3,
            Plant4,
        }

        public ItemType itemType = ItemType.None;
        public int amount = 0;

        public Item(ItemType itemType, int amount)
        {
            this.itemType = itemType;
            this.amount = amount;
        }

        public static Item None()
        {
            return new Item(ItemType.None, 0);
        }
    }

}
