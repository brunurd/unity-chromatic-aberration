using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float chromaticAberration = 1.0f;
    public bool onTheScreenEdges = true;

    private Shader shader;
    private Material material;

    public void Start()
    {
        shader = Shader.Find("Hidden/ChromaticAberration");
        material = new Material(shader);

        if (!shader && !shader.isSupported) {
            enabled = false;
        }
    }

    public void OnRenderImage(RenderTexture inTexture, RenderTexture outTexture)
    {
        if (shader == null) {
            Graphics.Blit(inTexture, outTexture);
            return;
        }

        material.SetFloat("_ChromaticAberration", 0.01f * chromaticAberration);
        material.SetFloat("_Center", onTheScreenEdges ? 0.5f : 0);
        Graphics.Blit(inTexture, outTexture, material);
    }
}

