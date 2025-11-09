Shader "Hidden/Image Effects/Cinematic/MotionBlur/FrameBlending" {
	Properties {
		_MainTex ("", 2D) = "" {}
		_History1LumaTex ("", 2D) = "" {}
		_History2LumaTex ("", 2D) = "" {}
		_History3LumaTex ("", 2D) = "" {}
		_History4LumaTex ("", 2D) = "" {}
		_History1ChromaTex ("", 2D) = "" {}
		_History2ChromaTex ("", 2D) = "" {}
		_History3ChromaTex ("", 2D) = "" {}
		_History4ChromaTex ("", 2D) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}