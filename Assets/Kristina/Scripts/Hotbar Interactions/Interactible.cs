using Raven;
using UnityEngine;

namespace kristina
{
    public abstract class Interactible : MonoBehaviour
    {
        protected Item.ItemType[] valid_interact_items;

        public abstract bool TryInteract(Item item);
    }
}