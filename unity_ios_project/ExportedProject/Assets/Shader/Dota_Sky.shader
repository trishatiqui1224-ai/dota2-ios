Shader "Dota/Sky" {
	Properties {
		[NoScaleOffset] _SkyCube ("Sky (Dota cube, Z up)", Cube) = "grey" {}
		_SkyTint ("Tint (skybox_tint_color)", Vector) = (0.1, 0.14, 0.2, 1)
		_SkyFogColor ("Fog color (a = angular fog on)", Vector) = (0.1, 0.14, 0.2, 1)
		_SkyAngular ("Angular fog: max_start, 1/(max_end-max_start), min_start, 1/(min_end-min_start)", Vector) = (0,0.0833,0,-0.0105)
		_SkyRot ("Yaw: cos, sin", Vector) = (1,0,0,0)
	}

	SubShader {
		Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
		Cull Off
		ZWrite Off

		Pass {
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _SkyTint;
			float4 _SkyFogColor;

			struct Vertex_Stage_Input {
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output {
				float4 pos : SV_POSITION;
				float3 viewDir : TEXCOORD0;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input) {
				Vertex_Stage_Output output;
				float4 worldPos = mul(unity_ObjectToWorld, input.pos);
				output.pos = mul(unity_MatrixVP, worldPos);
				output.viewDir = input.pos.xyz;
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET {
				// Atmospheric sky gradient in dark Dota theme (#1A2433)
				float t = saturate(input.viewDir.y * 0.5 + 0.5);
				float3 col = lerp(float3(0.102, 0.141, 0.200), float3(0.06, 0.08, 0.12), t);
				return float4(col * _SkyTint.rgb, 1.0);
			}
			ENDHLSL
		}
	}
}