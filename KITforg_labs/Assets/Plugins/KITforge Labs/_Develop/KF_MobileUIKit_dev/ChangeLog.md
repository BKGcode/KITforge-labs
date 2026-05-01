# KF_MobileUIKit — ChangeLog (Lab Tracker)

_Last updated: 2026-05-02_

## [Unreleased]

### Session — 2026-05-02 (cont.3) — TutorialPopup scaffolded · Group A code-complete · 59/59 GREEN
GOAL: Cerrar 4/4 TutorialPopup (último elemento de Group A en código), dejar 59/59 verde.
DONE:
- **8 decisiones de diseño cerradas antes de tocar código** (estructura List<TutorialStep>, single-Next con label dinámico, back=Skip, TapToAdvance flag, LoopBackToFirst flag, sin Time.timeScale, SetUpdate(true) defensivo, animator dedicado).
- **TutorialPopup (4/4 Group A)** scaffold completo:
  - `Runtime/Catalog/Popups/Tutorial/`: `TutorialStep` POCO, `TutorialPopupData` (steps + StartIndex + ShowPrevious/ShowSkip + LoopBackToFirst + CloseOnBackdrop + TapToAdvance + custom labels), `UIAnimTutorialPopup` (clon estructural de Pause con `SetUpdate(true)` defensivo show+hide), `TutorialPopup` (`UIModule<TData>`, dynamic Next-label que muta a DoneLabel en último paso, ProgressLabel "i / N" auto, public `GoNext`/`GoPrevious`/`SkipTutorial`/`CompleteTutorial`, `CurrentIndex`/`StepCount`/`IsFirstStep`/`IsLastStep` getters, theme-null warning one-shot, `ClearAllEvents` en Bind + OnDestroy).
  - `Tests/Editor/TutorialPopupTests.cs` — 10 tests (Bind null, Bind con StartIndex, GoNext+StepChanged, GoNext en último→Completed+dismiss, GoNext último con loop wrappea, GoPrevious en primero ignorado, GoPrevious decrementa, back→Skip+dismiss, back durante dismissing ignorado, Bind reset listeners).
- **EditMode 59/59 verde** confirmado por usuario vía TestRunner del Editor (38 Group 0 + 5 Confirm + 5 Toast + 6 Pause + 10 Tutorial). Headless bloqueado por Editor abierto — validación manual aceptada.
DECISIONS (sin consultar — flag para review junto con las 7 de Pause):
- Back press = Skip (no Previous). Convención modal móvil; Previous explícito vía botón.
- GoNext en último paso → `OnCompleted` + dismiss; con `LoopBackToFirst=true` wrappea a 0 y emite `OnNext` (NO completa).
- `TapToAdvance` gana sobre `CloseOnBackdrop` en backdrop tap.
- `GoPrevious` en primer paso silenciosamente ignorado (sin evento, sin audio cue).
- Sin `Time.timeScale` — Tutorial es gameplay-aware; pause se compone vía host (o dentro de Pause).
- Single Next button con label mutante (no Done button separado).
- Animator dedicado por elemento — duplicación intencional (regla del catálogo).
- `LoopBackToFirst` default `false`; `TapToAdvance` default `false`; `CloseOnBackdrop` default `false`.
PENDING (cierre Group A):
- Specs: `Documentation~/Specs/Catalog/{NotificationToast,PausePopup,TutorialPopup}.md`
- Prefabs + entries en Demo scene `Samples~/Catalog/` (Editor manual)
- Validar las **7 decisiones de Pause + 8 de Tutorial** con usuario antes de tag
- Bump `0.4.0-alpha` + tag al cerrar
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Runtime/Catalog/Popups/Tutorial/{TutorialPopupData,UIAnimTutorialPopup,TutorialPopup}.cs, Tests/Editor/TutorialPopupTests.cs, CHANGELOG.md}

