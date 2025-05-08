using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayButtonImage : MonoBehaviour
{
    public Sprite image;
    public Button button;

    public void ChangeButtonImage()
    {
        button.image.sprite = image;
    }
}
