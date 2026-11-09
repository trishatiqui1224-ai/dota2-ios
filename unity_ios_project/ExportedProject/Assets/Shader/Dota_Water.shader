Shader "Dota/Water" {
	Properties {
		_Color ("Water Color", Color) = (0.1, 0.25, 0.35, 0.8)
		_FlowSpeed ("Flow Speed", Float) = 0.5
	}

	SubShader {
		Tags { "Queue"="Transparent-10" "RenderType"="Transparent" }
		LOD 200
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _Color;

			struct Vertex_Stage_Input {
				float4 pos : POSITION;
				float3 normal : NORMAL;
			};

			struct Vertex_Stage_Output {
				float4 pos : SV_POSITION;
				float3 normalWS : TEXCOORD0;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input) {
				Vertex_Stage_Output output;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.normalWS = normalize(mul((float3x3)unity_ObjectToWorld, input.normal));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				return _Color;
			}
			ENDHLSL
		}
	}
}