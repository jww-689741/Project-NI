Shader "Hidden/TerrainTess" {
  Properties{
      _Tess("Tessellation", Range(1,32)) = 15
      _Displacement("Displacement", Range(0, 1.0)) = 0.125
      _Color("Color", color) = (1,1,1,0)
      _MainTex("Texture", 2D) = "black" { }
      _SpecColor("Spec color", color) = (0.5,0.5,0.5,0.5)
      _TextureSize("Texture size", Range(2,8192)) = 1024
      _NormalStrength("Normal strength", Range(0,1)) = 0.06
  }
    SubShader{
        Pass
        {
          ZWrite On
          ZTest LEqual
        CGPROGRAM
        #pragma target 5.0
        #pragma vertex VS
        #pragma fragment PS
        #pragma hull HS
        #pragma domain DS
        #include "Lighting.cginc"
        #include "Tessellation.cginc"


        struct appdata
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct HS_Input
        {
          float4 pos      : SV_POSITION;
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

        uint Resolution;
        float2 Size;
        float3 Position;
        float _PatchSize;
        static const float _HorizontalScale = 0.5f;

        sampler2D _HeatTex;
        float4 _BasicColor;
        float4 _HeatColor;

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

          uint res = ceil((float)Resolution / _PatchSize);
          uint patchY = patchID / (uint)res;
          uint patchX = patchID % (uint)res;

          float2 pointCoords = float2(patchX, patchY) + PointMapping[pointID];
          float2 posOff = (pointCoords / (res)) * Size.x;

          Output.pos = float4(posOff.x, 0.0f, posOff.y, 1);
          Output.uv = 1.0f - pointCoords / (res);
          return Output;
        }

#ifdef SHADER_API_D3D11            
            StructuredBuffer<float> _DispBuffer;

            float GetHeight(float2 uv)
            {
              float2 start = uv * (Resolution - 1);
              float2 offset = start - floor(start);

              int2 leftTop = (int2)start;
              int2 rightTop = clamp(start + int2(1, 0), 0, Resolution - 1);
              int2 leftBot = clamp(start + int2(0, 1), 0, Resolution - 1);
              int2 rightBot = clamp(start + int2(1, 1), 0, Resolution - 1);

              float leftTopV = _DispBuffer[leftTop.y * Resolution + leftTop.x];
              float rightTopV = _DispBuffer[rightTop.y * Resolution + rightTop.x];
              float leftBotV = _DispBuffer[leftBot.y * Resolution + leftBot.x];
              float rightBotV = _DispBuffer[rightBot.y * Resolution + rightBot.x];

              return lerp(lerp(leftTopV, rightTopV, offset.x), lerp(leftBotV, rightBotV, offset.x), offset.y);
            }


            float3 GetNormal(float2 uv, float2 texelSize)
            {
              float l = GetHeight(uv + texelSize * float2(-1,  0));
              float r = GetHeight(uv + texelSize * float2(1,  0));
              float t = GetHeight(uv + texelSize * float2(0, -1));
              float b = GetHeight(uv + texelSize * float2(0,  1));

              return normalize(float3(l - r, texelSize.x * 8.0f, t - b));
            }

            static const float _TriangleSize = 10;
            static const float _TessEndDistance = 100;

            // Gets mixed tesselation factor from two corners
            //////////////////////////////////////////////////////////////////
            float GetMixedTessellationFactor(float3 corner0, float3 corner1)
            {
              float3 sphereCenter = (corner0 + corner1) * 0.5f;
              float camDist = distance(_WorldSpaceCameraPos, sphereCenter);

              // Uniform
              float4 clipPos = mul(UNITY_MATRIX_VP, float4(sphereCenter, 1.0f));
              float size = abs(_PatchSize * UNITY_MATRIX_P[1][1] / clipPos.w);
              const float edgesPerHeight = _ScreenParams.y / _TriangleSize;
              float uniformTess = size * edgesPerHeight;

              // Distance
              const float minDist = 0;
              const float maxDist = _TessEndDistance;
              float distanceFactor = 1.0f - (camDist - minDist) / (maxDist - minDist);

              // Mix        
              float4 distanceClip = mul(UNITY_MATRIX_P, float4(0, 0, maxDist, 1.0f));
              float distanceSize = abs(_PatchSize * UNITY_MATRIX_P[1][1] / distanceClip.w) * edgesPerHeight;
              float distanceTess = lerp(distanceSize, 64.0f, pow(distanceFactor, 2));


              const float mixDistance = maxDist;
              float mixTess = lerp(uniformTess, distanceTess, step(0, distanceFactor));

              return clamp(mixTess, 1, 64);
            }


            // Hull Shader Constant Function 
            ////////////////////////////////
            HS_ConstantOutput HSConstant(InputPatch<HS_Input, 4> input)
            {
              HS_ConstantOutput output = (HS_ConstantOutput)0;

              // Diameter is distance between corners
              output.Edges[1] = GetMixedTessellationFactor(input[0].pos, input[1].pos);
              output.Edges[2] = GetMixedTessellationFactor(input[1].pos, input[2].pos);
              output.Edges[3] = GetMixedTessellationFactor(input[2].pos, input[3].pos);
              output.Edges[0] = GetMixedTessellationFactor(input[3].pos, input[0].pos);

              output.Inside[0] = (output.Edges[0] + output.Edges[2]) * 0.5f;
              output.Inside[1] = (output.Edges[1] + output.Edges[3]) * 0.5f;

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

              pos.y = GetHeight(output.uv) * Size.y;
              output.pos = UnityObjectToClipPos(pos.xyz + Position);
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

            float4 PS(PS_Input IN) : SV_TARGET
            {
              float3 N = GetNormal(IN.uv, 1.0f / (Resolution - 1));
              float light = saturate(dot(N, _WorldSpaceLightPos0));
              light += 0.15f;

              float3 color = lerp(_BasicColor.rgb, _HeatColor.rgb, tex2D(_HeatTex, IN.uv).r);

              return float4(color * light,1);
            }

            ENDCG
            }
      }
}