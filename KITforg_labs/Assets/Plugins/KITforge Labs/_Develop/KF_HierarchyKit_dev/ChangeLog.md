# ChangeLog — KF_HierarchyKit
Phase: BUILD
Last updated: 2026-04-11 (Session 4)

---

## Architecture — Simple Tier (inline spec)

```
FILES (5):
  KF_HierarchyKitInitializer.cs  [InitializeOnLoad] static. Subscribes hook. Single responsibility: wiring.
  KF_HierarchyKitRenderer.cs     Static. IMGUI drawing logic. DrawRow(instanceId, rowRect).
  KF_HierarchyKitSettings.cs     Config data class + EditorPrefs bridge. Lazy singleton via Current property.
  KF_HierarchyKitWindow.cs       EditorWindow (UI Toolkit). Settings panel. [Session 4+]
  KF_HierarchyKitMenuItems.cs    [MenuItem] + context menu "Add Header Above". [Session 4+]

DATA FLOW:
  [InitializeOnLoad ctor] → subscribes hierarchyWindowItemOnGUI → Renderer.DrawRow(id, rect)
    → reads KF_HierarchyKitSettings.Current (cached lazy load from EditorPrefs JSON)
    → IsHeader(name) → DrawHeaderRow / DrawBorderRow

PERF CONTRACT:
  No allocations inside DrawRow hot path except go.name (TODO: cache in Session 4 perf pass)
  GUIStyle cached as static lazy field. IsHeader uses StartsWith only (no LINQ, no Regex).
  Performance gate: checkpoint 10 → 200-object scene, no visible lag.
```

---

## [Unreleased]

### Phase transitions
- [2026-04-11] 🔄 Phase: BACKLOG → BRIEF
- [2026-04-11] ✅ Brief.md created and APPROVED

### Session 2 — 2026-04-11
GOAL: Completar infraestructura _KFL y Brief de HierarchyKit. X10Think sobre orientación del proyecto.
DONE:
- tools_lab_env.json → activeProduct: KF_HierarchyKit, labPhase: ACTIVE
- ChangeLog.Template.md creado
- _KFL_close command creado (9 comandos totales operativos)
- BOOTSTRAP.md actualizado con /KFL_close en índice de comandos
- X10Think: Infrastructure Trap detectado — regla noCodeBeforeArchitecture = overkill para Simple tier
- X10Think: prerequisitos reales para BUILD = package.json + asmdef (antes del primer .cs)
- Conclusiones guardadas en /memories/repo/kitforge-tools-lab-core-reference.md
PENDING:
- Definir package.json + asmdef para KF_HierarchyKit ← NEXT
- Empezar BUILD — primera sesión de código real
- Architecture para Simple tier = 15 líneas en ChangeLog, no doc separado
- Validar _KFL commands en uso real de BUILD
DECISIONS:
- Architecture.md separado solo para Moderate/Complex. Simple tier: spec de package.json + asmdef en ChangeLog es suficiente gate antes de código.
- _KFL_close es el último paso de cada sesión. Handoff block se pega como primer mensaje de la siguiente conversación.
REFS: Brief.md, BOOTSTRAP.md, tools_lab_env.json, /memories/repo/kitforge-tools-lab-core-reference.md

