using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class VignetteHandler : MonoBehaviour
{
    [SerializeField] float change_rate = .1f;
    [SerializeField] Volume volume;
    Vignette vignette;
    private void Awake()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }
    public void AddVignette()
    {
        StartCoroutine(ModifyVignette(change_rate, true));
    }
    public void RemoveVignette()
    {
        StartCoroutine(ModifyVignette(change_rate, false));
    }
    public IEnumerator ModifyVignette(float rate, bool adding)
    {
        if (adding)
        {
            while (vignette.smoothness.value < 1f)
            {
                vignette.smoothness.value += rate;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (vignette.smoothness.value >= 0.02f)
            {
                vignette.smoothness.value -= rate;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
