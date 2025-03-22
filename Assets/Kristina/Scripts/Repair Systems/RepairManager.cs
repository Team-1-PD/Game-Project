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
        public int ElapsedSinceMalfunction { get; private set; } = 0;

        private readonly List<RepairableModule> currentlyFunctioning = new();
        private readonly List<RepairableModule> currentlyMalfunctioning = new();
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
            ElapsedSinceMalfunction += ticks;
        }
        private int ChanceOfMalfunction() //multiplied by ChanceDayModifier
        {
            switch (ElapsedSinceMalfunction)
            {
                case < 300:
                    return 0; //0%
                case < 1500:
                    return 1; //1%
                case < 7500:
                    return 5; //5%
                default:
                    return 25; //25%
            }
        }
        private float ChanceDayModifier()
        {
            switch (TimeManager.GetTimeElapsed())
            {
                case < 4500: // up to 3 days
                    return 1f;
                case < 15000: // up to 10 days in
                    return 1.2f;
                case < 22500: // up to 15 days in
                    return 1.5f;
                case < 30000: // up to 20 days in
                    return 2.0f;
                case < 45000: // up to 30 days in
                    return 2.5f;
                case < 75000: // up to 50 days in
                    return 3.0f;
                default: // anything past 50 days
                    return 4.0f;
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
                float rolledValue = Random.Range(0, 100);
                if (rolledValue < ChanceOfMalfunction() * ChanceDayModifier())
                    StartNewMalfunction();

                yield return new WaitForSeconds(10f);
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
            ElapsedSinceMalfunction = 0;
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