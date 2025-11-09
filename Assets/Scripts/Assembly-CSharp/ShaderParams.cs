using System;
using UnityEngine;

[Serializable]
public struct ShaderParams
{
	public bool enabled;

	public Shader shader;

	public float intensity;

	public float valueX;

	public float valueY;

	public float valueZ;

	public bool switchV;

	public Color color;

	public Texture texture;
}
