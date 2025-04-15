using System.Collections;
using UnityEngine;

namespace kristina
{
    public class OxygenDropBox : DropBoxGeneric
    {
        [SerializeField] OxygenationSystem oxygenation;
        public void AddToOxygenation()
        {
            if (input_items.Count == 0) return;
            string[] items = input_items.ToArray();
            int[] amounts = input_amounts.ToArray();

            oxygenation.AddOxygenItemsToQueue(items, amounts);
            input_items.Clear();
            input_amounts.Clear();
        }

        private void OnTriggerExit(Collider other)
        {
            AddToOxygenation();
            sprite.gameObject.SetActive(false);
        }
    }
}