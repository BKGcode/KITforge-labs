# Brief — KF_MobileUIKit
Status: APPROVED (reconstructed retroactively 2026-05-02)
Phase: BUILD — Group A code-complete, Group B/C/D/E pending
Score: 32/35
Tier: Complex
Date created: 2026-05-02 (retroactive)
Date approved: 2026-05-02

> Originated from: Phase 1/2 PopupManager prototype + 2026-05-01 re-scope to "15 mid-core mobile UI elements catalog". This Brief mirrors what has actually been decided and built, NOT a pre-build plan.

---

## 0. Identidad

| Campo | Valor |
|-------|-------|
| Slug interno | `KF_MobileUIKit` |
| Nombre público | **Mobile UI Kit** |
| One-liner | 15 ready-to-use mobile UI elements. Plug, theme, ship. |
| Categoría Asset Store | GUI > UI |
| Precio | €40 (target — pending validation vs Doozy/Modern Procedural UI) |
| Tier de complejidad | Complex (15 elementos + sistema de animación + theme + DI-optional services + 3 managers) |
| Wave | 1 (priority — kit más ambicioso del lab) |

---

## 1. Objetivo

**Problema concreto:**
El indie mobile dev en Unity 6 arranca un nuevo juego y necesita los mismos 15 elementos de UI que cualquier otro juego mobile (popup de confirmar, toast de notificación, pause, tutorial, settings, shop, daily login, level complete, game over, HUD de monedas/gemas/energía/timer, loading screen, main menu). Construir cada uno desde cero le cuesta 1-2 días por elemento — 3-4 semanas de UI antes de tener gameplay jugable. Las alternativas del Asset Store o son enormes paquetes inflados con 200 prefabs descontextualizados (Modern Procedural UI), o son frameworks de bajo nivel sin elementos listos (Doozy UI), o son kits de "estilo" que no incluyen lógica (assets de skin).

