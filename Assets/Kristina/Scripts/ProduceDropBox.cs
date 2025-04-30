using HappyValley;
using Raven;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class ProduceDropBox : DropBoxGeneric
    {
        public UnityEvent OnOpen, OnClose;

        bool is_open = false;

        [SerializeField] int open_hour;
        [SerializeField] int close_hour;

        Player player;
        private void Awake()
        {
            player = FindFirstObjectByType<Player>();
            //TimeManager.OnDateTimeChanged += CheckTime;
        }

        public void CheckTime(DateTime time)
        {
            //Debug.Log($"hour: {time.hour}");
            if (time.hour < close_hour && time.hour >= open_hour)
            {
                if (is_open) return;
                //just opened
                OpenBox();
            }
            else
            {
                if (!is_open) return;
                //just closed
                CloseBox();
            }
        }

        public override void Interact(Item current_item)
        {
            //if (is_open)
                base.Interact(current_item);
        }

        public void SellCollectedProduce()
        {
            while (input_items.Count > 0)
            {
                string item = input_items.Pop();
                int count = input_amounts.Pop();

                int value = count * Database.ITEMS.Items[item].COST;
                player.getBank += value;
            }
        }

        public void OpenBox()
        {
            is_open = true;
            OnOpen?.Invoke();
        }
        public void CloseBox()
        {
            is_open = false;
            OnClose?.Invoke();
            sprite.gameObject.SetActive(false);
            SellCollectedProduce();
        }
        private void OnTriggerExit(Collider other)
        {
            CloseBox();
            OpenBox();
        }
    }
}



