// KF_WaterKit_Input.hlsl
// KITforgeLabs — WaterKIT
// Shared declarations: CBUFFER, textures, engine globals.
// Include FIRST in every pass via HLSLINCLUDE.

#ifndef KF_WATERKIT_INPUT_INCLUDED
#define KF_WATERKIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

// ─── CBUFFER ─────────────────────────────────────────────────────────────────
// All material properties MUST be here for SRP Batcher compatibility.
// Textures and samplers are declared outside (Unity requirement).
// Properties use float (not half) — D3D11 with UNITY_UNIFIED_SHADER_PRECISION_MODEL
// disabled compiles half as min16float, which causes X8000 bytecode errors when
// min16float values are involved in operations that generate specific D3D11 opcodes.
// Use half explicitly only in local shader computation variables, not in CBUFFER.
// ─────────────────────────────────────────────────────────────────────────────
CBUFFER_START(UnityPerMaterial)

    // Appearance
    float4 _ShallowColor;
    float4 _DeepColor;
    float  _DepthFade;
    float  _Transparency;

    // Normals
    float  _NormalStrength;
    float  _NormalTiling;
    float  _NormalSpeed;

    // Waves
    float  _WaveSpeed;
    float  _WaveScale;
    float4 _FlowDirection;       // xy = flow vector (River mode). zw unused.

    // Foam
    float4 _FoamColor;
    float  _FoamTiling;
    float  _FoamCutoff;
    float  _IntersectionFoamWidth;

    // Special FX
    float  _RefractionStrength;
    float  _CausticsScale;
    float  _CausticsSpeed;
    float  _CausticsIntensity;
    float  _SparkleIntensity;
    float  _SparkleScale;
    float  _SpecularPower;
    float  _SpecularIntensity;

    // Emissive Foam
    float4 _EmissiveFoamColor;
    float  _EmissiveFoamIntensity;

    // Texture ST companions — required by SRP Batcher even when TRANSFORM_TEX is not used.
    // Unity implicitly creates these float4 per-material properties for every 2D texture.
    float4 _NormalMap_ST;
    float4 _FoamTexture_ST;
    float4 _CausticsTexture_ST;
    float4 _FlowMap_ST;

CBUFFER_END

// ─── Texture Declarations ────────────────────────────────────────────────────
// TEXTURE2D and SAMPLER declarations go OUTSIDE the CBUFFER.
// _ST properties are not needed since we compute UVs manually with custom tiling.
// ─────────────────────────────────────────────────────────────────────────────
TEXTURE2D(_NormalMap);          SAMPLER(sampler_NormalMap);
TEXTURE2D(_FoamTexture);        SAMPLER(sampler_FoamTexture);
TEXTURE2D(_CausticsTexture);    SAMPLER(sampler_CausticsTexture);
TEXTURE2D(_FlowMap);            SAMPLER(sampler_FlowMap);

// ─── Vertex / Fragment Structs ───────────────────────────────────────────────
struct KF_Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float4 tangentOS    : TANGENT;
    float2 uv           : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct KF_Varyings
{
    float4 positionCS   : SV_POSITION;
    float3 positionWS   : TEXCOORD0;    // World position — for caustics, sparkles, reflection
    float4 screenPos    : TEXCOORD1;    // Screen pos — for depth sampling + refraction
    // Pre-computed animated UVs (avoids dependent texture reads on mobile)
    float2 uvNormal1    : TEXCOORD2;    // Normal map layer 1 (scrolling +X+Y)
    float2 uvNormal2    : TEXCOORD3;    // Normal map layer 2 (scrolling -X+Y, coarser)
    float2 uvFoam       : TEXCOORD4;    // Foam texture UV
    // TBN for tangent-space normal → world-space conversion
    float3 normalWS     : TEXCOORD5;
    float3 tangentWS    : TEXCOORD6;
    float3 bitangentWS  : TEXCOORD7;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

#endif // KF_WATERKIT_INPUT_INCLUDED
