using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace VLB
{
	public static class SRPHelper
	{
		public enum RenderPipeline
		{
			Undefined = 0,
			BuiltIn = 1,
			URP = 2,
			LWRP = 3,
			HDRP = 4
		}

		private static RenderPipeline m_RenderPipelineCached;

		public static RenderPipeline renderPipelineType
		{
			get
			{
				if (m_RenderPipelineCached == RenderPipeline.Undefined)
				{
					m_RenderPipelineCached = ComputeRenderPipeline();
				}
				return m_RenderPipelineCached;
			}
		}

		private static RenderPipeline ComputeRenderPipeline()
		{
			RenderPipelineAsset renderPipelineAsset = GraphicsSettings.renderPipelineAsset;
			if ((bool)renderPipelineAsset)
			{
				string text = renderPipelineAsset.GetType().ToString();
				if (text.Contains("Universal"))
				{
					return RenderPipeline.URP;
				}
				if (text.Contains("Lightweight"))
				{
					return RenderPipeline.LWRP;
				}
				if (text.Contains("HD"))
				{
					return RenderPipeline.HDRP;
				}
			}
			return RenderPipeline.BuiltIn;
		}

		public static bool IsUsingCustomRenderPipeline()
		{
			if (RenderPipelineManager.currentPipeline == null)
			{
				return GraphicsSettings.renderPipelineAsset != null;
			}
			return true;
		}

		public static void RegisterOnBeginCameraRendering(Action<Camera> cb)
		{
			if (IsUsingCustomRenderPipeline())
			{
				UnityEngine.Experimental.Rendering.RenderPipeline.beginCameraRendering -= cb;
				UnityEngine.Experimental.Rendering.RenderPipeline.beginCameraRendering += cb;
			}
		}

		public static void UnregisterOnBeginCameraRendering(Action<Camera> cb)
		{
			if (IsUsingCustomRenderPipeline())
			{
				UnityEngine.Experimental.Rendering.RenderPipeline.beginCameraRendering -= cb;
			}
		}
	}
}
