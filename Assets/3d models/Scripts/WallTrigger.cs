using UnityEngine;
using UnityEngine.Events;

namespace HappyValley
{
    public class WallTrigger : MonoBehaviour
    {
        [SerializeField] UnityEvent OnEnter;
        [SerializeField] UnityEvent OnExit;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke();
        }
    }
}
