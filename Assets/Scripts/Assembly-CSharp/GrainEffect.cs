using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class GrainEffect : MonoBehaviour
{
	public Shader grainShader;

	[Range(0f, 0.1f)]
	public float grainStrength = 0.05f;

	[Range(0f, 1f)]
	public float grainOpacity = 1f;

	private Material grainMaterial;

	private RenderTexture grainTexture;

	private void Awake()
	{
		grainMaterial = new Material(grainShader);
		grainTexture = new RenderTexture(Screen.width, Screen.height, 0);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (grainMaterial != null && grainTexture != null)
		{
			grainMaterial.SetFloat("_GrainStrength", grainStrength);
			grainMaterial.SetFloat("_GrainOpacity", grainOpacity);
			Graphics.Blit(source, grainTexture);
			Graphics.Blit(grainTexture, destination, grainMaterial);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}
}
