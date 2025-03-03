using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

namespace HappyValley
{
    public class BedActivate : MonoBehaviour
    {
        [SerializeField] UnityEvent OnActivate;

        private InputSystem_Actions input;
        private bool bedReady;

        private void Awake()
        {
            input = new InputSystem_Actions();
            input.Player.Enable();
        }

        private void Update()
        {
            input.Player.Bed.performed += ctx =>
            {
                if (bedReady)
                {
                    OnActivate?.Invoke();
                }
            };
        }

        private void OnCollisionEnter(Collision other)
        {
            bedReady = true;
        }

        private void OnCollisionExit(Collision other)
        {
            bedReady = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            bedReady = true;
        }
        private void OnTriggerExit(Collider other)
        {
            bedReady = false;
        }
    }
}