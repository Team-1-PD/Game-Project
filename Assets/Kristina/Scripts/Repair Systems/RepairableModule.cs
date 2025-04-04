using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class RepairableModule : MonoBehaviour
    {
        [SerializeField] protected float repair_rate = 1.0f;
        [SerializeField] protected float loss_rate = 1.0f;

        public UnityEvent OnMalfunction, OnRepair, OnTickMalfunctioning;

        protected float repair_progress { 
            get { return private_repair_progress; } 
            set { private_repair_progress = (value > 0) ? value : 0f; } }
        private float private_repair_progress = 0f;

        protected bool malfunctioning = false;
        public bool Repairing { get; protected set; } = false;
        [field: SerializeField] public ModuleType module_type { get; protected set; }
        public virtual void StartMalfunctioning()
        {
            repair_progress = 0f;
            malfunctioning = true;
            Repairing = false;
            OnMalfunction.Invoke();
            StartCoroutine(Malfunctioning());
        }

        protected virtual IEnumerator Malfunctioning()
        {
            while (malfunctioning)
            {
                if (Repairing)
                    repair_progress += Time.deltaTime * repair_rate;
                else
                    repair_progress -= Time.deltaTime * loss_rate;

                Debug.Log(repair_progress);

                if (repair_progress >= 1f)
                    FinishRepair();

                OnTickMalfunctioning.Invoke();

                yield return new WaitForEndOfFrame();
            }
        }
        public virtual void StartRepairing()
        {
            Debug.Log("Start Repairing");
            Repairing = true;
        }
        public virtual void StopRepairing()
        {
            Debug.Log("Stop Repairing");
            Repairing = false;
        }
        public virtual void FinishRepair()
        {
            malfunctioning = false;
            RepairManager.Instance.FixedMalfunction(this);
            WorldInteractions.Instance.Nearest_Repair = null;
            OnRepair.Invoke();
            Debug.Log("Repaired Oxygen");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (malfunctioning)
                WorldInteractions.Instance.Nearest_Repair = this;
        }
        private void OnTriggerExit(Collider other)
        {
            Repairing = false; //just in case
            if (WorldInteractions.Instance.Nearest_Repair == this)
                WorldInteractions.Instance.Nearest_Repair = null;
        }
        public enum ModuleType
        {
            OXYGEN,
            COMMS,
            POWER,
        }
    }
}