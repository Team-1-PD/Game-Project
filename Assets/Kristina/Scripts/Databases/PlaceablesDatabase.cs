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
        private SerializedDictionary<string, PlaceableObject> placeableObjects;
        private List<Vector2Int> validPlacements = new();
        public Dictionary<string, PlaceableObject> PlaceableObjects => placeableObjects;
        public List<Vector2Int> ValidPlacements => validPlacements;

        public void AddValidSpot(Vector2Int position)
        {
            validPlacements.Add(position);
        }

        public void RemoveValidSpot(Vector2Int position)
        {
            validPlacements.Remove(position);
        }
    }
}