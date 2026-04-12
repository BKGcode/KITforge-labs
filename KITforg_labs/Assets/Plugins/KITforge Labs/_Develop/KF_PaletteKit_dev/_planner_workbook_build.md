# Planner Workbook — KF_PaletteKit BUILD
Session: 2026-04-11
Phase: BUILD planning
Objective: Definir el plan de BUILD completo antes de escribir la primera línea de código.

---

## DISCOVER — Estado inicial confirmado

### Qué existe
| Artefacto | Path | Estado |
|-----------|------|--------|
| `Brief.md` | `_Develop/KF_PaletteKit_dev/Brief.md` | ✅ APPROVED |
| `ChangeLog.md` | `_Develop/KF_PaletteKit_dev/` | ✅ Existe |
| `Architecture.md` | `_Develop/KF_PaletteKit_dev/` | ❌ No existe (ver §Tier) |
| `TestPlan.md` | `_Develop/KF_PaletteKit_dev/` | ❌ No existe |
| Carpeta producto | `Assets/Plugins/KITforge Labs/KF_PaletteKit/` | ✅ Existe |
| `tools_lab_env.json` | `Assets/Settings/KITforgeLabs/` | ✅ Existe |

### tools_lab_env.json — campos a actualizar
```json
// Antes:
"activeProduct": null,
"KF_PaletteKit": { "phase": "BACKLOG", "tier": "Moderate" }

// Después:
"activeProduct": "KF_PaletteKit",
"KF_PaletteKit": { "phase": "BUILD", "tier": "Simple" }  // ver §Tier discrepancy
```

---

## DISCOVER — Discrepancia de Tier (⚠️ BLOQUEANTE)

### El problema
| Fuente | Tier |
|--------|------|
| `Brief.md` (§0) | **Simple** |
| `tools_lab_env.json` | **Moderate** |

### Implicación de BOOTSTRAP R1
- **Simple**: Brief APPROVED = gate suficiente para BUILD. No se necesita `Architecture.md`.
- **Moderate/Complex**: Architecture.md APPROVED es requisito previo al BUILD.

### Análisis objetivo: ¿qué tier es correcto?
El Brief en §0 dice "Simple (1 semana)". El JSON dice Moderate. Los criterios objetivos de tier típicamente son:
- Simple → 1 archivo editor principal + 1-2 SOs + UI básica. Tiempo: < 2 semanas.
- Moderate → varias subsistemas interconectados, múltiples ventanas, o lógica de procesamiento no trivial.

KF_PaletteKit tiene: 1 EditorWindow + 1 SO + 4-5 scripts de lógica (applier, scanner, preview, parser). La lógica de preview con MaterialPropertyBlock + UndoBatch son subtilezas técnicas, pero no son múltiples subsistemas. El producto está completamente especificado en el Brief sin ambigüedad. **Simple es la designación correcta.**

### Recomendación
Corregir el JSON a tier="Simple" y proceder sin Architecture.md. La arquitectura está suficientemente descrita en Brief §2.

**Decisión pendiente del usuario** → Pregunta #1 del chat.

---

## DISCOVER — Folder convention (✅ RESUELTO)

| Fuente | Patrón |
|--------|--------|
| `KF_NamingConventions.md §5` | `_Develop/KF_<Slug>_dev/` |
| `tools_lab_env.json` "devFolderPattern" | `_Develop/KF_<ToolSlug>_dev` |
| **Estructura en disco (migrada 2026-04-12)** | `_Develop/KF_PaletteKit_dev/` |

Migración ejecutada: `_Develop/_dev/KF_PaletteKit/` → `_Develop/KF_PaletteKit_dev/`. La convención y el disco ahora son coherentes.

---

## ALIGN — Arquitectura de archivos por capas

### Naming resuelto (KF_NamingConventions §2, §3, §4)

**Namespace:** `KITforgeLabs.Editor.PaletteKit`

**Assemblies:**
| Assembly | Nombre |
|----------|--------|
| Editor | `KITforgeLabs.PaletteKit.Editor` |
| Tests | `KITforgeLabs.PaletteKit.Tests` |

**Clases principales:**
| Clase | Rol | Naming rule |
|-------|-----|-------------|
| `KF_PaletteKitWindow` | EditorWindow | `KF_<Slug>Window` |
| `KF_PaletteKitSO` | ScriptableObject paleta (contenedor) | `KF_<Slug>SO` (datos de usuario, no settings) |
| `KF_PaletteKitColorRole` | Serializable struct de rol | `KF_<Slug><Rol>` |
| `KF_PaletteKitApplier` | Lógica Apply (SetColor + Undo) | `KF_<Slug><Processor>` |
| `KF_PaletteKitPreviewController` | Preview MPB (non-destructivo) | `KF_<Slug><Processor>` |
| `KF_PaletteKitMaterialScanner` | FindAssets + scope filter | `KF_<Slug>Scanner` |
| `KF_PaletteKitParser` | JSON Lospec + hex CSV | `KF_<Slug><Processor>` |
| `KF_PaletteKitMenuItems` | MenuItems | `KF_<Slug>MenuItems` |
| `KF_PaletteKitDevSetup` | Dev scene regenerator | `KF_<Slug>DevSetup` (interno) |

