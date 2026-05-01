// KF_WaterKit.shader
// KITforgeLabs — WaterKIT
// Production-ready stylized water for Unity 6 URP.
// Requires: Universal Render Pipeline 17.0+ | Render Graph enabled | Unity 6000.0.60f1+
//
// M1 state: All Full-tier features active inline. No keyword guards yet.
// Keyword gating (#pragma multi_compile / shader_feature) added in M2.
//
// SRP Batcher: All material properties in KF_WaterKit_Input.hlsl CBUFFER.
// Opaque Texture + Depth Texture must be enabled in the URP Renderer asset.
// The Setup Window (M4) validates this automatically.
Shader "KITforgeLabs/WaterKIT"
{
    Properties
    {
        [Header(Appearance)]
        _ShallowColor           ("Shallow Color",                   Color)   = (0.2, 0.85, 0.75, 1.0)
        _DeepColor              ("Deep Color",                      Color)   = (0.02, 0.12, 0.18, 1.0)
        _DepthFade              ("Depth Fade (world units)",        Float)   = 4.0
        _Transparency           ("Transparency",                    Float)   = 1.2

        [Header(Normals)]
        _NormalMap              ("Normal Map (reuse for both layers)", 2D)   = "bump" {}
        _NormalStrength         ("Normal Strength",                 Float)   = 0.6
        _NormalTiling           ("Normal Tiling",                   Float)   = 0.08
        _NormalSpeed            ("Normal Speed",                    Float)   = 0.15

        [Header(Waves)]
        _WaveSpeed              ("Wave Speed",                      Float)   = 0.5
        _WaveScale              ("Wave Scale",                      Float)   = 0.3
        _FlowDirection          ("Flow Direction XY (River mode)",  Vector)  = (0, 0, 0, 0)

        [Header(Foam)]
        _FoamTexture            ("Foam Texture",                    2D)      = "white" {}
        _FoamColor              ("Foam Color",                      Color)   = (1.0, 1.0, 0.95, 1.0)
        _FoamTiling             ("Foam Tiling",                     Float)   = 0.3
        _FoamCutoff             ("Foam Cutoff",                     Float)   = 0.65
        _IntersectionFoamWidth  ("Intersection Foam Width",         Float)   = 0.4

        [Header(Caustics and Sparkles)]
        _CausticsTexture        ("Caustics Texture",                2D)      = "white" {}
        _CausticsScale          ("Caustics Scale",                  Float)   = 1.5
        _CausticsSpeed          ("Caustics Speed",                  Float)   = 0.2
        _CausticsIntensity      ("Caustics Intensity",              Float)   = 0.65
        _SparkleIntensity       ("Sparkle Intensity",               Float)   = 0.8
        _SparkleScale           ("Sparkle Scale",                   Float)   = 90.0

        [Header(Specular and Reflection)]
        _SpecularPower          ("Specular Power",                  Float)   = 128.0
        _SpecularIntensity      ("Specular Intensity",              Float)   = 0.9

        [Header(Refraction)]
        _RefractionStrength     ("Refraction Strength",             Float)   = 0.04

        [Header(River)]
        _FlowMap                ("Flow Map",                        2D)      = "white" {}

        [Header(Emissive Foam)]
        _EmissiveFoamColor      ("Emissive Foam Color",             Color)   = (1.0, 0.8, 0.0, 1.0)
        _EmissiveFoamIntensity  ("Emissive Foam Intensity",         Float)   = 0.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"      = "Transparent"
            "Queue"           = "Transparent"
            "RenderPipeline"  = "UniversalPipeline"
            "IgnoreProjector" = "True"
        }

        // ── Shared declarations for all passes (SRP Batcher requires identical CBUFFER) ──
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Includes/KF_WaterKit_Input.hlsl"
        ENDHLSL

        // ── UniversalForward ─────────────────────────────────────────────────
        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex   KF_WaterVert
            #pragma fragment KF_WaterFrag

            // Required URP keyword variants
            #pragma multi_compile_fog
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_instancing

            // Feature keywords — EMPTY in M1 (all features active inline).
            // Populated in M2:
            //   #pragma multi_compile_local _ KF_REFRACTION
            //   #pragma multi_compile_local _ KF_CAUSTICS
            //   #pragma multi_compile_local _ KF_SPARKLES
            //   #pragma multi_compile_local _ KF_REFLECTIONS
            //   #pragma multi_compile_local _ KF_EMISSIVE_FOAM
            //   #pragma shader_feature_local  KF_FLAT_SHADING
            //   #pragma shader_feature_local  KF_RIVER_MODE

            // Lighting.hlsl is included inside KF_WaterKit_SpecialFX.hlsl — do not include here.
            #include "Includes/KF_WaterKit_Waves.hlsl"
            #include "Includes/KF_WaterKit_Depth.hlsl"
            #include "Includes/KF_WaterKit_Foam.hlsl"
            #include "Includes/KF_WaterKit_SpecialFX.hlsl"

            // ── Vertex Shader ────────────────────────────────────────────────
            KF_Varyings KF_WaterVert(KF_Attributes IN)
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                KF_Varyings OUT;
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);

                // World position before any displacement (for wave input)
                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);

                // Vertex wave: displace Y in object space
                // Note: We modify posOS.y before GetVertexPositionInputs for correct
                // ShadowCaster matching (added in M2 — for now forward pass only).
                float3 posOS = IN.positionOS.xyz;
                posOS.y += KF_VertexWave(worldPos);

                VertexPositionInputs vpi = GetVertexPositionInputs(posOS);
                VertexNormalInputs   vni = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);

                OUT.positionCS  = vpi.positionCS;
                OUT.positionWS  = vpi.positionWS;
                OUT.screenPos   = ComputeScreenPos(vpi.positionCS);

                // TBN uses undisplaced mesh normals — acceptable at low WaveScale.
                // High amplitude (_WaveScale > 1) = lighting artifacts at wave crests.
                // Procedural normal derivation from displaced geometry = v2 scope.
                OUT.normalWS    = vni.normalWS;
                OUT.tangentWS   = vni.tangentWS;
                OUT.bitangentWS = vni.bitangentWS;

                // Pre-compute animated UV sets (avoids dependent reads on mobile)
                KF_ComputeAnimatedUVs(IN.uv, worldPos,
                    OUT.uvNormal1, OUT.uvNormal2, OUT.uvFoam);

                return OUT;
            }

            // ── Fragment Shader ──────────────────────────────────────────────
            half4 KF_WaterFrag(KF_Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);

                // ── 1. Depth ─────────────────────────────────────────────────
                float waterDepth = KF_SampleWaterDepth(IN.screenPos);
                float alpha      = KF_DepthAlpha(waterDepth);

                // ── 2. Normals ───────────────────────────────────────────────
                float3 normalTS = KF_SampleNormals(IN.uvNormal1, IN.uvNormal2);
                float3 normalWS = KF_TangentToWorld(normalTS, IN.normalWS, IN.tangentWS, IN.bitangentWS);

                float3 viewDirWS = normalize(GetWorldSpaceViewDir(IN.positionWS));

                // ── 3. Refraction (sample background before color blending) ──
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                float3 bgColor  = KF_Refraction(screenUV, normalWS);

                // ── 4. Depth color gradient ──────────────────────────────────
                float3 waterColor = KF_DepthColor(waterDepth);

                // Blend: refracted background shows in shallow transparent water
                float  blendT = saturate(waterDepth / (_DepthFade * 0.6));
                float3 col    = lerp(bgColor, waterColor, saturate(blendT * _Transparency));

                // ── 5. Caustics (additive, shallow water) ────────────────────
                col += KF_Caustics(IN.positionWS, waterDepth);

                // ── 6. Specular highlight ────────────────────────────────────
                col += KF_Specular(normalWS, viewDirWS);

                // ── 7. Environment reflection ────────────────────────────────
                col += KF_Reflection(normalWS, viewDirWS, IN.positionWS);

                // ── 8. Sparkles (sun glints — additive gold) ─────────────────
                float  sparkleMask = KF_Sparkles(IN.positionWS, normalWS, viewDirWS, waterDepth);
                col  += sparkleMask * float3(1.0, 0.82, 0.42);

                // ── 9. Foam (alpha-blend over water color) ───────────────────
                float  foamMask  = KF_CombinedFoamMask(IN.uvFoam, IN.positionWS, IN.screenPos, waterDepth);
                float3 foamColor = KF_FoamColor(foamMask);
                col = lerp(col, foamColor, foamMask * _FoamColor.a);

                // ── 10. Fog ──────────────────────────────────────────────────
                float fogFactor = ComputeFogFactor(IN.positionCS.z);
                col = MixFog(col, fogFactor);

                return float4(col, alpha);
            }

            ENDHLSL
        }
    }

    FallBack Off
    CustomEditor "KITforgeLabs.Editor.WaterKit.KF_WaterKitShaderEditor"
}
