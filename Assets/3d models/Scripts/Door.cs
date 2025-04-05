using UnityEngine;

namespace HappyValley
{
    public class Door : MonoBehaviour
    {
        Animator animator;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void OpenDoor()
        {
            animator.SetTrigger("Open");
        }
        public void CloseDoor()
        {
            animator.SetTrigger("Close");
        }

    }
}