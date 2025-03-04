using JetBrains.Annotations;
using UnityEngine;

namespace HappyValley
{
    [CreateAssetMenu(menuName = "TimeData")]
    public class TimeData : ScriptableObject
    {
        private bool loadSave;

        public bool Save
        {
            get { return loadSave; }
        }

        public void NewGame()
        {
            loadSave = false;

        }

        public void LoadGame()
        {
            loadSave = true;
        }
    }
}