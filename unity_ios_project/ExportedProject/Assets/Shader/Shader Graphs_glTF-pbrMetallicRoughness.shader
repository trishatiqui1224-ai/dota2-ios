Shader "Shader Graphs/glTF-pbrMetallicRoughness" {
	Properties {
		baseColorFactor ("Base Color", Vector) = (1,1,1,1)
		[NoScaleOffset] baseColorTexture ("Base Color Tex", 2D) = "white" {}
		baseColorTexture_ST ("Base Color Tex ST", Vector) = (1,1,0,0)
		baseColorTexture_Rotation ("Base Color Tex Rotation", Vector) = (0,0,0,0)
		baseColorTexture_texCoord ("Base Color Tex UV", Float) = 0
		[NoScaleOffset] [Normal] normalTexture ("Normal Tex", 2D) = "bump" {}
		normalTexture_ST ("Normal Tex ST", Vector) = (1,1,0,0)
		normalTexture_Rotation ("Normal Tex Rotation", Vector) = (0,0,0,0)
		normalTexture_texCoord ("Normal Tex UV", Float) = 0
		normalTexture_scale ("Normal Scale", Range(0, 1)) = 1
		occlusionTexture_strength ("Occlusion Strength", Range(0, 1)) = 1
		[NoScaleOffset] occlusionTexture ("Occlusion Tex", 2D) = "white" {}
		occlusionTexture_ST ("Occlusion Tex ST", Vector) = (1,1,0,0)
		occlusionTexture_Rotation ("Occlusion Tex Rotation", Vector) = (0,0,0,0)
		occlusionTexture_texCoord ("Occlusion Tex UV", Float) = 0
		roughnessFactor ("Roughness", Range(0, 1)) = 1
		metallicFactor ("Metallic", Range(0, 1)) = 1
		[NoScaleOffset] metallicRoughnessTexture ("Metallic-Roughness Tex", 2D) = "white" {}
		metallicRoughnessTexture_ST ("Metallic-Roughness Tex ST", Vector) = (1,1,0,0)
		metallicRoughnessTexture_Rotation ("Metallic-Roughness Tex Rotation", Vector) = (0,0,0,0)
		metallicRoughnessTexture_texCoord ("Metallic-Roughness Tex UV", Float) = 0
		[HDR] emissiveFactor ("Emissive", Vector) = (0,0,0,1)
		[NoScaleOffset] emissiveTexture ("Emissive Tex", 2D) = "white" {}
		emissiveTexture_ST ("Emissive Tex ST", Vector) = (1,1,0,0)
		emissiveTexture_Rotation ("Emissive Tex Rotation", Vector) = (0,0,0,0)
		emissiveTexture_texCoord ("Emissive Tex UV", Float) = 0
		emissiveExposureWeight ("Emissive Exposure Weight", Range(0, 1)) = 0
		alphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
		transmissionFactor ("Transmission", Range(0, 1)) = 0
		[NoScaleOffset] transmissionTexture ("Transmission Tex", 2D) = "white" {}
		[HideInInspector] _WorkflowMode ("_WorkflowMode", Float) = 1
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 1
		[HideInInspector] _ReceiveShadows ("_ReceiveShadows", Float) = 1
		[HideInInspector] _Surface ("_Surface", Float) = 0
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 0
		[HideInInspector] _BlendModePreserveSpecular ("_BlendModePreserveSpecular", Float) = 0
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[HideInInspector] _SrcBlendAlpha ("_SrcBlendAlpha", Float) = 1
		[HideInInspector] _DstBlendAlpha ("_DstBlendAlpha", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("_ZWrite", Float) = 1
		[HideInInspector] _ZWriteControl ("_ZWriteControl", Float) = 0
		[HideInInspector] _ZTest ("_ZTest", Float) = 4
		[HideInInspector] _Cull ("_Cull", Float) = 2
		[HideInInspector] _AlphaToMask ("_AlphaToMask", Float) = 0
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] _XRMotionVectorsPass ("_XRMotionVectorsPass", Float) = 1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
		[HideInInspector] _BUILTIN_Surface ("Float", Float) = 0
		[HideInInspector] _BUILTIN_Blend ("Float", Float) = 0
		[HideInInspector] _BUILTIN_AlphaClip ("Float", Float) = 0
		[HideInInspector] _BUILTIN_SrcBlend ("Float", Float) = 1
		[HideInInspector] _BUILTIN_DstBlend ("Float", Float) = 0
		[HideInInspector] _BUILTIN_ZWrite ("Float", Float) = 1
		[HideInInspector] _BUILTIN_ZWriteControl ("Float", Float) = 0
		[HideInInspector] _BUILTIN_ZTest ("Float", Float) = 4
		[HideInInspector] _BUILTIN_CullMode ("Float", Float) = 2
		[HideInInspector] _BUILTIN_QueueOffset ("Float", Float) = 0
		[HideInInspector] _BUILTIN_QueueControl ("Float", Float) = -1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}