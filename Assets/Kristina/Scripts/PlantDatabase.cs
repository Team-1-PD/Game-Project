using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace kristina
{
    [CreateAssetMenu(menuName = "Plant Database")]
    public class PlantDatabase : ScriptableSingleton<PlantDatabase>
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
        public Sprite[] sprites { get; private set; }
        [field: SerializeField] 
        public string id { get; private set; }
        [field: SerializeField] 
        public string name { get; private set; }

        [field: SerializeField, Header("In Ticks (1500 ticks per day)")]
        public int growDuration { get; private set; }
    }
}