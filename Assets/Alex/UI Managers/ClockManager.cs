using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HappyValley
{
    public class ClockManager : MonoBehaviour
    {
        public TextMeshProUGUI Date, Time, Season, Week;

        private float startingRotation;

        public Light sunlight;
        public float nightIntensity;
        public float dayIntensity;

        public AnimationCurve dayNightCurve;

        private void OnEnable()
        {
            TimeManager.OnDateTimeChanged += UpdateDateTime;
        }

        private void OnDisable()
        {
            TimeManager.OnDateTimeChanged -= UpdateDateTime;
        }

        private void UpdateDateTime(DateTime dateTime)
        {
            Date.text = dateTime.DateToString();
            Time.text = dateTime.TimeToString();
            Season.text = dateTime.Season.ToString();
            Week.text = $"WK: {dateTime.CurrentWeek}";

            float t = (float)dateTime.Hour / 24f;

            float newRotation = Mathf.Lerp(0, 360, t);

            float dayNightT = dayNightCurve.Evaluate(t);

            sunlight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, dayNightT);
        }
    }
}


