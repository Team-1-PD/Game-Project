using UnityEngine;

namespace kristina
{
    [CreateAssetMenu(menuName = "Score")]
    public class ScoreKeeper : ScriptableObject
    {
        [field: SerializeField] public int score { get; private set; }
        public void AddToScore(int to_add)
        {
            score += to_add;
        }

        public void GameEnd()
        {
            score += FindFirstObjectByType<Player>().getBank * 10;
            score += HappyValley.TimeManager.GetTimeElapsed() / 100;
        }

        public void ResetScore() { score = 0; }
    }
}