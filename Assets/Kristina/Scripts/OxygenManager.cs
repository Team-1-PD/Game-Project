using HappyValley;
using Unity.Collections;
using UnityEngine;

namespace kristina
{
    public class OxygenManager : MonoBehaviour
    {
        [SerializeField] Oxygen oxygen;
        [field: SerializeField, WriteOnly] readonly float default_depletion_rate = 10f;
        float depletion_rate;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            depletion_rate = default_depletion_rate;
            TimeManager.TimeElapsed += ReduceOxygen;
        }

        public void IncreaseDepletionRate(float amount)
        {
            depletion_rate *= amount;
        }
        public void ReduceDepletionRate(float amount)
        {
            depletion_rate /= amount;
        }
        public void ResetDepletionRate()
        {
            depletion_rate = default_depletion_rate;
        }

        public void ReduceOxygen(int ticks)
        {
            Debug.Log(default_depletion_rate);
            oxygen.DecreaseOxygen(depletion_rate / ticks);
        }
    }
}