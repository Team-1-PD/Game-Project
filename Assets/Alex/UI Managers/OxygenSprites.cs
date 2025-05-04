using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HappyValley
{
    public class OxygenSprites : MonoBehaviour
    {
        [SerializeField] Image progress_bar;
        [SerializeField] Image cardboard;
        [SerializeField] Image pin;

        [SerializeField] Sprite bar100;
        [SerializeField] Sprite bar50;
        [SerializeField] Sprite bar25;

        [SerializeField] Sprite cardboard100;
        [SerializeField] Sprite cardboard50;
        [SerializeField] Sprite cardboard25;

        [SerializeField] Sprite pin100;
        [SerializeField] Sprite pin50;
        [SerializeField] Sprite pin25;

        [SerializeField] TextMeshProUGUI percentage;

        void Update()
        {
            percentage.text = ((int)(progress_bar.fillAmount * 100)).ToString();

            if (progress_bar.fillAmount > .5f)
            {
                progress_bar.sprite = bar100;
                cardboard.sprite = cardboard100;
                pin.sprite = pin100;
            }
            else if (progress_bar.fillAmount <= .5f && progress_bar.fillAmount > .30f)
            {
                progress_bar.sprite = bar50;
                cardboard.sprite = cardboard50;
                pin.sprite = pin50;
            }
            else if (progress_bar.fillAmount <= .30f && progress_bar.fillAmount >= 0f)
            {
                progress_bar.sprite = bar25;
                cardboard.sprite = cardboard25;
                pin.sprite = pin25;
            }
        }
    }
}