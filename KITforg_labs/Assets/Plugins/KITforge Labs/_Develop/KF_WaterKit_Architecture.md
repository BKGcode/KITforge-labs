# Architecture — KF_WaterKit
Status: APPROVED
Phase: ARCHITECTURE → BUILD
Parent Brief: KF_WaterKit_Brief.md (APPROVED 2026-04-13)
Date created: 2026-04-13
Date approved: 2026-04-13

---

## Regla de este documento

Si la arquitectura no puede describirse con texto en 2 páginas, el producto está sobrediseñado.
KISS antes de SOLID. Una clase, una responsabilidad. Escalar solo si el uso lo justifica.

---

## 1. Assembly Definitions

| Assembly | Nombre | Plataformas | Referencia a | Ubicación |
|---|---|---|---|---|
| Editor | `KITforgeLabs.WaterKit.Editor` | Editor only | — | `KF_WaterKit/Editor/` |
| Tests | `KITforgeLabs.WaterKit.Tests` | Editor only | Editor | `_Develop/Tests/KF_WaterKit_Tests/` |

**No Runtime assembly.** WaterKIT es 100% shader + editor tooling. No hay C# runtime en v1 (no buoyancy API, no wave height query — out of scope).

---

## 2. Namespaces

```
KITforgeLabs.Editor.WaterKit        ← CustomEditor, Setup Window, Preset Library, Keywords
KITforgeLabs.Editor.WaterKit.Tests  ← tests de editor
```

---

## 3. Inventario de clases

| Clase | Responsabilidad única | Base Unity | Namespace | Archivo |
|---|---|---|---|---|
| `KF_WaterKitShaderEditor` | Inspector 3-tab para el material del shader | `MaterialEditor` (IMGUI) | Editor.WaterKit | `Editor/KF_WaterKitShaderEditor.cs` |
| `KF_WaterKitSetupWindow` | EditorWindow de primera importación: valida URP + versión | `EditorWindow` (UI Toolkit) | Editor.WaterKit | `Editor/KF_WaterKitSetupWindow.cs` |
| `KF_WaterKitPresetLibrary` | Carga y cachea los 10 preset `.mat` + thumbnails `Texture2D` | `static class` | Editor.WaterKit | `Editor/KF_WaterKitPresetLibrary.cs` |
| `KF_WaterKitKeywords` | Constantes de string para los 7 keywords del shader | `static class` | Editor.WaterKit | `Editor/KF_WaterKitKeywords.cs` |

**Total: 4 clases C#.** Sin MonoBehaviours, sin ScriptableObjects, sin singletons.

**Clases que NO crearé (YAGNI):**
- `KF_WaterKitManager` — no hay estado runtime que gestionar
- `KF_WaterKitSettingsSO` — no hay configuración por proyecto; las preferencias son EditorPrefs por usuario
- `KF_WaterKitAPI` — buoyancy / wave height query = out of scope v1
- `KF_WaterKitValidator` separado — la validación de URP vive inline en `KF_WaterKitSetupWindow`

---

## 4. Keywords del shader (locked — M0)

Cambiar keywords después de publicar = breaking change para proyectos de clientes. Lista cerrada.

```
// En KF_WaterKitKeywords.cs
public const string REFRACTION      = "KF_REFRACTION";       // Full only
public const string CAUSTICS        = "KF_CAUSTICS";          // Full only
public const string SPARKLES        = "KF_SPARKLES";          // Full only
public const string REFLECTIONS     = "KF_REFLECTIONS";       // Full only
public const string EMISSIVE_FOAM   = "KF_EMISSIVE_FOAM";     // Full only
public const string FLAT_SHADING    = "KF_FLAT_SHADING";      // Full + Lite (free)
public const string RIVER_MODE      = "KF_RIVER_MODE";        // Full + Lite (free)
```

**Nota:** Son 7 keywords (D1 dijo "6 max" como presupuesto inicial). `KF_FLAT_SHADING` y `KF_RIVER_MODE` son `shader_feature` (no `multi_compile`): solo generan variante si el proyecto las usa. No impactan el variant count base. El presupuesto real es 5 `multi_compile` + 2 `shader_feature`. Dentro del límite.

