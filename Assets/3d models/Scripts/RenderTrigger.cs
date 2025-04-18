using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class RenderTrigger : MonoBehaviour
{
    public MeshRenderer render;

    /*public void EnableRender()
    {
        render.enabled = true;
    }

    public void DisableRender()
    {
        render.enabled = false;
    }*/

    public void EnableRender()
    {
        GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
    }

    public void DisableRender()
    {
        GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
    }
}
