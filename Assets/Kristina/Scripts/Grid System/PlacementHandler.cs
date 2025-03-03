using Raven;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace kristina
{
    public class PlacementHandler : MonoBehaviour
    {
        public UnityEvent<string> OnPlace, OnPickup;

        const float OFFSET = .5f;
        const int HEIGHT = 0;

        [SerializeField] private Grid grid;
        [SerializeField] private GridData data;

        //[SerializeField] private PlaceablesDatabase objectManager;

        public Vector2Int currentGridPos { get; private set; }

        [SerializeField] Transform highlighter;
        private Cursor placementCursor;
        bool highlighting;


        private void OnEnable()
        {
            placementCursor = FindFirstObjectByType<Cursor>();
            
            ActivateHighlighter(); //TEMPORARY, will be activated when a placeable item is selected
        }


        private void Update()
        {
            //TEMPORARY
            if (Input.GetKeyDown(KeyCode.P))
            {
                TryPlace("incubator");
            }
        }

        public bool TryPlace(string id)
        {
            /*if (!data.CheckValidPositions(currentGridPos, furniture.Data.OccupiedSize))
                return false;*/
            /*if (inventory.CurrentFurniture == null || !inventory.FurnitureInventory.ContainsKey(inventory.CurrentFurniture.Data.ID))
                return false;*/
            //if it's valid, continue
            Vector3 worldPos = grid.CellToWorld(new(currentGridPos.x, currentGridPos.y, HEIGHT));
            GameObject placedObject = Instantiate(PlaceablesDatabase.instance.PlaceableObjects[id], worldPos + new Vector3(OFFSET, OFFSET, OFFSET), new()); //maybe rotation?

            //furnitureGroup.InstantiateFurniture(currentGridPos, furnObject);

            //data.AddFurnAtPosition(currentGridPos, furniture.Data.OccupiedSize, furniture.Data.ID);

            //OnPlace.Invoke(furniture.Data.ID);
            return false;
        }



        public void ActivateHighlighter()
        {
            highlighting = true;
            StartCoroutine(MoveHighlighter());
        }

        public void DeactivateHighlighter()
        {
            highlighting = false;
        }

        private IEnumerator MoveHighlighter()
        {
            
            while (highlighting)
            {
                Vector3Int gridPos = grid.WorldToCell(placementCursor.transform.position);
                currentGridPos = new Vector2Int(gridPos.x, gridPos.y);

                highlighter.position = grid.CellToWorld(new(currentGridPos.x, currentGridPos.y, HEIGHT))
                    + new Vector3(OFFSET, 0, OFFSET);

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
