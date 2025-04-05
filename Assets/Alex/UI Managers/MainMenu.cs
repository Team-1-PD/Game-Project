using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;


namespace HappyValley
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] TimeData timeData;
        [SerializeField] AudioClip navigateMenu;
        [SerializeField] AudioClip selectMenu;

        public GameObject newGameButton;

        void Start()
        {
           EventSystem.current.SetSelectedGameObject(newGameButton);
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundFXManager.instance.PlaySoundFXClip(navigateMenu, transform, .1f);
            }
        }
        public void NewGame()
        {
            SoundFXManager.instance.PlaySoundFXClip(selectMenu, transform, .1f);
            timeData.NewGame();
            SceneManager.LoadScene(2);
        }

        public void LoadGame()
        {
            SoundFXManager.instance.PlaySoundFXClip(selectMenu, transform, .1f);
            timeData.LoadGame();
            SceneManager.LoadScene(2);
        }

        public void QuitGame()
        {
            SoundFXManager.instance.PlaySoundFXClip(selectMenu, transform, .1f);
            EditorApplication.isPlaying = false;
            //Application.Quit();
        }
    }
}
