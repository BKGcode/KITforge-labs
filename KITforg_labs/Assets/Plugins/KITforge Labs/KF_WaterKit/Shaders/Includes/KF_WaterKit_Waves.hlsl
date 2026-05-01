// KF_WaterKit_Waves.hlsl
// KITforgeLabs — WaterKIT
// Vertex wave displacement + animated normal map blending + flow direction.
// Requires: KF_WaterKit_Input.hlsl included first.

#ifndef KF_WATERKIT_WAVES_INCLUDED
#define KF_WATERKIT_WAVES_INCLUDED

// ─── KF_VertexWave ───────────────────────────────────────────────────────────
// Applies subtle sine-based vertex displacement on the Y axis.
// posOS     = original object-space position
// worldXZ   = world-space XZ coordinates for world-coherent wave pattern
// Returns modified object-space Y offset to add to posOS.y.
// Note: uses float for world positions per precision rules.
// ─────────────────────────────────────────────────────────────────────────────
float KF_VertexWave(float3 worldPos)
{
    float scale  = _WaveScale;
    float speed  = _WaveSpeed;

    // Two sine waves at different angles and frequencies for interference pattern
    float w1 = sin(worldPos.x * scale        + _Time.y * speed);
    float w2 = sin(worldPos.z * scale * 0.7  + _Time.y * speed * 0.85 + 1.3);
    float w3 = sin((worldPos.x + worldPos.z) * scale * 0.4 + _Time.y * speed * 0.6 + 2.5);

    // Displace amplitude: ~0.1 at _WaveScale = 1, scales linearly
    return (w1 + w2 + w3) * 0.033 * scale;
}

// ─── KF_ComputeAnimatedUVs ───────────────────────────────────────────────────
// Computes all pre-animated UV sets in the vertex shader.
// Called per-vertex; results interpolated to fragment (no dependent texture reads).
// baseUV = mesh UV0 (world-scaled or [0,1] quad UV)
// ─────────────────────────────────────────────────────────────────────────────
void KF_ComputeAnimatedUVs(float2 baseUV, float3 worldPos,
                            out float2 uvNormal1, out float2 uvNormal2,
                            out float2 uvFoam)
{
    float t = _Time.y;

    // Layer 1: fine detail, scrolls +X +Y slightly
    // Internal multipliers scaled *0.1 so _NormalSpeed=1 is a comfortable default.
    uvNormal1 = baseUV * _NormalTiling + t * _NormalSpeed * float2(0.040,  0.070);

    // Layer 2: coarser, counter-scrolls to create interference ripples
    uvNormal2 = baseUV * (_NormalTiling * 0.6) + t * _NormalSpeed * float2(-0.060, 0.030);

    // Foam UV: separate tiling, slow scroll to avoid lock-step with normals
    uvFoam = baseUV * _FoamTiling + t * _NormalSpeed * 0.02;

    // River mode: offset both normal layers by global flow direction
    // KF_RIVER_MODE keyword gating happens in KF_WaterKit.shader (M2).
    // In M1 all features are always active.
    float2 flowOffset = _FlowDirection.xy * t * 0.1;
    uvNormal1 += flowOffset;
    uvNormal2 += flowOffset * 0.8;
}

// ─── KF_FlowMapUV ────────────────────────────────────────────────────────────
// (Optional) Samples the Flow Map texture to get a per-pixel flow direction,
// then returns a distorted UV. Called in the fragment shader in full River mode.
// uv = base UV, uvFoam used as proxy for flow map UV (same tiling).
// Returns a flow-offset UV to apply on top of the base.
// ─────────────────────────────────────────────────────────────────────────────
float2 KF_FlowMapOffset(float2 uvFoam)
{
    // Flow map: R = flow X, G = flow Y, range [0,1] remapped to [-1,1]
    float2 flowVec = SAMPLE_TEXTURE2D(_FlowMap, sampler_FlowMap, uvFoam).rg * 2.0 - 1.0;
    return flowVec * _NormalSpeed * _Time.y * 0.03;
}

// ─── KF_SampleNormals ────────────────────────────────────────────────────────
// Blends two normal map layers to produce a detailed animated water surface.
// Returns tangent-space normal, normalized. All float — see KF_WaterKit_Depth.hlsl note.
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_SampleNormals(float2 uvN1, float2 uvN2)
{
    float3 n1 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uvN1));
    float3 n2 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uvN2));

    // Reoriented Normal Blending: better than additive for multi-layer normals
    // Reference: https://blog.selfshadow.com/publications/blending-in-detail/
    float3 blended = float3(n1.xy + n2.xy, n1.z * n2.z);
    blended = normalize(blended);

    // Lerp toward flat (0,0,1) to control overall strength
    return normalize(lerp(float3(0.0, 0.0, 1.0), blended, _NormalStrength));
}

// ─── KF_TangentToWorld ───────────────────────────────────────────────────────
// Converts a tangent-space normal to world space using the interpolated TBN.
// normalTS = tangent-space normal from KF_SampleNormals
// ─────────────────────────────────────────────────────────────────────────────
float3 KF_TangentToWorld(float3 normalTS, float3 normalWS, float3 tangentWS, float3 bitangentWS)
{
    float3x3 tbn = float3x3(tangentWS, bitangentWS, normalWS);
    return normalize(mul(normalTS, tbn));
}

#endif // KF_WATERKIT_WAVES_INCLUDED
