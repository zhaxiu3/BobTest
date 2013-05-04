Shader "Custom/2AlphaBlended" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BlendTex("Blend(RGB)", 2D) = "black"{}
	}
	SubShader {
		Pass{
			SetTexture[_MainTex]{
				Combine texture
			}

			SetTexture[_BlendTex]{
				Combine texture lerp(texture) previous
			}

		}
	} 
	FallBack "Diffuse"
}
