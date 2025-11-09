using UnityEngine;
using UnityEngine.Serialization;

namespace VLB
{
	[HelpURL("http://saladgamer.com/vlb-doc/config.html")]
	public class Config : ScriptableObject
	{
		public const string ClassName = "Config";

		public bool geometryOverrideLayer = true;

		public int geometryLayerID = 1;

		public string geometryTag = "Untagged";

		public int geometryRenderQueue = 3000;

		[FormerlySerializedAs("renderPipeline")]
		[SerializeField]
		private RenderPipeline _RenderPipeline;

		[FormerlySerializedAs("renderingMode")]
		[SerializeField]
		private RenderingMode _RenderingMode = RenderingMode.SinglePass;

		public float ditheringFactor;

		public int sharedMeshSides = 24;

		public int sharedMeshSegments = 5;

		[Range(0.01f, 2f)]
		public float globalNoiseScale = 0.5f;

		public Vector3 globalNoiseVelocity = Consts.Beam.NoiseVelocityDefault;

		public string fadeOutCameraTag = "MainCamera";

		[HighlightNull]
		public Texture3D noiseTexture3D;

		[HighlightNull]
		public ParticleSystem dustParticlesPrefab;

		public Texture2D ditheringNoiseTexture;

		public FeatureEnabledColorGradient featureEnabledColorGradient = FeatureEnabledColorGradient.HighOnly;

		public bool featureEnabledDepthBlend = true;

		public bool featureEnabledNoise3D = true;

		public bool featureEnabledDynamicOcclusion = true;

		public bool featureEnabledMeshSkewing = true;

		public bool featureEnabledShaderAccuracyHigh = true;

		[SerializeField]
		private int pluginVersion = -1;

		[SerializeField]
		private Material _DummyMaterial;

		[SerializeField]
		private Shader _BeamShader;

		private Transform m_CachedFadeOutCamera;

		private static Config ms_Instance;

		public RenderPipeline renderPipeline
		{
			get
			{
				return _RenderPipeline;
			}
			set
			{
				Debug.LogError("Modifying the RenderPipeline in standalone builds is not permitted");
			}
		}

		public RenderingMode renderingMode
		{
			get
			{
				return _RenderingMode;
			}
			set
			{
				Debug.LogError("Modifying the RenderingMode in standalone builds is not permitted");
			}
		}

		public RenderingMode actualRenderingMode
		{
			get
			{
				RenderingMode renderingMode2 = renderingMode;
				int num = 2;
				if (renderingMode == RenderingMode.SRPBatcher && !IsSRPBatcherSupported())
				{
					return RenderingMode.SinglePass;
				}
				if (renderPipeline != 0 && renderingMode == RenderingMode.MultiPass)
				{
					return RenderingMode.SinglePass;
				}
				return renderingMode;
			}
		}

		public bool useSinglePassShader
		{
			get
			{
				return actualRenderingMode != RenderingMode.MultiPass;
			}
		}

		public bool requiresDoubleSidedMesh
		{
			get
			{
				return useSinglePassShader;
			}
		}

		public Shader beamShader
		{
			get
			{
				return _BeamShader;
			}
		}

		public Transform fadeOutCameraTransform
		{
			get
			{
				if (m_CachedFadeOutCamera == null)
				{
					ForceUpdateFadeOutCamera();
				}
				return m_CachedFadeOutCamera;
			}
		}

		public bool hasRenderPipelineMismatch
		{
			get
			{
				return SRPHelper.renderPipelineType == SRPHelper.RenderPipeline.BuiltIn != (_RenderPipeline == RenderPipeline.BuiltIn);
			}
		}

		public static Config Instance
		{
			get
			{
				return GetInstance(true);
			}
		}

		public bool IsSRPBatcherSupported()
		{
			if (renderPipeline == RenderPipeline.BuiltIn)
			{
				return false;
			}
			SRPHelper.RenderPipeline renderPipelineType = SRPHelper.renderPipelineType;
			if (renderPipelineType != SRPHelper.RenderPipeline.URP)
			{
				return renderPipelineType == SRPHelper.RenderPipeline.HDRP;
			}
			return true;
		}

