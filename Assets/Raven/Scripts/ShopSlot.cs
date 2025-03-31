using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Raven
{
    public class ShopSlot : MonoBehaviour
    {
        [SerializeField] Image itemIcon;
        [SerializeField] TMP_Text itemName;

        public void SetItem(Sprite icon, string name)
        {
            itemIcon.sprite = icon;
            itemName.text = name;
        }
    }
}
