using UnityEngine;
using HappyValley;
using UnityEngine.Events;

namespace kristina
{
    public class PlantIncubator : MonoBehaviour
    {
        public UnityAction OnInputPlant, OnCollectPlant;
        public bool fullyGrown { get; protected set; } = false;

        //[SerializeField] PlantDatabase plantHandler;
        [SerializeField] protected SpriteRenderer plantSprite;

        [SerializeField] protected GameObject glassCase;
        
        protected float timeElapsed;

        protected int age = 0;
        protected int stageAge;
        protected int stageInterval => currentPlant.growDuration / (currentPlant.sprites.Length - 1);

        protected int currentStage = 0;
        protected Plant currentPlant;

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

            OnInputPlant?.Invoke();
            return true;
        }
        public Plant CollectPlant()
        {
            Plant returnPlant = currentPlant;
            fullyGrown = false;
            plantSprite.gameObject.SetActive(false);
            currentPlant = null;

            OnCollectPlant?.Invoke();
            return returnPlant;
        }

        protected virtual void AddToAge(int tick)
        {
            age += tick;
            Debug.Log("age: " + age);
            Debug.Log("interval: " + stageInterval);
            stageAge += tick;
            if (stageAge >= stageInterval)
            {
                IncrementStage();
            }
        }
        protected void IncrementStage()
        {
            Debug.Log("increment stage");
            currentStage++;
            stageAge = 0;

            plantSprite.sprite = currentPlant.sprites[currentStage];

            if (currentStage == currentPlant.sprites.Length - 1)
            {
                FinishGrowing();
            }
        }
        protected void FinishGrowing()
        {
            glassCase.SetActive(false);
            TimeManager.TimeElapsed -= AddToAge;
            fullyGrown = true;
        }

        protected void OnDisable()
        {
            TimeManager.TimeElapsed -= AddToAge;
        }

        protected void OnTriggerEnter(Collider other)
        {
            WorldInteractions.Instance.nearestIncubator = this;
        }
        protected void OnTriggerExit(Collider other)
        {
            WorldInteractions.Instance.nearestIncubator = null;
        }
    }
}