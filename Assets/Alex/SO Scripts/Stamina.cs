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
        }

        public float MaxStamina
        {
            get { return maxStamina; }
            set { maxStamina = value; }
        }

        public void IncreaseStamina(float x)
        {
            currentStamina += x;
        }

        public void DecreaseStamina(float y)
        {
            currentStamina -= y;
        }

        public void FullyRestoreStamina()
        {
            currentStamina = maxStamina;
        }

        public void PartiallyRestoreStamina()
        {
            if(currentStamina < (maxStamina * 3 / 4))
            {
                currentStamina = (maxStamina * 3 / 4);
            }
        }
    }
}
