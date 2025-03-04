using Raven;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class TriggerSystem : MonoBehaviour
    {
        public UnityEvent<Item> EnterTrigger, ExitTrigger;

        public Item item;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnterTrigger.Invoke(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ExitTrigger.Invoke(item);
            }
        }
    }
}