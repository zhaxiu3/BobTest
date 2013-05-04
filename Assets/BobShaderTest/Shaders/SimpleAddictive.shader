Shader "Custom/SimpleAddictive" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		Pass{
			Blend One OneMinusDstAlpha
			SetTexture[_MainTex]{
				Combine texture
			}
		}
	} 
}
