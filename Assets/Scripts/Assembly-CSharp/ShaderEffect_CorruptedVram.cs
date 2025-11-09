using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_CorruptedVram : MonoBehaviour
{
	public float shift = 10f;

	private Texture texture;

	private Material material;

	private void Awake()
	{
		material = new Material(Shader.Find("Hidden/Distortion"));
		texture = Resources.Load<Texture>("Checkerboard-big");
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_ValueX", shift);
		material.SetTexture("_Texture", texture);
		Graphics.Blit(source, destination, material);
	}
}