**Nota PaletteSO naming:**
`KF_PaletteKitSO` — omitir el sufijo "Settings" cuando el SO es datos de usuario, no settings del tool.

**CreateAssetMenu:**
```csharp
[CreateAssetMenu(menuName = "KITforge Labs/PaletteKit Palette", fileName = "KF_PaletteKit_Default")]
```

---

## ALIGN — Árbol de archivos completo

### Producto (lo que recibe el comprador)
```
Assets/Plugins/KITforge Labs/KF_PaletteKit/
  Editor/
    KF_PaletteKitWindow.cs
    KF_PaletteKitSO.cs
    KF_PaletteKitColorRole.cs
    KF_PaletteKitMenuItems.cs
    UXML/
      KF_PaletteKitWindow.uxml
    USS/
      KF_PaletteKitWindow.uss
      KF_PaletteKitSpecific.uss
    KITforgeLabs.PaletteKit.Editor.asmdef
  Runtime/                              ← carpeta vacía (future use)
  Settings/                             ← SOs del usuario van aquí
  Demo/
    Demo_KF_PaletteKit.unity
    DemoAssets/
  README.md
  package.json
```

### ⚠️ Nota sobre Settings/
Los SOs creados en `Settings/` están dentro del package folder. Exportar el package incluye las palettes del usuario — útil para compartir con el equipo, pero el usuario debe saberlo.

### Desarrollo interno (nunca shippeado)
```
_Develop/
  KF_PaletteKit_dev/
    Brief.md                              ✅
    _planner_workbook_build.md            ✅ este archivo
    ChangeLog.md                          ✅
    TestPlan.md                           ← pendiente QA phase
  Tests/KF_PaletteKit_Tests/
    KF_PaletteKitParserTests.cs
    KF_PaletteKitApplierTests.cs
    KF_PaletteKitValidatorTests.cs
    KITforgeLabs.PaletteKit.Tests.asmdef
```

---

## PLAN — Secuencia de BUILD (orden de dependencias)

### Pre-BUILD (sin código — 2 operaciones)
| Paso | Archivo | Acción |
|------|---------|--------|
| 0a | `tools_lab_env.json` | Actualizar `activeProduct`, phase BACKLOG→BUILD, tier Moderate→Simple |
| 0b | `ChangeLog.md` | Crear desde template con phase transition BRIEF→BUILD |

### Bloque 1 — Scaffold (sin código de lógica aún)
| Paso | Archivo | Desbloquea |
|------|---------|------------|
| 1a | `package.json` | Package Identity |
| 1b | `KITforgeLabs.PaletteKit.Editor.asmdef` | Compilación aislada |
| 1c | `KITforgeLabs.PaletteKit.Tests.asmdef` | Tests independientes |

### Bloque 2 — Data model
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 2a | `KF_PaletteKitColorRole.cs` | — | asmdef |
| 2b | `KF_PaletteKitSO.cs` | CP2 | ColorRole |

### Bloque 3 — Window shell + menu
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 3a | `KF_PaletteKitMenuItems.cs` | — | asmdef |
| 3b | `KF_PaletteKitWindow.cs` (shell vacío) | CP1 | MenuItems |
| 3c | `KF_PaletteKitWindow.uxml` + `.uss` | CP1 full | Window.cs |
| 3d | Binding roles en Window | CP3, CP4 | SO, ColorRole |

### Bloque 4 — Lógica core
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 4a | `KF_PaletteKitMaterialScanner.cs` | CP8-10 | asmdef |
| 4b | `KF_PaletteKitApplier.cs` | CP7 | SO, Scanner |
| 4c | `KF_PaletteKitPreviewController.cs` | CP6 | SO |

### Bloque 5 — Material assignment en UI
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 5a | Material drag+drop / ObjectField en Window | CP5 | Scanner, SO |

