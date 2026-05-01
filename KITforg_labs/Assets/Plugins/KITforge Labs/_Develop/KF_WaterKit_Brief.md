# Brief — KF_WaterKit
Status: APPROVED
Phase: BRIEF → ARCHITECTURE
Score: —/35
Tier: Complex
Date created: 2026-04-13
Date approved: 2026-04-13

> Originated from: `_Lab/_planner_workbook_KF_WaterKit.md` — DISCOVER ✅ ALIGN ✅ STRESS TEST ✅ PLAN ✅

---

## 0. Identidad

| Campo | Valor |
|-------|-------|
| Slug interno | `KF_WaterKit` |
| Nombre público | **WaterKIT** |
| One-liner | Production-ready stylized mobile water. Presets. One price. |
| Categoría Asset Store | VFX > Shaders |
| Precio | €40 (all-in, no extensions) |
| Tier de complejidad | Complex (HLSL shader + CustomEditor 3-tab + Setup Window) |
| Wave | — (shader lab, fuera del backlog de editor tools) |

---

## 1. Objetivo

**Problema concreto:**
El indie developer 3D en Unity 6 mobile tiene una escena con un lago, río u océano. El agua por defecto es una mancha azul plana. Stylized Water 3 (el referente del mercado) cuesta €45 de base y €86 para la experiencia completa — y cuando lo importa, no sabe qué prefab arrastrar ni cómo llegar al resultado que vio en el store. Pasa 30 minutos leyendo docs antes de ver su primer frame de agua decente.

**Buyer profile:**
- Tipo de dev: Indie 3D, estudio pequeño, también TAs que hacen escenas de nivel
- Momento de uso: media pieza del desarrollo — la escena ya existe, el agua está pendiente
- Nivel técnico: intermedio (entiende Unity, no necesariamente shaders)
- Device target: Android/iOS mobile, presupuesto de GPU ajustado

**Una sola frase de valor:**
> *"WaterKIT permite al indie developer poner agua hermosa en escena en menos de 5 minutos, con presets que ya se ven bien por defecto, sin leer una página de documentación y sin pagar extensiones adicionales."*

---

## 2. Solución propuesta

**Enfoque técnico:**
- Shader HLSL hand-written para Unity 6 URP, Render Graph nativo
- Un shader + 6 keywords: Full→Lite = desactivar keywords (mismo asset, dos tiers de coste)
- 10 presets con identidad editorial (nombres + preview renders 512×512)
- Inspector 3 tabs: **Presets** (grid visual) / **Appearance** (param control) / **Performance** (Full↔Lite toggle + coste por feature)
- Setup Window en primera importación: valida URP config + versión Unity, ofrece Quick Showcase Scene
- River mode: float2 global direction + Flow Map texture slot opcional

**APIs de Unity principales:**
- URP Render Graph (Unity 6000.0.60f1+)
- `ShaderLab` + `HLSL` (no ShaderGraph)
- `MaterialEditor` / `MaterialProperty` (Custom Shader Inspector)
- `EditorUtility.DisplayDialog` (Apply preset confirmation)
- `InitializeOnLoad` (Setup Window)
- `AssetDatabase` (preset .mat loading en Tab 1)

**Explícitamente FUERA de scope (v1):**
- No 2D Renderer / Tilemaps
- No underwater camera effects
- No dynamic ripple system (physics interaction)
- No buoyancy API
- No HDRP
- No Built-in RP
- No tessellation
- No ocean mesh generator (mesh estático provisto)
- No caustics visibles desde cámara submarina
- No custom preset save (el TA no puede guardar sus variantes como preset en Tab 1) — v2

**Extensiones futuras (no v1):**
- Underwater Rendering (v2)
- Dynamic Effects — shoreline waves, ripples (v2)
- Custom Preset Save desde Inspector (v2)
- 2D Renderer support (v2)

---

## 3. Referencias

**Competidores directos:**

