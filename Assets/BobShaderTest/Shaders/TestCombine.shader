Shader "Custom/TestCombine" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}        
		_Color ("Main Color", Color) = (1,1,1,0)
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
        _Emission ("Emmisive Color", Color) = (0,0,0,0)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.7
	}
	SubShader {
		Material{
			Diffuse [_Color]
            Ambient [_Color]
            Shininess [_Shininess]
            Specular [_SpecColor]
            Emission [_Emission]
		}

		Lighting On
		Pass{
			SetTexture[_MainTex]{
				Combine primary*texture
			}
			SetTexture[_MainTex]{
				Combine previous+texture
			}
		}
	} 
	FallBack "Diffuse"
}
