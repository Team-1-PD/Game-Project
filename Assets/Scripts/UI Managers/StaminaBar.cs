using UnityEngine;
using UnityEngine.UI;

namespace HappyValley
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] Stamina playerStamina;
        [SerializeField] Image staminaBar;
        [SerializeField] int growth;

        void Start()
        {
            TimeManager.TimeElapsed += Growth;
        }

        void Update()
        {
            staminaBar.fillAmount = playerStamina.CurrentStamina / playerStamina.MaxStamina;
        }

        void Growth(int x)
        {
            growth += x;
        }
    }
}
