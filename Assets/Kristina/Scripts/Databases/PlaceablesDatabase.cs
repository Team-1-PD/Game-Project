using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace kristina
{
    //using UnityEngine.Rendering;

    [CreateAssetMenu(menuName = "Placeables Database")]
    public class PlaceablesDatabase : ScriptableObject
    {
        //<ID, object>
        [field: SerializeField, SerializedDictionary("ID", "Objects")]
        private SerializedDictionary<string, GameObject> placeableObjects;
        public Dictionary<string, GameObject> PlaceableObjects => placeableObjects;
    }
}