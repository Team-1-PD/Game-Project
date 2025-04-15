using HappyValley;
using kristina;
using Raven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenationSystem : MonoBehaviour
{
    [SerializeField] OxygenManager oxygen_manager;
    [SerializeField] Oxygen oxygen;

    Queue<string> to_be_oxygen = new Queue<string>();

    bool currently_oxygenating;
    public void AddOxygenItemsToQueue(string[] items, int[] counts)
    {
        for (int index = 0; index < items.Length; index++) 
        {
            for (int count = counts[index]; count > 0; count--)
            {
                to_be_oxygen.Enqueue(items[index]);
            }
        }
    }

    private void Update()
    {
        if (!currently_oxygenating && to_be_oxygen.Count > 0)
        {
            currently_oxygenating = true;
            Debug.Log("start new");
            StartCoroutine(AddToOxygen(to_be_oxygen.Dequeue()));
        }
    }

    private IEnumerator AddToOxygen(string item_id)
    {
        Item item = Database.ITEMS.Items[item_id];
        //wait until oxygen is below the item's value
        while ((oxygen.CurrentOxygen + item.OXYGEN_VALUE) > oxygen.MaxOxygen)
        {
            yield return new WaitForSeconds(1);
        }
        oxygen_manager.AddToOxygen(item.OXYGEN_VALUE);
        yield return new WaitForSeconds(1);
        currently_oxygenating = false;
    }
}
