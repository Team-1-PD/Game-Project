using UnityEngine;
using Raven;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using HappyValley;

namespace kristina
{
    public class WorldInteractions : MonoBehaviour
    {
        public static WorldInteractions Instance { get; private set; }

        [SerializeField] private UI_Hotbar hotbar;
        

        public BedActivate Nearest_Bed;
        public PlantIncubator Nearest_Incubator;
        public RepairableModule Nearest_Repair;
        // nearestBox

        bool placement_activated = false;
        bool removal_activated = false;

        Item current_item = Item.None();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //Debug.Log("initializing interactions");
            Instance = this;

            HotbarSelector.ChangeSelectedItem += SelectItem;
            PlayerInput.Input.Player.Interact.performed += PrimaryInteractions;
            PlayerInput.Input.Player.Interact.canceled += PrimaryInteractions;

            PlayerInput.Input.Player.Attack.performed += SecondaryInteractions;
            PlayerInput.Input.Player.Attack.canceled += SecondaryInteractions;

        }
        public void PrimaryInteractions(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && !SceneManager.GetSceneByName("ShopTerminal").isLoaded)
            {
                if (InteractBed()) return; //try sleep first

                if (InteractPlacing()) return; //try place next

                if (InteractIncubator()) return; //try incubator last
            }
            //Debug.Log("Interacting");            
        }
        public void SecondaryInteractions(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (InteractRepair()) return; //try repairing first

                if (InteractRemovePlacement()) return; //try removing last
            }
            else if (ctx.canceled)
            {
                if (InteractCancelRepair()) return;
            }
        }

        #region Primary Interaction Types
        bool InteractBed() 
        {
            //Debug.Log("try bed");
            return Nearest_Bed != null && Nearest_Bed.Sleep();
        }

        bool InteractPlacing()
        {
            //Debug.Log("try place");
            if (placement_activated)
            {
                if (PlacementHandler.instance.TryPlace(current_item.ID))
                {
                    hotbar.RemoveItem(current_item, 1);
                }
                return true;
            }

            return false;
        }

        bool InteractIncubator()
        {
            //Debug.Log("try incubator");
            if (Nearest_Incubator != null)
            {
                if (Nearest_Incubator.fully_grown)
                {
                    //try pickup produce next
                    Plant plant = Nearest_Incubator.CollectPlant();

                    hotbar.AddItem(Database.ITEMS.Items[plant.Produce_ID], plant.Production_Amount);
                    return true;
                }
                else if (current_item.TYPE == Item.ItemType.Seed && Nearest_Incubator.InputPlant(current_item.ID))
                {
                    //finally try inputting seeds into incubator
                    hotbar.RemoveItem(current_item, 1);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Secondary Interaction Types
        public bool InteractRepair()
        {
            if (!Nearest_Repair) return false;

            Nearest_Repair.StartRepairing();
            return true;
        }
        public bool InteractCancelRepair()
        {
            if (!Nearest_Repair) return false;

            Nearest_Repair.StopRepairing();
            return true;
        }
        public bool InteractRemovePlacement()
        {
            if (removal_activated)
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
            current_item = Database.ITEMS.Items[ID];

            if (current_item.TYPE == Item.ItemType.Placeable)
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
            if (!placement_activated)
                PlacementHandler.instance.ActivateHighlighter();
            placement_activated = true;
            removal_activated = true;
        }
        private void PlacementDeselected()
        {
            if (placement_activated)
            {
                PlacementHandler.instance.DeactivateHighlighter();
                placement_activated = false;
                removal_activated= false;
            }
        }
        #endregion
    }
}