using System.Security.Cryptography;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace kristina
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed = 7.5f;
        bool sprinting = false;
        [SerializeField] float sprint = 10;
        Vector3 movement = Vector3.zero;

        [SerializeField] Transform charObject;
        [SerializeField] Animator animator;

        //Rigidbody rb;
        CharacterController cc;
        void Start()
        {
            PlayerInput.Input.Player.Enable();
            PlayerInput.Input.Player.Move.performed += MoveInput;
            PlayerInput.Input.Player.Move.canceled += MoveInput;
            PlayerInput.Input.Player.Sprint.performed += Sprint;
            PlayerInput.Input.Player.Sprint.canceled += Sprint;

            //rb = GetComponent<Rigidbody>();
            cc = GetComponent<CharacterController>();
        }
        void Update()
        {
            //rb.linearVelocity = movement * Time.deltaTime * speed;
            if (sprinting)
                cc.Move(movement * Time.deltaTime * sprint);
            else
                cc.Move(movement * Time.deltaTime * speed);

            if (movement.magnitude > 0.1f)
            {
                charObject.LookAt(this.transform.position + movement);
            }

            animator.SetFloat("Horizontal", (float)movement.x);

            animator.SetFloat("Vertical", (float)movement.z);

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

        public void Sprint(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }
        }
    }
}