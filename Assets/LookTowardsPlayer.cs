using Unity.VisualScripting;
using UnityEngine;

namespace kristina
{
    public class LookTowardsPlayer : MonoBehaviour
    {
        Transform cam;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            cam = Camera.main.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (cam == null) return;
            Vector3 lookAt = cam.transform.position;
            lookAt.y = transform.position.y;

            transform.LookAt(lookAt);
        }
    }
}