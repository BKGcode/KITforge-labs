// KF_WaterKit_SpecialFX.hlsl
// KITforgeLabs — WaterKIT
// Refraction, caustics, sparkles, environment reflection, specular.
// Requires: KF_WaterKit_Input.hlsl included first.

#ifndef KF_WATERKIT_SPECIALFX_INCLUDED
#define KF_WATERKIT_SPECIALFX_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

// ─── KF_Refraction ───────────────────────────────────────────────────────────
// Samples the opaque texture with a normal-driven UV offset to simulate
// underwater refraction. Returns the refracted background color.
// normalTS  = tangent-space normal (xy used for offset)
// screenPos = SV_POSITION or ComputeScreenPos output (pre-divided by w outside)
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_Refraction(float2 screenUV, float3 normalWS)
{
    // Project normal XZ onto screen for the offset direction
    float2 offset    = normalWS.xz * _RefractionStrength;
    float2 refractUV = clamp(screenUV + offset, 0.001, 0.999);
    return SampleSceneColor(refractUV);
}

// ─── KF_Caustics ─────────────────────────────────────────────────────────────
// Animated caustics pattern projected from world-space top-down.
// Two-layer min-blend (intersection caustics) creates bright hotspots.
// The golden sparkle signature from the Sea of Thieves reference comes
// primarily from this function combined with a gold tint.
//
// posWS      = world-space fragment position
// waterDepth = depth of water column (from KF_SampleWaterDepth)
// Returns additive RGB contribution.
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_Caustics(float3 posWS, float waterDepth)
{
    float t = _Time.y;

    // Internal multipliers scaled *0.1 — consistent with NormalSpeed UV scroll budget.
    float2 uv1 = posWS.xz * _CausticsScale       + t * _CausticsSpeed * float2( 0.07,  0.05);
    float2 uv2 = posWS.xz * _CausticsScale * 0.8  + t * _CausticsSpeed * float2(-0.05,  0.08);

    float c1 = SAMPLE_TEXTURE2D(_CausticsTexture, sampler_CausticsTexture, uv1).r;
    float c2 = SAMPLE_TEXTURE2D(_CausticsTexture, sampler_CausticsTexture, uv2).r;

    // Intersection technique: min of two layers creates sharp caustic focus lines
    float caustics = min(c1, c2);

    // Depth attenuation: caustics brightest near shore, fade in deep water
    float depthFade = saturate(1.0 - waterDepth * 0.35) * saturate(waterDepth * 2.0);

    // Golden tint matching the Sea of Thieves reference (#FFD16A approximation)
    float3 goldTint = float3(1.0, 0.82, 0.42);

    return caustics * _CausticsIntensity * depthFade * goldTint;
}

// ─── KF_Sparkles ─────────────────────────────────────────────────────────────
// High-frequency specular sparkle points on the water surface.
// Reuses the caustics texture at a much finer tiling with a hard threshold
// to produce discrete bright flecks (sun glints).
//
// posWS      = world-space fragment position
// normalWS   = world-space surface normal (oriented by waves)
// viewDirWS  = normalized view direction
// waterDepth = depth of water column
// ─────────────────────────────────────────────────────────────────────────────
float KF_Sparkles(float3 posWS, float3 normalWS, float3 viewDirWS, float waterDepth)
{
    float t = _Time.y;

    float2 sparkleUV  = posWS.xz * _SparkleScale + t * float2(0.2, -0.15);
    float2 sparkleUV2 = posWS.xz * _SparkleScale * 0.7 - t * float2(0.1,  0.17);

    float s1 = SAMPLE_TEXTURE2D(_CausticsTexture, sampler_CausticsTexture, sparkleUV).r;
    float s2 = SAMPLE_TEXTURE2D(_CausticsTexture, sampler_CausticsTexture, sparkleUV2).r;

    // Hard threshold: only the brightest texels become sparkles
    float sparkle = step(0.88, s1) * step(0.88, s2);

    // Angle-based modulation: sparkles concentrate in the specular highlight cone
    // float precision required — pow(half,...) causes D3D11 min-precision error (X8000)
    Light  mainLight = GetMainLight();
    float3 halfVec   = normalize((float3)mainLight.direction + viewDirWS);
    float  nDotH     = saturate(dot(normalWS, halfVec));
    float  angleMask = pow(nDotH, 32.0f);

    // Depth: sparkles only on surface (near-zero depth) or shallow water
    float depthMask = saturate(1.0 - waterDepth * 0.8);

    return sparkle * angleMask * depthMask * _SparkleIntensity;
}

// ─── KF_Specular ─────────────────────────────────────────────────────────────
// Blinn-Phong specular highlight from the main directional light.
// Gives the diffuse sun glare seen across the SoT water surface.
// Returns additive RGB specular contribution.
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_Specular(float3 normalWS, float3 viewDirWS)
{
    // float precision required — pow(half,...) causes D3D11 min-precision error (X8000)
    Light  mainLight = GetMainLight();
    float3 halfVec   = normalize((float3)mainLight.direction + viewDirWS);
    float  nDotH     = saturate(dot(normalWS, halfVec));
    float  specular  = pow(nDotH, (float)_SpecularPower) * _SpecularIntensity;
    return specular * mainLight.color;
}

// ─── KF_Reflection ───────────────────────────────────────────────────────────
// Environment / sky reflection from Unity reflection probes + GI.
// Fresnel-weighted: strongest at grazing angles (edge of water).
// Returns RGB reflection contribution.
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_Reflection(float3 normalWS, float3 viewDirWS, float3 positionWS)
{
    float3 reflectDir = reflect(-viewDirWS, normalWS);

    // Sample the baked reflection probe / sky at a low roughness (stylized = glossy water)
    half4  envSample = SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0, reflectDir, 0.5);
    float3 envColor  = DecodeHDREnvironment(envSample, unity_SpecCube0_HDR);

    // Schlick Fresnel — float precision required for pow (D3D11 min-precision X8000)
    float fresnel = pow(1.0f - saturate(dot(normalWS, viewDirWS)), 4.0f);

    return envColor * fresnel * 0.4; // 0.4 = reflection strength cap (stylized, not PBR)
}

#endif // KF_WATERKIT_SPECIALFX_INCLUDED
