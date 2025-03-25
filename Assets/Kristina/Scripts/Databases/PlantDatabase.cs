using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

namespace kristina
{
    [CreateAssetMenu(menuName = "Plant Database")]
    public class PlantDatabase : ScriptableObject
    {
        /*[SerializeField] private Plant[] plants;
        public Plant[] Plants => plants;*/

        [field: SerializeField, SerializedDictionary("ID", "Items")]
        private SerializedDictionary<string, Plant> plants;
        public Dictionary<string, Plant> Plants => plants;
    }

    [System.Serializable]
    public class Plant
    {
        [field: SerializeField] 
        public Sprite[] Sprites { get; private set; }
        [field: SerializeField] 
        public string ID { get; private set; }
        [field: SerializeField] 
        public string Name { get; private set; }

        [field: SerializeField]
        public string Produce_ID { get; private set; }
        [field: SerializeField]
        public int Production_Amount { get; private set; }

        [field: SerializeField, Header("In Ticks (1500 ticks per day)")]
        public int Grow_Duration { get; private set; }
    }
}