    Shader "Hidden/TerrainTess" {
        Properties {
            _Tess ("Tessellation", Range(1,32)) = 15
            _Displacement ("Displacement", Range(0, 1.0)) = 0.125
            _Color ("Color", color) = (1,1,1,0)
            _MainTex("Texture", 2D) = "black" { }
            _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
            _TextureSize ("Texture size", Range(2,8192)) = 1024
            _NormalStrength ("Normal strength", Range(0,1)) = 0.06

            _Mask("Mask", 2D) = "Black" {}
            _Brush("Brush", 2D) = "white" {}
            _BrushSize("BrushSize", Range(0,10)) = 1.0
            _BrushPosition("BrushPosition", Vector) = (0,0,0,0)
            _BrushStrength("BrushStrength", float) = 0.2
            _BrushRotation("BrushRotation", float) = 0.0
        }
        SubShader {
            Pass
            {
            Cull Off
            ZWrite Off
            ZTest Always
            
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex VS
            #pragma fragment PS
            #pragma hull HS
            #pragma domain DS
            #include "Tessellation.cginc"

            
            struct appdata 
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct HS_Input
            {
              float4 pos      : POS;
              float2 uv       : TEXCOORD;
            };

            struct HS_ConstantOutput
            {
              float Edges[4]  : SV_TessFactor;
              float Inside[2] : SV_InsideTessFactor;
            };

            struct HS_ControlPointOutput
            {
              float3 pos   : POS;
              float2 uv    : TEXCOORD;
            };
            
            struct DS_Output
            {
              float4 pos        : SV_Position;
              float4 uv         : TEXCOORD;
              float3 nor        : NORMAL;
            };


            struct PS_Input
            {
              float4 pos        : SV_Position;
              float4 uv         : TEXCOORD;
              float3 nor        : NORMAL;
            };


            float _TextureSize;
            float _Displacement;
            float _NormalStrength;

            sampler2D _Mask;
            sampler2D _Brush;
            float4 _BrushPosition;
            float _BrushRotation;
            float _BrushSize;
            float _BrushStrength;
            
            float2 _PatchDimensions;
            float _HorizontalScale;
            float _PatchSize;

            static const uint2 PointMapping[4] =
            {
              uint2(0, 0),
              uint2(0, 1),
              uint2(1, 1),
              uint2(1, 0)
            };


            // Vertex Shader
            ////////////////
            HS_Input VS(uint ID : SV_VERTEXID)
            {
              HS_Input Output;

              // Generate Vertex
              //////////////////
              uint patchID = ID / 4;
              uint pointID = ID % 4;

              uint patchY = patchID / (uint)_PatchDimensions.x;
              uint patchX = patchID % (uint)_PatchDimensions.x;

              float2 pointCoords = float2(patchX, patchY) + PointMapping[pointID];
              float2 posOff = pointCoords * _PatchSize / _HorizontalScale;

              Output.pos = float4(posOff.x, 0.0f, posOff.y, 1);
              Output.uv = pointCoords / _PatchDimensions;
              return Output;
            }

#ifdef SHADER_API_D3D11            
            StructuredBuffer<float> _DispBuffer; 
    
            float GetHeight(float2 uv)
            {
              float2 start = uv * (_TextureSize - 1);
              float2 offset = start - floor(start);
              
              int2 leftTop = (int2)start;
              int2 rightTop = clamp(start + int2(1, 0), 0, _TextureSize - 1);
              int2 leftBot = clamp (start + int2(0, 1), 0, _TextureSize - 1);
              int2 rightBot = clamp(start + int2(1, 1), 0, _TextureSize - 1);
              
              float leftTopV  = _DispBuffer[leftTop.y * _TextureSize + leftTop.x];
              float rightTopV = _DispBuffer[rightTop.y * _TextureSize + rightTop.x];
              float leftBotV  = _DispBuffer[leftBot.y * _TextureSize + leftBot.x];
              float rightBotV = _DispBuffer[rightBot.y * _TextureSize + rightBot.x];
              
              return lerp(lerp(leftTopV, rightTopV, offset.x), lerp(leftBotV, rightBotV, offset.x), offset.y);
            }
            
            
            float3 GetNormal(float2 uv, float2 texelSize)
            {
              float l = GetHeight(uv + texelSize * float2(-1,  0));
              float r = GetHeight(uv + texelSize * float2( 1,  0));
              float t = GetHeight(uv + texelSize * float2( 0, -1));
              float b = GetHeight(uv + texelSize * float2( 0,  1));
              
              return normalize(float3(l - r, b - t, 1.0f / _NormalStrength * texelSize.x));
            }

            // Hull Shader Constant Function 
            ////////////////////////////////
            HS_ConstantOutput HSConstant(InputPatch<HS_Input, 4> input)
            {
              HS_ConstantOutput output = (HS_ConstantOutput)0;

              // Diameter is distance between corners
              output.Edges[1] = 1;//GetMixedTessellationFactor(input[0].pos, input[1].pos);
              output.Edges[2] = 1;//GetMixedTessellationFactor(input[1].pos, input[2].pos);
              output.Edges[3] = 1;//GetMixedTessellationFactor(input[2].pos, input[3].pos);
              output.Edges[0] = 1;//GetMixedTessellationFactor(input[3].pos, input[0].pos);

              output.Inside[0] = 1;//(output.Edges[0] + output.Edges[2]) * 0.5f;
              output.Inside[1] = 1;//(output.Edges[1] + output.Edges[3]) * 0.5f;

              return output;
            }

            // Hull Shader
            //////////////
            [domain("quad")]
            [partitioning("fractional_odd")]
            [outputtopology("triangle_cw")]
            [patchconstantfunc("HSConstant")]
            [outputcontrolpoints(4)]
            HS_ControlPointOutput HS(InputPatch<HS_Input, 4> Input, uint uCPID : SV_OutputControlPointID)
            {
              HS_ControlPointOutput Output = (HS_ControlPointOutput)0;
              Output.pos = Input[uCPID].pos.xyz;
              Output.uv = Input[uCPID].uv;
              return Output;
            }

            // Domain Shader
            ////////////////
            [domain("quad")]
            DS_Output DS(HS_ConstantOutput HSConstantData, const OutputPatch<HS_ControlPointOutput, 4> Input,
              float2 bCoords : SV_DomainLocation)
            {
              DS_Output output = (DS_Output)0;

              float3 pos = lerp(lerp(Input[0].pos, Input[1].pos, bCoords.x), lerp(Input[3].pos, Input[2].pos, bCoords.x), bCoords.y);
              float2 uv = 1.0f - lerp(lerp(Input[0].uv, Input[1].uv, bCoords.x), lerp(Input[3].uv, Input[2].uv, bCoords.x), bCoords.y);
              output.uv = float4(uv.x, uv.y, pos.x, pos.z);

              output.pos = mul(UNITY_MATRIX_VP, float4(pos.xyz, 1.0));

              return output;
            }
#else
            float GetHeight(float2 uv) { return 0; }
            float3 GetNormal(float2 uv, float2 texelSize) { return 0; }
#endif
            
            //void disp (inout appdata v)
            //{
            //  v.texcoord.xy = float2(1.0f, 1.0f) - v.texcoord.xy;
            //  float d = GetHeight(v.texcoord.xy) * _Displacement;
            //  v.vertex.xyz += v.normal * d;
            //  v.normal = GetNormal(v.texcoord.xy, float2(1.0f,1.0f) / _TextureSize);
            //}


            fixed4 _Color;

            float4 PS (PS_Input IN) : SV_TARGET 
            {
              return float4(1,0,0,1);
              float2 uv = IN.uv;

              float2 brushMin = _BrushPosition.xy - _BrushSize.xx * 0.5f;
              float2 pixPos = (uv - brushMin) / _BrushSize.xx;

              pixPos -= 0.5f;
              float cR = cos(_BrushRotation);
              float sR = sin(_BrushRotation);
              pixPos = float2(pixPos.x * cR - pixPos.y * sR, pixPos.x * sR + pixPos.y * cR);
              pixPos += 0.5f;

              float brushBorder = step(pixPos.x, 1.0f) * step(0.0f, pixPos.x) *
                step(pixPos.y, 1.0f) * step(0.0f, pixPos.y);


              float brushValue = tex2D(_Brush, pixPos).r * _BrushStrength * brushBorder;
              float maskValue = tex2D(_Mask, uv).r;

              float finalValue = saturate(maskValue + brushValue);
              fixed3 terrainColor = fixed3(1, 1, 1);

              return float4(lerp(terrainColor, fixed3(1, 0, 0), finalValue), 1);
            }

            ENDCG
            }
        }
    }