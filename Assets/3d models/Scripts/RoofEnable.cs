using UnityEngine;

namespace HappyValley
{
    public class RoofEnable : MonoBehaviour
    {
        [SerializeField] GameObject roof;

        public void ToggleRoof()
        {
            roof.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            ToggleRoof();
        }
    }
}