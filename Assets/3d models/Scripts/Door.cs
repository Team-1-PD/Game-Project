using UnityEngine;

namespace HappyValley
{
    public class Door : MonoBehaviour
    {
        Animator animator;

        [SerializeField] AudioClip open;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void OpenDoor()
        {
            animator.SetTrigger("Open");
            SoundFXManager.instance.PlaySoundFXClip(open, transform, .3f);
        }
        public void CloseDoor()
        {
            animator.SetTrigger("Close");
        }

    }
}