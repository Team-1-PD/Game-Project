using UnityEngine;

namespace kristina
{
    public class PlaceableObject : MonoBehaviour
    {
        [field: SerializeField] public Placeable information { get; private set; }
    }

    [System.Serializable]
    public class Placeable
    {
        public enum placedType
        {
            CONTAINER,
            INCUBATOR,
            OTHER
        }
        [field:SerializeField] public string ID { get; private set; }
        
        public Placeable(string _id)
        {
            ID = _id;
        }
    }
}