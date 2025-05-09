using TMPro;
using UnityEngine;

namespace kristina
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] TMP_Text text_asset;
        [SerializeField] Player player;

        // Update is called once per frame
        void Update()
        {
            text_asset.text = player.getBank.ToString("D5");
        }
    }
}