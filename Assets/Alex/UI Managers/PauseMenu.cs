using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace HappyValley
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] GameObject UI;
        [SerializeField] GameObject resumeButton;
        [SerializeField] GameObject timeButton;
        [SerializeField] UnityEvent stopPause;

        public static bool GameIsPaused = false;
        private InputSystem_Actions input;
        private bool timeManagerOpen;

        void Awake()
        {
            input = new InputSystem_Actions();
            input.Player.Enable();
        }

        void Update()
        {
            input.Player.Pause.performed += ctx =>
            {
                if(!timeManagerOpen)
                {
                    if (!GameIsPaused)
                    {
                        Pause();
                    }
                    else
                    {
                        Resume();
                    }
                }
            };
        }

        public void Resume()
        {
            if (GameIsPaused)
            {
                stopPause?.Invoke();
                pauseMenuUI.SetActive(false);
                UI.SetActive(true);
                timeButton.SetActive(true);
                Time.timeScale = 1f;
                GameIsPaused = false;
            }
        }

        void Pause()
        {
            if (!timeManagerOpen)
            {
                if (!GameIsPaused)
                {
                    stopPause?.Invoke();
                    pauseMenuUI.SetActive(true);
                    UI.SetActive(false);
                    timeButton.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(resumeButton);
                    Time.timeScale = 0f;
                    GameIsPaused = true;
                }

            }
             
        }

        public void TimeManagerOpen()
        {
            timeManagerOpen = !timeManagerOpen;
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

