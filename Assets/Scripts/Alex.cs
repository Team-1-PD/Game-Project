using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace HappyValley
{
    public class Alex : MonoBehaviour
    {
        [SerializeField] float speed = 25;
        Vector3 movement = Vector3.zero;

        Rigidbody rb;

        [SerializeField] Stamina playerStamina;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            rb.linearVelocity = movement * Time.deltaTime * speed;
        }

        public void MoveInput(InputAction.CallbackContext ctx)
        {
            //camera directional movement
            Vector3 camRight = Camera.main.transform.right;
            camRight.y = 0;
            camRight.Normalize();

            Vector3 camForward = Camera.main.transform.forward;
            camForward.y = 0;
            camForward.Normalize();
            //--

            Vector3 input = ctx.ReadValue<Vector2>();
            movement = (input.x * camRight + input.y * camForward).normalized;
        }
    }
}

    
