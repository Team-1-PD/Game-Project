using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace kristina
{
    public class Database : MonoBehaviour
    {
        //public static Database Instance { get; private set; }

        [SerializeField] PlantDatabase plantDatabase;
        [SerializeField] ItemDatabase itemDatabase;
        [SerializeField] PlaceablesDatabase placeablesDatabase;

        [FixedAddressValueType]
        public static PlantDatabase PLANTS;
        public static ItemDatabase ITEMS { get; private set; }
        public static PlaceablesDatabase PLACEABLES { get; private set; }

        private void Awake()
        {
            PLANTS = plantDatabase;
            ITEMS = itemDatabase;
            PLACEABLES = placeablesDatabase;
        }
    }
}