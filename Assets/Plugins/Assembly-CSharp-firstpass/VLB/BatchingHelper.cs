using UnityEngine;

namespace VLB
{
	public static class BatchingHelper
	{
		public const bool isGpuInstancingSupported = true;

		public static bool forceEnableDepthBlend
		{
			get
			{
				if (Config.Instance.actualRenderingMode != RenderingMode.GPUInstancing)
				{
					return Config.Instance.actualRenderingMode == RenderingMode.SRPBatcher;
				}
				return true;
			}
		}

		public static bool IsGpuInstancingEnabled(Material material)
		{
			return material.enableInstancing;
		}

		public static void SetMaterialProperties(Material material, bool enableGpuInstancing)
		{
			material.enableInstancing = enableGpuInstancing;
		}

		public static bool CanBeBatched(VolumetricLightBeam beamA, VolumetricLightBeam beamB, ref string reasons)
		{
			RenderPipeline renderPipeline = Config.Instance.renderPipeline;
			if (Config.Instance.actualRenderingMode != RenderingMode.GPUInstancing && Config.Instance.actualRenderingMode != RenderingMode.SRPBatcher)
			{
				reasons = string.Format("Current Render Pipeline is '{0}'. To enable batching, use 'GPU Instancing'", Config.Instance.renderPipeline);
				if (Config.Instance.renderPipeline != 0)
				{
					reasons += " or 'SRP Batcher'";
				}
				return false;
			}
			bool result = true;
			if (!CanBeBatched(beamA, ref reasons))
			{
				result = false;
			}
			if (!CanBeBatched(beamB, ref reasons))
			{
				result = false;
			}
			if (Config.Instance.featureEnabledDynamicOcclusion && beamA.GetComponent<DynamicOcclusionAbstractBase>() == null != (beamB.GetComponent<DynamicOcclusionAbstractBase>() == null))
			{
				AppendErrorMessage(ref reasons, string.Format("{0}/{1}: dynamically occluded and non occluded beams cannot be batched together", beamA.name, beamB.name));
				result = false;
			}
			if (Config.Instance.featureEnabledColorGradient != 0 && beamA.colorMode != beamB.colorMode)
			{
				AppendErrorMessage(ref reasons, string.Format("'Color Mode' mismatch: {0} / {1}", beamA.colorMode, beamB.colorMode));
				result = false;
			}
			if (beamA.blendingMode != beamB.blendingMode)
			{
				AppendErrorMessage(ref reasons, string.Format("'Blending Mode' mismatch: {0} / {1}", beamA.blendingMode, beamB.blendingMode));
				result = false;
			}
			if (Config.Instance.featureEnabledNoise3D && beamA.isNoiseEnabled != beamB.isNoiseEnabled)
			{
				AppendErrorMessage(ref reasons, string.Format("'3D Noise' enabled mismatch: {0} / {1}", beamA.noiseMode, beamB.noiseMode));
				result = false;
			}
			if (Config.Instance.featureEnabledDepthBlend && !forceEnableDepthBlend && beamA.depthBlendDistance > 0f != beamB.depthBlendDistance > 0f)
			{
				AppendErrorMessage(ref reasons, string.Format("'Opaque Geometry Blending' mismatch: {0} / {1}", beamA.depthBlendDistance, beamB.depthBlendDistance));
				result = false;
			}
			if (Config.Instance.featureEnabledShaderAccuracyHigh && beamA.shaderAccuracy != beamB.shaderAccuracy)
			{
				AppendErrorMessage(ref reasons, string.Format("'Shader Accuracy' mismatch: {0} / {1}", beamA.shaderAccuracy, beamB.shaderAccuracy));
				result = false;
			}
			return result;
		}

		public static bool CanBeBatched(VolumetricLightBeam beam, ref string reasons)
		{
			bool result = true;
			if (Config.Instance.actualRenderingMode == RenderingMode.GPUInstancing && beam.geomMeshType != 0)
			{
				AppendErrorMessage(ref reasons, string.Format("{0} is not using shared mesh", beam.name));
				result = false;
			}
			if (Config.Instance.featureEnabledDynamicOcclusion && beam.GetComponent<DynamicOcclusionDepthBuffer>() != null)
			{
				AppendErrorMessage(ref reasons, string.Format("{0} is using the DynamicOcclusion DepthBuffer feature", beam.name));
				result = false;
			}
			return result;
		}

		private static void AppendErrorMessage(ref string message, string toAppend)
		{
			if (message != "")
			{
				message += "\n";
			}
			message = message + "- " + toAppend;
		}
	}
}
