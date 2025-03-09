using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class PlacementHandler : MonoBehaviour
    {
        public static PlacementHandler instance;

        public UnityEvent<string> OnPlace, OnPickup;

        const float OFFSET = .5f;
        const int HEIGHT = 0;

        [SerializeField] private Grid grid;
        [SerializeField] private GridData data;

        //private UI_Hotbar hotbar;

        //[SerializeField] private PlaceablesDatabase objectManager;

        public Vector2Int currentGridPos { get; private set; }

        [SerializeField] Transform highlighter;
        private Cursor placementCursor;
        bool highlighting;


        private void OnEnable()
        {
            instance = this;

            placementCursor = FindFirstObjectByType<Cursor>();
            
            ActivateHighlighter();

            //hotbar = FindFirstObjectByType<UI_Hotbar>();
        }

        public bool TryPlace(string id)
        {
            if (!data.CheckValidPositions(currentGridPos, id))
                return false;
            //if it's valid, continue
            Vector3 worldPos = grid.CellToWorld(new(currentGridPos.x, currentGridPos.y, HEIGHT));

            GameObject placedObject = Instantiate(Database.PLACEABLES.PlaceableObjects[id], worldPos + new Vector3(OFFSET, 0, OFFSET), new()); //maybe rotation?

            data.AddToGrid(currentGridPos, placedObject, id);
            OnPlace.Invoke(id);
            return true;
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
