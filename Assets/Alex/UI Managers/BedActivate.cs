using UnityEngine;
using UnityEngine.Events;
using kristina;

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
            /*input.Player.Bed.performed += ctx =>
            {
                if (bedReady)
                {
                    OnActivate?.Invoke();
                }
            };*/
        }

        public bool Sleep()
        {
            if (bedReady)
            {
                OnActivate?.Invoke();
                return true;
            }

            return false;
        }

        private void OnCollisionEnter(Collision other)
        {
            WorldInteractions.instance.nearestBed = this;
            bedReady = true;
        }

        private void OnCollisionExit(Collision other)
        {
            WorldInteractions.instance.nearestBed = null;
            bedReady = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldInteractions.instance.nearestBed = this;
            bedReady = true;
        }
        private void OnTriggerExit(Collider other)
        {
            WorldInteractions.instance.nearestBed = null;
            bedReady = false;
        }
    }
}