**Buyer profile:**
- Tipo de dev: Indie mobile, estudio 1-3 personas, también prototype-first studios
- Momento de uso: arranque de proyecto (semana 1-2) o sprint de UI a mitad de desarrollo
- Nivel técnico: junior-medio (entiende C#, no quiere construir managers desde cero)
- Device target: Android/iOS portrait-first, 1080×1920 reference

**Una sola frase de valor:**
> *"Mobile UI Kit te da los 15 elementos UI que todo juego mobile necesita, ya con manager + animación + theme + tests, listos para skin y ship."*

---

## 2. Solución propuesta

**Enfoque técnico:**
- 15 elementos en 3 capas: **Screens** (UIManager) · **Popups** (PopupManager) · **Transient/HUD** (ToastManager + HUD bind directo)
- **Group 0 foundation** (no-visible) — `IUIAnimator` + `UIAnimPreset` SO library, `UIThemeConfig` (10 sprites + 6 audios + tokens), `UIServices` container DI-optional, `SafeAreaFitter`, `UIKitValidator` Editor
- **10 estilos de animación** (Snappy/Bouncy/Playful/Punchy/Smooth/Elegant/Juicy/Soft/Mechanical/Cinematic) seleccionables por Theme + override por elemento
- **Por cada elemento**: `<Element>Data` POCO + `<Element>Popup` (UIModule<TData>) + `UIAnim<Element>` (IUIAnimator + DOTween) + EditMode tests + spec micro-doc + prefab + entrada en Demo
- DOTween-guarded: `Runtime/` asmdef DOTween-free; `Catalog/` asmdef referencia DOTween
- Plug-and-play contracts (10 MUST + 8 MUSTN'T) firmados en `Documentation~/Specs/CATALOG.md`

**APIs de Unity principales:**
- UGUI (Canvas, Button, Image, TMP_Text, Toggle, RectTransform, CanvasGroup, ScrollRect)
- DOTween Pro (Sequence + Tween + SetLink)
- Unity Test Framework (NUnit EditMode)
- `IPreprocessBuildWithReport` (validador pre-build)
- `Screen.safeArea` (notch/home-indicator)

**Explícitamente FUERA de scope (v1):**
- No UI Toolkit (UGUI-only — coherente con catálogo y pipeline existente)
- No localización built-in (DTOs usan strings raw; el host wraps i18n)
- No save system (popups emiten eventos, host persiste)
- No analytics built-in (eventos disponibles para que el host los enrutara)
- No skinning runtime de prefabs (theme cambia tokens, no swaps de prefab)
- No editor wizard / setup window v1 (validador silencioso solo)
- No HDRP / Built-in RP support
- No landscape-first (portrait 1080×1920 primary; landscape funciona pero no es target)
- No drag-and-drop / advanced gesture handling
- No store-page templates de IAP (ShopPopup es UI; el wiring de IAP va al host)

**Extensiones futuras (no v1):**
- Skin packs (visual reskins vendidos como add-on)
- Setup Window con preview de animaciones (v1.1)
- 5 elementos extra de monetización (Battle Pass / Season End / Subscription) — pack v2

---

## 3. Referencias

**Competidores directos:**

| Nombre | Precio | Reviews | Lo que hacen bien | Nuestra diferencia |
|---|---|---|---|---|
| **Doozy UI Manager** | €60 | 200+ | Framework potente, signal/visibility system, FSM por UI | Framework sin elementos listos. El comprador construye desde cero. KF da los 15 elementos hechos. |
| **Modern Procedural UI Kit** | €30 | 400+ | Catálogo enorme de prefabs (200+) | Cero lógica — solo skins. El comprador tiene que cablear todo. KF incluye manager + tests + animación. |
| **UI Builder Pro / similar** | €20-40 | varía | Generación procedural de panels | Visual-only, sin patrón mobile específico. KF tiene HUDs + Toast + popup-stack reales. |
| **GUI Pro (Layer Lab)** | €15-30 | 500+ | Skins muy pulidos, mood específicos | Solo arte. Cero managers. KF complementa, no compite directamente. |

**Inspiraciones de UX:**
- **Disney Getaway Blast** — referencia visual de "Playful" como animación default. Bouncy, juicy, mobile mid-core.
- **Gardenscapes / Royal Match** — convención de PausePopup con timeScale directo, NotificationToast Snackbar-style, TutorialPopup con steps + skip.
- **Doozy UI** — qué NO hacer: framework abstracto que asusta al junior dev. KF prioriza "1 prefab arrastrado funciona".

**Documentación de Unity relevante:**
- [UGUI Optimization](https://docs.unity3d.com/Manual/UIOptimizationTips.html)
- [TextMesh Pro](http://digitalnativestudios.com/textmeshpro/docs/)
- [Screen.safeArea](https://docs.unity3d.com/ScriptReference/Screen-safeArea.html)
- [IPreprocessBuildWithReport](https://docs.unity3d.com/ScriptReference/Build.IPreprocessBuildWithReport.html)

---

## 4. Mejoras vs competencia

- **Vs Doozy:** Elementos listos, no framework. El junior dev no construye desde cero — arrastra y skin.
- **Vs Modern Procedural UI Kit:** Lógica + tests incluidos. No solo prefabs descontextualizados; cada elemento tiene su `Data` DTO + manager + animación + EditMode tests.
- **Vs LayerLab GUI Pro:** Lógica además del arte. KF no compite en estilo — complementa: el comprador puede usar GUI Pro como skin sobre KF.
- **Ángulo único**: Único kit con (a) animation preset library swappeable en runtime via Theme, (b) plug-and-play contracts firmados (MUST/MUSTN'T) que garantizan que cualquier combinación de elementos funcione, (c) 100% EditMode-testable sin escena gracias a `Initialize(theme, services)` injection.

---

## 5. Posibles dificultades

| Riesgo | Probabilidad | Impacto | Mitigación |
|---|---|---|---|
| DOTween Pro como dependencia (€15 extra para el comprador) | Alta | Medio | Documentado en README. Considerar PrimeTween free como alternativa en v1.1 si reviews lo piden. |
| Breaking changes entre Group 0 → Group A (Toast `Initialize`) | Alta (ya ocurrió) | Medio | Cada bump documenta migration steps. Pre-1.0 = breaks aceptables; post-1.0 = strict semver. |
| 15 elementos = scope grande, riesgo de inconsistencia entre elementos | Alta | Alto | Cada elemento sigue plantilla idéntica (Data + Module + Animator + Tests + Spec). `_checker` periódico durante desarrollo. |
| Comprador no entiende la separación Theme / Services / Animator | Media | Alto | README con diagrama "donde van mis IAP/economy/audio". Demo scene con un host wireup completo. |
| Conflictos UGUI vs UI Toolkit en proyectos del comprador | Baja | Bajo | UGUI-only documentado. Coexistencia es OK (Unity soporta ambos). |
| Asset Store rechaza el kit por "demasiado parecido a Doozy" | Baja | Alto | Énfasis en elementos prefabricados vs framework. Página de tienda muy clara: "15 elementos listos". |

**Compatibilidad:**
- Versión mínima: **Unity 6000.0.60f1** (alineado con WaterKit y stack del lab)
- URP: agnóstico (es UI, no shaders 3D)
- HDRP: agnóstico
- Built-in RP: agnóstico
- Dependencias externas: **DOTween Pro** (Demigiant) + **TextMesh Pro** (incluido en Unity)

**Casos límite peligrosos:**
- `PausePopup` con `Time.timeScale` mientras existen otros tweens no-`SetUpdate(true)` → quedan congelados. Mitigado: `UIAnimPausePopup` usa `SetUpdate(true)` y la doc lo explica.
- `ToastManager` con stack > `_maxConcurrent` → cola pendiente puede crecer infinitamente si el host emite toasts en bucle. Pendiente: cap configurable.
- `PopupManager` re-enter durante `OnHide` → `IsDismissing` guard en `UIModuleBase` mitiga, validado en tests.
- Build con `[InternalsVisibleTo]` y assembly stripping IL2CPP → revisar antes de v1.0.

---

## 6. UI Design

**Tipo de interfaz:**
☑ Runtime UI (UGUI Canvas) — el producto ES la UI runtime
☑ Editor: solo validador silencioso (`Kitforge/UI Kit/Validate Active Scene`) + IPreprocessBuildWithReport
☐ No EditorWindow standalone v1

**Sistema de UI:**
☑ UGUI (Canvas + RectTransform + TMP) — runtime
☑ IMGUI minimal (validador menu item)

**Convención por elemento (locked):**
```
Runtime/Catalog/Popups/<Element>/
├── <Element>Data.cs       (POCO, [Serializable])
├── <Element>Popup.cs      (UIModule<TData>, [RequireComponent(UIAnim<Element>)])
└── UIAnim<Element>.cs     (IUIAnimator, MonoBehaviour, DOTween)

Tests/Editor/
└── <Element>Tests.cs      (NUnit, headless-runnable)

Documentation~/Specs/Catalog/
└── <Element>.md           (DTO + events + animation contract + edge cases + QA scenarios)

Samples~/Catalog/
└── <Element>.prefab + entry en Demo scene con [ContextMenu] QA triggers
```

**Theme tokens** (fijados en Group 0):
- 10 sprite slots (panel, button, backdrop, divider, 6 icons)
- 6 audio slots (button-click, popup-show, popup-hide, success, error, notification)
- Color palette (Primary, Accent, Success, Danger, Background, Foreground)
- `MinTouchTarget` (default 88pt / ~44dp)
- `DefaultAnimStyle` (default `Playful`)
- `AnimPresetLibrary` (SO ref con 10 presets)

---

## 7. UX Flow

**Primer uso del comprador** (tras importar):
1. Abre Demo scene `Samples~/Catalog/Demo.unity`
2. Play → ve los 15 elementos en acción mediante `[ContextMenu]` triggers
3. Inspecciona el prefab que le interesa (e.g. ConfirmPopup) — un solo prefab autocontenido
4. Arrastra el prefab a su escena, cablea el `UIServices` y el `UIThemeConfig` propios
5. Llama `popupManager.Show<ConfirmPopup>(new ConfirmPopupData { Title = ..., OnConfirmed = ... })`

**Momento "aha"** (< 90 segundos desde import):
> Play en Demo scene, click en "Confirm Popup" trigger → popup aparece con animación Playful → click Confirm → desaparece con tween de hide. Sin tocar código.

**Caso de uso típico (el indie cablea su propio juego):**
1. Crea su `UIThemeConfig` SO con sus colores/sprites/audios
2. Pone `UIManager` + `PopupManager` + `ToastManager` + `UIServices` en su escena de UI
3. Cablea `IEconomyService` / `IAdsService` / etc. en `UIServices` (Inspector o DI)
4. Llama popups por código desde su gameplay; el host responde a los eventos

---

## 8. Checkpoints

| ID | Checkpoint | Criterio observable | Estado |
|----|-----------|---------------------|--------|
| CP1 | Group 0 foundation cerrado | F1-F8 implementados, tag `v0.3.0-alpha` | ✅ 2026-05-01 |
| CP2 | ConfirmPopup cerrado | Code + tests + spec + entrada CATALOG.md | ✅ 2026-05-02 (sin prefab/demo) |
| CP3 | NotificationToast scaffolded | Code + tests verde | ✅ 2026-05-02 |
| CP4 | PausePopup scaffolded | Code + tests verde | ✅ 2026-05-02 |
| CP5 | TutorialPopup scaffolded | Code + tests verde | ✅ 2026-05-02 |
| CP6 | Group A specs (3 pendientes) | 3 specs en Documentation~/Specs/Catalog/ | ⏳ pending |
| CP7 | Group A prefabs + Demo | 4 prefabs + 4 entries con [ContextMenu] | ⏳ pending (Editor manual) |
| CP8 | Group A cierre + tag `v0.4.0-alpha` | CATALOG.md updated, CHANGELOG cerrado, git tag | ⏳ pending |
| CP9 | Group B (Settings/Reward/NotEnoughCurrency) | 3 elementos completos | ⏳ pending |
| CP10 | Group C (Shop/DailyLogin/GameOver/LevelComplete) | 4 elementos completos | ⏳ pending |
| CP11 | Group D (HUDs Coins/Gems/Energy/Timer) | 4 elementos completos | ⏳ pending |
| CP12 | Group E (Loading/MainMenu screens) | 2 elementos completos | ⏳ pending |
| CP13 | Pre-store: README + benchmarks + reference AudioRouter | Tag `v0.9.0-rc` | ⏳ pending |
| CP14 | Asset Store submission | Approved listing | ⏳ pending |

---

## 9. Validador / QA — NUNCA debe ocurrir

1. **NRE en runtime** por servicio no cableado en `UIServices` — popups deben usar `Services?.Audio?.Play(cue)` null-safe siempre.
2. **Tween residual** que sobrevive a `SceneManager.LoadScene` — todos los tweens deben usar `SetLink(gameObject)`.
3. **Acumulación de event handlers** entre re-Show del mismo popup — `Bind()` debe llamar `ClearAllEvents()` siempre.
4. **Interacción durante hide-animation** (double-click confirm, back-spam) — `IsDismissing` guard obligatorio en todo handler de input.
5. **Time.timeScale colgado en valor != 1** tras destruir PausePopup sin Resume — `OnDestroy` debe restaurar si `IsPaused`.
6. **Prefab del kit con referencia hardcoded** a un `UIThemeConfig` específico — los prefabs deben venir con refs nulas, el comprador inyecta su theme.
7. **Validación de build** que silenciosamente skipea elementos sin asignar — `UIKitValidator` debe abortar build con `IPreprocessBuildWithReport`.
8. **Toasts apilándose sin límite** si el host emite en bucle — `ToastManager` debe respetar `_maxConcurrent` con cola pendiente.

---

## 10. Tests

**Lógica testeable como funciones puras (NUnit EditMode, headless-runnable):**

Por cada elemento del catálogo:
1. `Bind` con null usa defaults sin errores
2. Routing de events (back press, confirm, cancel, etc.)
3. Race-condition guard (interacción durante dismiss → ignorada)
4. Re-bind resetea event listeners (anti event-leak)
5. Edge cases específicos del elemento (LoopBackToFirst en Tutorial, timeScale restore en Pause, severity mapping en Toast, etc.)

**Foundation tests (Group 0):**
- `PopupManager` push/pop/back-routing (38 tests)
- `UIManager` lifecycle
- `UIServices` validation

**Cobertura actual:** 59/59 GREEN (38 G0 + 5 Confirm + 5 Toast + 6 Pause + 10 Tutorial)
**Target a v1.0:** 100+ tests, headless CI-runnable.

---

## 11. Ejemplos

**Ejemplo 1 — Indie mobile dev arrancando un Match-3:**
- Arrastra Demo prefab `PausePopup` a su escena de gameplay
- Cablea `OnResume`/`OnRestart`/`OnHome` a sus métodos de GameController
- Resultado: pause funcional con timeScale + animación bouncy en 5 minutos

**Ejemplo 2 — Estudio de 3 personas con sprint de monetización:**
- Usa `ShopPopup` (Group C) con `_data.Items` rellenado desde su catálogo IAP
- Cablea `OnPurchaseRequested(itemId)` a su `IAPService` propio
- Resultado: tienda funcional + animación juicy + safe-area respetada en 1 día (vs 1 semana from scratch)

**Ejemplo 3 — TA refrescando UI de un proyecto existente:**
- Cambia `UIThemeConfig.DefaultAnimStyle` de `Playful` a `Elegant`
- Reskin via 10 sprite slots del Theme — sin tocar prefabs
- Resultado: nuevo "look & feel" del juego en una tarde, todos los elementos coherentes
