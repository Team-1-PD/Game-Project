using UnityEngine;
using UnityEngine.SceneManagement;

namespace kristina
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] string scene_name;
        public void LoadAdditiveScene()
        {
            SceneManager.LoadScene(scene_name, LoadSceneMode.Additive);
        }
        public void UnloadAdditiveScene()
        {
            SceneManager.UnloadSceneAsync(scene_name);
        }
        public void LoadScene()
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}