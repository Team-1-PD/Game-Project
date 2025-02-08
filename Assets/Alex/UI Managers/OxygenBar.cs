using UnityEngine;
using UnityEngine.UI;

namespace HappyValley
{
    public class OxygenBar : MonoBehaviour
    {
        [SerializeField] Oxygen playerOxygen;
        [SerializeField] Image oxygenBar;

        void Start()
        {
            InvokeRepeating("DepleteOxygen", 0, 1f);
        }

        void Update()
        {
            oxygenBar.fillAmount = playerOxygen.CurrentOxygen / playerOxygen.MaxOxygen;
        }

        void DepleteOxygen()
        {
            playerOxygen.CurrentOxygen -= 1;
        }
    }
}
