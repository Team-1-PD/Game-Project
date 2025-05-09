using HappyValley;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class OxygenManager : MonoBehaviour
    {
        [SerializeField] UnityEvent OnOxygenEmpty;

        [SerializeField] Oxygen oxygen;
        [field: SerializeField] float default_depletion_rate = 10f;
        
        [SerializeField, Header("<Current, Max>")] UnityEvent<float, float> OxygenAmount; 

        float depletion_rate;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            depletion_rate = default_depletion_rate;
            OxygenAmount?.Invoke(oxygen.CurrentOxygen, oxygen.MaxOxygen);
            StartCoroutine(OxygenReducer());
        }

        public IEnumerator OxygenReducer()
        {
            while (oxygen.CurrentOxygen > 0)
            {
                ReduceOxygen();
                yield return new WaitForSeconds(1);
            }
            OnOxygenEmpty.Invoke();
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

        public void ReduceOxygen()
        {
            oxygen.DecreaseOxygen(depletion_rate);
            OxygenAmount?.Invoke(oxygen.CurrentOxygen, oxygen.MaxOxygen);
        }

        public void AddToOxygen(int amount)
        {
            oxygen.IncreaseOxygen(amount);
            OxygenAmount?.Invoke(oxygen.CurrentOxygen, oxygen.MaxOxygen);
        }
    }
}