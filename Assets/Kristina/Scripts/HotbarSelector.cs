using Raven;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace kristina
{
    public class HotbarSelector : MonoBehaviour
    {
        public static UnityEvent<string> ChangeSelectedItem;
        //public static string currentItemID { get; private set; }
        int currentSlotIndex;

        //RectTransform rect;
        Transform selectedItem;

        [SerializeField] UI_Hotbar hotbar;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //rect = GetComponent<RectTransform>();

            PlayerInput.Input.Hotbar.select_slot.performed += KeyPressToSlot;

            PlayerInput.Input.Hotbar.Enable();

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
            Debug.Log("Selected slot " + index);

            if (index >= UI_Hotbar.HOTBAR_SIZE || index < 0 || index == currentSlotIndex)
                return currentSlotIndex;

            currentSlotIndex = index;


            selectedItem = hotbar.GetItemSlotAt(index).transform;
            transform.position = selectedItem.position;

            ChangeSelectedItem.Invoke(hotbar.GetItemAt(index).ID);

            return index;
        }

        public void KeyPressToSlot(InputAction.CallbackContext ctx)
        {
            int slot = (int)ctx.ReadValue<float>();
            SelectSlot(slot);
        }
    }
}