using System.Collections;
using UnityEngine;

namespace kristina
{
    public class OxygenRepairable : RepairableModule
    {
        public override void StartMalfunctioning()
        {
            Debug.Log("OXYGEN MALFUNCTION");
            base.StartMalfunctioning();
        }
        protected override IEnumerator Malfunctioning()
        {
            while (malfunctioning)
            {
                if (Repairing)
                    repairProgress += Time.deltaTime * repairRate;
                else
                    repairProgress -= Time.deltaTime * lossRate;

                if (repairProgress >= 1f)
                    FinishRepair();

                //--- where we do the oxygen reduction ---


                yield return new WaitForEndOfFrame();
            }
        }
        public override void StartRepairing()
        {
            Debug.Log("Start Repairing");
            Repairing = true;
        }
        public override void StopRepairing()
        {
            Debug.Log("Stop Repairing");
            Repairing= false;
        }
        public override void FinishRepair()
        {
            Debug.Log("Repaired Oxygen");
            base.FinishRepair();
        }
    }
}