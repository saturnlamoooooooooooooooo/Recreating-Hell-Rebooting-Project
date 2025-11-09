using UnityEngine;

public class ShaderToTexture : MonoBehaviour
{
	public ComputeShader shader;

	private void RunShader()
	{
		int kernelIndex = shader.FindKernel("CSMain");
		RenderTexture renderTexture = new RenderTexture(256, 256, 24);
		renderTexture.enableRandomWrite = true;
		renderTexture.Create();
		shader.SetTexture(kernelIndex, "Result", renderTexture);
		shader.Dispatch(kernelIndex, 32, 32, 1);
	}

	private void Start()
	{
		RunShader();
	}

	private void Update()
	{
	}
}