| Nombre | Precio | Reviews | Lo que hacen bien | Nuestra diferencia |
|---|---|---|---|---|
| **Stylized Water 3** (Staggart) | €45 base + €41 ext = **€86 full** | 117 (Unity 6 era) | HLSL optimizado, VR tested, 9 presets, excellent support | €40 all-in. Preset-first UX (visual grid, no blank material). Setup wizard. Published benchmarks. |
| **Stylized Water 2** (Staggart) | €32.20 | 2,662 | Enorme base instalada | No Unity 6 native. Staggart empuja a los users a SW3. Ese público es nuestro. |
| **Flat Kit** (Dustyroom) | €36.71 | 198 | Full toon art suite | Water es feature secundaria, no standalone shader. No compite directamente. |
| **Thalassophobia** (Distant Lands) | €27.60 | 14 | UX premium, preset-first assets | Environment pack (660 prefabs), no shader standalone. No Unity 6 native. Not a competitor. |

**Inspiraciones de UX:**
- Distant Lands / COZY Weather 3: preset-led, zero config to first result, premium Inspector, module-based philosophy
- SW3 Setup Validator: checkbox wizard de configuración de URP (SW3 tiene esto — nosotros debemos igualarlo y superarlo)
- GamerRealities review + Staggart reply: "Perhaps a 'Water wizard' would be a welcome addition" — el gap está confirmado por el propio competidor

