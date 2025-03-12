using HappyValley;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class RepairManager : MonoBehaviour
    {
        public UnityEvent<RepairableModule.ModuleType> OnMalfunction;
        public UnityEvent<RepairableModule.ModuleType> OnRepair;

        public static RepairManager Instance { get; private set; }
        private bool sleeping = false;
        public int elapsedSinceMalfunction { get; private set; } = 0;
        private float percentChanceOfMalfuction = 0.0f;

        private List<RepairableModule> currentlyFunctioning = new();
        private List<RepairableModule> currentlyMalfunctioning = new();

        void Start()
        {
            foreach (var module in FindObjectsByType<RepairableModule>(FindObjectsSortMode.None)) 
            {
                currentlyFunctioning.Add(module);
            }
            
            TimeManager.TimeElapsed += AddToElapsed;
            TimeManager.OnSleep += OnSleeping;
            TimeManager.OnWakeup += OnWaking;

            Instance = this;

            StartCoroutine(TryMalfunction());
        }
        public void AddToElapsed(int ticks)
        {
            elapsedSinceMalfunction += ticks;
            switch (elapsedSinceMalfunction)
            {
                case < 300:
                    percentChanceOfMalfuction = 0.0f;
                    break;
                case < 1500:
                    percentChanceOfMalfuction = 0.15f;
                    break;
                case < 3000:
                    percentChanceOfMalfuction = 0.40f;
                    break;
                default:
                    percentChanceOfMalfuction = 1.0f;
                    break;
            }
        }

        private IEnumerator TryMalfunction()
        {
            while (true)
            {
                if (Time.timeScale <= 0.1 || sleeping)
                {
                    yield return new WaitForEndOfFrame();
                    continue; //don't run when paused or sleeping
                }

                Debug.Log("try malfunction");

                if (Random.Range(0, 1) < percentChanceOfMalfuction)
                    StartNewMalfunction();

                yield return new WaitForSeconds(5f);
            }
        }

        public void StartNewMalfunction()
        {
            int count = currentlyFunctioning.Count;
            if (count <= 0) return;

            int index = Random.Range(0, count);

            RepairableModule module = currentlyFunctioning[index];
            currentlyFunctioning.RemoveAt(index);

            currentlyMalfunctioning.Add(module);
            elapsedSinceMalfunction = 0;
            module.StartMalfunctioning();

            OnMalfunction.Invoke(module.moduleType);
            Debug.Log("start new malfunction");
        }

        public void FixedMalfunction(RepairableModule module)
        {
            currentlyMalfunctioning.Remove(module);

            currentlyFunctioning.Add(module);

            OnRepair.Invoke(module.moduleType);
        }

        public void OnSleeping()
        {
            sleeping = true;
        }
        public void OnWaking()
        {
            sleeping = false;
        }
    }
}