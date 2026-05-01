# KF_MobileUIKit — ChangeLog (Lab Tracker)

_Last updated: 2026-05-02_

## [Unreleased]

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
