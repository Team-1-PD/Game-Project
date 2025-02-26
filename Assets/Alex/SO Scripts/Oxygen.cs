using UnityEngine;

namespace HappyValley
{
    [CreateAssetMenu(menuName = "Oxygen")]
    public class Oxygen : ScriptableObject
    {
        [SerializeField] float currentOxygen;
        [SerializeField] float maxOxygen;

        public float CurrentOxygen
        {
            get { return currentOxygen; }
        }

        public float MaxOxygen
        {
            get { return maxOxygen; }
            set { maxOxygen = value; }
        }
        public void IncreaseOxygen(float x)
        {
            currentOxygen += x;
        }

        public void DecreaseOxygen(float y)
        {
            currentOxygen -= y;
        }

        public void FullyRestoreOxygen()
        {
            currentOxygen = maxOxygen;
        }
    }
}
