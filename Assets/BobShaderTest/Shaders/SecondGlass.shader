Shader "Custom/SecondGlass" {
	Properties {
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Transparency (A)", 2D) = "white" {}
		_Reflection("Base (RGB) Gloss (A)", Cube) = "skybox"{TexGen CubeReflect}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
				Material{
					Diffuse[_Color]
			}
			Lighting On
				SetTexture[_MainTex]{
					Combine texture* primary double, texture *primary
			}
		}

		Pass{
			Blend One One
				Material{
					Diffuse[_Color]
			}
			Lighting On
				SetTexture[_Reflection]{
					Combine texture
						Matrix[_Reflection]
			}
		}

	} 
	FallBack "Diffuse"
}
