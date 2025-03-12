using UnityEngine;
using HappyValley;

namespace kristina
{
    public class PlantIncubator : MonoBehaviour
    {
        public bool fullyGrown { get; private set; } = false;

        //[SerializeField] PlantDatabase plantHandler;
        [SerializeField] SpriteRenderer plantSprite;

        [SerializeField] GameObject glassCase;
        
        float timeElapsed;

        int age = 0;
        int stageAge;
        int stageInterval => currentPlant.growDuration / currentPlant.sprites.Length - 1;

        int currentStage = 0;


        Plant currentPlant;

        void Start()
        {
            //InputPlant("plant_one"); //TEMPORARY
            //TimeManager.TimeElapsed += AddToAge;
            //TimeManager.TimeElapsed += DebugTime;
        }
        void DebugTime(int tick)
        {
            Debug.Log(tick);
        }

        public bool InputPlant(string plantID)
        {
            if (currentPlant != null) return false;
            
            currentPlant = Database.PLANTS.Plants[plantID];
            age = 0;
            stageAge = 0;
            currentStage = 0;
            TimeManager.TimeElapsed += AddToAge;

            glassCase.SetActive(true);
            plantSprite.gameObject.SetActive(true);
            plantSprite.sprite = currentPlant.sprites[currentStage];

            return true;
        }
        public Plant CollectPlant()
        {
            Plant returnPlant = currentPlant;
            fullyGrown = false;
            plantSprite.gameObject.SetActive(false);
            currentPlant = null;

            return returnPlant;
        }

        void AddToAge(int tick)
        {
            age += tick;
            stageAge += tick;
            if (stageAge >= stageInterval)
            {
                IncrementStage();
            }
        }
        void IncrementStage()
        {
            currentStage++;
            stageAge = 0;

            plantSprite.sprite = currentPlant.sprites[currentStage];

            if (currentStage == currentPlant.sprites.Length - 1)
            {
                FinishGrowing();
            }
        }
        void FinishGrowing()
        {
            glassCase.SetActive(false);
            TimeManager.TimeElapsed -= AddToAge;
            fullyGrown = true;
        }

        private void OnDisable()
        {
            TimeManager.TimeElapsed -= AddToAge;
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldInteractions.Instance.nearestIncubator = this;
        }
        private void OnTriggerExit(Collider other)
        {
            WorldInteractions.Instance.nearestIncubator = null;
        }
    }
}