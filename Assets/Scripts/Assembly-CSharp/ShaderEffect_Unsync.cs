using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_Unsync : MonoBehaviour
{
	public enum Movement
	{
		JUMPING_FullOnly = 0,
		SCROLLING_FullOnly = 1,
		STATIC = 2
	}

	public Movement movement = Movement.STATIC;

	public float speed = 1f;

	private float position;

	private Material material;

	private void Awake()
	{
		material = new Material(Shader.Find("Hidden/VUnsync"));
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		position = speed * 0.1f;
		material.SetFloat("_ValueX", position);
		Graphics.Blit(source, destination, material);
	}
}