		public void ForceUpdateFadeOutCamera()
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(fadeOutCameraTag);
			if ((bool)gameObject)
			{
				m_CachedFadeOutCamera = gameObject.transform;
			}
		}

		[RuntimeInitializeOnLoadMethod]
		private static void OnStartup()
		{
			Instance.m_CachedFadeOutCamera = null;
			Instance.RefreshGlobalShaderProperties();
			if (Instance.hasRenderPipelineMismatch)
			{
				Debug.LogError("It looks like the 'Render Pipeline' is not correctly set in the config. Please make sure to select the proper value depending on your pipeline in use.", Instance);
			}
		}

		public void Reset()
		{
			geometryOverrideLayer = true;
			geometryLayerID = 1;
			geometryTag = "Untagged";
			geometryRenderQueue = 3000;
			sharedMeshSides = 24;
			sharedMeshSegments = 5;
			globalNoiseScale = 0.5f;
			globalNoiseVelocity = Consts.Beam.NoiseVelocityDefault;
			renderPipeline = RenderPipeline.BuiltIn;
			renderingMode = RenderingMode.SinglePass;
			ditheringFactor = 0f;
			fadeOutCameraTag = "MainCamera";
			featureEnabledColorGradient = FeatureEnabledColorGradient.HighOnly;
			featureEnabledDepthBlend = true;
			featureEnabledNoise3D = true;
			featureEnabledDynamicOcclusion = true;
			featureEnabledMeshSkewing = true;
			featureEnabledShaderAccuracyHigh = true;
			ResetInternalData();
		}

		private void RefreshGlobalShaderProperties()
		{
			Shader.SetGlobalFloat(ShaderProperties.GlobalUsesReversedZBuffer, SystemInfo.usesReversedZBuffer ? 1f : 0f);
			Shader.SetGlobalFloat(ShaderProperties.GlobalDitheringFactor, ditheringFactor);
			Shader.SetGlobalTexture(ShaderProperties.GlobalDitheringNoiseTex, ditheringNoiseTexture);
		}

		public void ResetInternalData()
		{
			noiseTexture3D = Resources.Load("Noise3D_64x64x64") as Texture3D;
			dustParticlesPrefab = Resources.Load("DustParticles", typeof(ParticleSystem)) as ParticleSystem;
			ditheringNoiseTexture = Resources.Load("VLBDitheringNoise", typeof(Texture2D)) as Texture2D;
		}

		public ParticleSystem NewVolumetricDustParticles()
		{
			if (!dustParticlesPrefab)
			{
				if (Application.isPlaying)
				{
					Debug.LogError("Failed to instantiate VolumetricDustParticles prefab.");
				}
				return null;
			}
			ParticleSystem particleSystem = Object.Instantiate(dustParticlesPrefab);
			particleSystem.useAutoRandomSeed = false;
			particleSystem.name = "Dust Particles";
			particleSystem.gameObject.hideFlags = Consts.Internal.ProceduralObjectsHideFlags;
			particleSystem.gameObject.SetActive(true);
			return particleSystem;
		}

		private void OnEnable()
		{
			HandleBackwardCompatibility(pluginVersion, 1960);
			pluginVersion = 1960;
		}

		private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
		{
			if (serializedVersion != -1)
			{
			}
		}

		private static Config GetInstance(bool assertIfNotFound)
		{
			if (ms_Instance == null)
			{
				ConfigOverride configOverride = Resources.Load<ConfigOverride>("VLBConfigOverride" + PlatformHelper.GetCurrentPlatformSuffix());
				if (configOverride == null)
				{
					configOverride = Resources.Load<ConfigOverride>("VLBConfigOverride");
				}
				ms_Instance = configOverride;
				bool flag = ms_Instance == null;
			}
			return ms_Instance;
		}
	}
}
