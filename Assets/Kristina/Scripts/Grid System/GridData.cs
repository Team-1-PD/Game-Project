using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace kristina
{
    public class GridData : MonoBehaviour
    {
        private Dictionary<Vector2Int, PlaceableObject> placedTileObjects = new();
        private static List<Vector2Int> illegalLocations;

        //private Dictionary<Vector2Int, string> placedTiles = new();
        public bool SaveTiles()
        {
            //save placedTileObjects to json
            return true;
        }
        public bool LoadTiles()
        {
            //load from json to placedTileObjects
            //then loop through each Tile to instantiate new gameObjects
            return true;
        }

        public void AddToGrid(Vector2Int position, PlaceableObject tileObject, string id)
        {
            placedTileObjects.Add(position, tileObject);
            //placedTiles.Add(position, id);
        }

        public string RemoveFromGrid(Vector2Int position)
        {
            GameObject obj = placedTileObjects[position].gameObject;
            string id = placedTileObjects[position].information.ID;

            placedTileObjects.Remove(position);
            //placedTiles.Remove(position);

            Destroy(obj);

            return id;
        }

        public bool CheckValidPositions(Vector2Int position)
        {
            //List<Vector2Int> occupyingPositions = CalculatePositions(gridPositions, occupiedSize);
            /*foreach (var position in occupyingPositions)
            {
            }*/

            if (!Database.PLACEABLES.ValidPlacements.Contains(position) || placedTileObjects.ContainsKey(position))
            {
                return false;
            }

            return true;
        }

        /*private List<Vector2Int> CalculatePositions(Vector2Int gridPositions, Vector2Int occupiedSize)
        {
            List<Vector2Int> allPositions = new();
            for (int x = 0; x < occupiedSize.x; x++)
            {
                for (int y = 0; y < occupiedSize.y; y++)
                {
                    allPositions.Add(gridPositions + new Vector2Int(x, y));
                }
            }
            return allPositions;
        }*/
    }
}