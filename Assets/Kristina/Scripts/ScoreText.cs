using TMPro;
using UnityEngine;

namespace kristina
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] TMP_Text tmp_text;
        [SerializeField] ScoreKeeper score_keeper;
        private void Awake()
        {
            tmp_text.text = "SCORE " + score_keeper.score;
            score_keeper.ResetScore();
        }
    }
}