### Session — 2026-05-02 (cont.2) — PausePopup scaffolded · 49/49 GREEN · pushed
GOAL: Cerrar 2/4 NotificationToast (TA polish) + scaffoldear 3/4 PausePopup con tests verde, persistir estado en ambos repos.
DONE:
- **`/_checker as qa`** sobre NotificationToast → 2 false positives, no se aplicaron fixes innecesarios. Lección: stress-test deep dives requieren 1-2 file reads antes de simular escenarios.
- **NotificationToast (2/4) TA polish**: `[Tooltip]` en `_defaultDuration` + warning one-shot vía `_themeWarningLogged` cuando `Theme==null` en `OnShow`.
- **PausePopup (3/4 Group A)** scaffold completo:
  - `Runtime/Catalog/Popups/Pause/`: `PausePopupData` (7 botones + 3 toggles inline + flags), `UIAnimPausePopup` (clon de Confirm con `SetUpdate(true)` para unscaled time), `PausePopup` (`UIModule<TData>`, captura/restaura `Time.timeScale` alrededor de show/hide, categorías Dismissing vs Shortcut, toggles que mutan `_data` sin cerrar, public `Resume()`, `OnBackPressed`→`HandleResume`, theme-null warning, `OnDestroy` restaura timeScale si `IsPaused`).
  - `Tests/Editor/PausePopupTests.cs` — 6 tests (Bind null, Back→Resume+Dismiss, Back ignored si IsDismissing, OnShow pausa y Resume restaura, restaura el valor original no hardcoded 1f, Bind reset listeners).
- **EditMode 49/49 verde** headless confirmado (38 Group 0 + 5 Confirm + 5 Toast + 6 Pause).
- **Persistencia**:
  - Package `com.kitforgelabs.mobile-ui-kit` commit `f8d6885` (57 archivos, 1809+/6−) + push (`2175e6f..f8d6885`).
  - Lab tracker commit `51fd7e1` + push (`5f6bd8d..51fd7e1`).
DECISIONS (sin consultar — flag para review):
- Dos categorías de botones en Pause: **Dismissing** (Resume/Restart/Home/Quit) cierran, **Shortcut** (Settings/Shop/Help) emiten evento y mantienen popup abierto.
- Toggles inline (Sound/Music/Vibration) mutan `_data.XxxOn` + emiten `OnXxxChanged(bool)`, nunca cierran.
- `Time.timeScale` directo, sin `ITimeService` (YAGNI hasta 2º consumidor).
- Pause se aplica DESPUÉS de show-anim (callback de `PlayShow`); restore ANTES de hide-anim.
- `UIAnimPausePopup` usa `SetUpdate(true)` defensivo en hide-anim también.
- `CloseOnBackdrop` default = `false` (consistente con catálogo).
- Public `Resume()` para triggers externos (botón gameplay, etc.).
- NO se ha tageado `v0.4.0-alpha` — Group A aún no cerrado (falta Tutorial + specs + prefabs).
PENDING:
- 4/4 **Tutorial** ← NEXT
- Specs: `NotificationToast.md`, `PausePopup.md`, `TutorialPopup.md`
- Prefabs + entries en Demo scene `Samples~/Catalog/`
- Cierre Group A → bump `0.4.0` + tag
- Validar las 7 decisiones de Pause con usuario
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Runtime/Catalog/Popups/Pause/{PausePopupData,UIAnimPausePopup,PausePopup}.cs, Runtime/Catalog/Toasts/NotificationToast.cs, Tests/Editor/PausePopupTests.cs, CHANGELOG.md}

### Session — 2026-05-02 (cont.) — `/_checker as dev` + NotificationToast scaffolded · tests GREEN
GOAL: Auditoría adversarial dev sobre ConfirmPopup, aplicar fixes, arrancar 2/4 NotificationToast.
DONE:
- **`/_checker as dev`** — 5 escenarios (compile drift, AddComponent path, theme rebind, audio router swap, animator override). 2 hallazgos accionables:
  1. `UIModuleBase.OnHide` semántica ambigua (post-hide hook vs trigger) → XML doc clarificadora añadida.
  2. ConfirmPopup `_animator` tipo concreto bloquea testing/sustitución → migrado a `IUIAnimator` + `internal SetAnimatorForTests` seam + `[InternalsVisibleTo]` via `Runtime/Catalog/AssemblyInfo.cs`. `ResetEventListeners` renombrado a `ClearAllEvents` y se llama también en `OnDestroy`. Null-safe en `Animator?.Skip()` y guard en `DismissWithAnimation` cuando animator==null.
