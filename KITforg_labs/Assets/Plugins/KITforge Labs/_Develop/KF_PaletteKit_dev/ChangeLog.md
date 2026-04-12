# ChangeLog — KF_PaletteKit
Phase: BUILD
Last updated: 2026-04-12

---

## [Unreleased]

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
- [     ] ⬜ CP11: JSON Import — Lospec JSON → roles creados con colores
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
