Shader "Dota/Multiblend" {
	Properties {
		_Color0 ("Color 0", 2D) = "white" {}
		_Color1 ("Color 1", 2D) = "white" {}
		_Color2 ("Color 2", 2D) = "white" {}
		_Color3 ("Color 3", 2D) = "white" {}
		[Normal] _Normal0 ("Normal 0", 2D) = "bump" {}
		[Normal] _Normal1 ("Normal 1", 2D) = "bump" {}
		[Normal] _Normal2 ("Normal 2", 2D) = "bump" {}
		[Normal] _Normal3 ("Normal 3", 2D) = "bump" {}
		_Tint0 ("Tint 0", Vector) = (1,1,1,1)
		_ObjectTint ("Object tint", Vector) = (1,1,1,1)
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[HideInInspector] _ZWrite ("_ZWrite", Float) = 1
	}

	SubShader {
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
		LOD 200
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
			float4 _Color0_ST;
			float4 _Tint0;
			float4 _ObjectTint;

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
				output.uv = (input.uv.xy * _Color0_ST.xy) + _Color0_ST.zw;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, input.normal));
				output.color = input.color;
				return output;
			}

			Texture2D<float4> _Color0;
			SamplerState sampler_Color0;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 col = _Color0.Sample(sampler_Color0, input.uv.xy);
				col *= _Tint0 * _ObjectTint;
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.4, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
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
			float4 _Color0_ST;
			float4 _Tint0;
			float4 _ObjectTint;

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
				output.uv = (input.uv.xy * _Color0_ST.xy) + _Color0_ST.zw;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, input.normal));
				output.color = input.color;
				return output;
			}

			Texture2D<float4> _Color0;
			SamplerState sampler_Color0;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 col = _Color0.Sample(sampler_Color0, input.uv.xy);
				col *= _Tint0 * _ObjectTint;
				float3 lightDir = normalize(float3(0.5, 0.8, 0.3));
				float ndotl = max(0.4, dot(input.normalWS, lightDir));
				col.rgb *= ndotl;
				return col;
			}
			ENDHLSL
		}
	}
}