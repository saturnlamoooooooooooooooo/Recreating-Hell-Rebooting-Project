Shader "Hidden/Tint" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_ValueX ("Y Shift", Float) = 1
		_ValueY ("U Shift", Float) = 1
		_ValueZ ("V Shift", Float) = 1
		_Switch ("Swap U and V channels - Full version only", Float) = 0
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