**Documentación de Unity relevante:**
- [URP Render Graph](https://docs.unity3d.com/6000.0/Documentation/Manual/urp/render-graph.html)
- [SRP Batcher compatibility](https://docs.unity3d.com/Manual/SRPBatcher.html)
- [Custom Material Editors](https://docs.unity3d.com/Manual/editor-CustomEditorForMaterial.html)

---

## 4. Mejoras vs competencia

- **Vs SW3:** Un precio, experiencia completa. Sin paywall de extensiones. €40 vs €86.
- **Vs SW3 UX:** Preset-first desde el Inspector. El buyer no ve un material en blanco — ve un grid con 10 opciones llamadas "Ocean Tropical", "Swamp Dark", "Toon Cartoon". Click → Apply → agua en escena.
- **Vs SW3 confianza:** Published benchmark table por dispositivo (A54, A14). SW3 tiene cero números publicados. KF publica ms reales.
- **Ángulo único:** El único Unity 6 standalone water shader donde el primer resultado viene de elegir un mood, no de ajustar parámetros.

---

## 5. Posibles dificultades

| Riesgo | Probabilidad | Impacto | Mitigación |
|---|---|---|---|
| Render Graph API cambia entre Unity 6 minor versions | Media | Alto | Minimum required: 6000.0.60f1. Setup Window valida versión al importar y muestra aviso. |
| SRP Batcher rompe con CBUFFER mal declarado en shader | Alta (común) | Alto | Verificar CBUFFER layout antes de M2. Añadir a checklist de M2. |
| Keyword count explosion (> 6) | Media | Medio | Keyword list locked en M0. Cualquier nueva feature debe reemplazar un keyword existente, no sumar. |
| Inspector 3-tab en versiones old de Unity Editor | Baja | Bajo | Target Unity 6 only. No dar soporte a UI Toolkit en versiones previas. |
| Trust gap: 0 reviews en launch | Alta | Alto | Plan de mitigación: beta seeding (5-10 devs, Q5), demo scene excepcional, store video 60s. |
| Flow Map: el TA no sabe crear uno | Media | Medio | Incluir flow map de ejemplo en P2. Documentar en Quick Start. |

**Compatibilidad:**
- Versión mínima: **Unity 6000.0.60f1**
- URP requerido: **Sí** (Render Graph, no Compatibility Mode)
- HDRP: No
- Built-in RP: No
- Dependencias externas: ninguna

**Casos límite peligrosos:**
- Unity 6 Compatibility Mode activo → shader no compila. Setup Window debe detectarlo y bloquearlo con mensaje claro.
- Proyecto con múltiples materiales en Full aplicando Switch to Lite → estado inconsistente si el toggle no preserva valores.
- **IMGUI thumbnail grid en CustomEditor:** carga de `Texture2D` por preview vía `AssetDatabase.LoadAssetAtPath` sin caché → Inspector lento en cada repaint con 10 thumbnails. Requiere caché estática de texturas en el CustomEditor. Añadir a checklist de M4.

---

## 6. UI Design

**Tipo de interfaz:**
☑ CustomEditor / Inspector extension (shader material)
☑ EditorWindow standalone (Setup Window — primera importación)

**Sistema de UI:**
☑ IMGUI para el CustomEditor de material (MaterialEditor hereda de IMGUI — no UI Toolkit compatible)
☑ UI Toolkit para el Setup Window (EditorWindow de bienvenida)

**Inspector — Wireframe (3 tabs):**

```
┌─────────────────────────────────────────────────┐
│  WaterKIT Material Inspector                     │
├───────────────┬───────────────┬─────────────────┤
│  [PRESETS]    │ [APPEARANCE]  │  [PERFORMANCE]  │
├───────────────┴───────────────┴─────────────────┤
│                                                  │
│  TAB 1 — PRESETS                                 │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐        │
│  │[preview] │ │[preview] │ │[preview] │        │
│  │Ocean     │ │Ocean     │ │Lake      │        │
│  │Tropical  │ │Stormy    │ │Misty     │        │
│  └──────────┘ └──────────┘ └──────────┘        │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐        │
│  │[preview] │ │[preview] │ │[preview] │        │
│  │Lake      │ │River     │ │Arctic    │        │
│  │Clear     │ │Autumn    │ │Ice       │        │
│  └──────────┘ └──────────┘ └──────────┘        │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐        │
│  │[preview] │ │[preview] │ │[preview] │  🔒    │
│  │Swamp     │ │Toon      │ │Lowpoly   │  Lava  │
│  │Dark      │ │Cartoon   │ │Flat      │ (Full) │
│  └──────────┘ └──────────┘ └──────────┘        │
│                                                  │
│              [ Apply Preset ]                    │
├──────────────────────────────────────────────────┤
│  TAB 2 — APPEARANCE (collapsible sections)       │
│  ▼ Appearance    ▼ Waves & Flow                  │
│  ▼ Foam          ▼ Special Effects               │
│  ▼ Performance                                   │
├──────────────────────────────────────────────────┤
│  TAB 3 — PERFORMANCE                             │
│  Mode:  [ Full ▼ ]  ← dropdown                  │
│  "Switching modes hides params but does          │
│   not reset them."                               │
│                                                  │
│  Feature cost (this device tier):                │
│  ● Refraction .............. [TBD — M2]          │
│  ● Caustics ................ [TBD — M2]          │
│  ● Sparkles ................ [TBD — M2]          │
│  ● Reflections ............. [TBD — M2]          │
│  ● Emissive Foam ........... [TBD — M2]          │
│  ─────────────────────────────────               │
│  Total Full est. ........... [TBD — M2]          │
│  Total Lite est. ........... [TBD — M2]          │
└──────────────────────────────────────────────────┘
```

**Setup Window — Wireframe:**
```
┌──────────────────────────────────────────┐
│  Welcome to WaterKIT                     │
│  v1.0.0                                  │
├──────────────────────────────────────────┤
│  ✅ Unity 6000.0.60f1+ detected          │
│  ✅ URP active                           │
│  ✅ Render Graph enabled                 │
│  ⚠️  URP Asset not assigned to renderer  │
│      [Fix automatically]                 │
├──────────────────────────────────────────┤
│  [ Open Quick Showcase Scene ]           │
│  [ Open Documentation ]                  │
│                          [ Close ]       │
└──────────────────────────────────────────┘
```

---

## 7. UX Flow

**Primer uso** (tras importar, sin saber nada):
1. Import completa → Setup Window se abre automáticamente (`InitializeOnLoad`)
2. Setup Window comprueba URP config y Unity version → muestra ✅ o aviso con fix automático
3. User hace click en "Open Quick Showcase Scene"
4. Escena de demo carga → ve agua en 5 presets distintos en tiempo real
5. User selecciona un mesh de agua en su escena → Inspector muestra Tab 1 con grid
6. Click en "Ocean Tropical" → click "Apply Preset" → agua en escena
   *→ Momento aha aquí — ocurre en este paso, < 2 minutos desde import si el mesh ya existe en escena*
7. *(Flujo completo incluyendo Setup Window + demo scene: < 5 minutos)*

**Uso diario** (TA ajustando water para una escena específica):
1. Seleccionar water material → Inspector Tab 2 → abrir sección "Appearance" → ajustar Deep Color
2. Si cambia preset: Tab 1 → click preset → dialog de confirmación → Apply

**Uso avanzado** (dev optimizando para low-end device):
1. Tab 3 → ver tabla de coste por feature → toggle a Lite → valores preservados
2. Leer feature cost table → desactivar Reflections manualmente en Tab 2 si necesita más budget

**Momento "aha":**
> Inmediatamente al hacer Apply en Tab 1 por primera vez. El mesh de agua plano pasa a tener profundidad, foam, y animación. Sin ajustar un solo parámetro.

---

## 8. Checkpoints (Milestones)

| # | Checkpoint | Criterio de done |
|---|---|---|
| M0 | Architecture locked | Decision log completo, keyword list locked, folder scaffold creado, `.asmdef` definidas |
| M1 | Shader MVP | Screenshot "Ocean Tropical" aprobado por referencia visual gold-standard (imagen Sea of Thieves bloqueada antes de empezar M1). Sin keywords aún. |
| M2 | Full/Lite split + benchmarks | 6 keywords activos. SRP Batcher compatible verificado. Benchmark table publicada: Lite ≤ 0.8ms en A54, Full ≤ 2ms en A54. |
| M3 | Preset library | 10 presets `.mat` cargan en proyecto limpio. 8 Lite-compatibles, 10 Full. Preview renders 512×512 producidas. Zero console errors. |
| M4 | Inspector UX + Setup Window | User flow completo (Import → Apply preset → agua en escena) completado en < 5 minutos **sin abrir ningún doc**. Tab 2 con secciones colapsables. Apply dialog funciona. Full↔Lite preserva valores. Setup Window valida versión y URP. |
| M5 | Demo scene | Escena con 5 water bodies, ms counter visible en Play Mode. Suficiente para grabar el store video. |
| M6 | Launch package | Store submission pasa primera revisión. 3+ beta reviews existen al subir. |

---

## 9. Validador / QA

**Cómo verificamos que funciona:**
- Proyecto limpio Unity 6000.0.60f1 + URP 17.x: import sin errores
- Cada preset carga y se ve correcto (comparar contra preview render de referencia)
- Benchmark profiler en dispositivo físico A54 + A14

**Casos que NUNCA deben ocurrir:**
- El shader causa error de compilación en proyecto URP limpio
- Switch Full→Lite resetea valores de parámetros
- Apply Preset sin confirmation dialog cuando el material fue modificado
- Setup Window no se abre en primera importación (o se abre en re-importaciones)
- Tab 1 muestra preset Lava como disponible en Lite mode sin marcar que requiere Full
- El SRP Batcher muestra "Not compatible" en el Frame Debugger

**Datos de test necesarios:**
- Proyecto limpio Unity 6000.0.60f1 + URP (sin otros assets)
- Proyecto con Compatibility Mode activo (debe fallar con mensaje claro)
- Dispositivo físico Samsung A54 (o emulador con Profiler conectado)

**Demo scene** `Demo/Demo_KF_WaterKit.unity`:
- 5 water bodies: océano (Ocean Tropical), lago (Lake Misty), río (River Autumn), toon (Toon Cartoon), special (Lava — Full only)
- Ms counter overlay visible en Play Mode
- Direct sunlight + skybox que favorezca los reflejos

---

## 10. Tests

**Lógica unit-testeable (puro C#, sin Unity context):**
- Keyword enable/disable logic: dado un preset config, los keywords correctos se activan/desactivan
- Full vs Lite keyword matrix: para cada preset, qué keywords deben estar ON/OFF
- Version validation: dado un Unity version string, ¿supera el mínimo requerido?

**Tests de integración editor (EditModeTests):**
- Todos los `.mat` de preset cargan sin errores en `AssetDatabase`
- Setup Window abre en `InitializeOnLoad` en proyecto limpio
- Apply preset en material modificado: dialog aparece

**Tests manuales (documentados en TestPlan):**
- UX flow completo del primer uso (< 5 minutos sin docs)
- Survival: domain reload no rompe Setup Window state
- Survival: compilación en proyecto limpio (sin otros assets)
- Survival: Lite mode en cada plataforma target (Android, iOS, PC)
- Adversarial: Unity 6 Compatibility Mode → mensaje de error visible

**Localización de tests:**
`Assets/Plugins/KITforge Labs/_Develop/Tests/KF_WaterKit_Tests/`

---

## 11. Ejemplos de uso

**Escenario A — Indie dev, primer proyecto Unity:**
María está haciendo su primer juego 3D de aventuras con un lago en escena. Importa WaterKIT. Setup Window se abre, le dice que todo está configurado. Abre la demo scene, ve el lago animado, arrastra el material "Lake — Clear" a su mesh. En 4 minutos tiene agua que parece un juego real. No ha abierto la documentación.

**Escenario B — TA en pequeño estudio, 3 escenas de bioma distintas:**
Jordi gestiona agua para un bosque, un desierto y un volcán. Aplica "Lake Misty" al bosque, "Arctic Ice" al desierto (agua helada), "Lava" al volcán. Tab 1 le recuerda que Lava requiere Full mode — hace click en Tab 3, verifica que Full en esa escena está dentro del presupuesto (< 2ms en A54). Listo en 15 minutos, tres escenas distintas sin ajustar un solo parámetro.

**Escenario C — Dev optimizando para lanzamiento en low-end Android:**
Kenji tiene el juego casi terminado. El profiler muestra que el agua en Full consume 1.8ms en un A14. Va a Tab 3 → Switch to Lite → Lite baja a 0.3ms → sus valores de Deep Color y foam siguen intactos. Graba un vídeo de la escena en Lite — se ve bien. Lanza con confianza.

---

## Surviving Assumptions (del planner — no resueltas)

| # | Assumption | Impacto si está mal | Cuándo validar |
|---|---|---|---|
| Q5 | Lava preset = Full-only asumido. Sin confirmar. | Mínimo — Lite users pierden 1 preset. Aceptable. | Antes de M3 |
| Q6 | Beta seeding: táctica y contactos no definidos | Alto si no hay reviews en launch | Antes de M6 |
| Q7 | Duración por milestone: no estimada | Bajo — afecta planning de calendario | M0 |
| Q8 | Goldstandard visual ref (Sea of Thieves screenshot) no bloqueada | Bloquea M1 done-condition — sin referencia, M1 no tiene criterio de aceptación | Antes de M1 |

---

## Lifecycle state

```
BACKLOG → [BRIEF ←YOU ARE HERE] → ARCHITECTURE → BUILD → QA → STORE_PREP → PUBLISHED
```

**Próximo paso:** Aprobar este Brief → crear `KF_WaterKit_Architecture.md` (tier Complex requiere Architecture.md) → crear scaffold de carpetas + `.asmdef` → M0 locked.
