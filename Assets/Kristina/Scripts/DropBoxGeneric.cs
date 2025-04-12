using Raven;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace kristina
{
    public class DropBoxGeneric : MonoBehaviour
    {
        public UnityEvent<Item> OnInputItem, OnCollectItem;
        [SerializeField] protected UI_Hotbar hotbar;
        [SerializeField] protected Image sprite;

        [SerializeField] protected Item.ItemType[] valid_types;

        
        protected readonly Stack<string> input_items = new Stack<string>();
        protected readonly Stack<int> input_amounts = new Stack<int>();

        public virtual void Interact(Item current_item)
        {
            if (current_item.TYPE == Item.ItemType.None)
            {
                CollectItem();
            }
            else
            {
                InputItem(current_item.ID);
            }
        }


        private void CollectItem()
        {
            if (input_items.Count <= 0) return;
            string item_id = input_items.Pop();
            int amount = input_amounts.Pop();
            hotbar.AddItem(Database.ITEMS.Items[item_id], amount);


            if (input_items.Count <= 0)
                sprite.gameObject.SetActive(false);
            else
            {
                sprite.sprite = Database.ITEMS.ItemSprites[input_items.Peek()];
            }
            OnCollectItem?.Invoke(Database.ITEMS.Items[item_id]);
        }
        private void InputItem(string item_id)
        {
            bool is_valid = false;
            foreach (Item.ItemType type in valid_types) 
            {
                if (Database.ITEMS.Items[item_id].TYPE == type)
                {
                    is_valid = true;
                    break;
                }
            }
            if (!is_valid) return;

            hotbar.RemoveItem(Database.ITEMS.Items[item_id], 1);
            if (input_items.Count > 0 && input_items.Peek().Equals(item_id))
            {
                int amount = input_amounts.Pop() + 1;
                input_amounts.Push(amount);
            }
            else
            {
                input_items.Push(item_id);
                input_amounts.Push(1);

                sprite.sprite = Database.ITEMS.ItemSprites[item_id];
                sprite.gameObject.SetActive(true);
            }
            OnInputItem?.Invoke(Database.ITEMS.Items[item_id]);
        }
    }
}