### Session 4 — 2026-04-11
GOAL: Checkpoints 3+4+5 + perf pass.
DONE:
- KF_HierarchyKitMenuItems.cs: MenuItem "KITforge/HierarchyKit/Settings..." + "Toggle Enable" (checkmark via Menu.SetChecked) + context menu "GameObject/KITforge/Add Header Above" (Undo-safe, validate: requires selection)
- KF_HierarchyKitWindow.cs: EditorWindow UI Toolkit — Toggle(enabled), ColorField(borderColor), Toggle(showTypeIcons). Auto-save on change via RegisterValueChangedCallback + RepaintHierarchyWindow.
- Perf pass: s_NameCache (Dictionary<int,string>) en Renderer — evita InstanceIDToObject + go.name en cada repaint. Invalidado via EditorApplication.hierarchyChanged en Initializer.
PENDING:
- ⚠️ Probar checkpoints 1–5 en Unity (compile clean + visuals + menus + ventana)
- Checkpoint 6: test explícito persistencia EditorPrefs (change settings → recompile → verify)
- Checkpoint 7: Settings SO (equipo)
- Checkpoint 8: Demo scene 5+ headers + 3+ color rules
- Checkpoint 9: zero idle errors (sin escena, ventana cerrada)
- Checkpoint 10: perf gate 200-object scene
DECISIONS:
- "Add Header Above" crea un GO vacío con nombre "--- New Header ---" (usuario puede renombrar con F2). El "zero pollution" del Brief = sin componentes ocultos, no impide que el usuario cree GOs header intencionalmente.
- Window auto-save (sin botón Save) — UX más limpia para settings.
- s_NameCache como Dictionary no concurrent — OK: EditorApplication callbacks son single-threaded.
REFS: Editor/KF_HierarchyKitMenuItems.cs, Editor/KF_HierarchyKitWindow.cs, Editor/KF_HierarchyKitRenderer.cs, Editor/KF_HierarchyKitInitializer.cs

### Session 3 — 2026-04-11
GOAL: package.json + asmdef + primera sesión BUILD — checkpoints 1+2.
DONE:
- package.json creado: com.kitforgelabs.hierarchykit v0.1.0, unity 6000.0
- asmdef creado: KITforgeLabs.HierarchyKit.Editor (Editor-only, autoReferenced: false)
- Architecture spec escrita inline en ChangeLog (Simple tier — 15 líneas, sin doc separado)
- KF_HierarchyKitInitializer.cs: [InitializeOnLoad] → suscripción a hierarchyWindowItemOnGUI
- KF_HierarchyKitSettings.cs: config data class + EditorPrefs bridge (lazy Current property)
- KF_HierarchyKitRenderer.cs: DrawRow, IsHeader, DrawHeaderRow, DrawBorderRow
- README.md placeholder creado
PENDING:
- Probar checkpoints 1+2 en Unity (compile clean + visual auto-activation)
- Checkpoint 3+4: contexto menu "Add Header Above" → KF_HierarchyKitMenuItems.cs
- Checkpoint 5: Settings Window → KF_HierarchyKitWindow.cs (UI Toolkit)
- Checkpoint 6: test de persistencia EditorPrefs tras domain reload
- Perf pass: caché de go.name (Dictionary<int,string>, invalidar en HierarchyChanged)
DECISIONS:
- Sin Runtime assembly: 100% editor-only. Settings SO vive en editor assembly.
- Header auto-detection: prefijos --- / === / [xxx] sin configuración previa → AHA inmediato.
- GUIStyle cacheado como property lazy (reset automático en domain reload por ser static).
- go.name sin caché en v0.1 — aceptado conscientemente hasta checkpoint perf (10).
REFS: KF_HierarchyKit/package.json, Editor/asmdef, Editor/*.cs

### Build Checkpoints
| # | Checkpoint | Status |
|---|-----------|--------|
| 1 | Package imports clean — zero compile errors | PENDING TEST |
| 2 | Auto-activation — visual enhancements visible | PENDING TEST |
| 3 | Context menu "Add Header Above" | PENDING TEST |
| 4 | Header renders (color bg + bold text) | PENDING TEST |
| 5 | Settings window opens, no errors | PENDING TEST |
| 6 | EditorPrefs persistence across domain reload | NOT STARTED |
| 7 | Optional Settings SO | NOT STARTED |
| 8 | Demo scene with 5+ headers + 3+ color rules | NOT STARTED |
| 9 | Zero idle errors (no scene loaded, window closed) | NOT STARTED |
| 10 | Performance gate: 200-object scene, no lag | NOT STARTED |

---

## Version History (populated post-publish)
(none)
