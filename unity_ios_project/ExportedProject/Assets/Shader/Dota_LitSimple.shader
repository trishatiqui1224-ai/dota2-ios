Shader "Dota/LitSimple" {
	Properties {
		_ColorTex ("Color (g_tColor)", 2D) = "white" {}
		[Normal] _NormalTex ("Normal (g_tNormal)", 2D) = "bump" {}
		_SpecTex ("Specular (R refl, G self-illum, B bloom)", 2D) = "black" {}
		_TintMask ("Tint mask", 2D) = "white" {}
		_ScrollSpeedTex ("Scroll speed", 2D) = "white" {}
		_Tint ("Color tint", Vector) = (1,1,1,1)
		_Tint2 ("Color tint 2 (F_TINT_MASK: where mask = 0)", Vector) = (1,1,1,1)
		_SpecIntensity ("Specular intensity", Float) = 1
		_BumpStrength ("Bump strength", Float) = 1
		_Opacity ("Opacity scale", Float) = 1
		_Cutoff ("Alpha ref", Range(0, 1)) = 0.5
		_UVTransform ("UV scale (xy) offset (zw)", Vector) = (1,1,0,0)
		_Scroll ("Scroll dir (xy) speed (z)", Vector) = (0,0,0,0)
		_Fullbright ("Fullbright", Float) = 0
		_ReceiveTreeShadow ("Receive tree shadows", Float) = 1
		_ObjectTint ("Object tint (rendercolor)", Vector) = (1,1,1,1)
		[Toggle(_NORMALMAP)] _UseNormal ("F_NORMAL_MAP", Float) = 0
		[Toggle(_SPECULAR)] _UseSpec ("F_SPECULAR", Float) = 0
		[Toggle(_TINT_MASK)] _UseTintMask ("F_TINT_MASK", Float) = 0
		[Toggle(_ALPHATEST_ON)] _AlphaTest ("F_ALPHA_TEST", Float) = 0
		[Toggle(_VERTEX_COLORS)] _VertexColors ("F_PAINT_VERTEX_COLORS", Float) = 0
		[Toggle(_SCROLL_UV)] _UseScroll ("F_SCROLL_UV", Float) = 0
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[HideInInspector] _ZWrite ("_ZWrite", Float) = 1
		[HideInInspector] _Cull ("_Cull", Float) = 2
		[HideInInspector] _OffsetFactor ("_OffsetFactor", Float) = 0
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
			float4 _Tint;
			float4 _ObjectTint;
			float _Opacity;
			float _Cutoff;

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
				col *= _Tint * _ObjectTint;
				#if defined(_ALPHATEST_ON)
				clip(col.a - _Cutoff);
				#endif
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.35, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
				col.a *= _Opacity;
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
			float4 _Tint;
			float4 _ObjectTint;
			float _Opacity;
			float _Cutoff;

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
				col *= _Tint * _ObjectTint;
				#if defined(_ALPHATEST_ON)
				clip(col.a - _Cutoff);
				#endif
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.35, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
				col.a *= _Opacity;
				return col;
			}
			ENDHLSL
		}
	}
}