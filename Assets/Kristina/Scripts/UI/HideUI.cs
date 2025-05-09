using UnityEngine;
using UnityEngine.InputSystem;

public class HideUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool ui_hidden = false;

    private void Awake()
    {
        PlayerInput.Input.Hotbar.HideUI.performed += ToggleHideUI;
    }
    private void OnDestroy()
    {
        PlayerInput.Input.Hotbar.HideUI.performed -= ToggleHideUI;
    }

    public void ToggleHideUI(InputAction.CallbackContext ctx)
    {
        if (ui_hidden)
        {
            animator.SetTrigger("SlideIn");
            ui_hidden = false;
        }
        else
        {
            animator.SetTrigger("SlideOut");
            ui_hidden = true;
        }
    }
}
