# KF_UIBible — KITforge UI/UX Design System
Scope: KITforge labs — all editor tools
Applies: BUILD phase, any session generating or reviewing EditorWindow UI
Last updated: 2026-04-12

---

## OBJETIVO

Toda herramienta KITforge debe ser **reconocible como KITforge** antes de que el usuario lea su nombre.
Eso se consigue con tres elementos únicos + disciplina de no añadir más decoración que la necesaria.
El resto de la calidad premium viene de UX depurada, no de decoración adicional.

---

## 1. Identidad Visual — Los 3 Elementos

### 1.1 Accent Stripe (2px #fec100 arriba y abajo)

El elemento más distintivo. Un borde de 2px en el color accent corporativo en los bordes
superior e inferior de toda EditorWindow KITforge. No aparece en ningún otro contexto.

**Implementación en UI Toolkit:**

```css
/* En KF_Window.uss — clase base de toda ventana KITforge */
.kf-root {
    flex-grow: 1;
    border-top-color: var(--kf-accent);
    border-top-width: 2px;
    border-bottom-color: var(--kf-accent);
    border-bottom-width: 2px;
    border-left-width: 0;
    border-right-width: 0;
}
```

`kf-root` es el primer hijo del `rootVisualElement` de cada EditorWindow. Contiene todo.

**Regla de uso:**
- El stripe SOLO existe en las dos posiciones descritas. Nunca en bordes laterales.
- No añadir sombras, gradientes ni glows sobre el stripe. Es línea plana. Su potencia viene de la consistencia.

---

### 1.2 Bulb Icon — Logo KITforge en el Header

El icono de la bombilla (logo KITforge) aparece a la izquierda del nombre de la tool en el header.

