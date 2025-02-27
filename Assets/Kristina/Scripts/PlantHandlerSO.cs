using System.Collections.Generic;
using UnityEngine;

namespace kristina
{
    [CreateAssetMenu(menuName = "Plant Handler")]
    public class PlantHandlerSO : ScriptableObject
    {
        [SerializeField] private Plant[] plants;
        public Plant[] Plants => plants;
    }

    [System.Serializable]
    public class Plant
    {
        [field: SerializeField] 
        public Sprite[] sprites { get; private set; }
        [field: SerializeField] 
        public int id { get; private set; }
        [field: SerializeField] 
        public string name { get; private set; }

        [Header("(in tick minutes)")] [field: SerializeField] 
        public int growDuration { get; private set; }
    }
}