**Declaración en el shader:**
```hlsl
// multi_compile → variante siempre generada si keyword existe en el material
#pragma multi_compile_local _ KF_REFRACTION
#pragma multi_compile_local _ KF_CAUSTICS
#pragma multi_compile_local _ KF_SPARKLES
#pragma multi_compile_local _ KF_REFLECTIONS
#pragma multi_compile_local _ KF_EMISSIVE_FOAM

// shader_feature → variante solo si el material la activa
#pragma shader_feature_local KF_FLAT_SHADING
#pragma shader_feature_local KF_RIVER_MODE
```

---

## 5. API surface pública

```csharp
// KF_WaterKitShaderEditor — nada público salvo lo heredado de MaterialEditor
// Unity invoca OnGUI() internamente; el usuario no llama nada de esta clase directamente.

// KF_WaterKitSetupWindow
public static void Open();                      // llamado por [InitializeOnLoad] y MenuItem

// KF_WaterKitPresetLibrary
public static WaterPreset[] GetAll();           // retorna los 10 presets cacheados
public static void ApplyToMaterial(Material target, WaterPreset preset);  // copia props + keywords

// KF_WaterKitKeywords
// Solo constantes públicas — ver sección 4
```

**`WaterPreset`** — struct (no clase) con:
```
string Name          // "Ocean — Tropical"
Texture2D Thumbnail  // 512x512, usado en Tab 1 grid
Material Source      // el .mat de referencia cargado desde AssetDatabase
bool RequiresFull    // true si algún keyword Full-only está activo en Source
```

Todo lo no listado aquí es `private`.

---

## 6. Flujo de datos

### Inspector — Tab 1 (Presets)
```
Usuario selecciona material de agua en Project/Scene
    ↓
Unity invoca KF_WaterKitShaderEditor.OnInspectorGUI()
    ↓
Tab 1 activo →
  KF_WaterKitPresetLibrary.GetAll() → WaterPreset[] (cacheado en static)
  Renderizar grid: 3 columnas, thumbnails 80x80, nombre debajo
  [si preset.RequiresFull y material en Lite → thumbnail + icono 🔒]
    ↓
Usuario hace click en preset →
  [si material fue modificado respecto a preset base]
    → EditorUtility.DisplayDialog("Overwrite settings?", ...) → Cancel = no action
  [si confirma o material no fue modificado]
    → KF_WaterKitPresetLibrary.ApplyToMaterial(material, preset)
    → Undo.RecordObject(material, "Apply WaterKIT Preset")  ← registrar ANTES de modificar
    → Copiar todas las propiedades del preset.Source al material target
    → Copiar estado de keywords
    → EditorUtility.SetDirty(material)
```

### Inspector — Tab 3 (Performance: Full ↔ Lite toggle)
```
Usuario cambia dropdown Full/Lite →
  Undo.RecordObject(material, "WaterKIT Mode Switch")
  Full → Lite:
    EnableKeyword(KF_FLAT_SHADING) y KF_RIVER_MODE si estaban activos: preservado
    DisableKeyword(KF_REFRACTION, KF_CAUSTICS, KF_SPARKLES, KF_REFLECTIONS, KF_EMISSIVE_FOAM)
    Valores de propiedades: NO tocar. Solo keywords.
  Lite → Full: re-enable keywords que correspondan al preset base o al estado previo.
  EditorUtility.SetDirty(material)
```

**Invariante: el toggle NUNCA resetea valores de propiedades. Solo activa/desactiva keywords.**

### Setup Window (primera importación)
```
[InitializeOnLoad] KF_WaterKitSetupWindow static constructor
    ↓
EditorApplication.delayCall += () => {
  bool alreadyShown = EditorPrefs.GetBool("KF_WaterKit_v1_shown_" + PlayerSettings.productName)
  if (!alreadyShown) → KF_WaterKitSetupWindow.Open()
}
    ↓
Window.CreateGUI() [UI Toolkit]:
  Validar Unity version >= 6000.0.60f1
  Validar URPAsset asignado al renderer activo
  Validar Render Graph Compatibility Mode = disabled
  → Mostrar ✅ / ⚠️ / 🔴 por cada check
  [Fix automatically] si URP Asset falta → intenta asignar el primero en el proyecto
    ↓
[Open Quick Showcase Scene] → EditorSceneManager.OpenScene(showcasePath, OpenSceneMode.Single)
[Close] → EditorPrefs.SetBool("KF_WaterKit_v1_shown_" + PlayerSettings.productName, true)
```

---

