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

            TimeManager.OnWakeup += WakeUpPlayer;
        }

        public bool Sleep()
        {
            if (bedReady)
            {
                player.SetActive(false);
                sleepingPlayer?.SetActive(true);
                timeManager.Sleep();

                return true;
            }

            return false;
        }
        public void WakeUpPlayer()
        {
            player.SetActive(true);
            sleepingPlayer?.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldInteractions.instance.nearestBed = this;
            bedReady = true;
        }
        private void OnTriggerExit(Collider other)
        {
            WorldInteractions.instance.nearestBed = null;
            bedReady = false;
        }
    }
}