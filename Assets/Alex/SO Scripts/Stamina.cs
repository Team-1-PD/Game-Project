using UnityEngine;

namespace HappyValley
{
    [CreateAssetMenu(menuName = "Stamina")]
    public class Stamina : ScriptableObject
    {
        [SerializeField] float currentStamina;
        [SerializeField] float maxStamina;

        public float CurrentStamina
        {
            get { return currentStamina; }
            set { currentStamina = value; }
        }

        public float MaxStamina
        {
            get { return maxStamina; }
            set { maxStamina = value; }
        }
    }
}
