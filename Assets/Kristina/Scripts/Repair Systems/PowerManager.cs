using UnityEngine;

namespace kristina
{
    public class PowerManager : MonoBehaviour
    {
        public static bool power_to_base { get; private set; } = true;
        private static int amount_powered_down = 0;
        public void PowerOn(RepairableModule.ModuleType module)
        {
            if (module != RepairableModule.ModuleType.POWER) return;

            amount_powered_down--;
            if (amount_powered_down == 0)
                power_to_base = true;
        }
        public void PowerOff(RepairableModule.ModuleType module)
        {
            if (module != RepairableModule.ModuleType.POWER) return;
            amount_powered_down++;
            power_to_base = false;
        }
    }
}