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
            set { currentOxygen = value; }
        }

        public float MaxOxygen
        {
            get { return maxOxygen; }
            set { maxOxygen = value; }
        }
    }
}
