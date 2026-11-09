Shader "Dota/Hero" {
	Properties {
		_ColorTex ("Color (g_tColor)", 2D) = "white" {}
		_Masks1 ("Masks1 (g_tMasks1)", 2D) = "black" {}
		_Masks2 ("Masks2 (g_tMasks2)", 2D) = "black" {}
		[Normal] _NormalTex ("Normal (g_tNormal)", 2D) = "bump" {}
		_DiffuseWarp ("Diffuse Warp (g_tDiffuseWarp)", 2D) = "white" {}
		[Toggle] _UseDiffuseWarp ("F_DIFFUSE_WARP", Float) = 0
		[Toggle] _WarpMaskSelects ("g_bDiffuseWarpMaskSelectsWarpTopOrBottom", Float) = 1
		[Toggle] _NormalFlipY ("Normal: flip green", Float) = 0
		_FresnelWarp ("Fresnel Warp (g_tFresnelWarp)", 2D) = "gray" {}
		[NoScaleOffset] _CubeMap ("Cube map (g_tCubeMap)", Cube) = "black" {}
		_CubeMapScalar ("g_flCubeMapScalar", Float) = 0
		[Toggle] _MaskCubeMapByMetalness ("g_bMaskCubeMapByMetalness", Float) = 0
		_DetailTex ("Detail (g_tDetail)", 2D) = "black" {}
		[Enum(None,0,Add,1,AddSelfIllum,2,Mod2x,3)] _DetailMode ("Detail mode (F_DETAIL)", Float) = 2
		_DetailBlendFactor ("Detail blend", Float) = 0.2
		_DetailUV ("Detail UV scale (xy) scroll (zw)", Vector) = (2,2,0,0)
		_DetailTint ("Detail tint", Vector) = (1,1,1,1)
		_SpecularColor ("Specular color", Vector) = (1,1,1,1)
		_SpecularScale ("Specular scale", Float) = 5
		_SpecularExponent ("Specular exponent", Float) = 16
		_RimLightColor ("Rim color", Vector) = (1,1,1,1)
		_RimLightScale ("Rim scale", Float) = 0.25
		_AmbientScale ("Ambient scale", Float) = 1
		_OverbrightFactor ("g_flOverbrightFactor", Float) = 1
		[Toggle(_ALPHATEST_ON)] _AlphaTest ("Alpha test (F_ALPHA_TEST)", Float) = 1
		_Cutoff ("Alpha ref", Range(0, 1)) = 0.5
		_StatusColor ("Status tint", Vector) = (0,0,0,0)
		_Fade ("Fade (invisible to allies)", Range(0, 1)) = 1
		_ObjectTint ("Object tint (rendercolor)", Vector) = (1,1,1,1)
		[HideInInspector] _SrcBlend ("Src blend", Float) = 1
		[HideInInspector] _DstBlend ("Dst blend", Float) = 0
		[HideInInspector] _ZWrite ("ZWrite", Float) = 1
		[HideInInspector] _Cull ("Cull", Float) = 2
	}

	SubShader {
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
		LOD 200
		Cull [_Cull]
		ZWrite [_ZWrite]
		Blend [_SrcBlend] [_DstBlend]

		Pass {
			Name "UniversalForward"
			Tags { "LightMode" = "UniversalForward" }
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _ColorTex_ST;
			float4 _ObjectTint;
			float4 _StatusColor;
			float _Cutoff;
			float _Fade;

			struct Vertex_Stage_Input {
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
				float3 normalWS : TEXCOORD1;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input) {
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _ColorTex_ST.xy) + _ColorTex_ST.zw;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, input.normal));
				output.color = input.color;
				return output;
			}

			Texture2D<float4> _ColorTex;
			SamplerState sampler_ColorTex;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 col = _ColorTex.Sample(sampler_ColorTex, input.uv.xy);
				col *= _ObjectTint;
				#if defined(_ALPHATEST_ON)
				clip(col.a - _Cutoff);
				#endif
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.35, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
				col.rgb += _StatusColor.rgb * _StatusColor.a;
				col.a *= _Fade;
				return col;
			}
			ENDHLSL
		}

		Pass {
			Name "ForwardBase"
			Tags { "LightMode" = "ForwardBase" }
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _ColorTex_ST;
			float4 _ObjectTint;
			float4 _StatusColor;
			float _Cutoff;
			float _Fade;

			struct Vertex_Stage_Input {
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
				float3 normalWS : TEXCOORD1;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input) {
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _ColorTex_ST.xy) + _ColorTex_ST.zw;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, input.normal));
				output.color = input.color;
				return output;
			}

			Texture2D<float4> _ColorTex;
			SamplerState sampler_ColorTex;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 col = _ColorTex.Sample(sampler_ColorTex, input.uv.xy);
				col *= _ObjectTint;
				#if defined(_ALPHATEST_ON)
				clip(col.a - _Cutoff);
				#endif
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.35, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
				col.rgb += _StatusColor.rgb * _StatusColor.a;
				col.a *= _Fade;
				return col;
			}
			ENDHLSL
		}
	}
}