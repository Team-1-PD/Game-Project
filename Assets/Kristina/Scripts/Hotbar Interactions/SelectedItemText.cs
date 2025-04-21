using System.Collections;
using TMPro;
using UnityEngine;

namespace kristina
{
    public class SelectedItemText : MonoBehaviour
    {
        [SerializeField] Color textColor;
        [SerializeField] Color hiddenColor;

        [SerializeField] TMP_Text textBox;
        //[SerializeField] HotbarSelector hotbarSelector;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnEnable()
        {
            HotbarSelector.ChangeSelectedItem += ChangeSelectedItemName;
        }
        private void OnDisable()
        {
            HotbarSelector.ChangeSelectedItem -= ChangeSelectedItemName;
        }

        public void ChangeSelectedItemName(string item_id)
        {
            //Debug.Log("change item");
            StopAllCoroutines();

            textBox.color = textColor;
            textBox.text = Database.ITEMS.Items[item_id].NAME;
            StartCoroutine(FadeAway());
        }

        private IEnumerator FadeAway()
        {
            yield return new WaitForSeconds(2f);

            for (float i = 0; i < 1.3f; i += Time.deltaTime)
            {
                textBox.color = Color.Lerp(textColor, hiddenColor, i);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}