## 7. Persistencia de estado

| Dato | Scope | Mecanismo | Por qué |
|---|---|---|---|
| Setup Window mostrada | Por proyecto/usuario | `EditorPrefs` keyed by `productName` | No molestar en cada domain reload |
| Thumbnail cache | Por sesión | `static WaterPreset[]` en `KF_WaterKitPresetLibrary` | Lazy-reload en null check — barato (10 texturas pequeñas) |
| Parámetros Full-only cuando está en Lite | En el material | Material properties en `.mat` file | Unity serializa todo aunque el keyword esté off |

**Domain reload safety:**
- `KF_WaterKitPresetLibrary._cache` static → se pierde en domain reload → `GetAll()` hace null check y recarga. Coste: 10 x `AssetDatabase.LoadAssetAtPath` = despreciable.
- `[InitializeOnLoad]` re-ejecuta su constructor tras cada domain reload → el check de EditorPrefs evita que el Setup Window vuelva a abrirse.
- `KF_WaterKitShaderEditor` no tiene estado serializado — se recrea en cada selección del material.

---

## 8. CBUFFER layout (crítico para SRP Batcher)

**Regla:** Todas las propiedades del material deben estar dentro de `CBUFFER_START(UnityPerMaterial)`. Si una propiedad queda fuera, el SRP Batcher marca el material como "Not compatible" — silenciosamente.

```hlsl
CBUFFER_START(UnityPerMaterial)
    // Appearance
    half4 _ShallowColor;
    half4 _DeepColor;
    half  _DepthFade;
    half  _Transparency;
    half  _NormalStrength;
    half  _NormalTiling;
    half  _NormalSpeed;

    // Waves
    half  _WaveSpeed;
    half  _WaveScale;
    half2 _FlowDirection;         // River mode: global float2
    
    // Foam
    half4 _FoamColor;
    half  _FoamTiling;
    half  _FoamCutoff;
    half  _IntersectionFoamWidth;

    // Special FX (Full only — valores preservados en Lite)
    half  _RefractionStrength;
    half  _CausticsScale;
    half  _CausticsSpeed;
    half  _CausticsIntensity;
    half  _SparkleIntensity;
    half  _SparkleScale;
    half4 _EmissiveFoamColor;
    half  _EmissiveFoamIntensity;

    // Textures — declaradas FUERA del CBUFFER (texturas nunca van dentro)
CBUFFER_END

// Texture declarations (outside CBUFFER — Unity requirement)
TEXTURE2D(_NormalMap);            SAMPLER(sampler_NormalMap);
TEXTURE2D(_FoamTexture);          SAMPLER(sampler_FoamTexture);
TEXTURE2D(_CausticsTexture);      SAMPLER(sampler_CausticsTexture);
TEXTURE2D(_FlowMap);              SAMPLER(sampler_FlowMap);
```

**Propiedad count estimada: ~22 escalares + 4 texturas.** Tab 2 con 5 secciones colapsables = ~4-5 fields por sección. Manejable.

---

## 9. Estructura de carpetas del producto

```
Assets/Plugins/KITforge Labs/KF_WaterKit/
├── Editor/
│   ├── KF_WaterKitShaderEditor.cs
│   ├── KF_WaterKitSetupWindow.cs
│   ├── KF_WaterKitSetupWindow.uxml
│   ├── KF_WaterKitSetupWindow.uss
│   ├── KF_WaterKitPresetLibrary.cs
│   ├── KF_WaterKitKeywords.cs
│   └── KITforgeLabs.WaterKit.Editor.asmdef
├── Shaders/
│   ├── KF_WaterKit.shader
│   └── Includes/
│       ├── KF_WaterKit_Input.hlsl       ← CBUFFER + texture declarations
│       ├── KF_WaterKit_Depth.hlsl       ← depth color gradient + intersection foam
│       ├── KF_WaterKit_Waves.hlsl       ← normal animation + flow direction
│       ├── KF_WaterKit_Foam.hlsl        ← surface foam + intersection foam
│       └── KF_WaterKit_SpecialFX.hlsl   ← refraction, caustics, sparkles, reflections, emissive
├── Materials/
│   └── Presets/
│       ├── KF_Ocean_Tropical.mat
│       ├── KF_Ocean_Stormy.mat
│       ├── KF_Lake_Misty.mat
│       ├── KF_Lake_Clear.mat
│       ├── KF_River_Autumn.mat
│       ├── KF_Arctic_Ice.mat
│       ├── KF_Swamp_Dark.mat
│       ├── KF_Toon_Cartoon.mat
│       ├── KF_Lowpoly_Flat.mat
│       └── KF_Lava.mat                  ← Full only (KF_EMISSIVE_FOAM required)
├── Textures/
│   ├── Previews/                        ← 512x512 PNG, una por preset (loaded in Tab 1)
│   │   ├── Preview_Ocean_Tropical.png
│   │   └── ... (10 total)
│   └── FlowMaps/
│       └── KF_FlowMap_Example.png       ← flow map de ejemplo incluido en P2
├── Meshes/
│   └── KF_WaterPlane_8km.fbx            ← mesh estático para demo + buyer use
├── Demo/
│   ├── Demo_KF_WaterKit.unity
│   └── DemoAssets/                      ← terreno, skybox, luz del demo
├── Documentation/
│   └── QuickStart.md
└── README.md
```

