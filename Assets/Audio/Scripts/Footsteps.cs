using UnityEngine;

namespace HappyValley
{
    public class Footsteps : MonoBehaviour
    {
        [SerializeField] AudioSource baseFootstep1;
        [SerializeField] AudioSource baseFootstep2;

        [SerializeField] AudioSource gravelFootstep1;
        [SerializeField] AudioSource gravelFootstep2;

        bool inside = false;

        private void FirstStep()
        {
            if (!baseFootstep1.isPlaying && inside == true)
            {
                baseFootstep1.Play();
            }
            else if (!baseFootstep1.isPlaying)
            {
                gravelFootstep1.Play();
            }
        }

        private void SecondStep()
        {
            if (!baseFootstep2.isPlaying && inside == true)
            {
                baseFootstep2.Play();
            }
            else if (!baseFootstep2.isPlaying)
            {
                gravelFootstep2.Play();
            }

            //SoundFXManager.instance.PlaySoundFXClip(secondFootstep, transform, .05f);
        }

        public void Change()
        {
            inside = !inside;
        }

        public void Inside()
        {
            inside = true;
            Debug.Log("Inside");
        }
    }
}