using System;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	[ExecuteInEditMode]
	[ImageEffectAllowedInSceneView]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Cinematic/Temporal Anti-aliasing")]
	public class TemporalAntiAliasing : MonoBehaviour
	{
		public enum Sequence
		{
			Halton = 0
		}

		[Serializable]
		public struct JitterSettings
		{
			[Tooltip("The sequence used to generate the points used as jitter offsets.")]
			public Sequence sequence;

			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 3f)]
			public float spread;

			[Tooltip("Number of temporal samples. A larger value results in a smoother image but takes longer to converge; whereas a smaller value converges fast but allows for less subpixel information.")]
			[Range(4f, 64f)]
			public int sampleCount;
		}

		[Serializable]
		public struct SharpenFilterSettings
		{
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float amount;
		}

		[Serializable]
		public struct BlendSettings
		{
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float stationary;

			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float moving;

			[Tooltip("Amount of motion amplification in percentage. A higher value will make the final blend more sensitive to smaller motion, but might result in more aliased output; while a smaller value might desensitivize the algorithm resulting in a blurry output.")]
			[Range(30f, 100f)]
			public float motionAmplification;
		}

		[Serializable]
		public struct DebugSettings
		{
			[Tooltip("Forces the game view to update automatically while not in play mode.")]
			public bool forceRepaint;
		}

		[Serializable]
		public class Settings
		{
			[AttributeUsage(AttributeTargets.Field)]
			public class LayoutAttribute : PropertyAttribute
			{
			}

			[Layout]
			public JitterSettings jitterSettings;

			[Layout]
			public SharpenFilterSettings sharpenFilterSettings;

			[Layout]
			public BlendSettings blendSettings;

			[Layout]
			public DebugSettings debugSettings;

			public static Settings defaultSettings
			{
				get
				{
					return new Settings
					{
						jitterSettings = new JitterSettings
						{
							sequence = Sequence.Halton,
							spread = 1f,
							sampleCount = 8
						},
						sharpenFilterSettings = new SharpenFilterSettings
						{
							amount = 0.25f
						},
						blendSettings = new BlendSettings
						{
							stationary = 0.98f,
							moving = 0.8f,
							motionAmplification = 60f
						},
						debugSettings = new DebugSettings
						{
							forceRepaint = false
						}
					};
				}
			}
		}

		[SerializeField]
		public Settings settings = Settings.defaultSettings;

		private Shader m_Shader;

		private Material m_Material;

		private Camera m_Camera;

		private RenderTexture m_History;

		private int m_SampleIndex;

		public Shader shader
		{
			get
			{
				if (m_Shader == null)
				{
					m_Shader = Shader.Find("Hidden/Temporal Anti-aliasing");
				}
				return m_Shader;
			}
		}

		public Material material
		{
			get
			{
				if (m_Material == null)
				{
					if (shader == null || !shader.isSupported)
					{
						return null;
					}
					m_Material = new Material(shader);
				}
				return m_Material;
			}
		}

		public Camera camera_
		{
			get
			{
				if (m_Camera == null)
				{
					m_Camera = GetComponent<Camera>();
				}
				return m_Camera;
			}
		}

		private void RenderFullScreenQuad(int pass)
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			material.SetPass(pass);
			GL.Begin(7);
			GL.TexCoord2(0f, 0f);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.End();
			GL.PopMatrix();
		}

		private float GetHaltonValue(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}

		private Vector2 GenerateRandomOffset()
		{
			Vector2 result = new Vector2(GetHaltonValue(m_SampleIndex & 0x3FF, 2), GetHaltonValue(m_SampleIndex & 0x3FF, 3));
			if (++m_SampleIndex >= settings.jitterSettings.sampleCount)
			{
				m_SampleIndex = 0;
			}
			return result;
		}

		private Matrix4x4 GetPerspectiveProjectionMatrix(Vector2 offset)
		{
			float num = Mathf.Tan((float)Math.PI / 360f * camera_.fieldOfView);
			float num2 = num * camera_.aspect;
			offset.x *= num2 / (0.5f * (float)camera_.pixelWidth);
			offset.y *= num / (0.5f * (float)camera_.pixelHeight);
			float num3 = (offset.x - num2) * camera_.nearClipPlane;
			float num4 = (offset.x + num2) * camera_.nearClipPlane;
			float num5 = (offset.y + num) * camera_.nearClipPlane;
			float num6 = (offset.y - num) * camera_.nearClipPlane;
			Matrix4x4 result = default(Matrix4x4);
			result[0, 0] = 2f * camera_.nearClipPlane / (num4 - num3);
			result[0, 1] = 0f;
			result[0, 2] = (num4 + num3) / (num4 - num3);
			result[0, 3] = 0f;
			result[1, 0] = 0f;
			result[1, 1] = 2f * camera_.nearClipPlane / (num5 - num6);
			result[1, 2] = (num5 + num6) / (num5 - num6);
			result[1, 3] = 0f;
			result[2, 0] = 0f;
			result[2, 1] = 0f;
			result[2, 2] = (0f - (camera_.farClipPlane + camera_.nearClipPlane)) / (camera_.farClipPlane - camera_.nearClipPlane);
			result[2, 3] = (0f - 2f * camera_.farClipPlane * camera_.nearClipPlane) / (camera_.farClipPlane - camera_.nearClipPlane);
			result[3, 0] = 0f;
			result[3, 1] = 0f;
			result[3, 2] = -1f;
			result[3, 3] = 0f;
			return result;
		}

		private Matrix4x4 GetOrthographicProjectionMatrix(Vector2 offset)
		{
			float orthographicSize = camera_.orthographicSize;
			float num = orthographicSize * camera_.aspect;
			offset.x *= num / (0.5f * (float)camera_.pixelWidth);
			offset.y *= orthographicSize / (0.5f * (float)camera_.pixelHeight);
			float left = offset.x - num;
			float right = offset.x + num;
			float top = offset.y + orthographicSize;
			float bottom = offset.y - orthographicSize;
			return Matrix4x4.Ortho(left, right, bottom, top, camera_.nearClipPlane, camera_.farClipPlane);
		}

		private void OnEnable()
		{
			camera_.depthTextureMode = DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
			camera_.useJitteredProjectionMatrixForTransparentRendering = true;
		}

		private void OnDisable()
		{
			if (m_History != null)
			{
				RenderTexture.ReleaseTemporary(m_History);
				m_History = null;
			}
			camera_.depthTextureMode &= ~DepthTextureMode.MotionVectors;
			m_SampleIndex = 0;
		}

		private void OnPreCull()
		{
			Vector2 vector = GenerateRandomOffset();
			vector *= settings.jitterSettings.spread;
			camera_.nonJitteredProjectionMatrix = camera_.projectionMatrix;
			camera_.projectionMatrix = (camera_.orthographic ? GetOrthographicProjectionMatrix(vector) : GetPerspectiveProjectionMatrix(vector));
			vector.x /= camera_.pixelWidth;
			vector.y /= camera_.pixelHeight;
			material.SetVector("_Jitter", vector);
		}

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (m_History == null || m_History.width != source.width || m_History.height != source.height)
			{
				if ((bool)m_History)
				{
					RenderTexture.ReleaseTemporary(m_History);
				}
				m_History = RenderTexture.GetTemporary(source.width, source.height, 0, source.format, RenderTextureReadWrite.Default);
				m_History.filterMode = FilterMode.Bilinear;
				m_History.hideFlags = HideFlags.HideAndDontSave;
				Graphics.Blit(source, m_History, material, 2);
			}
			material.SetVector("_SharpenParameters", new Vector4(settings.sharpenFilterSettings.amount, 0f, 0f, 0f));
			material.SetVector("_FinalBlendParameters", new Vector4(settings.blendSettings.stationary, settings.blendSettings.moving, 100f * settings.blendSettings.motionAmplification, 0f));
			material.SetTexture("_MainTex", source);
			material.SetTexture("_HistoryTex", m_History);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format, RenderTextureReadWrite.Default);
			temporary.filterMode = FilterMode.Bilinear;
			RenderTexture renderTexture = destination;
			bool flag = false;
			if (destination == null)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format, RenderTextureReadWrite.Default);
				renderTexture.filterMode = FilterMode.Bilinear;
				flag = true;
			}
			Graphics.SetRenderTarget(new RenderBuffer[2] { renderTexture.colorBuffer, temporary.colorBuffer }, renderTexture.depthBuffer);
			RenderFullScreenQuad(camera_.orthographic ? 1 : 0);
			RenderTexture.ReleaseTemporary(m_History);
			m_History = temporary;
			if (flag)
			{
				Graphics.Blit(renderTexture, destination);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			RenderTexture.active = destination;
		}

		public void OnPostRender()
		{
			camera_.ResetProjectionMatrix();
		}
	}
}
