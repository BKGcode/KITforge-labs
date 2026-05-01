# KF_WaterKit — Keyword Lock (M0)
Locked: 2026-04-13
Architecture ref: KF_WaterKit_Architecture.md Section 4

## Regla
Esta lista está CERRADA desde M0. Añadir un keyword = Architecture revision obligatoria.
Cualquier nueva feature en v2 debe reemplazar un keyword existente o abrir una nueva revisión.

## Keywords activos

### multi_compile_local (variante siempre generada si el keyword existe en el material)
| Keyword | Tier | Feature |
|---|---|---|
| `KF_REFRACTION` | Full only | Refracción (opaque texture copy) |
| `KF_CAUSTICS` | Full only | Caustics animados (texture) |
| `KF_SPARKLES` | Full only | Sun scatter / sparkles |
| `KF_REFLECTIONS` | Full only | Planar reflections / SSR |
| `KF_EMISSIVE_FOAM` | Full only | Foam emisivo (Lava preset) |

### shader_feature_local (variante solo si el material activa el keyword)
| Keyword | Tier | Feature |
|---|---|---|
| `KF_FLAT_SHADING` | Full + Lite | Flat shading (lowpoly mode) |
| `KF_RIVER_MODE` | Full + Lite | Directional flow (float2 + optional FlowMap) |

## Conteo de variantes
- multi_compile_local: 2^5 = 32 variantes teóricas
- shader_feature_local: solo si se usan en el proyecto (stripped at build)
- Variantes reales esperadas en producción: < 20 (la mayoría de proyectos no activarán todos los Full keywords)

## Lite configuration (keywords OFF)
Un material en modo Lite tiene desactivados: KF_REFRACTION, KF_CAUSTICS, KF_SPARKLES, KF_REFLECTIONS, KF_EMISSIVE_FOAM
KF_FLAT_SHADING y KF_RIVER_MODE pueden estar activos en Lite si el preset los usa.
