using Raven;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class Interactible : MonoBehaviour
    {
        public static Interactible current_interactible { get; private set; }
        [SerializeField] UnityEvent<Item> OnInteract;
        public bool Interact(Item current_item)
        {
            OnInteract?.Invoke(current_item);
            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            current_interactible = this;
        }
        private void OnTriggerExit(Collider other)
        {
            current_interactible = null;
        }
    }
}