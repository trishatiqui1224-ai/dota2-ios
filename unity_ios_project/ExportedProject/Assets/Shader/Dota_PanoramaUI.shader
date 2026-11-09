Shader "Dota/PanoramaUI" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Wash ("Wash color", Color) = (0,0,0,0)
		_Saturation ("Saturation", Float) = 1
		_Brightness ("Brightness", Float) = 1
		_Contrast ("Contrast", Float) = 1
		_MaskTex ("Opacity mask", 2D) = "white" {}
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src blend", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst blend", Float) = 10
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
	}

	SubShader {
		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Stencil {
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend [_SrcBlend] [_DstBlend]
		ColorMask [_ColorMask]

		Pass {
			Name "Default"
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;
			float4 _Color;
			float4 _Wash;
			float _Brightness;

			struct Vertex_Stage_Input {
				float4 pos : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output {
				float4 pos : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input) {
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				output.color = input.color * _Color;
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 col = _MainTex.Sample(sampler_MainTex, input.uv.xy) * input.color;
				col.rgb = lerp(col.rgb, _Wash.rgb, _Wash.a);
				col.rgb *= _Brightness;
				return col;
			}
			ENDHLSL
		}
	}
}