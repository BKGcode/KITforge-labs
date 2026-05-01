// KF_WaterKit_Depth.hlsl
// KITforgeLabs — WaterKIT
// Depth-based color gradient + edge intersection foam.
// Requires: KF_WaterKit_Input.hlsl included first.

#ifndef KF_WATERKIT_DEPTH_INCLUDED
#define KF_WATERKIT_DEPTH_INCLUDED

// ─── KF_SampleWaterDepth ─────────────────────────────────────────────────────
// Returns the depth of the water column at this fragment:
//   0   = water surface / scene geometry exactly at water level
//   N   = N world-units of water above the scene floor
// Uses the URP depth texture helper from DeclareDepthTexture.hlsl.
// ─────────────────────────────────────────────────────────────────────────────
float KF_SampleWaterDepth(float4 screenPos)
{
    float2 uv = screenPos.xy / screenPos.w;

    // Raw scene depth at this screen position
    float rawDepth = SampleSceneDepth(uv);

    // Convert raw depth to linear eye depth (distance from camera)
    float sceneDepth = LinearEyeDepth(rawDepth, _ZBufferParams);

    // screenPos.w = view-space depth of the water surface itself
    float surfaceDepth = screenPos.w;

    return max(0.0, sceneDepth - surfaceDepth);
}

// ─── KF_DepthColor ───────────────────────────────────────────────────────────
// Shallow → deep color gradient.
// t = 0 at surface (shallow teal), t = 1 at full depth (deep dark teal).
// Note: all float — CBUFFER properties are float; mixing half/float on D3D11
// without UNITY_UNIFIED_SHADER_PRECISION_MODEL produces invalid bytecode (X8000).
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_DepthColor(float waterDepth)
{
    float t = saturate(waterDepth / _DepthFade);
    return lerp(_ShallowColor.rgb, _DeepColor.rgb, t);
}

// ─── KF_DepthAlpha ───────────────────────────────────────────────────────────
float KF_DepthAlpha(float waterDepth)
{
    float t = saturate(waterDepth / (_DepthFade * 0.5));
    return saturate(t * _Transparency);
}

// ─── KF_IntersectionFoam ─────────────────────────────────────────────────────
// Returns a 0–1 gradient mask: 1 at the waterline, 0 beyond _IntersectionFoamWidth.
// The caller (KF_CombinedFoamMask) multiplies this by a foam texture sample
// to produce chunky foam instead of a smooth white gradient.
// Guard: rawDepth == 0 (sky / far plane in reversed-Z) = no intersection.
// ─────────────────────────────────────────────────────────────────────────────
float KF_IntersectionFoam(float4 screenPos, float waterDepth)
{
    // Sky guard: if the scene pixel is sky (rawDepth ≈ 0 in reversed-Z), return 0.
    float2 uv     = screenPos.xy / screenPos.w;
    float  raw    = SampleSceneDepth(uv);
    float  skyMask = step(0.0001, raw);   // 0 when sky, 1 when real geometry

    float gradient = 1.0 - saturate(waterDepth / _IntersectionFoamWidth);
    return step(0.05, gradient) * gradient * skyMask;
}

#endif // KF_WATERKIT_DEPTH_INCLUDED
