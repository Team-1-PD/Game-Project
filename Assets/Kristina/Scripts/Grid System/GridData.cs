using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace kristina
{
    public class GridData : MonoBehaviour
    {
        private Dictionary<Vector2Int, GameObject> placedTileObjects = new();
        private Dictionary<Vector2Int, string> placedTiles = new();
        public bool SaveTiles()
        {
            //save placedTiles to json
            return true;
        }
        public bool LoadTiles()
        {
            //load from json to placedTiles
            //then loop through each Tile to instantiate new gameObjects
            return true;
        }

        public void AddToGrid(Vector2Int position, GameObject tileObject, string id)
        {
            placedTileObjects.Add(position, tileObject);
            placedTiles.Add(position, id);
        }

        public bool CheckValidPositions(Vector2Int position, string id)
        {
            //List<Vector2Int> occupyingPositions = CalculatePositions(gridPositions, occupiedSize);
            /*foreach (var position in occupyingPositions)
            {
            }*/

            if (placedTileObjects.ContainsKey(position))
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

    public class Tile
    {
        public string ID { get; private set; }
        public Tile(string _id)
        {
            ID = _id;
        }
    }
}