### Bloque 6 — Import/Export
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 6a | `KF_PaletteKitParser.cs` | CP11 | asmdef (puro C#) |
| 6b | PNG Export en Window | CP12 | Unity Texture2D |

### Bloque 7 — Tests
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 7a | `KF_PaletteKitParserTests.cs` | CP11+ | Parser, Tests asmdef |
| 7b | `KF_PaletteKitApplierTests.cs` | CP7+ | Applier |

### Bloque 8 — Dev scene + Demo
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 8a | `KF_PaletteKitDevSetup.cs` | — | asmdef |
| 8b | `KF_PaletteKit_DevScene.unity` | — | DevSetup ejecutado |
| 8c | `Demo_KF_PaletteKit.unity` | CP13 | todos los anteriores |
| 8d | `DemoAssets/` (materiales + meshes) | CP13 | demo scene |

### Bloque 9 — Finales
| Paso | Archivo | CP | Deps |
|------|---------|-----|------|
| 9a | `README.md` | — | producto completo |
| 9b | CP14: Zero console errors | CP14 | todo |

---

## STRESS TEST — Riesgos del plan actual

### R1: `KF_PaletteKitColorRole` con `List<Material>` en un Editor assembly
**No bloqueante.** Unity serializa Object References por GUID. ✅

### R2: MaterialPropertyBlock en domain reload
**Mitiga:** Flag serializable `_previewActive` que se resetea en OnEnable. ✅

### R3: Tests assembly referenciando Editor assembly
`KITforgeLabs.PaletteKit.Tests.asmdef` referencia `KITforgeLabs.PaletteKit.Editor`. Válido para TestRunner EditMode. ✅

### R4: PNG Export — API
`Texture2D.EncodeToPNG` + `File.WriteAllBytes`. Funciona en editor. Path: `Assets/Settings/KITforgeLabs/Exports/`.

### R5: Un solo .asmdef para todo el Editor
1 EditorWindow + ~8 scripts. Un único asmdef es correcto. ✅

### R6: La Demo scene necesita materiales
DemoAssets/ contendrá al menos 5 materiales URP/Lit: Mat_Primary, Mat_Accent, etc.

### R7: Package.json — versión mínima de Unity
`"unity": "6000.0"`. ✅

---

## PLAN — Checkpoint → Session mapping

| Sesión | Bloques | CPs objetivo | Output |
|--------|---------|-------------|--------|
| BUILD-1 | Pre-BUILD + 1 + 2 + 3a-3c | CP1, CP2 | Ventana abre, SO crea |
| BUILD-2 | 3d + 4a-4c + 5a | CP3-CP7 | Roles funcionales, Apply, Preview |
| BUILD-3 | 4a (scope) + 6a | CP8-CP11 | Scopes, JSON import |
| BUILD-4 | 6b + 7a-7b | CP12 + Tests | Export, Tests |
| BUILD-5 | 8a-8d + 9a-9b | CP13-CP14 | Demo, README, Zero errors |

Estimación total: 4-5 sesiones de BUILD → QA.

---

## OPEN QUESTIONS

| # | Pregunta | Impacto | Estado |
|---|----------|---------|--------|
| 1 | Tier: Simple vs Moderate → ¿Architecture.md necesaria? | BLOQUEANTE para iniciar Bloque 1 | ✅ RESUELTO: Simple |
| 2 | Nombre SO: `KF_PaletteKitSO` vs `KF_PaletteSO` | Class naming | ✅ RESUELTO: `KF_PaletteKitSO` |
| 3 | UXML/USS: `Editor/` root vs subdirectorios `Editor/UXML/` | Folder structure | ✅ RESUELTO: subdirectorios |
| 4 | Pre-BUILD research (competitors) — ¿ahora o skip? | Opcional, no bloqueante | ✅ Skip para BUILD |
| 5 | TestPlan.md — ¿crear ahora desde template o defer a QA? | QA phase readiness | ✅ Defer a QA phase |
| 6 | Sesiones de BUILD: ¿usar el mapping propuesto (BUILD-1..5) o sesión única? | Organización | ✅ RESUELTO: mapping propuesto |

---

## DECISIONS LOG

| Fecha | Decisión | Rationale |
|-------|----------|-----------|
| 2026-04-11 | Precio $20, sin freemium v1 | Sesión anterior |
| 2026-04-11 | Editor-only absoluto | Sesión anterior |
| 2026-04-11 | UI Toolkit (UXML+USS) | Sesión anterior |
| 2026-04-11 | Namespace: KITforgeLabs.Editor.PaletteKit | Sesión anterior |
| 2026-04-11 | Scope: package.json + asmdef ANTES del primer .cs | Sesión anterior |
| 2026-04-11 | Tier=Simple (corregir JSON) | Brief §0 explicit; análisis objetivo confirma |
| 2026-04-11 | UXML/USS en `Editor/UXML/` y `Editor/USS/` | Convención UI Toolkit de Unity |
| 2026-04-11 | asmdef nombres: KITforgeLabs.PaletteKit.Editor + .Tests | KF_NamingConventions §2 |
| 2026-04-12 | SO naming: `KF_PaletteKitSO` sin sufijo "Settings" | El SO es datos de usuario, no config del tool |
| 2026-04-12 | Dev folder: `_Develop/KF_PaletteKit_dev/` | Migrado desde `_dev/KF_PaletteKit/`; alineado con convención |
| 2026-04-12 | SO path: `KF_PaletteKit/Settings/` | Dentro del package; comparte con equipo via repo |
| 2026-04-12 | Runtime/ folder vacío con .keep | Placeholder para future runtime assets |
