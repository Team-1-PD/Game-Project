using UnityEngine;

namespace HappyValley
{
    public class RoofDisable : MonoBehaviour
    {
        [SerializeField] GameObject roof;

        public void ToggleRoof()
        {
            roof.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            ToggleRoof();
        }
    }
}