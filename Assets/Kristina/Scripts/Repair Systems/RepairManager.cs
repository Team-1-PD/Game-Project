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
        public int Elapsed_Since_Malfunction { get; private set; } = 0;

        private readonly List<RepairableModule> currently_functioning = new();
        private readonly List<RepairableModule> currently_malfunctioning = new();
        void Start()
        {
            foreach (var module in FindObjectsByType<RepairableModule>(FindObjectsSortMode.None)) 
            {
                currently_functioning.Add(module);
            }
            
            TimeManager.TimeElapsed += AddToElapsed;
            TimeManager.OnSleep += OnSleeping;
            TimeManager.OnWakeup += OnWaking;

            Instance = this;

            StartCoroutine(TryMalfunction());
        }
        public void AddToElapsed(int ticks)
        {
            Elapsed_Since_Malfunction += ticks;
        }
        private int ChanceOfMalfunction() //multiplied by ChanceDayModifier
        {
            switch (Elapsed_Since_Malfunction)
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

        [ContextMenu("Start a Malfunction")]
        public void StartNewMalfunction()
        {
            int count = currently_functioning.Count;
            if (count <= 0) return;

            int index = Random.Range(0, count);

            RepairableModule module = currently_functioning[index];
            currently_functioning.RemoveAt(index);

            currently_malfunctioning.Add(module);
            Elapsed_Since_Malfunction = 0;
            module.StartMalfunctioning();

            OnMalfunction?.Invoke(module.module_type);
            Debug.Log("start new malfunction");
        }

        public void FixedMalfunction(RepairableModule module)
        {
            currently_malfunctioning.Remove(module);

            currently_functioning.Add(module);

            OnRepair?.Invoke(module.module_type);
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