- **Group 0 extendido**: `UIToastBase` ahora tiene `DismissRequested`/`IsDismissing`/`Theme`/`Services`/`virtual Initialize`/`RaiseDismissRequested` — alineado con Q1/Q2 de Confirm. Pause/Tutorial heredarán gratis.
- **NotificationToast (2/4 Group A)** scaffold:
  - `Runtime/Catalog/Toasts/`: `ToastSeverity` (Info/Success/Warning/Error), `NotificationToastData` (Message + Severity + DurationOverride + TapToDismiss=true), `NotificationToast` (`UIToast<TData>`, `[RequireComponent]`, severity → tint+icon+cue, `OnTapped`/`OnDismissed`, idempotent `DismissNow`, `ClearAllEvents`), `UIAnimNotificationToast` (slide via `PositionOffset` + fade, sin scale).
  - `ToastManager` ahora hace `Initialize(theme, services)` por toast + lifecycle de `DismissRequested`. ⚠️ **Breaking**: añadido slot `_services` en Inspector — buyers de Group 0 deben re-cablear.
- **EditMode tests**: `NotificationToastTests` (5 tests). **43/43 verde confirmadas por usuario.**
- **Incident**: `ToastManager.cs` quedó corrupto por dos `replace_string_in_file` solapados en la misma franja → archivo borrado y reescrito completo. Lección aprendida: para refactors >1 zona del mismo archivo, dump completo > parches secuenciales.
DECISIONS (sin consultar — flag para review):
- Severity Warning usa `Theme.AccentColor` (sin añadir `WarningColor` dedicado — YAGNI hasta 2º consumidor).
- Solo Success tiene icono (`Theme.IconCheck`); Info/Warning/Error ocultan el Image.
- `TapToDismiss = true` por defecto (convención Snackbar/Toast móvil).
- Toast no maneja back press (no-blocking por definición).
PENDING:
- Spec `Documentation~/Specs/Catalog/NotificationToast.md` (al final del elemento).
- CATALOG.md fila #11 con link a la spec.
- Prefab + Demo scene (diferido al final de Group A).
- 3/4 **Pause** (siguiente).
- 4/4 **Tutorial** (después).
- Bump a `0.4.0` al cerrar Group A entero.
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Runtime/{Toast/{UIToastBase,ToastManager}.cs, Catalog/{AssemblyInfo.cs, Toasts/*.cs, Popups/Confirm/ConfirmPopup.cs}}, Tests/Editor/NotificationToastTests.cs, CHANGELOG.md}

### Session — 2026-05-02 — ConfirmPopup CLOSED · spec + catalog link · tests GREEN
GOAL: Cerrar el elemento entero (1/4 de Group A).
DONE:
- **Tests verde confirmadas por usuario**: 5/5 ConfirmPopupTests + Group 0 PopupManager/UIManager tests (los `LogError` que aparecen en consola son esperados vía `LogAssert.Expect`). Total 38/38.
- **Spec**: `Documentation~/Specs/Catalog/ConfirmPopup.md` creada — DTO completo, services, events, animation contract, theme tokens, edge cases, 10 QA scenarios, file layout, status checklist.
- **CATALOG.md actualizado**: fila ConfirmPopup linka a la spec + nota de single-button alert mode (`ShowCancel=false`).
- **Package CHANGELOG.md actualizado**: entrada completa en `[Unreleased]` con Q1/Q2/Q3, race fixes, event-leak fix, null-safe, magic value killed, TA polish, RequireComponent, tests, spec.
DECISIONS:
- Prefab + Demo scene se cierran al final de Group A (bloque único Confirm+Toast+Pause+Tutorial) → un solo bump a `0.4.0`. No bumpeamos por elemento.
- Spec format = template del CATALOG.md sección 6, ampliado con "EditMode coverage" + "Files" + "Status" para auditoría rápida.
PENDING:
- ConfirmPopup prefab (Editor manual) — bloqueado fuera de chat.
- Entry en Demo scene `Samples~/Catalog/` con `[ContextMenu]` para los 10 QA scenarios.
- Siguiente elemento Group A: **NotificationToast** (Theme tokens + UIAnim_Toast + ToastManager hookup).
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Documentation~/Specs/{CATALOG.md, Catalog/ConfirmPopup.md}, CHANGELOG.md}


> **Source of truth**: `H:/==GIT==/PACHINKO/PACHINKO/Packages/com.kitforgelabs.mobile-ui-kit/CHANGELOG.md`
> This file mirrors high-level milestones only.

## [Unreleased]

### Session — 2026-05-01 — Group A hardening · arch decisions closed
GOAL: Cerrar las 3 preguntas arquitectónicas abiertas + fixes QA críticos antes de seguir con Toast.
DONE:
- **Q1 — Theme/Services injection**: `UIModuleBase.Initialize(theme, services)` virtual + `protected Theme/Services` getters. PopupManager y UIManager inyectan al instanciar. Fuera `GetComponentInParent`. Testable sin escena.
- **Q2 — Dismiss flow**: `UIModuleBase.DismissRequested` (event Action<UIModuleBase>) + `RaiseDismissRequested()` protected. PopupManager se suscribe en ResolvePopup y desuscribe en OnDestroy. Popup ya no necesita ref directa al manager.
- **Q3 — Audio routing**: nuevo `IUIAudioRouter` + `UIAudioCue` enum (None/PopupOpen/PopupClose/ButtonTap/Success/Error/Notification). Slot `Audio` en UIServices (+SetAudio + Resolve + Validate). ConfirmPopup llama `Services?.Audio?.Play(cue)` null-safe en Show/Hide/ButtonTap. MUSTN'T #8 cumplido (cue semántico ≠ audio inline).
- **QA fixes (race conditions)**: `UIModuleBase.IsDismissing` (protected set) elevado a base — todos los popups heredan. ConfirmPopup ignora interacción si IsDismissing=true. Cubre: doble-click confirm/cancel, back press durante hide-anim, backdrop spam.
- **QA fix (event leak)**: ConfirmPopup.Bind() resetea OnConfirmed/OnCancelled/OnDismissed antes de re-bindear. Evita acumulación de handlers en re-Show.
- **QA fix (null DTO)**: ConfirmPopup.OnShow llama Bind(null) si _data==null. Bind(null) ya tolera con `data ?? new ConfirmPopupData()`.
- **TA fixes**: campos del popup agrupados en `[Serializable] private struct Refs` con `[Tooltip]` por campo. Animator: tooltips en _canvasGroup y _card aclarando que _card es el rect que escala (NO la raíz).
- **Magic value killed**: `UIAnimPreset._hideScaleTo` (default 0.9f) reemplaza el `* 0.9f` hardcodeado en UIAnimConfirmPopup. Tunable desde SO sin tocar código.
- **[RequireComponent(typeof(UIAnimConfirmPopup))]** en ConfirmPopup. Auto-resolve del animator en Awake si quedó vacío en Inspector. Cumple MUST 4 (animation per element).
- **EditMode tests**: `Tests/Editor/KitforgeLabs.MobileUIKit.Catalog.Tests.asmdef` + ConfirmPopupTests con 5 tests cubriendo Bind null, back-press routing (con/sin cancel), back-press durante dismiss (race), Bind reset listeners (event leak). **Resultado: 5/5 verde (38/38 total).**
- **Lazy animator resolve**: `ConfirmPopup.Animator` property con fallback `GetComponent` en cada acceso. Fix de NRE en EditMode tests donde `Awake + [SerializeField]` no garantizan asignación cuando el component se añade vía `AddComponent` puro (no prefab). Beneficio extra: cubre instanciación dinámica por código además del flujo Inspector.
DECISIONS:
- Patrón `IsDismissing + DismissRequested` elevado a UIModuleBase desde el inicio. Pause/Tutorial/Toast lo heredan gratis. Coste cero, beneficio multiplicado por 4+ elementos.
- `OnHide` se mantiene como **post-hide hook** (skip de animación residual). El hide real lo dispara el popup desde DismissWithAnimation. Documentado vía contrato (no rename para no romper Group 0).
- IUIAudioRouter sin implementación default — el comprador la provee. Group A no incluye AudioRouter concreto. Diferido a Group post-A si justifica.
PENDING:
- ConfirmPopup prefab + Demo scene + [ContextMenu] QA scenarios
- `Documentation~/Specs/Catalog/ConfirmPopup.md` micro-spec (al final del elemento)
- CATALOG.md: nota sobre single-button mode (alert collapsado en ConfirmPopup vía ShowCancel=false)
- Definition of Done de "elemento Group A" (5 entregables: código + prefab + UIAnim + spec + entry en demo) — pendiente de chat con usuario
- Continuar Group A: NotificationToast (Theme tokens + UIAnim_Toast), luego Pause, luego Tutorial
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Runtime/{Core/{UIModuleBase.cs, PopupManager.cs, UIManager.cs}, Services/{UIServices.cs, IUIAudioRouter.cs, UIAudioCue.cs}, Animation/UIAnimPreset.cs, Catalog/Popups/Confirm/{ConfirmPopup.cs, UIAnimConfirmPopup.cs}}, Tests/Editor/{KitforgeLabs.MobileUIKit.Catalog.Tests.asmdef, ConfirmPopupTests.cs}}

### Session — 2026-05-01 — Group A opened · ConfirmPopup scaffold
GOAL: Open Group A (UI atómica). Pick first element, lock contract, scaffold code.
DONE:
- Group A order locked: Confirm → Toast → Pause → Tutorial
- ConfirmPopup contract closed: DTO (Title/Message/ConfirmLabel/CancelLabel/Tone/ShowCancel/CloseOnBackdrop), 3 events (OnConfirmed/OnCancelled/OnDismissed), tones Neutral|Destructive|Positive
- ConfirmTone enum + Positive added to scope
- Catalog asmdef created: `KitforgeLabs.MobileUIKit.Catalog` (refs Runtime + DOTween.Modules + TMP)
- `Runtime/Catalog/_Internal/UIAnimEaseConverter` shared helper (UIAnimEase → DG.Tweening.Ease)
- `Runtime/Catalog/Popups/Confirm/`: ConfirmTone, ConfirmPopupData, ConfirmPopup (UIModule<TData>), UIAnimConfirmPopup (IUIAnimator + DOTween)
- Theme/Services binding: popup resolves PopupManager via GetComponentInParent (no Find/Singleton); reads theme + anim preset from there
- MUSTN'T compliance: no audio inline (deferred to Theme/host), no Resources.Load, no static refs, no PopupManager.Show calls to other popups
DECISIONS:
- New asmdef per layer (catalog separated from runtime) — runtime stays DOTween-free as planned
- Localization out: DTO uses raw strings (no *Key suffix). Buyer handles i18n
- Single ConfirmPopup with `ShowCancel=false` covers Alert use case (no separate AlertPopup)
- `CloseOnBackdrop=false` is the default for the whole catalog
- Animator pattern: per-element MonoBehaviour implementing IUIAnimator; preset resolved by popup from Theme.AnimPresetLibrary
PENDING:
- ConfirmPopup prefab + Demo scene + [ContextMenu] QA scenarios
- `Documentation~/Specs/Catalog/ConfirmPopup.md` micro-spec (deferred to end of element per session decision)
- EditMode tests for ConfirmPopup (Bind/back-press routing/event ordering)
- Continue Group A: NotificationToast (Theme tokens + UIAnim_Toast), then Pause, then Tutorial
REFS: Packages/com.kitforgelabs.mobile-ui-kit/Runtime/Catalog/{KitforgeLabs.MobileUIKit.Catalog.asmdef, _Internal/UIAnimEaseConverter.cs, Popups/Confirm/{ConfirmTone.cs, ConfirmPopupData.cs, ConfirmPopup.cs, UIAnimConfirmPopup.cs}}

### Session — 2026-05-01 — Group 0 foundation closed
GOAL: Build the foundation layer (F1–F8) for the 15-element catalog and tag v0.3.0-alpha.
DONE:
- F1 ToastManager / F2 UIHUDBase / F3 UIServices container / F4 PopupManager.Theme exposure
- F5 UIAnimStyle + UIAnimEase + UIAnimChannel + UIAnimPreset (SO) + UIAnimPresetLibrary (SO) + IUIAnimator
- F6 UIThemeConfig extended (10 sprite slots, 6 audio slots, MinTouchTarget, DefaultAnimStyle, AnimPresetLibrary)
- F7 SafeAreaFitter / F8 UIKitValidator (menu + IPreprocessBuildWithReport)
- `Documentation~/Specs/CATALOG.md`: 15 elements, 10 MUST + 8 MUSTN'T, 5-group order, Disney Getaway Blast as visual reference, Playful as default style
- Commit `6b2e43c` + tag `v0.3.0-alpha`
PENDING:
- Group A (UI atómica): primer elemento del catálogo  ← NEXT
- 10 default UIAnimPreset SO assets (deferred until Group A demands them)
- _tween / _tween-dev agent update to consume UIAnimPreset (deferred)
- Group 0 demo sample + EditMode tests for new components (deferred)
DECISIONS:
- Animation system uses ScriptableObject preset library (style→preset map) so buyers can tune without code
- Service binding pattern locked = UIServices container (Inspector-driven, DI-optional)
- Runtime asmdef stays DOTween-free; catalog asmdef will reference DOTween
- Versioning at end of each Group (not per element)
REFS: Packages/com.kitforgelabs.mobile-ui-kit/{Runtime/{Animation,Toast,HUD,SafeArea,Services}/*, Editor/Validation/UIKitValidator.cs, Documentation~/Specs/CATALOG.md, CHANGELOG.md}
