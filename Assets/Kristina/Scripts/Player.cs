using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace kristina
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed = 25;
        Vector3 movement = Vector3.zero;

        [SerializeField] Transform charObject;

        Rigidbody rb;

        void Start()
        {
            PlayerInput.Input.Player.Enable();
            PlayerInput.Input.Player.Move.performed += MoveInput;
            PlayerInput.Input.Player.Move.canceled += MoveInput;

            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            rb.linearVelocity = movement * Time.deltaTime * speed;
            if (movement.magnitude > 0.1f)
            {
                charObject.LookAt(this.transform.position + rb.linearVelocity);
            }
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