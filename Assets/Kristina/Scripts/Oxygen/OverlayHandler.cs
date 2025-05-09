using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace kristina
{
    public class OverlayHandler : MonoBehaviour
    {
        [SerializeField] Image overlay;
        [SerializeField] Color transparent;
        [SerializeField] Color final_color;

        public void EnableOverlay()
        {
            StartCoroutine(AddOverlay());
        }
        public void DisableOverlay()
        {
            StartCoroutine(RemoveOverlay());
        }
        private IEnumerator AddOverlay()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                overlay.color = Color.Lerp(transparent, final_color, i);
                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator RemoveOverlay()
        {
            for (float i = 1; i > 0; i -= Time.deltaTime)
            {
                overlay.color = Color.Lerp(transparent, final_color, i);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}