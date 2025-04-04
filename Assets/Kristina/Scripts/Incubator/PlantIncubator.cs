using HappyValley;
using UnityEngine;
using UnityEngine.Events;

namespace kristina
{
    public class PlantIncubator : MonoBehaviour
    {
        private IncubatorInteractions interactions;

        public UnityAction OnInputPlant, OnCollectPlant;
        public bool fully_grown { get; protected set; } = false;

        //[SerializeField] PlantDatabase plantHandler;
        [SerializeField] protected SpriteRenderer plant_sprite;

        [SerializeField] protected GameObject glass_case;
        
        protected float time_elapsed;

        protected int age = 0;
        protected int stage_age;
        protected int stage_interval => current_plant.Grow_Duration / (current_plant.Sprites.Length - 1);

        protected int current_stage = 0;
        protected Plant current_plant;

        public bool InputPlant(string plantID)
        {
            if (current_plant != null) return false;
            
            current_plant = Database.PLANTS.Plants[plantID];
            age = 0;
            stage_age = 0;
            current_stage = 0;
            TimeManager.TimeElapsed += AddToAge;

            glass_case.SetActive(true);
            plant_sprite.gameObject.SetActive(true);
            plant_sprite.sprite = current_plant.Sprites[current_stage];

            OnInputPlant?.Invoke();
            return true;
        }
        public Plant CollectPlant()
        {
            Plant return_plant = current_plant;
            fully_grown = false;
            plant_sprite.gameObject.SetActive(false);
            current_plant = null;

            OnCollectPlant?.Invoke();
            return return_plant;
        }

        protected virtual void AddToAge(int tick)
        {
            if (!PowerManager.power_to_base) return; //no growth w/o power

            age += tick;
            Debug.Log($"age: {age}");
            Debug.Log($"interval: {stage_interval}");
            stage_age += tick;
            if (stage_age >= stage_interval)
            {
                IncrementStage();
            }
        }
        protected void IncrementStage()
        {
            Debug.Log("increment stage");
            current_stage++;
            stage_age = 0;

            plant_sprite.sprite = current_plant.Sprites[current_stage];

            if (current_stage == current_plant.Sprites.Length - 1)
            {
                FinishGrowing();
            }
        }
        protected void FinishGrowing()
        {
            glass_case.SetActive(false);
            TimeManager.TimeElapsed -= AddToAge;
            fully_grown = true;
        }
        private void OnEnable()
        {
            interactions = FindFirstObjectByType<IncubatorInteractions>();
        }

        private void OnDisable()
        {
            interactions.RemoveNearbyIncubator(this);
            TimeManager.TimeElapsed -= AddToAge;
        }

        private void OnTriggerEnter(Collider other)
        {
            interactions.AddNearbyIncubator(this);
        }
        private void OnTriggerExit(Collider other)
        {
            interactions.RemoveNearbyIncubator(this);
        }
    }
}