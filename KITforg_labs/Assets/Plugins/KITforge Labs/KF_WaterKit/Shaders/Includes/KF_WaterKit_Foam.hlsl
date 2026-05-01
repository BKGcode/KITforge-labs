// KF_WaterKit_Foam.hlsl
// KITforgeLabs — WaterKIT
// Surface foam + intersection foam composition.
// Requires: KF_WaterKit_Input.hlsl, KF_WaterKit_Depth.hlsl included first.

#ifndef KF_WATERKIT_FOAM_INCLUDED
#define KF_WATERKIT_FOAM_INCLUDED

// ─── KF_SurfaceFoamMask ──────────────────────────────────────────────────────
// Foam that appears on the water surface driven by the foam texture.
// Two samples at different scale/offset are averaged to break visible tiling.
// Depth attenuation ensures foam fades in open deep water (SoT reference).
//
// uvFoam     = animated foam UV (pre-computed in vertex shader)
// waterDepth = water column depth from KF_SampleWaterDepth
// Returns 0–1 mask. Tune _FoamCutoff in [0.65–0.85] range.
// ─────────────────────────────────────────────────────────────────────────────
// posWS = world-space position (from KF_Varyings) — used to drive second layer
// at a different scale and independent scroll so the tile pattern is broken.
float KF_SurfaceFoamMask(float2 uvFoam, float3 posWS)
{
    // Layer 1: mesh-UV based, scrolls with _NormalSpeed (set in vertex shader)
    float f1 = SAMPLE_TEXTURE2D(_FoamTexture, sampler_FoamTexture, uvFoam).r;

    // Layer 2: world-XZ based, different tiling + independent counter-scroll
    // This layer moves at a different rate regardless of mesh UV layout.
    float2 uvWorld = posWS.xz * (_FoamTiling * 0.6) + _Time.y * _NormalSpeed * float2(-0.018, 0.011);
    float f2 = SAMPLE_TEXTURE2D(_FoamTexture, sampler_FoamTexture, uvWorld).r;

    float foamSample = (f1 + f2) * 0.5;
    return smoothstep(_FoamCutoff, _FoamCutoff + 0.15, foamSample);
}

// ─── KF_CombinedFoamMask ─────────────────────────────────────────────────────
// Combines surface foam with intersection foam from depth.
// Intersection foam is multiplied by a slow foam texture sample so it produces
// chunky foam chunks at shorelines/objects instead of a plain white gradient.
// Returns the final 0–1 foam mask to apply over the water color.
// ─────────────────────────────────────────────────────────────────────────────
float KF_CombinedFoamMask(float2 uvFoam, float3 posWS, float4 screenPos, float waterDepth)
{
    float surface = KF_SurfaceFoamMask(uvFoam, posWS);

    // Intersection gradient from depth (sky-guarded)
    float intGradient = KF_IntersectionFoam(screenPos, waterDepth);

    // Mask gradient by foam texture at a coarser, slower scroll — chunky foam effect
    float2 uvInt = posWS.xz * (_FoamTiling * 0.4) + _Time.y * _NormalSpeed * float2(0.012, -0.008);
    float  intTex = SAMPLE_TEXTURE2D(_FoamTexture, sampler_FoamTexture, uvInt).r;
    // Lower threshold so intersection foam always shows some chunks even at low cutoff
    float  intMask = smoothstep(0.35, 0.55, intTex) * intGradient;

    return saturate(max(surface, intMask));
}

// ─── KF_FoamColor ────────────────────────────────────────────────────────────
// Returns the final foam contribution (color × mask).
// Includes emissive enhancement for the KF_EMISSIVE_FOAM feature.
// In M1 emissive foam is always active (keyword gating added at M2).
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_FoamColor(float foamMask)
{
    float3 baseFoam     = _FoamColor.rgb * foamMask;
    float3 emissiveFoam = _EmissiveFoamColor.rgb * _EmissiveFoamIntensity * foamMask;
    return baseFoam + emissiveFoam;
}

#endif // KF_WATERKIT_FOAM_INCLUDED
