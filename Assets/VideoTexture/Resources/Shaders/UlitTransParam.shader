Shader "Transparent/Unlight" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_FadeStart("Fade Start", float) = 1.0
	_FadeEnd("Fade End", float) = 10.0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
CGPROGRAM
#pragma surface surf NoLight alpha
half4 LightingNoLight(SurfaceOutput s, half3 lightDir, half atten) {
	half4 c;
    c.rgb = s.Albedo;
    c.a = s.Alpha;
    return c;
}
struct Input {
	float2 uv_MainTex;
    float3 worldPos;
};
sampler2D _MainTex;
float _FadeStart;
float _FadeEnd;
fixed4 _Color;
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	float interval = saturate((_FadeEnd-IN.worldPos.z)/(_FadeEnd-_FadeStart));
	o.Albedo = c.rgb;
	o.Alpha = c.a*interval;
}
ENDCG
}

Fallback "Transparent/VertexLit"
}