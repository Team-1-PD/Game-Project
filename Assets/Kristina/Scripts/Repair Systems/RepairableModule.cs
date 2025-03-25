using System.Collections;
using UnityEngine;

namespace kristina
{
    public abstract class RepairableModule : MonoBehaviour
    {
        [SerializeField] protected float repair_rate = 1.0f;
        [SerializeField] protected float loss_rate = 1.0f;

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
            StartCoroutine(Malfunctioning());
        }

        protected abstract IEnumerator Malfunctioning();
        public abstract void StartRepairing();
        public abstract void StopRepairing();
        public virtual void FinishRepair()
        {
            malfunctioning = false;
            RepairManager.Instance.FixedMalfunction(this);
            WorldInteractions.Instance.Nearest_Repair = null;
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