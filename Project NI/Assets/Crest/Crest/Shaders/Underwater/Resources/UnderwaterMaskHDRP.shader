// Crest Ocean System

// Copyright 2021 Wave Harmonic Ltd

Shader "Hidden/Crest/Underwater/Ocean Mask HDRP"
{
	Properties
	{
		// Needed so it can be scripted.
		_StencilRef("Stencil Reference", Int) = 0
	}

	SubShader
	{
		Pass
		{
			Name "Ocean Surface Mask"
			// We always disable culling when rendering ocean mask, as we only
			// use it for underwater rendering features.
			Cull Off

			Stencil
			{
				Ref [_StencilRef]
				Comp Equal
			}

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			// for VFACE
			#pragma target 3.0

			#pragma multi_compile_local _ CREST_WATER_VOLUME

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			#include "../UnderwaterMaskShared.hlsl"
			ENDHLSL
		}

		Pass
		{
			Name "Ocean Horizon Mask"
			Cull Off
			ZWrite Off
			// Horizon must be rendered first or it will overwrite the mask with incorrect values. ZTest not needed.
			ZTest Always

			Stencil
			{
				Ref [_StencilRef]
				Comp Equal
			}

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

			#include "../../OceanConstants.hlsl"
			#include "../../OceanGlobals.hlsl"

			float _FarPlaneOffset;

			struct Attributes
			{
				uint vertexID : SV_VertexID;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vert(Attributes input)
			{
				Varyings output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID, _FarPlaneOffset);

				return output;
			}

			half4 Frag(Varyings input) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				PositionInputs positions = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, _FarPlaneOffset, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);

#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				positions.positionWS.y += _WorldSpaceCameraPos.y;
#endif

				return (half4) positions.positionWS.y > _OceanCenterPosWorld.y
					? CREST_MASK_ABOVE_SURFACE
					: CREST_MASK_BELOW_SURFACE;
			}

			ENDHLSL
		}
	}
}
