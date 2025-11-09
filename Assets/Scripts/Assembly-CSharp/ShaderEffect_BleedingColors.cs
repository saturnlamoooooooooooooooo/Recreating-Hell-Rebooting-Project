using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour
{
	public float intensity = 3f;

	public float shift = 0.5f;

	private Material material;

	private void Awake()
	{
		material = new Material(Shader.Find("Hidden/BleedingColors"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ValueX", shift);
		Graphics.Blit(source, destination, material);
	}
}
