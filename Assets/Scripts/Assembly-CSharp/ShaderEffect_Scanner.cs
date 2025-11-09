using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Scanner : MonoBehaviour
{
	public float area;

	private Material material_a;

	private Material material_b;

	private void Awake()
	{
		material_a = new Material(Shader.Find("Hidden/Shift"));
		material_b = new Material(Shader.Find("Hidden/Shift"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material_a.SetFloat("_ValueY", area);
		material_b.SetFloat("_ValueY", 0f - area);
		Graphics.Blit(source, source, material_a);
		Graphics.Blit(source, destination, material_b);
	}
}
