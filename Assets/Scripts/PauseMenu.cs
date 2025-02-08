using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace HappyValley
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject UI;
        public GameObject resumeButton;
        private InputSystem_Actions input;


        void Awake()
        {
            input = new InputSystem_Actions();
            input.Player.Enable();
        }

        void Update()
        {
            input.Player.Pause.performed += ctx =>
            {
                if (!GameIsPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            };
        }

        public void Resume()
        {
            if (GameIsPaused)
            {
                pauseMenuUI.SetActive(false);
                UI.SetActive(true);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
        }

        void Pause()
        {
            if(!GameIsPaused)
            {
                pauseMenuUI.SetActive(true);
                UI.SetActive(false);
                EventSystem.current.SetSelectedGameObject(resumeButton);
                Time.timeScale = 0f;
                GameIsPaused = true;
            }
        }

        public void LoadMenu()
        {
            Debug.Log("Loading menu...");
        }

        public void SaveGame()
        {
            Debug.Log("Saving game...");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

