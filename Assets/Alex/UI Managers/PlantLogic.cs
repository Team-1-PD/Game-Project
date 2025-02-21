using UnityEngine;

namespace HappyValley
{
    public class PlantLogic : MonoBehaviour
    {
        private float timeElapsed;
        private float age;

        void Start()
        {
            timeElapsed = TimeManager.GetTimeElapsed();
        }

        void Update()
        {
            age = TimeManager.GetTimeElapsed() - timeElapsed;

            if (transform.localScale.y < 3)
            {
                transform.localScale += new Vector3(age * (3 * Mathf.Pow(10, -7)), 3 * age * (3 * Mathf.Pow(10, -7)), 0);
            }
        }
    }
}