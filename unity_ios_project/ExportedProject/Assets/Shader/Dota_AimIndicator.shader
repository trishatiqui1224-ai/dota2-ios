Shader "Dota/AimIndicator" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1, 0.85, 0.5, 0.8)
		_Mode ("0 circle, 1 line, 2 rect", Float) = 0
	}

	SubShader {
		Tags { "Queue"="Transparent+50" "RenderType"="Transparent" "IgnoreProjector"="True" }
		LOD 200
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;
			float4 _Color;

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
				output.color = input.color;
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				float4 tex = _MainTex.Sample(sampler_MainTex, input.uv.xy);
				return tex * _Color * input.color;
			}
			ENDHLSL
		}
	}
}