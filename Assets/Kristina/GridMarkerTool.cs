using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace kristina
{
    public class GridMarkerTool : MonoBehaviour
    {
        [SerializeField] GameObject marker;
        [SerializeField] Camera cam;
        Grid grid;

        Dictionary<Vector2Int, GameObject> markers = new Dictionary<Vector2Int, GameObject>();

        void Start()
        {
            PlayerInput.Input.Player.Crouch.performed += ToggleMarkerAt;
            grid = FindFirstObjectByType<Grid>();

            foreach (var position in Database.PLACEABLES.ValidPlacements)
            {
                CreateMarker(position);
            }
        }

        public void ToggleMarkerAt(InputAction.CallbackContext ctx)
        {
            Vector3 mouse = Input.mousePosition;

            Ray ray = cam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, 50f)) return;

            Vector3Int pos = grid.WorldToCell(hit.point);

            Vector2Int gridPos = new(pos.x, pos.y);

            if (Database.PLACEABLES.ValidPlacements.Contains(gridPos))
            {
                Database.PLACEABLES.RemoveValidSpot(gridPos);
                DestroyMarker(gridPos);
            }
            else
            {
                Database.PLACEABLES.AddValidSpot(gridPos);
                CreateMarker(gridPos);
            }
        }

        private void CreateMarker(Vector2Int gridPos)
        {
            GameObject obj = Instantiate(marker, grid.CellToWorld(new (gridPos.x, gridPos.y, 0)), new(), transform);
            obj.transform.position += new Vector3(.5f, 0, .5f);
            markers.Add(gridPos, obj);
        }
        private void DestroyMarker(Vector2Int gridPos)
        {
            GameObject obj = markers[gridPos];
            markers.Remove(gridPos);

            Destroy(obj);
        }
    }
}