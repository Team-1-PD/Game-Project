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
                    repair_progress += Time.deltaTime * repair_rate;
                else
                    repair_progress -= Time.deltaTime * loss_rate;

                Debug.Log(repair_progress);

                if (repair_progress >= 1f)
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