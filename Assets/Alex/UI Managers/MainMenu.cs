using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


namespace HappyValley
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject newGameButton;

        void Start()
        {
           EventSystem.current.SetSelectedGameObject(newGameButton);
        }

        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {
            Debug.Log("Loading game...");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
