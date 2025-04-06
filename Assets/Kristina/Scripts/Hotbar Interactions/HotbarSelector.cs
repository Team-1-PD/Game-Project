using Raven;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace kristina
{
    public class HotbarSelector : MonoBehaviour
    {
        bool currentlyScrolling = false;
        [SerializeField] float gamepadScrollRate = .25f;

        public static UnityAction<string> ChangeSelectedItem;
        //public static string currentItemID { get; private set; }
        public int currentSlotIndex { get; private set; } = -1;

        //RectTransform rect;
        Transform selectedItem;

        [SerializeField] UI_Hotbar hotbar;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //rect = GetComponent<RectTransform>();

            PlayerInput.Input.Hotbar.SelectSlot.performed += KeyPressToSlot;
            PlayerInput.Input.Hotbar.ScrollSlot.performed += ScrollToSlot;
            PlayerInput.Input.Hotbar.ScrollSlotHold.performed += HoldScroll;
            PlayerInput.Input.Hotbar.ScrollSlotHold.canceled += HoldScroll;

            PlayerInput.Input.Hotbar.Enable();

            // Removed so that the selector didnt appear at random position on screen until item selected
            SelectSlot(0);

            //StartCoroutine(DefaultSlot());
        }

        public IEnumerator DefaultSlot()
        {
            yield return new WaitForEndOfFrame();
            SelectSlot(0);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public int SelectSlot(int index)
        {
            //Debug.Log("Selected slot " + index);

            if (index >= UI_Hotbar.HOTBAR_SIZE || index < 0)
                return currentSlotIndex;

            currentSlotIndex = index;


            selectedItem = hotbar.GetItemSlotAt(index).transform;
            transform.position = selectedItem.position;

            Item item = hotbar.GetItemAt(index);
            if (item == null)
                ChangeSelectedItem?.Invoke("none");
            else
                ChangeSelectedItem?.Invoke(item.ID);

            return index;
        }
        public void ScrollToSlot(InputAction.CallbackContext ctx)
        {
            int index = currentSlotIndex;
            Vector2 direction = ctx.ReadValue<Vector2>();

            if (direction.y > 0)
            {
                index++;
                if (index >= UI_Hotbar.HOTBAR_SIZE)
                {
                    index = 0;
                }
                //Debug.Log("scroll right");
            }
            if (direction.y < 0)
            {
                index--;
                if (index < 0)
                {
                    index = UI_Hotbar.HOTBAR_SIZE - 1;
                }
                //Debug.Log("scroll left");
            }

            SelectSlot(index);
            //Debug.Log(direction);
        }
        public void HoldScroll(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                currentlyScrolling = true;
                StartCoroutine(Scrolling(ctx));
            }
            if (ctx.canceled)
            {
                currentlyScrolling = false;
            }
        }

        private IEnumerator Scrolling(InputAction.CallbackContext ctx)
        {
            while (currentlyScrolling)
            {
                ScrollToSlot(ctx);
                yield return new WaitForSeconds(gamepadScrollRate);
            }
        }

        public void KeyPressToSlot(InputAction.CallbackContext ctx)
        {
            int slot = (int)ctx.ReadValue<float>();
            SelectSlot(slot);
        }
    }
}