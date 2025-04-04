using UnityEngine;
using UnityEngine.UI;

namespace kristina
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] Image progress_bar;

        public void UpdateProgressBar(float current_amount, float max)
        {
            progress_bar.fillAmount = current_amount / max;
        }
    }
}