using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Tint : MonoBehaviour
{
	public float y = 1f;

	public float u = 1f;

	public float v = 1f;

	private Material material;

	private void Awake()
	{
		material = new Material(Shader.Find("Hidden/Tint"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_ValueX", y);
		material.SetFloat("_ValueY", u);
		material.SetFloat("_ValueZ", v);
		Graphics.Blit(source, destination, material);
	}
}
