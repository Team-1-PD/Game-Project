using UnityEngine;
using UnityEngine.InputSystem;

namespace kristina
{
    public class DropBox : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerInput.Input.Player.Disable();
            PlayerInput.Input.Menu.Enable();
            PlayerInput.Input.Menu.Interact.performed += AddToBox;
        }
        private void OnDisable()
        {
            PlayerInput.Input.Menu.Disable();
            PlayerInput.Input.Player.Enable();
            PlayerInput.Input.Menu.Interact.performed -= AddToBox;
        }

        public void AddToBox(InputAction.CallbackContext ctx)
        {
            
        }
    }
}