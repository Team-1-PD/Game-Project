using System;
using System.Collections.Generic;
using UnityEngine;

namespace kristina
{
    public class GridData : MonoBehaviour
    {
        private Dictionary<Vector2Int, GameObject> occupiedTiles = new();

        public bool CheckValidPositions(Vector2Int gridPositions, Vector2Int occupiedSize)
        {
            List<Vector2Int> occupyingPositions = CalculatePositions(gridPositions, occupiedSize);
            foreach (var position in occupyingPositions)
            {
                if (occupiedTiles.ContainsKey(position))
                {
                    return false;
                }
            }
            return true;
        }

        private List<Vector2Int> CalculatePositions(Vector2Int gridPositions, Vector2Int occupiedSize)
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
        }
    }
}