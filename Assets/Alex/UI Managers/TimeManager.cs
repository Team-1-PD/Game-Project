using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Rendering.LookDev;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace HappyValley
{
    public class TimeManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] GameObject player;
        [SerializeField] GameObject playerAsleep;
        [SerializeField] Stamina stamina;
        [SerializeField] GameObject timeMenuUI;
        [SerializeField] UnityEvent timeMenuON;

        [Header("Date & Time Settings")]
        [Range(1, 28)]
        public int dateInMonth;
        [Range(1, 4)]
        public int season;
        [Range(1, 99)]
        public int year;
        [Range(0, 24)]
        public int hour;
        [Range(0, 6)]
        public int minutes;

        private DateTime DateTime;

        [Header("Tick Settings")]
        public int TickMinutesIncreased = 10;
        public float TimeBetweenTicks = 1;
        private float currentTimeBetweenTicks = 0;

        private int tTickMinutesIncreased;
        private float tTimeBetweenTicks;

        public static UnityAction<DateTime> OnDateTimeChanged;
        public static UnityAction<int> TimeElapsed;

        private InputSystem_Actions input;
        private bool MenuActive = false;

        public CanvasGroup blackScreen;
        private bool freezeClock;
        private bool fadeIn;
        private bool fadeOut;

        private bool sleeping;
        private static bool twoAM;
        private static bool fullRestore;
        private static int timeElapsed = 0;
        private bool paused;
        #endregion

        #region Start, Update, etc
        private void Awake()
        {
            input = new InputSystem_Actions();
            input.Player.Enable();

            DateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);
        }

        private void Start()
        {
            blackScreen.alpha = 0;

            OnDateTimeChanged?.Invoke(DateTime);
            InvokeRepeating("PassTimeElapsed", 0, 1f);
            InvokeRepeating("ForwardElapsedTime", 0, 1f);
        }

        private void Update()
        {
            currentTimeBetweenTicks += Time.deltaTime;

            if (currentTimeBetweenTicks >= TimeBetweenTicks)
            {
                currentTimeBetweenTicks = 0;
                if(!freezeClock)
                {
                    Tick();
                }
            }

            if (fadeIn)
            {
                if (blackScreen.alpha < 1)
                {
                    blackScreen.alpha += Time.deltaTime;
                    if (blackScreen.alpha >= 1)
                    {
                        fadeIn = false;
                    }
                }
            }

            if (fadeOut)
            {
                if (blackScreen.alpha >= 0)
                {
                    blackScreen.alpha -= Time.deltaTime;
                    if (blackScreen.alpha == 0)
                    {
                        fadeOut = false;
                    }
                }
            }

            if(twoAM)
            {
                PlayerFaint();
            }

            input.Player.Time.performed += ctx =>
            {
                if(!paused)
                {
                    TimeSettings();
                }
            };
        }
        #endregion

        #region Clock
        void PassTimeElapsed()
        {
            TimeElapsed?.Invoke(1);
        }

        private void Tick()
        {
            AdvanceTime();
        }

        private void AdvanceTime()
        {
            DateTime.AdvanceMinutes(TickMinutesIncreased);

            OnDateTimeChanged?.Invoke(DateTime);
        }
        #endregion

        #region Time Editing
        public void TimeSettings()
        {
            if(!MenuActive)
            {
                timeMenuON?.Invoke();
                timeMenuUI.SetActive(true);
                Time.timeScale = 0f;
                MenuActive = true;
            }
            else
            {
                timeMenuON?.Invoke();
                timeMenuUI.SetActive(false);
                Time.timeScale = 1f;
                MenuActive = false;
            }
        }

        public void ChangeTickMinutesIncreased(string x)
        {
            TickMinutesIncreased = int.Parse(x);
        }

        public void ChangeTimeBetweenTicks(string y)
        {
            TimeBetweenTicks = int.Parse(y);
        }

        public void FastForwardTime(string z)
        {
            tTickMinutesIncreased = TickMinutesIncreased;
            tTimeBetweenTicks = TimeBetweenTicks;

            TickMinutesIncreased = 60;
            TimeBetweenTicks = 1;
            TimeSettings();

            Time.timeScale = 10f;
            StartCoroutine(StopFastForward(int.Parse(z)));
        }

        private IEnumerator StopFastForward(int a)
        {
            yield return new WaitForSeconds(a);
            Time.timeScale = 1f;
            TickMinutesIncreased = tTickMinutesIncreased;
            TimeBetweenTicks = tTimeBetweenTicks;
        }

        public void ForwardElapsedTime()
        {
            SetTimeElapsed(TickMinutesIncreased);
        }

        public static void SetTimeElapsed(int x)
        {
            timeElapsed += x;
            //Debug.Log("Timer: " + timeElapsed);
        }

        public static int GetTimeElapsed()
        {
            return timeElapsed;
        }

        public void Sleep()
        {
            sleeping = true;

            player.SetActive(false);
            playerAsleep.SetActive(true);
            timeMenuUI.SetActive(false);

            FadeIn();
            freezeClock = true;
            StartCoroutine(nextDay());
        }

        private IEnumerator nextDay()
        {
            yield return new WaitForSeconds(2);
            DateTime.Sleep();
            RestoreStamina();
            OnDateTimeChanged?.Invoke(DateTime);
            StartCoroutine(WakeUp());
        }

        private IEnumerator WakeUp()
        {
            yield return new WaitForSeconds(2);
            player.SetActive(true);
            playerAsleep.SetActive(false);
            FadeOut();
            sleeping = false;
            StartCoroutine(reactivateClock());
        }

        private IEnumerator reactivateClock()
        {
            yield return new WaitForSeconds(2);
            freezeClock = false;
        }
        #endregion

        #region Other
        private void FadeIn()
        {
            fadeIn = true;
        }

        private void FadeOut()
        {
            fadeOut = true;
        }

        public static void IsTwoAM()
        {
            twoAM = true;
        }

        public static void IsNotTwoAM()
        {
            twoAM = false;
        }

        public void PlayerFaint()
        {
            if (!sleeping)
            {
                Debug.Log("Faint");
                Sleep();
            }
        }

        public static void FullRestoreStamina()
        {
            fullRestore = true;
        }

        public static void PartialRestoreStamina()
        {
            fullRestore = false;
        }

        private void RestoreStamina()
        {
            if(fullRestore)
            {
                stamina.FullyRestoreStamina();
            }
            else
            {
                stamina.PartiallyRestoreStamina();
            }
        }

        public void Paused()
        {
            paused = !paused;
        }
        #endregion
    }

    [System.Serializable]
    public struct DateTime
    {
        #region Fields
        public Days day;
        public int date;
        public int year;

        public int hour;
        public int minutes;

        public Season season;

        public int totalNumDays;
        public int totalNumWeeks;
        #endregion

        #region Properties
        public Days Day => day;
        public int Date => date;
        public int Hour => hour;
        public int Minutes => minutes;
        public Season Season => season;
        public int Year => year;
        public int TotalNumDays => totalNumDays;
        public int TotalNumWeeks => totalNumWeeks;
        public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;
        #endregion

        #region Constructors

        public DateTime(int date, int season, int year, int hour, int minutes)
        {
            this.day = (Days)(date % 7);
            if (day == 0) day = (Days)7;
            this.date = date;
            this.season = (Season)season;
            this.year = year;

            this.hour = hour;
            this.minutes = minutes;


            totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

            totalNumWeeks = 1 + totalNumDays / 7;
        }

        #endregion

        #region Time Advancement

        public void AdvanceMinutes(int SecondsToAdvanceBy)
        {
            if (minutes + SecondsToAdvanceBy >= 60)
            {
                minutes = (minutes + SecondsToAdvanceBy) % 60;
                AdvanceHour();
            }
            else
            {
                minutes += SecondsToAdvanceBy;
            }
        }

        private void AdvanceHour()
        {
            if ((hour + 1) == 24)
            {
                hour = 0;
                AdvanceDay();
            }
            else
            {
                hour++;
            }

            if(hour == 2)
            {
                TimeManager.IsTwoAM();
            }
            else
            {
                TimeManager.IsNotTwoAM();
            }
        }

        private void AdvanceDay()
        {
            day++;

            if (day > (Days)7)
            {
                day = (Days)1;
                totalNumWeeks++;
            }

            date++;

            if (date % 29 == 0)
            {
                AdvanceSeason();
                date = 1;
            }

            totalNumDays++;

        }

        private void AdvanceSeason()
        {
            if (Season == Season.Winter)
            {
                season = Season.Spring;
                AdvanceYear();
            }
            else season++;
        }

        private void AdvanceYear()
        {
            date = 1;
            year++;
        }

        public void Sleep()
        {
            if(hour > 2 && Hour < 24)
            {
                TimeManager.FullRestoreStamina();
            }
            else
            {
                TimeManager.PartialRestoreStamina();
            }

            for (int i = 1440; i > 0; i--)
            {
                AdvanceMinutes(1);
                TimeManager.SetTimeElapsed(1);
                if (hour == 6 && minutes == 0)
                {
                    i = 0;
                }
            }
        }
        #endregion

        #region Bool Checks
        public bool IsNight()
        {
            return hour > 18 || hour < 6;
        }

        public bool IsMorning()
        {
            return hour >= 6 && hour <= 12;
        }

        public bool IsAfternoon()
        {
            return hour > 12 && hour < 18;
        }

        public bool IsWeekend()
        {
            return day > Days.Fri ? true : false;
        }

        public bool IsParticularDay(Days _day)
        {
            return day == _day;
        }
        #endregion

        #region Key Dates

        public DateTime NewYearsDay(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(1, 0, year, 6, 0);
        }

        public DateTime SummerSolstice(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(28, 1, year, 6, 0);
        }
        public DateTime PumpkinHarvest(int year)
        {
            if (year == 0) year = 1;
            return new DateTime(28, 2, year, 6, 0);
        }

        #endregion

        #region Start Of Season

        public DateTime StartOfSeason(int season, int year)
        {
            season = Mathf.Clamp(season, 0, 3);
            if (year == 0) year = 1;

            return new DateTime(1, season, year, 6, 0);
        }

        public DateTime StartOfSpring(int year)
        {
            return StartOfSeason(0, year);
        }

        public DateTime StartOfSummer(int year)
        {
            return StartOfSeason(1, year);
        }

        public DateTime StartOfAutumn(int year)
        {
            return StartOfSeason(2, year);
        }

        public DateTime StartOfWinter(int year)
        {
            return StartOfSeason(3, year);
        }

        #endregion

        #region To Strings

        public override string ToString()
        {
            return $"Date: {DateToString()} Season: {season} Time: {TimeToString()} " +
                $"\nTotal Days: {totalNumDays} | Total Weeks: {totalNumWeeks}";
        }
        public string DateToString()
        {
            return $"{Day} {Date} {Year.ToString("D2")}";
        }

        public string TimeToString()
        {
            int adjustedHour = 0;

            if (hour == 0)
            {
                adjustedHour = 12;
            }
            else if (hour >= 13)
            {
                adjustedHour = hour - 12;
            }
            else
            {
                adjustedHour = hour;
            }

            string AmPm = hour < 12 ? "AM" : "PM";

            return $"{adjustedHour.ToString("D2")}:{minutes.ToString("D2")} {AmPm}";
        }

        #endregion
    }

    [System.Serializable]
    public enum Days
    {
        NULL = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 5,
        Sat = 6,
        Sun = 7
    }

    [System.Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autumn = 2,
        Winter = 3
    }
}