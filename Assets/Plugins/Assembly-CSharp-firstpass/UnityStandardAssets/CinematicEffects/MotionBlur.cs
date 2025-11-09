using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityStandardAssets.CinematicEffects
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Cinematic/Motion Blur")]
	public class MotionBlur : MonoBehaviour
	{
		private class FrameBlendingFilter
		{
			private struct Frame
			{
				public RenderTexture lumaTexture;

				public RenderTexture chromaTexture;

				public float time;

				private RenderBuffer[] _mrt;

				public float CalculateWeight(float strength, float currentTime)
				{
					if (time == 0f)
					{
						return 0f;
					}
					float num = Mathf.Lerp(80f, 16f, strength);
					return Mathf.Exp((time - currentTime) * num);
				}

				public void Release()
				{
					if (lumaTexture != null)
					{
						RenderTexture.ReleaseTemporary(lumaTexture);
					}
					if (chromaTexture != null)
					{
						RenderTexture.ReleaseTemporary(chromaTexture);
					}
					lumaTexture = null;
					chromaTexture = null;
				}

				public void MakeRecord(RenderTexture source, Material material)
				{
					Release();
					lumaTexture = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.R8);
					chromaTexture = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.R8);
					lumaTexture.filterMode = FilterMode.Point;
					chromaTexture.filterMode = FilterMode.Point;
					if (_mrt == null)
					{
						_mrt = new RenderBuffer[2];
					}
					_mrt[0] = lumaTexture.colorBuffer;
					_mrt[1] = chromaTexture.colorBuffer;
					Graphics.SetRenderTarget(_mrt, lumaTexture.depthBuffer);
					Graphics.Blit(source, material, 0);
					time = Time.time;
				}

				public void MakeRecordRaw(RenderTexture source, RenderTextureFormat format)
				{
					Release();
					lumaTexture = RenderTexture.GetTemporary(source.width, source.height, 0, format);
					lumaTexture.filterMode = FilterMode.Point;
					Graphics.Blit(source, lumaTexture);
					time = Time.time;
				}
			}

			private bool _useCompression;

			private RenderTextureFormat _rawTextureFormat;

			private Material _material;

			private Frame[] _frameList;

			private int _lastFrameCount;

			private int _History1LumaTex;

			private int _History2LumaTex;

			private int _History3LumaTex;

			private int _History4LumaTex;

			private int _History1ChromaTex;

			private int _History2ChromaTex;

			private int _History3ChromaTex;

			private int _History4ChromaTex;

			private int _History1Weight;

			private int _History2Weight;

			private int _History3Weight;

			private int _History4Weight;

			public FrameBlendingFilter()
			{
				_useCompression = CheckSupportCompression();
				_rawTextureFormat = GetPreferredRenderTextureFormat();
				_material = new Material(Shader.Find("Hidden/Image Effects/Cinematic/MotionBlur/FrameBlending"));
				_material.hideFlags = HideFlags.DontSave;
				_frameList = new Frame[4];
				FetchUniformLocations();
			}

			public void Release()
			{
				UnityEngine.Object.DestroyImmediate(_material);
				_material = null;
				Frame[] frameList = _frameList;
				foreach (Frame frame in frameList)
				{
					frame.Release();
				}
				_frameList = null;
			}

			public void PushFrame(RenderTexture source)
			{
				int frameCount = Time.frameCount;
				if (frameCount != _lastFrameCount)
				{
					int num = frameCount % _frameList.Length;
					if (_useCompression)
					{
						_frameList[num].MakeRecord(source, _material);
					}
					else
					{
						_frameList[num].MakeRecordRaw(source, _rawTextureFormat);
					}
					_lastFrameCount = frameCount;
				}
			}

			public void BlendFrames(float strength, RenderTexture source, RenderTexture destination)
			{
				float time = Time.time;
				Frame frameRelative = GetFrameRelative(-1);
				Frame frameRelative2 = GetFrameRelative(-2);
				Frame frameRelative3 = GetFrameRelative(-3);
				Frame frameRelative4 = GetFrameRelative(-4);
				_material.SetTexture(_History1LumaTex, frameRelative.lumaTexture);
				_material.SetTexture(_History2LumaTex, frameRelative2.lumaTexture);
				_material.SetTexture(_History3LumaTex, frameRelative3.lumaTexture);
				_material.SetTexture(_History4LumaTex, frameRelative4.lumaTexture);
				_material.SetTexture(_History1ChromaTex, frameRelative.chromaTexture);
				_material.SetTexture(_History2ChromaTex, frameRelative2.chromaTexture);
				_material.SetTexture(_History3ChromaTex, frameRelative3.chromaTexture);
				_material.SetTexture(_History4ChromaTex, frameRelative4.chromaTexture);
				_material.SetFloat(_History1Weight, frameRelative.CalculateWeight(strength, time));
				_material.SetFloat(_History2Weight, frameRelative2.CalculateWeight(strength, time));
				_material.SetFloat(_History3Weight, frameRelative3.CalculateWeight(strength, time));
				_material.SetFloat(_History4Weight, frameRelative4.CalculateWeight(strength, time));
				Graphics.Blit(source, destination, _material, _useCompression ? 1 : 2);
			}

			private static bool CheckSupportCompression()
			{
				if (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8))
				{
					return SystemInfo.supportedRenderTargetCount > 1;
				}
				return false;
			}

			private static RenderTextureFormat GetPreferredRenderTextureFormat()
			{
				RenderTextureFormat[] array = new RenderTextureFormat[3]
				{
					RenderTextureFormat.RGB565,
					RenderTextureFormat.ARGB1555,
					RenderTextureFormat.ARGB4444
				};
				foreach (RenderTextureFormat renderTextureFormat in array)
				{
					if (SystemInfo.SupportsRenderTextureFormat(renderTextureFormat))
					{
						return renderTextureFormat;
					}
				}
				return RenderTextureFormat.Default;
			}

			private Frame GetFrameRelative(int offset)
			{
				int num = (Time.frameCount + _frameList.Length + offset) % _frameList.Length;
				return _frameList[num];
			}

			private void FetchUniformLocations()
			{
				_History1LumaTex = Shader.PropertyToID("_History1LumaTex");
				_History2LumaTex = Shader.PropertyToID("_History2LumaTex");
				_History3LumaTex = Shader.PropertyToID("_History3LumaTex");
				_History4LumaTex = Shader.PropertyToID("_History4LumaTex");
				_History1ChromaTex = Shader.PropertyToID("_History1ChromaTex");
				_History2ChromaTex = Shader.PropertyToID("_History2ChromaTex");
				_History3ChromaTex = Shader.PropertyToID("_History3ChromaTex");
				_History4ChromaTex = Shader.PropertyToID("_History4ChromaTex");
				_History1Weight = Shader.PropertyToID("_History1Weight");
				_History2Weight = Shader.PropertyToID("_History2Weight");
				_History3Weight = Shader.PropertyToID("_History3Weight");
				_History4Weight = Shader.PropertyToID("_History4Weight");
			}
		}

		private class ReconstructionFilter
		{
			private const float kMaxBlurRadius = 5f;

			private Material _material;

			private bool _unroll;

			private RenderTextureFormat _vectorRTFormat = RenderTextureFormat.RGHalf;

			private RenderTextureFormat _packedRTFormat = RenderTextureFormat.ARGB2101010;

			private int _VelocityScale;

			private int _MaxBlurRadius;

			private int _TileMaxOffs;

			private int _TileMaxLoop;

			private int _LoopCount;

			private int _NeighborMaxTex;

			private int _VelocityTex;

			public ReconstructionFilter()
			{
				Shader shader = Shader.Find("Hidden/Image Effects/Cinematic/MotionBlur/Reconstruction");
				if (shader.isSupported && CheckTextureFormatSupport())
				{
					_material = new Material(shader);
					_material.hideFlags = HideFlags.DontSave;
				}
				_unroll = SystemInfo.graphicsDeviceName.Contains("Adreno");
				FetchUniformLocations();
			}

			public void Release()
			{
				if (_material != null)
				{
					UnityEngine.Object.DestroyImmediate(_material);
				}
				_material = null;
			}

			public void ProcessImage(float shutterAngle, int sampleCount, RenderTexture source, RenderTexture destination)
			{
				if (_material == null)
				{
					Graphics.Blit(source, destination);
					return;
				}
				int num = (int)(5f * (float)source.height / 100f);
				int num2 = ((num - 1) / 8 + 1) * 8;
				float value = shutterAngle / 360f * 1.45f;
				_material.SetFloat(_VelocityScale, value);
				_material.SetFloat(_MaxBlurRadius, num);
				RenderTexture temporaryRT = GetTemporaryRT(source, 1, _packedRTFormat);
				Graphics.Blit(null, temporaryRT, _material, 0);
				RenderTexture temporaryRT2 = GetTemporaryRT(source, 4, _vectorRTFormat);
				Graphics.Blit(temporaryRT, temporaryRT2, _material, 1);
				RenderTexture temporaryRT3 = GetTemporaryRT(source, 8, _vectorRTFormat);
				Graphics.Blit(temporaryRT2, temporaryRT3, _material, 2);
				ReleaseTemporaryRT(temporaryRT2);
				Vector2 vector = Vector2.one * ((float)num2 / 8f - 1f) * -0.5f;
				_material.SetVector(_TileMaxOffs, vector);
				_material.SetInt(_TileMaxLoop, num2 / 8);
				RenderTexture temporaryRT4 = GetTemporaryRT(source, num2, _vectorRTFormat);
				Graphics.Blit(temporaryRT3, temporaryRT4, _material, 3);
				ReleaseTemporaryRT(temporaryRT3);
				RenderTexture temporaryRT5 = GetTemporaryRT(source, num2, _vectorRTFormat);
				Graphics.Blit(temporaryRT4, temporaryRT5, _material, 4);
				ReleaseTemporaryRT(temporaryRT4);
				_material.SetInt(_LoopCount, Mathf.Clamp(sampleCount / 2, 1, 64));
				_material.SetFloat(_MaxBlurRadius, num);
				_material.SetTexture(_NeighborMaxTex, temporaryRT5);
				_material.SetTexture(_VelocityTex, temporaryRT);
				Graphics.Blit(source, destination, _material, _unroll ? 6 : 5);
				ReleaseTemporaryRT(temporaryRT);
				ReleaseTemporaryRT(temporaryRT5);
			}

			private bool CheckTextureFormatSupport()
			{
				if (!SystemInfo.SupportsRenderTextureFormat(_vectorRTFormat))
				{
					return false;
				}
				if (!SystemInfo.SupportsRenderTextureFormat(_packedRTFormat))
				{
					_packedRTFormat = RenderTextureFormat.ARGB32;
				}
				return true;
			}

			private RenderTexture GetTemporaryRT(Texture source, int divider, RenderTextureFormat format)
			{
				int width = source.width / divider;
				int height = source.height / divider;
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, format);
				temporary.filterMode = FilterMode.Point;
				return temporary;
			}

			private void ReleaseTemporaryRT(RenderTexture rt)
			{
				RenderTexture.ReleaseTemporary(rt);
			}

			private void FetchUniformLocations()
			{
				_VelocityScale = Shader.PropertyToID("_VelocityScale");
				_MaxBlurRadius = Shader.PropertyToID("_MaxBlurRadius");
				_TileMaxOffs = Shader.PropertyToID("_TileMaxOffs");
				_TileMaxLoop = Shader.PropertyToID("_TileMaxLoop");
				_LoopCount = Shader.PropertyToID("_LoopCount");
				_NeighborMaxTex = Shader.PropertyToID("_NeighborMaxTex");
				_VelocityTex = Shader.PropertyToID("_VelocityTex");
			}
		}

		[Serializable]
		public class Settings
		{
			[SerializeField]
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			[SerializeField]
			[Tooltip("The amount of sample points, which affects quality and performance.")]
			public int sampleCount;

			[SerializeField]
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending")]
			public float frameBlending;

			public static Settings defaultSettings
			{
				get
				{
					return new Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}
		}

		[SerializeField]
		private Settings _settings = Settings.defaultSettings;

		[SerializeField]
		private Shader _reconstructionShader;

		[SerializeField]
		private Shader _frameBlendingShader;

		private ReconstructionFilter _reconstructionFilter;

		private FrameBlendingFilter _frameBlendingFilter;

		public Settings settings
		{
			get
			{
				return _settings;
			}
			set
			{
				_settings = value;
			}
		}

		private void OnEnable()
		{
			_reconstructionFilter = new ReconstructionFilter();
			_frameBlendingFilter = new FrameBlendingFilter();
		}

		private void OnDisable()
		{
			_reconstructionFilter.Release();
			_frameBlendingFilter.Release();
			_reconstructionFilter = null;
			_frameBlendingFilter = null;
		}

		private void Update()
		{
			if (_settings.shutterAngle > 0f)
			{
				GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (_settings.shutterAngle > 0f && _settings.frameBlending > 0f)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				_reconstructionFilter.ProcessImage(_settings.shutterAngle, _settings.sampleCount, source, temporary);
				_frameBlendingFilter.BlendFrames(_settings.frameBlending, temporary, destination);
				_frameBlendingFilter.PushFrame(temporary);
				RenderTexture.ReleaseTemporary(temporary);
			}
			else if (_settings.shutterAngle > 0f)
			{
				_reconstructionFilter.ProcessImage(_settings.shutterAngle, _settings.sampleCount, source, destination);
			}
			else if (_settings.frameBlending > 0f)
			{
				_frameBlendingFilter.BlendFrames(_settings.frameBlending, source, destination);
				_frameBlendingFilter.PushFrame(source);
			}
			else
			{
				Graphics.Blit(source, destination);
			}
		}
	}
}
