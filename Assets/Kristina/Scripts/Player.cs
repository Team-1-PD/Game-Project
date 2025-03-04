using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace kristina
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed = 25;
        Vector3 movement = Vector3.zero;

        Rigidbody rb;
        private InputSystem_Actions input;

        void Awake()
        {
            input = new InputSystem_Actions();
            input.Player.Enable();
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Update()
        {
            rb.linearVelocity = movement * Time.deltaTime * speed;

            input.Player.SlowTime.performed += ctx =>
            {
                Time.timeScale = 5;
            };

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