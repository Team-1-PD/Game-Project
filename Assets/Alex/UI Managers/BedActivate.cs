using UnityEngine;
using UnityEngine.Events;
using kristina;

namespace HappyValley
{
    public class BedActivate : MonoBehaviour
    {
        TimeManager timeManager;
        GameObject player;
        [SerializeField] GameObject sleepingPlayer;
        //[SerializeField] UnityEvent OnSleep;

        private bool bedReady;

        private void Start()
        {
            timeManager = FindFirstObjectByType<TimeManager>();
            player = FindFirstObjectByType<Player>().gameObject;

            
        }

        public bool Sleep()
        {
            if (bedReady)
            {
                player.SetActive(false);
                sleepingPlayer.SetActive(true);
                timeManager.Sleep();
                TimeManager.OnWakeup += WakeUpPlayer;
                return true;
            }

            return false;
        }
        public void WakeUpPlayer()
        {
            player.SetActive(true);
            sleepingPlayer.SetActive(false);
            TimeManager.OnWakeup -= WakeUpPlayer;
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldInteractions.Instance.nearestBed = this;
            bedReady = true;
        }
        private void OnTriggerExit(Collider other)
        {
            WorldInteractions.Instance.nearestBed = null;
            bedReady = false;
        }
    }
}