**Spec del asset (proveer al AI):**
- Formato: PNG con canal alpha
- Tamaños: `KF_BulbIcon.png` (16×16px) + `KF_BulbIcon@2x.png` (32×32px)
- Color del icono: **blanco (#FFFFFF)** sobre fondo transparente
- El USS aplica el tinte #fec100 vía `--unity-image-tint-color: var(--kf-accent)` → un solo asset, sin variantes de color
- Location en producto: `KF_<Slug>/Editor/Icons/KF_BulbIcon.png` + `KF_BulbIcon@2x.png`
- Unity carga el @2x automáticamente en displays HiDPI

**Carga en C#:**
```csharp
// En KF_WindowBase.cs o en el CreateGUI de cada ventana
var bulbIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(
    "Assets/Plugins/KITforge Labs/KF_<Slug>/Editor/Icons/KF_BulbIcon.png");
_headerIcon.style.backgroundImage = new StyleBackground(bulbIcon);
```

Hasta que el asset PNG esté disponible: usar `EditorGUIUtility.IconContent("d_lightRig Icon")` como placeholder.
No bloquear BUILD por el icono.

---

### 1.3 Header Structure — la anatomía estándar

Toda EditorWindow KITforge tiene exactamente este layout vertical:

```
╔═════════════════════════════════════════════════════════╗  ← 2px #fec100 (kf-root border-top)
║                                                         ║
║  [💡]  PaletteKit                      v1.0   [?]  [⋮] ║  ← kf-header
║─────────────────────────────────────────────────────────║  ← kf-header-divider (1px border-bottom)
║                                                         ║
║                   CONTENT ZONE                          ║  ← kf-content
║                                                         ║
║─────────────────────────────────────────────────────────║  ← kf-statusbar-divider (1px border-top)
║  ● Applied to 15 materials                    [Undo]   ║  ← kf-statusbar
╚═════════════════════════════════════════════════════════╝  ← 2px #fec100 (kf-root border-bottom)
```

**Jerarquía de VisualElements:**
```
rootVisualElement
  └── kf-root                   ← .kf-root (tiene los 2 bordes accent)
        ├── kf-header            ← .kf-header
        │     ├── [icon]         ← .kf-header__icon
        │     ├── [tool name]    ← .kf-header__title
        │     ├── [spacer]       ← flex-grow: 1
        │     ├── [version]      ← .kf-header__version
        │     ├── [?]            ← .kf-header__btn-help
        │     └── [⋮]            ← .kf-header__btn-menu
        ├── kf-content           ← .kf-content (flex-grow: 1)
        └── kf-statusbar         ← .kf-statusbar
```

---

## 2. Design Tokens (KF_DesignTokens.uss)

Archivo: `KF_<Slug>/Editor/USS/KF_DesignTokens.uss`
Este archivo se copia/duplica en cada producto hasta que exista `KF_Shared`. Cuando haya 2+
productos, se centraliza. Hasta entonces, NUNCA crear dependencias cross-producto.

```css
/* KF_DesignTokens.uss — NO modificar valores sin actualizar todos los productos */
:root {
    /* ── ACCENT (uso MUY restringido — ver reglas) ───────────────── */
    --kf-accent:              #FEC100;
    --kf-accent-hover:        #FFD740;
    --kf-accent-text:         #1A1400;   /* texto sobre fondo #fec100 */

    /* ── BACKGROUNDS ─────────────────────────────────────────────── */
    --kf-bg-window:           #2C2C2C;   /* fondo principal de ventana */
    --kf-bg-surface:          #383838;   /* cards, rows, panels */
    --kf-bg-surface-hover:    #424242;
    --kf-bg-surface-alt:      #303030;   /* filas alternas en listas */
    --kf-bg-input:            #252525;   /* TextField, ObjectField */

    /* ── BORDERS ─────────────────────────────────────────────────── */
    --kf-border:              #1E1E1E;
    --kf-border-subtle:       #2E2E2E;
    --kf-border-focus:        #FEC100;   /* focus ring = accent */

    /* ── TEXT ────────────────────────────────────────────────────── */
    --kf-text-primary:        #E0E0E0;
    --kf-text-secondary:      #9E9E9E;
    --kf-text-disabled:       #616161;
    --kf-text-accent:         #FEC100;   /* solo para labels de énfasis */

    /* ── SEMANTIC ────────────────────────────────────────────────── */
    --kf-success:             #4CAF50;
    --kf-success-bg:          rgba(76,175,80,0.12);
    --kf-warning:             #FFC107;
    --kf-warning-bg:          rgba(255,193,7,0.12);
    --kf-error:               #F44336;
    --kf-error-bg:            rgba(244,67,54,0.12);
    --kf-info:                #42A5F5;
    --kf-info-bg:             rgba(66,165,245,0.12);

    /* ── INTERACTIVE ─────────────────────────────────────────────── */
    --kf-btn-primary-bg:      #FEC100;
    --kf-btn-primary-hover:   #FFD740;
    --kf-btn-primary-text:    #1A1400;
    --kf-btn-secondary-bg:    #424242;
    --kf-btn-secondary-hover: #4E4E4E;
    --kf-btn-danger-bg:       #C62828;
    --kf-btn-danger-hover:    #D32F2F;

    /* ── SPACING ─────────────────────────────────────────────────── */
    --kf-space-xs:  4px;
    --kf-space-s:   8px;
    --kf-space-m:   12px;
    --kf-space-l:   16px;
    --kf-space-xl:  24px;
    --kf-space-2xl: 32px;

    /* ── TYPOGRAPHY ──────────────────────────────────────────────── */
    --kf-font-xs:   10px;
    --kf-font-s:    11px;
    --kf-font-m:    12px;   /* base del editor Unity */
    --kf-font-l:    14px;   /* tool name en header */
    --kf-font-xl:   16px;

    /* ── BORDER RADIUS ───────────────────────────────────────────── */
    --kf-radius-s:    2px;
    --kf-radius-m:    4px;
    --kf-radius-full: 9999px;
}
```

**REGLA DE USO DEL ACCENT (#FEC100):**
El amarillo tiene alto contraste y llamatividad. Usarlo mal destruye la identidad y produce cansancio visual.
Posiciones permitidas:
1. `border-top` + `border-bottom` de `kf-root` → firma de marca
2. Icono de la bombilla en header (tint)
3. Focus ring en inputs/campos cuando están activos
4. Texto de enlace o badge de "New" (solo si hay funcionalidad nueva)
5. Botón CTA primario (background en botones de acción principal)
**PROHIBIDO:** fondos de sección, headers de grupos, separadores decorativos, badges numéricos, iconos random.

---

## 3. Componentes UI (UXML + USS)

Cada componente es un `VisualElement` con clase USS bien definida. Implementación: C# separado o UXML fragment.

---

### KF_Header

**Propósito:** Header universal de toda EditorWindow KITforge.
**Archivo UXML:** `KF_Header.uxml` (fragment, no standalone window)
**Altura:** 32px fija

```xml
<!-- KF_Header.uxml -->
<ui:VisualElement class="kf-header">
    <ui:VisualElement name="kf-header-icon" class="kf-header__icon" />
    <ui:Label name="kf-header-title" class="kf-header__title" text="ToolName" />
    <ui:VisualElement class="kf-header__spacer" />
    <ui:Label name="kf-header-version" class="kf-header__version" text="v1.0" />
    <ui:Button name="kf-header-help" class="kf-header__btn" tooltip="Documentation" />
    <ui:Button name="kf-header-menu" class="kf-header__btn" tooltip="Options" />
</ui:VisualElement>
```

```css
/* En KF_Window.uss */
.kf-header {
    flex-direction: row;
    align-items: center;
    height: 32px;
    padding-left: var(--kf-space-s);
    padding-right: var(--kf-space-s);
    background-color: var(--kf-bg-window);
    border-bottom-width: 1px;
    border-bottom-color: var(--kf-border);
}
.kf-header__icon {
    width: 16px;
    height: 16px;
    margin-right: var(--kf-space-s);
    --unity-image-tint-color: var(--kf-accent);
}
.kf-header__title {
    font-size: var(--kf-font-l);
    color: var(--kf-text-primary);
    -unity-font-style: bold;
}
.kf-header__version {
    font-size: var(--kf-font-xs);
    color: var(--kf-text-disabled);
    margin-right: var(--kf-space-s);
}
.kf-header__spacer {
    flex-grow: 1;
}
.kf-header__btn {
    width: 20px;
    height: 20px;
    background-color: transparent;
    border-width: 0;
    padding: 0;
    margin: 0 2px;
}
.kf-header__btn:hover {
    background-color: var(--kf-bg-surface-hover);
    border-radius: var(--kf-radius-s);
}
```

---

### KF_StatusBar

**Propósito:** Barra inferior de toda EditorWindow KITforge. Muestra el resultado de la última operación.
**Estado por defecto:** vacío/invisible. Aparece tras cada operación.
**Altura:** 24px fija
**Behavior:** el mensaje desaparece después de 5 segundos o al ejecutar otra operación.

```css
.kf-statusbar {
    flex-direction: row;
    align-items: center;
    height: 24px;
    padding: 0 var(--kf-space-s);
    background-color: var(--kf-bg-window);
    border-top-width: 1px;
    border-top-color: var(--kf-border);
}
.kf-statusbar__dot {          /* ● indicador de estado */
    width: 6px;
    height: 6px;
    border-radius: var(--kf-radius-full);
    margin-right: var(--kf-space-s);
}
.kf-statusbar__dot--success { background-color: var(--kf-success); }
.kf-statusbar__dot--warning { background-color: var(--kf-warning); }
.kf-statusbar__dot--error   { background-color: var(--kf-error);   }
.kf-statusbar__message {
    flex-grow: 1;
    font-size: var(--kf-font-s);
    color: var(--kf-text-secondary);
}
.kf-statusbar__action {       /* botón secundario inline (ej: [Undo]) */
    font-size: var(--kf-font-s);
    color: var(--kf-text-accent);
    background-color: transparent;
    border-width: 0;
    cursor: link;
}
```

**API en C# (helper estático):**
```csharp
// Llamada desde cualquier operación:
KF_StatusBar.Show(statusBar, KF_StatusType.Success, "Applied to 15 materials", onUndo: RevertAll, autoClearSeconds: 5f);
```

---

### KF_EmptyState

**Propósito:** Estado vacío de cualquier lista, panel o zona de contenido sin datos.
**Regla:** Nunca dejar un panel en blanco. SIEMPRE un KF_EmptyState.

```xml
<!-- KF_EmptyState.uxml -->
<ui:VisualElement class="kf-empty-state">
    <ui:VisualElement class="kf-empty-state__icon" />
    <ui:Label class="kf-empty-state__title" text="No palettes yet" />
    <ui:Label class="kf-empty-state__subtitle" text="Create a palette to get started." />
    <ui:Button class="kf-empty-state__cta kf-btn--primary" text="+ Create Palette" />
</ui:VisualElement>
```

```css
.kf-empty-state {
    flex-grow: 1;
    align-items: center;
    justify-content: center;
    padding: var(--kf-space-2xl);
    /* gap: NOT supported in UITK — use margin-bottom on children instead */
}
.kf-empty-state__icon {
    width: 32px;
    height: 32px;
    opacity: 0.3;
    margin-bottom: var(--kf-space-s);
}
.kf-empty-state__title {
    font-size: var(--kf-font-l);
    color: var(--kf-text-primary);
    -unity-font-style: bold;
    -unity-text-align: middle-center;
}
.kf-empty-state__subtitle {
    font-size: var(--kf-font-m);
    color: var(--kf-text-secondary);
    -unity-text-align: middle-center;
    margin-bottom: var(--kf-space-m);
}
.kf-btn--primary {
    background-color: var(--kf-btn-primary-bg);
    color: var(--kf-btn-primary-text);
    border-width: 0;
    border-radius: var(--kf-radius-m);
    padding: var(--kf-space-s) var(--kf-space-l);
    -unity-font-style: bold;
    font-size: var(--kf-font-m);
}
.kf-btn--primary:hover { background-color: var(--kf-btn-primary-hover); }
```

---

### KF_InlineBanner

**Propósito:** Feedback inline (error, warning, info, success) sin recurrir a la consola.
**Regla:** Los errores de USER INPUT van aquí, NUNCA a la consola.

```css
.kf-banner {
    flex-direction: row;
    align-items: flex-start;
    padding: var(--kf-space-s) var(--kf-space-m);
    margin: var(--kf-space-s);
    border-radius: var(--kf-radius-m);
    border-left-width: 3px;
}
.kf-banner--error   { background-color: var(--kf-error-bg);   border-left-color: var(--kf-error);   }
.kf-banner--warning { background-color: var(--kf-warning-bg); border-left-color: var(--kf-warning); }
.kf-banner--success { background-color: var(--kf-success-bg); border-left-color: var(--kf-success); }
.kf-banner--info    { background-color: var(--kf-info-bg);    border-left-color: var(--kf-info);    }
.kf-banner__text {
    font-size: var(--kf-font-m);
    color: var(--kf-text-primary);
    flex-grow: 1;
    white-space: normal;
}
```

---

### KF_PlayModeBanner

**Propósito:** Superpone o reemplaza la content zone durante Play Mode. Bloquea toda operación de escritura.
**Comportamiento:** Se muestra si `EditorApplication.isPlayingOrWillChangePlaymode == true`.

```css
.kf-playmode-banner {
    position: absolute;
    top: 0; left: 0; right: 0; bottom: 0;
    background-color: rgba(0,0,0,0.65);
    align-items: center;
    justify-content: center;
    flex-direction: column;
    /* gap: NOT supported in UITK — use margin-bottom on children instead */
}
.kf-playmode-banner__title {
    font-size: var(--kf-font-xl);
    color: var(--kf-warning);
    -unity-font-style: bold;
    margin-bottom: var(--kf-space-s);
}
.kf-playmode-banner__subtitle {
    font-size: var(--kf-font-m);
    color: var(--kf-text-secondary);
}
```

**Activación en C#:**
```csharp
// En CreateGUI o OnEnable:
EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

private void OnPlayModeStateChanged(PlayModeStateChange state)
{
    bool isPlaying = state == PlayModeStateChange.EnteredPlayMode
                  || state == PlayModeStateChange.ExitingEditMode;
    _playModeBanner.style.display = isPlaying ? DisplayStyle.Flex : DisplayStyle.None;
}
```

---

## 4. UX Patterns — Reglas de Comportamiento

### P1 — Operación destructiva (Apply, Reset, Delete batch)

1. Botón primario con label que cuantifica: `"Apply to 15 Materials"` (no `"Apply"`)
2. Si la operación afecta > 20 items o es irreversible: diálogo de confirmación de 1 línea.
   Si tiene Undo: NO pedir confirmación — el Undo es suficiente salvaguarda.
3. Post-operación: `KF_StatusBar.Show(Success, "Applied to N · Ctrl+Z to undo", autoClear: 5s)`
4. Si ocurre error parcial: `KF_StatusBar.Show(Warning, "Applied to 12. Skipped 3 — [See details]")`

### P2 — Empty state (primera vez o lista vacía)

1. Nunca un panel en blanco. Siempre `KF_EmptyState`.
2. Título: estado actual. Subtítulo: qué hacer. CTA: acción directa.
3. El icono del empty state debe ser el icono built-in más relacionado con el contenido esperado.
4. No usar iconos de error para empty states — no es un error, es un estado inicial.

### P3 — Operación lenta (> 500ms)

1. Mostrar `ProgressBar` inmediatamente al iniciar (no esperar al primer tick).
2. Label dinámico: `"Scanning... 47 / 120 materials"` → progreso cuantificado.
3. Si cancelable: botón `"Cancel"` visible junto a la barra. Si no: no ofrecer Cancel.
4. Al terminar: ocultar ProgressBar, mostrar resultado en StatusBar.

### P4 — Play Mode

1. Detectar en `OnEnable` + suscribir a `EditorApplication.playModeStateChanged`.
2. Mostrar `KF_PlayModeBanner` sobre la content zone. No `SetActive(false)` de la window entera.
3. Desuscribir en `OnDisable`.
4. PROHIBIDO ejecutar cualquier `AssetDatabase`, `Undo`, `Material.SetColor` en Play Mode.

### P5 — Domain Reload

1. Guardar estado mínimo necesario en `EditorPrefs` en `OnDisable`.
2. En `OnEnable`: restaurar estado + limpiar todo state no persistible (previews, MPB).
3. Si había preview activo: revertir silenciosamente + loguear con `Debug.Log`.
4. La ventana NUNCA lanza NullReferenceException tras domain reload. Si puede hacerlo, añadir null guard.

### P6 — Primera ejecución (onboarding)

1. Si la tool no tiene configuración guardada: mostrar `KF_EmptyState` con CTA de primer paso.
2. No mostrar wizards, pasos, paneles modales de onboarding. Una CTA es suficiente.
3. El primer uso debe funcionar en < 2 minutos sin documentación (Brief §7 de todo producto).

### P7 — Undo

1. TODA operación que modifica assets debe usar `Undo.RecordObject` / `Undo.RegisterCompleteObjectUndo` ANTES de modificar.
2. El undo de batch: registrar cada asset individualmente en un loop (no un único RegisterCompleteObjectUndo sobre el batch).
3. Después de Apply: el StatusBar muestra `"Ctrl+Z reverts all N changes"` como recordatorio.

---

## 5. Copy — Voz de la UI

### Principios

| Principio | Mal | Bien |
|-----------|-----|------|
| Orientado al resultado | `"Apply Colors"` | `"Apply Palette to 12 Materials"` |
| Cuantificado | `"Done"` | `"Applied to 12 materials (1 skipped)"` |
| Conciso | `"This operation will apply the colors defined in the current palette to all materials assigned to roles"` | `"Apply palette to all assigned materials?"` |
| Empoderador | `"No materials found"` | `"No materials assigned yet. [+ Add Material]"` |
| Sin jerga interna | `"Undo.RecordObject failed"` | `"Could not register undo. Try again."` |
| Sin capitales random | `"Apply Colors To Materials"` | `"Apply colors to materials"` — Solo primera letra en mayúscula |

### Formato de mensajes de error

```
[Qué pasó] — [Por qué] — [Qué hacer]

Ejemplo:
"Material 'Mat_Player' no longer exists. Remove it from this role to continue."
"Apply failed: no materials assigned to any role. [+ Assign Materials]"
```

### Tooltips

Formato: acción + resultado + shortcut si existe.
```
"Apply Palette — Writes colors to all assigned materials using Undo. [Ctrl+Enter]"
"Preview — Shows colors in Scene View using MaterialPropertyBlock (non-destructive)."
```

---

## 6. Iconos — Biblioteca Unity Built-in

Usar iconos built-in de Unity al máximo. No añaden tamaño al package.
Acceso: `EditorGUIUtility.IconContent("iconName")` o en USS `background-image: resource("icon-name")`.

| Acción | Nombre icon Unity | Uso |
|--------|------------------|-----|
| Add / New | `d_Toolbar Plus` | Botón añadir rol, material |
| Remove / Delete | `d_Toolbar Minus` | Quitar asignación |
| Scan / Refresh | `d_Refresh` | Escanear proyecto |
| Settings / Options | `d_Settings` | Menú options |
| Warning | `d_console.warnicon.sml` | InlineBanner warning |
| Error | `d_console.erroricon.sml` | InlineBanner error |
| Info | `d_console.infoicon.sml` | InlineBanner info |
| Preview / Eye | `d_scenevis_visible_hover` | Botón preview |
| Apply / Check | `d_FilterByLabel` | Botón apply (alternativa) |
| Undo | `d_Undo` | Botón undo en StatusBar |
| Folder | `d_FolderOpened Icon` | Scope selector |
| Search | `d_Search Icon` | Campo de búsqueda |
| Script | `d_cs Script Icon` | Referencia a código |
| Material | `d_Material Icon` | Lista de materiales |

Referencia completa: https://github.com/halak/unity-editor-icons

---

## 7. Archivo USS por producto — Estructura mínima

Cada producto tiene un único USS que importa los tokens y define estilos locales:

```
KF_<Slug>/Editor/USS/
    KF_DesignTokens.uss     ← Copia de los tokens (hasta que exista KF_Shared)
    KF_Window.uss           ← Layout base (kf-root, kf-header, kf-content, kf-statusbar)
    KF_Components.uss       ← Componentes (kf-empty-state, kf-banner, kf-btn-*)
    KF_<Slug>Specific.uss   ← Estilos específicos del producto (si los hay)
```

```css
/* KF_Window.uss — import chain */
@import url("KF_DesignTokens.uss");
@import url("KF_Components.uss");

/* Layout base */
.kf-root {
    flex-grow: 1;
    border-top-color: var(--kf-accent);
    border-top-width: 2px;
    border-bottom-color: var(--kf-accent);
    border-bottom-width: 2px;
    border-left-width: 0;
    border-right-width: 0;
    background-color: var(--kf-bg-window);
}
.kf-content {
    flex-grow: 1;
    padding: var(--kf-space-m);
}
```

---

## 8. Checklist de calidad UI (integrar en KF_QARules)

Antes de marcar cualquier CP de BUILD como ✅, verificar:

- [ ] Ventana tiene accent stripe arriba y abajo (2px #fec100)
- [ ] Header contiene: bulb icon (o placeholder) + tool name + versión + [?] + [⋮]
- [ ] Toda lista vacía tiene KF_EmptyState (no panel en blanco)
- [ ] Toda operación destructiva muestra resultado en StatusBar
- [ ] Play Mode muestra KF_PlayModeBanner, bloquea todas las operaciones de escritura
- [ ] El botón de acción principal está en posición consistente (bottom-right o action bar)
- [ ] No hay texto en mayúsculas random ni jerga técnica interna expuesta al usuario
- [ ] Tooltips en TODOS los botones sin label visible
- [ ] `raycastTarget = false` en todos los elementos puramente decorativos
- [ ] Zero errores en consola al abrir la ventana por primera vez
- [ ] Zero errores en consola tras domain reload con la ventana abierta