**Dev folder (fuera del producto, no se incluye en el package):**
```
Assets/Plugins/KITforge Labs/_Develop/KF_WaterKit_dev/
├── Scenes/
│   └── Dev_KF_WaterKit_ShaderTest.unity
└── BenchmarkScene/
    └── Dev_KF_WaterKit_Benchmark.unity  ← usada en M2 para profiling
```

---

## 10. Dependencias

**Unity APIs:**
- `UnityEditor.MaterialEditor` + `MaterialProperty` — CustomEditor de material
- `UnityEditor.EditorWindow` — Setup Window
- `UnityEditorInternal.UnityEditorInternal` — no; solo las APIs listadas
- `UnityEngine.Rendering.Universal` — `UniversalRenderPipelineAsset` (validación en Setup Window)
- `UnityEditor.AssetDatabase` — carga de preset .mat y thumbnails
- `UnityEditor.Undo` — registro de cambios en material antes de Apply
- `UnityEditor.EditorUtility.DisplayDialog` — confirmación de Apply
- `UnityEditor.EditorPrefs` — estado del Setup Window
- `UnityEditor.SceneManagement.EditorSceneManager` — abrir Quick Showcase Scene

**Packages de Unity (mínimos requeridos):**
- `com.unity.render-pipelines.universal` ≥ 17.0 (Unity 6000.0.60f1 bundled version)

**Dependencias de otros productos KITforge:** NINGUNA.

---

## 11. Riesgos técnicos confirmados

| Riesgo | Mitigación en arquitectura |
|---|---|
| CBUFFER incompleto → SRP Batcher incompatible | CBUFFER layout definido aquí (Section 8). Verificar en M2 con Frame Debugger antes de cualquier otra cosa. |
| IMGUI thumbnail grid lento sin caché | `KF_WaterKitPresetLibrary._cache` static con lazy-load. Null check en `GetAll()`. Coste max = 1 reload por domain reload. |
| Setup Window se abre en cada domain reload | `EditorPrefs` key per project. `[InitializeOnLoad]` + `delayCall` con guard. |
| Unity 6 Compatibility Mode → shader no compila | `KF_WaterKitSetupWindow` comprueba `GraphicsSettings.renderPipelineAsset` + versión. Mensaje bloqueante claro. |
| Preset Apply sin Undo → trabajo perdido | `Undo.RecordObject(material, ...)` ANTES de cualquier modificación. Confirmado en flujo de datos (Section 6). |
| Keyword explosion por nuevas features en v2 | Keyword list locked en `KF_WaterKitKeywords.cs`. Cualquier nueva keyword en v2 = Architecture revision required. |

---

## Approval checklist

- [ ] 4 clases listadas con responsabilidad única clara
- [ ] No Runtime assembly — justificado
- [ ] Keywords locked en Section 4 con distinción multi_compile / shader_feature
- [ ] CBUFFER layout explícito — crítico para SRP Batcher
- [ ] API surface explícita — nada público sin razón
- [ ] Flujo de datos trazable: Tab 1, Tab 3, Setup Window
- [ ] Domain reload safety resuelta para cada pieza de estado
- [ ] Dependencias de Unity APIs listadas exhaustivamente
- [ ] Estructura de carpetas definida incluyendo Includes/ del shader
- [ ] Dev folder separado del producto (no se incluye en el package)
