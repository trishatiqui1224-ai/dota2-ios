Shader "Dota/HealthBar" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0.2, 0.85, 0.2, 1)
		_Back ("Background", Vector) = (0.03,0.03,0.03,0.9)
		_Border ("Border (x) aspect (y) marker px (z)", Vector) = (0.06,0.25,2,0)
	}

	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
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
			float4 _Back;

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
				float4 col = tex * _Color * input.color;
				return col;
			}
			ENDHLSL
		}
	}
}