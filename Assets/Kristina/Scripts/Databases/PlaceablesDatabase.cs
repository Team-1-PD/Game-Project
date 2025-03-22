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
        [SerializeField]
        private List<Vector2Int> validPlacements;
        public Dictionary<string, PlaceableObject> PlaceableObjects => placeableObjects;
        public List<Vector2Int> ValidPlacements => validPlacements;

#if UNITY_EDITOR
        public void AddValidSpot(Vector2Int position)
        {
            Undo.RecordObject(this, "added " + position + " to validPlacements");

            if (validPlacements == null) 
                validPlacements = new();
            validPlacements.Add(position);
        }

        public void RemoveValidSpot(Vector2Int position)
        {
            Undo.RecordObject(this, "removed " + position + " from validPlacements");
            if (validPlacements == null)
                validPlacements = new();
            validPlacements.Remove(position);
        }
    }
#endif
}