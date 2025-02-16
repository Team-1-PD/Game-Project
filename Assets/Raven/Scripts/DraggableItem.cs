using UnityEngine;
using UnityEngine.EventSystems;

namespace Raven
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        Transform parentAfterDrag;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End dragging");
            transform.SetParent(parentAfterDrag);
        }
    }
}
