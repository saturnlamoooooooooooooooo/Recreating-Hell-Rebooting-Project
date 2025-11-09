Shader "Hidden/BleedingColors" {
	Properties {
		_Intensity ("Black & White blend", Range(0, 15)) = 3
		_MainTex ("Texture", 2D) = "white" {}
		_ValueX ("Degree of bleeding colors", Range(-10, 10)) = 0.2
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