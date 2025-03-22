using System.Collections;
using UnityEngine;

namespace kristina
{
    public abstract class RepairableModule : MonoBehaviour
    {
        [SerializeField] protected float repairRate = 1.0f;
        [SerializeField] protected float lossRate = 1.0f;

        protected float repairProgress { 
            get { return privateRepairProgress; } 
            set { privateRepairProgress = (value > 0) ? value : 0f; } }
        private float privateRepairProgress = 0f;

        protected bool malfunctioning = false;
        public bool Repairing { get; protected set; } = false;
        [field: SerializeField] public ModuleType moduleType { get; protected set; }
        public virtual void StartMalfunctioning()
        {
            repairProgress = 0f;
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
            WorldInteractions.Instance.nearestRepair = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (malfunctioning)
                WorldInteractions.Instance.nearestRepair = this;
        }
        private void OnTriggerExit(Collider other)
        {
            Repairing = false; //just in case
            if (WorldInteractions.Instance.nearestRepair == this)
                WorldInteractions.Instance.nearestRepair = null;
        }
        public enum ModuleType
        {
            OXYGEN,
            COMMS,
            POWER,
        }
    }
}