using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;


namespace HappyValley
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] TimeData timeData;

        public GameObject newGameButton;

        void Start()
        {
           EventSystem.current.SetSelectedGameObject(newGameButton);
        }

        public void NewGame()
        {
            timeData.NewGame();
            SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {
            timeData.LoadGame();
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            //EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
