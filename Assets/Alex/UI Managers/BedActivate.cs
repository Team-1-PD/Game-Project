using UnityEngine;
using UnityEngine.Events;
using kristina;
using System.Collections;

namespace HappyValley
{
    public class BedActivate : MonoBehaviour
    {
        TimeManager timeManager;
        GameObject player;
        [SerializeField] GameObject sleepingPlayer;
        //[SerializeField] UnityEvent OnSleep;

        private bool bedReady = true;

        private void Start()
        {
            timeManager = FindFirstObjectByType<TimeManager>();
            player = FindFirstObjectByType<Player>().gameObject;

            
        }

        public void Sleep()
        {
            if (bedReady)
            {
                bedReady = false;
                player.SetActive(false);
                sleepingPlayer.SetActive(true);
                timeManager.Sleep();
                TimeManager.OnWakeup += WakeUpPlayer;
            }
        }
        public void WakeUpPlayer()
        {
            player.SetActive(true);
            sleepingPlayer.SetActive(false);
            TimeManager.OnWakeup -= WakeUpPlayer;
            StartCoroutine(WaitToAllowSleep());
        }
        
        private IEnumerator WaitToAllowSleep()
        {
            yield return new WaitForSeconds(5);
            bedReady = true;
        }
    }
}