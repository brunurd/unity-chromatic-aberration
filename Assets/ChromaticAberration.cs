using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour {
	private Shader shader;
	private Material material;
	[Range(0.0f, 1.0f)]
	public float chromaticAberration = 1.0f;
    public bool onTheScreenEdges = true;

	public void Start() {
		shader = new Shader();
		shader = Shader.Find("Hidden/ChromaticAberration");
		material = new Material(shader);

		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}

		if (!shader && !shader.isSupported) {
			enabled = false;
		}
	}

	public void OnRenderImage(RenderTexture inTexture, RenderTexture outTexture) {
		if (shader != null) {
			material.SetFloat("_ChromaticAberration", 0.01f * chromaticAberration);

            if (onTheScreenEdges)
				material.SetFloat("_Center", 0.5f);

            else
				material.SetFloat("_Center", 0);

            Graphics.Blit(inTexture, outTexture, material);
		}
		else {
			Graphics.Blit (inTexture, outTexture);
		}
	}
}

