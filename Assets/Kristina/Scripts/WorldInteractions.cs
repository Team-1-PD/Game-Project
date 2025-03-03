using UnityEngine;
using Raven;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using HappyValley;
using JetBrains.Annotations;

namespace kristina
{
    public class WorldInteractions : MonoBehaviour
    {
        public static WorldInteractions instance;

        [SerializeField] private UI_Hotbar hotbar;

        public BedActivate nearestBed;
        public PlantIncubator nearestIncubator;        

        bool placementActivated = false;

        Item currentItem = Item.None();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("initializing interactions");
            instance = this;

            HotbarSelector.ChangeSelectedItem += SelectItem;
            PlayerInput.Input.Player.Interact.performed += Interactions;
        }

        public void Interactions(InputAction.CallbackContext ctx)
        {
            Debug.Log("Interacting");

            if (InteractBed()) return; //try sleep first

            if (InteractPlacing()) return; //try place next

            if (InteractIncubator()) return; //try incubator last
        }

        #region interaction_types
        bool InteractBed() 
        {
            Debug.Log("try bed");
            return nearestBed != null && nearestBed.Sleep();
        }

        bool InteractPlacing()
        {
            Debug.Log("try place");
            if (placementActivated)
            {
                if (PlacementHandler.instance.TryPlace(currentItem.ID))
                    hotbar.RemoveItem(currentItem, 1);
                return true;
            }

            return false;
        }

        bool InteractIncubator()
        {
            Debug.Log("try incubator");
            if (nearestIncubator != null)
            {
                if (nearestIncubator.fullyGrown)
                {
                    //try pickup produce next
                    Plant plant = nearestIncubator.CollectPlant();

                    hotbar.AddItem(ItemDatabase.instance.Items[plant.produceID], plant.productionAmount);
                    return true;
                }
                else if (currentItem.itemType == Item.ItemType.Seed)
                {
                    //finally try inputting seeds into incubator
                    return nearestIncubator.InputPlant(currentItem.ID);
                }
            }
            return false;
        }
        #endregion
        public void SelectItem(string ID)
        {
            currentItem = ItemDatabase.instance.Items[ID];

            if (currentItem.itemType == Item.ItemType.Placeable)
            {
                PlacementSelected();
            }
            else
            {
                PlacementDeselected();
            }
        }
        private void PlacementSelected()
        {
            if (!placementActivated)
                SceneManager.LoadSceneAsync("GridSystem", LoadSceneMode.Additive);
            placementActivated = true;
        }
        private void PlacementDeselected()
        {
            if (placementActivated)
            {
                SceneManager.UnloadSceneAsync("GridSystem");
                placementActivated = false;
            }
        }
    }
}