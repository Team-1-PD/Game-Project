using UnityEngine;
using Raven;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using HappyValley;

namespace kristina
{
    public class WorldInteractions : MonoBehaviour
    {
        public static WorldInteractions instance;

        [SerializeField] private UI_Hotbar hotbar;
        

        public BedActivate nearestBed;
        public PlantIncubator nearestIncubator;

        bool placementActivated = false;
        bool removalActivated = false;

        Item currentItem = Item.None();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //Debug.Log("initializing interactions");
            instance = this;

            HotbarSelector.ChangeSelectedItem += SelectItem;
            PlayerInput.Input.Player.Interact.performed += PrimaryInteractions;
            PlayerInput.Input.Player.Attack.performed += SecondaryInteractions;
        }
        public void PrimaryInteractions(InputAction.CallbackContext ctx)
        {
            //Debug.Log("Interacting");

            if (InteractBed()) return; //try sleep first

            if (InteractPlacing()) return; //try place next

            if (InteractIncubator()) return; //try incubator last
        }
        public void SecondaryInteractions(InputAction.CallbackContext ctx)
        {
            if (InteractRepair()) return; //try repairing first

            if (InteractRemovePlacement()) return; //try removing last
        }

        #region Primary Interaction Types
        bool InteractBed() 
        {
            //Debug.Log("try bed");
            return nearestBed != null && nearestBed.Sleep();
        }

        bool InteractPlacing()
        {
            //Debug.Log("try place");
            if (placementActivated)
            {
                if (PlacementHandler.instance.TryPlace(currentItem.ID))
                {
                    hotbar.RemoveItem(currentItem, 1);
                }
                return true;
            }

            return false;
        }

        bool InteractIncubator()
        {
            //Debug.Log("try incubator");
            if (nearestIncubator != null)
            {
                if (nearestIncubator.fullyGrown)
                {
                    //try pickup produce next
                    Plant plant = nearestIncubator.CollectPlant();

                    hotbar.AddItem(Database.ITEMS.Items[plant.produceID], plant.productionAmount);
                    return true;
                }
                else if (currentItem.itemType == Item.ItemType.Seed && nearestIncubator.InputPlant(currentItem.ID))
                {
                    //finally try inputting seeds into incubator
                    hotbar.RemoveItem(currentItem, 1);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Secondary Interaction Types
        public bool InteractRepair()
        {
            return false;
        }
        public bool InteractRemovePlacement()
        {
            if (removalActivated)
            {
                string id = PlacementHandler.instance.TryRemove();
                if (id == null) return false;

                hotbar.AddItem(Database.ITEMS.Items[id], 1);
                return true;
            }

            return false;
        }
        #endregion

        #region Change Selection
        public void SelectItem(string ID)
        {
            currentItem = Database.ITEMS.Items[ID];

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
                PlacementHandler.instance.ActivateHighlighter();
            placementActivated = true;
            removalActivated = true;
        }
        private void PlacementDeselected()
        {
            if (placementActivated)
            {
                PlacementHandler.instance.DeactivateHighlighter();
                placementActivated = false;
                removalActivated= false;
            }
        }
        #endregion
    }
}