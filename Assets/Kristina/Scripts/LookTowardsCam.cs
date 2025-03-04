using Unity.VisualScripting;
using UnityEngine;

namespace kristina
{
    public class LookTowardsCam : MonoBehaviour
    {
        [SerializeField] private bool lookDirect = false; //default false
        [SerializeField] private float divideByHeight = 1f;
        Transform cam;


        void Start()
        {
            cam = Camera.main.transform;
        }

        void Update()
        {
            if (cam == null) return;
            Vector3 lookAt = cam.transform.position;

            if (!lookDirect)
            {
                lookAt.y = transform.position.y;
            }
            else
            {
                lookAt.y /= divideByHeight;
            }

            transform.LookAt(lookAt);
        }
    }
}