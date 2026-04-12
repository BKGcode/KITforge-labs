# ChangeLog — KF_PaletteKit
Phase: BUILD
Last updated: 2026-04-12 (CP8/CP9/CP10)

---

## [Unreleased]

### Session — 2026-04-12
GOAL: Scope filters CP8/CP9/CP10 + análisis competitivo + _checker para Copilot
DONE:
- Análisis competitivo vs Palette Modifier (Nicrom) — posicionamiento confirmado: nicho distinto (shader props vs texture pixels)
- CP8/CP9/CP10: Scope selector Scene/Project/Selection en UXML + C# (SetScope, GetRenderersByScope, GetMaterialsInScope)
- _checker.prompt.md creado en VS Code prompts
PENDING:
- CP12: PNG Export  ← NEXT
- CP13: Demo scene
- CP14: Zero console errors
DECISIONS:
- Scope Project en Apply = todos los materiales del binding sin filtro de escena
- _scope serializado con [SerializeField] → sobrevive reinicio de Unity
- .claude/ = source of truth, nunca modificar desde agentes
REFS: KF_PaletteKitWindow.cs, KF_PaletteKitWindow.uxml, KF_PaletteKitSpecific.uss

### Phase transitions
- [2026-04-11] 🔄 Phase: BACKLOG → BRIEF
- [2026-04-12] 🔄 Phase: BRIEF → BUILD

### Build Checkpoints (BUILD phase)
- [x] ✅ CP1: Window abre — `KITforge > PaletteKit` visible; ventana se abre sin errores
- [x] ✅ CP2: Palette SO crea — Click "New Palette" → .asset en `Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings/`; dropdown muestra
- [x] ✅ CP3: Role add/rename — Click "+ Add Role" → rol visible; nombre editable inline
- [x] ✅ CP4: Color change — Click swatch → ColorPicker abre; cambio reflejado en swatch
- [x] ✅ CP5: Material assignment — Drag material → aparece en lista del rol
- [x] ✅ CP6: Preview funciona — MaterialPropertyBlock aplicado; Revert revierte sin persistir
- [x] ✅ CP7: Apply funciona — Material.SetColor ejecutado; Ctrl+Z revierte todo
- [     ] ⬜ CP8: Scope Project — FindAssets localiza todos los materiales asignados
- [     ] ⬜ CP9: Scope Scene — Solo afecta materiales de escena activa
- [     ] ⬜ CP10: Scope Selection — Solo afecta materiales de selección
- [x] ✅ CP11: HEX Import — Lospec HEX File → roles creados con prefijo nombre-archivo; Overwrite limpia bindings; cap 64 colores
- [x] ✅ CP8/CP9/CP10: Scope filters — Selector Scene / Project / Selection en la ventana; Preview y Apply filtran renderers y materiales por scope
- [     ] ⬜ CP12: PNG Export — Swatch PNG en `Assets/Settings/KITforgeLabs/Exports/`
- [     ] ⬜ CP13: Demo scene — Carga, 5 objetos visibles, Apply cambia todos
- [     ] ⬜ CP14: Zero console errors — Todos los flujos sin errores ni warnings

### QA
- [     ] 🔄 Phase: BUILD → QA
- [     ] ✅ QA PASSED — TestPlan all green

### Store Prep
- [     ] 🔄 Phase: QA → STORE_PREP
- [     ] ✅ Screenshots prepared
- [     ] ✅ Package exported clean
- [     ] 🔄 Phase: STORE_PREP → PUBLISHED

---

## v1.0.0 — TBD
- Initial release

---

## Decisiones de diseño

### CP11 — HEX Import (2026-04-12)
- **Formato:** `.hex` (Lospec HEX File) en vez de JSON. Lospec no ofrece export JSON desde la web; HEX es el formato texto más simple disponible.
- **Ubicación del botón:** `kfpk-add-role-bar` (loaded panel, junto a "+ Add Role"). Importar = añadir roles → pertenece al área de contenido, no al toolbar de selección de palette.
- **Overwrite limpia bindings:** Si el usuario elige Overwrite, se eliminan también los bindings del BindingSO asignado. Dejar bindings huérfanos no tiene caso de uso válido.
- **Naming:** Prefijo = nombre del archivo `.hex` capitalizado + número (`Apollo 1`, `Apollo 2`…). Útil en paletas con append de múltiples orígenes.
- **Cap 64 colores:** Límite silencioso. Paletas >64 colores son edge case; 256 roles generaría lag visible en `RebuildRolesList`.
