using UnityEngine;
using HappyValley;
using UnityEngine.InputSystem.Switch;

namespace kristina
{
    public class PlantIncubator : MonoBehaviour
    {
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
            InputPlant("plant_one"); //TEMPORARY
            //TimeManager.TimeElapsed += AddToAge;
            //TimeManager.TimeElapsed += DebugTime;
        }
        void DebugTime(int tick)
        {
            Debug.Log(tick);
        }

        void InputPlant(string plantID)
        {
            if (currentPlant != null) return;
            
            currentPlant = PlantDatabase.instance.Plants[plantID];
            age = 0;
            stageAge = 0;
            currentStage = 0;
            TimeManager.TimeElapsed += AddToAge;

            glassCase.SetActive(true);
            plantSprite.gameObject.SetActive(true);
            plantSprite.sprite = currentPlant.sprites[currentStage];

        }
        void CollectPlant()
        {
            //TimeManager.TimeElapsed -= AddToAge;
            plantSprite.gameObject.SetActive(false);
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
        }

        private void OnDisable()
        {
            TimeManager.TimeElapsed -= AddToAge;
        }
    }
}