# Planner Workbook — KITforge UI/UX Bible Research
Session: 2026-04-11
Objective: Identificar referentes, extraer patrones, proponer estructura de la "biblia de UI"

---

## RESEARCH — Referentes analizados (Asset Store)

### Tier 1 — El gold standard

#### Odin Inspector (Sirenix)
- **€50.60 | 12,038 favoritos | 776 reviews 5★ | Verified Solution**
- Por qué ES la referencia:
  - Más de 100 atributos, todos con apariencia visual coherente
  - Group attributes con foldouts, tabs, boxes, horizontal groups → información organizada por jerarquía
  - Inline validation: los errores y warnings aparecen en el Inspector, no en la consola
  - List drawer: drag&drop, reorder, cross-window dragging, paging
  - Consistent visual language: sea cual sea el atributo, todo se ve "del mismo palo"
  - La herramienta ENSEÑA qué puede hacer solo con usarla — autodescubrimiento
- Lección clave: **el inspector ES el producto**. Nadie ve el código. La UX del inspector es lo que vende el screenshot, lo que genera la review.

#### Behavior Designer (Opsive)
- **€87.40 | 10,090 favoritos | 761 reviews 5★ | Mature product**
- Por qué: Made complex AI authoring APPROACHABLE through visual design
  - Visual editor de behavior trees con layout automático limpio
  - Real-time debugger visual (colores de estado en nodos durante Play)
  - Contextual Aborts visualmente diferenciados
  - Toolbar consistente con iconos que comunican acción inmediata
- Lección clave: **la complejidad técnica se oculta detrás de una UX coherente**. El dev no piensa en el código, piensa en el árbol.

#### Feel (More Mountains)
- **€46 | 8,635 favoritos | 233 reviews 5★ | Unity Awards 2021 + Publisher of the Year 2023**
- Por qué: El MEJOR ejemplo de brand identity cross-product en el Asset Store
  - El inspector de MMFeedbacks es reconocible: lista de feedbacks, toggle, enable/disable, iconos consistentes
  - La misma identidad visual en Corgi Engine, TopDown Engine → **marca reconocible de un solo vistazo**
  - Website, docs, Discord, todas las propiedades digitales siguen la misma paleta
  - Micro-award: "Author of the Year" — signal de que la calidad de toda la suite es coherente
- Lección clave: **la marca vende la suite, no cada tool individual.** "More Mountains" tiene fans que compran TODO lo que publican.

#### Shapes (Freya Holmér)
- **€92 | 5,514 favoritos | 97 reviews 5★**
- Por qué: La API ES la UX. El código que escribe el usuario es legible, expresivo, correcto gramáticamente como prosa.
- Secundario: la página web, el changelog público, la personalidad de la autora son PARTE del producto.
- Lección clave: **la calidad se percibe antes de download**. El store page, las screenshots, el changelog son la primera impresión del estándar de calidad.

---

### Tier 2 — Lo que separa "bueno" de "mediocre"

#### Lo que hace cada herramienta MEDIOCRE de la store

| Anti-patrón | Origen | Consecuencia |
|-------------|--------|-------------|
| GUILayout sin estilo | IMGUI por defecto | Parece Unity 2013 en screenshots |
| Colores aleatorios en windows | Sin design token | Sin identidad de marca |
| Lista vacía = panel en blanco | No hay empty states | El usuario no sabe qué hacer en primer uso |
| Errores van a la consola | Sin inline feedback | Interrumpe el flujo, no sabe que falló |
| Botones sin jerarquía visual | Sin primary/secondary | El usuario no sabe qué acción es principal |
| Window sin header ni identidad | Sin branding | No sabe de qué empresa / qué versión |
| No Play Mode awareness | Sin guards | Opera durante Play, modifica assets activos |
| No undo feedback | Silent operations | El usuario no sabe si el operación funcionó |

#### Lo que hace cada herramienta PREMIUM de la store

| Patrón | Ejemplo real | Efecto |
|--------|-------------|--------|
| Window header con nombre del producto | Odin "OdinInspector", Feel package header | Identidad inmediata, trust |
| Inline validation colorada | Odin: Required muestra rojo, warnings amarillo | Zero contexto-switch al console |
| Empty states con CTA | Window sin contenido → "Create your first X [button]" | Primer uso exitoso |
| Progress + resultado cuantificado | "Scanning... 47/120 materials" → "Applied to 47 materials" | Feedback loop satisfactorio |
| Section collapsibles con estado persistente | Odin TabGroup, FoldoutGroup | Organización sin scrolling infinito |
| Consistent action zone | Action bar arriba o abajo, nunca flotante random | Motor memory: siempre sé dónde está |
| Play Mode banner | Feel: window disabled durante Play | No modifica assets activos accidentalmente |
| Domain reload survival | Odin: state persiste, no explota | Trust en la herramienta |

---

## ANÁLISIS — ¿Qué oportunidad tiene KITforge?

### El gap del mercado

**Nadie en 2026 tiene un design system cross-tool para Unity editor tools.**

Está así el estado del arte:
- **IMGUI tools**: parecen viejas y parecen hechas por devs, no por diseñadores.
- **UI Toolkit sin inversión**: funcionales, no memorables. Sin identidad.
- **Feel/CorgiEngine/TopDown**: misma publisher, misma paleta en inspectors. Pero son frameworks de gameplay, no tools de editor.
- **Odin**: tiene consistencia interna magnifica, pero solo un producto con muchos atributos.
- **Nadie**: ha dicho "tenemos 10 editor tools y TODAS tienen el mismo look & feel, el mismo header, los mismos patterns, los mismos mensajes de error".

**Esa es la ventaja diferencial KITforge: ser la primera editorial con un design system real y coherente para su suite de tools.**

No es "herramienta bonita". Es "las herramientas de KITforge son inmediatamente reconocibles porque vienen de un estudio que se toma en serio la calidad visual."

---

## ANÁLISIS — Estructura propuesta para la KF_UIBible.md

### Capítulo 1: Fundamentos — Design Tokens (USS Variables)
Variables únicas definidas en un USS compartido (`KF_Shared.uss` o similar):
- **Color palette**: background, surface, border, interactive, danger, success, warning + sus variantes dark/hover/disabled
- **Spacing system**: token de 4px base → 4, 8, 12, 16, 24, 32, 48px
- **Typography**: tamaños (10, 12, 14px), pesos (regular, medium, bold), colores (primary, secondary, disabled, accent)
- **Border radius**: solid (0), subtle (2px), medium (4px), full (9999px)
- **Elevation/shadows**: editor no usa mucho, pero hay border tokens para separadors

### Capítulo 2: Identidad Visual de Ventana

**KF Window Anatomy** — toda EditorWindow KITforge tiene:
```
┌──────────────────────────────────────────────────┐
│ [K] KITforge  PaletteKit            v1.0 [?] [⋮] │  ← KF_Header (componente)
├──────────────────────────────────────────────────┤
│                                                  │
│              CONTENT ZONE                        │
│                                                  │
├──────────────────────────────────────────────────┤
│ ● Applied to 15 materials  [Undo]                │  ← KF_StatusBar (componente)
└──────────────────────────────────────────────────┘
```

Reglas del header:
- Logo KITforge (icono [K] o wordmark, 16x16) + nombre del tool
- Versión en texto pequeño/secundario
- Botón [?] abre documentación (URL externa o inline panel)
- Botón [⋮] menú extra (About, Report Bug, Release Notes)

### Capítulo 3: Componente Library (UI Toolkit)

| Componente | Clase VisualElement | Uso |
|------------|---------------------|-----|
| `KF_WindowHeader` | Header de toda EditorWindow | Universal |
| `KF_StatusBar` | Barra inferior de estado | Universal |
| `KF_EmptyState` | Panel vacío con icono + texto + CTA | Toda lista vacía |
| `KF_InlineBanner` | Warning / Error / Info inline | Feedback no-consola |
| `KF_ActionButton` | Botón CTA (primary/secondary/danger) | Toda acción destructiva |
| `KF_SectionHeader` | Header de sección colapsable | Organización de contenido |
| `KF_ProgressBar` | Barra de progreso con label | Operaciones > 0.5s |
| `KF_CountBadge` | Badge numérico | Contadores en tabs/secciones |
| `KF_PlayModeBanner` | Banner rojo "Disabled in Play Mode" | Toda window con Write ops |
| `KF_TitleWithSearch` | Section header + search field | Toda lista filtrable |

### Capítulo 4: UX Patterns

#### Pattern: Operación destructiva (Apply, Delete, Reset)
1. Botón CTA con label descriptivo ("Apply to 15 Materials")
2. Si > 10 items o irreversible: confirmación modal (brevísima, sin parrafos)
3. Post-operación: KF_StatusBar muestra "Applied to N items · Ctrl+Z to undo" por 5s

#### Pattern: Primera vez (empty state)
1. No blank panel. Siempre un KF_EmptyState
2. Icono neutro (no error icon) + título + 1 frase descriptiva + CTA button
3. Ejemplo: "No palette yet. [+ Create your first palette]"

#### Pattern: Operación lenta (> 0.5s)
1. KF_ProgressBar visible inmediatamente
2. Label dinámico: "Scanning... 47/120"
3. Botón Cancel si la operación puede interrumpirse
4. Resultado final en KF_StatusBar

#### Pattern: Play Mode
1. Detectar `EditorApplication.isPlayingOrWillChangePlaymode`
2. Mostrar KF_PlayModeBanner sobre el content zone
3. Deshabilitar TODOS los botones de Write durante Play

#### Pattern: Domain Reload
1. Guardar estado serializable en `EditorPrefs` o via `ISerializationCallbackReceiver`
2. En `OnEnable`: restaurar estado, limpiar preview (MaterialPropertyBlock), no explotar
3. Regla: si hay preview activo y ocurre domain reload → revert silencioso + log

#### Pattern: Inline Error vs Console
- Los errores de USER INPUT van inline (KF_InlineBanner) — el usuario lo ve sin cambiar foco
- Los errores de CODE (NullRef, etc.) van al console — esos no deberían llegar a producción (zenith zero-errors R7)
- Warnings que el usuario debe atender: KF_InlineBanner amarillo
- Operación bloqueada: KF_InlineBanner rojo con razón y sugestión de fix

### Capítulo 5: Brand Voice en UI

#### Principios de texto en UI

| Principio | Mal ejemplo | Buen ejemplo |
|-----------|-------------|-------------|
| Orientado a resultado | "Apply Color" | "Apply Palette to 15 Materials" |
| Sin jerga técnica | "Undo.RecordObject failed" | "Could not register undo. Try again." |
| Conciso | "This operation will apply the colors defined in the current palette to all the materials that have been assigned to color roles" | "Apply palette to all assigned materials?" |
| Empoderador | "No materials found" (terminativo) | "No materials assigned yet. [+ Add Material]" |
| Cuantificado | "Done" | "Applied to 12 materials (1 skipped — missing)" |

#### Tooltips
- Formato: acción + resultado esperado + shortcut si aplica
- Ejemplo: `Apply Palette — Writes color to all assigned materials. Supports undo. [Ctrl+Enter]`
- Nunca explicar EL CAMPO, explicar EL EFECTO

#### Error messages
- Formato: Qué pasó + Por qué + Qué hacer
- Ejemplo: `"Material 'Mat_Player' was deleted from the project. Remove it from this role to continue."`

### Capítulo 6: Icon System

#### Filosofía
- Usar iconos built-in de Unity Editor al máximo. Son gratuitos, reconocibles, no añaden tamaño.
- `EditorGUIUtility.IconContent("iconName")` — inventario en: https://github.com/halak/unity-editor-icons
- Opciones para iconos custom KITforge: solo si el icono built-in no existe O si el icono es parte de la identidad de marca (logo [K])

#### Uso estándar en UI
| Acción | Icono Unity Built-in |
|--------|---------------------|
| Add/Create | `d_Toolbar Plus` |
| Delete/Remove | `d_Toolbar Minus` |
| Settings / Options | `d__Popup` o `d_Settings` |
| Warning | `d_console.warnicon` |
| Error | `d_console.erroricon` |
| Info | `d_console.infoicon` |
| Refresh/Scan | `d_Refresh` |
| Folder/Asset | `d_FolderOpened Icon` |
| Preview/Eye | `d_scenevis_visible_hover` |
| Apply/Check | `d_Toggle Icon` |
| Undo | `d_Undo` |
| Search | `d_Search Icon` |

### Capítulo 7: Paleta de Color — Design Tokens KITforge

Nota: ajustar según identidad de marca confirmada (ver #fec100 como accent corporativo)

```css
/* KF_DesignTokens.uss */
:root {
    /* Backgrounds */
    --kf-bg-window: #2C2C2C;        /* superficie principal de ventana */
    --kf-bg-surface: #383838;       /* tarjeta, panel, row */
    --kf-bg-surface-hover: #424242; /* hover state */
    --kf-bg-surface-alt: #303030;   /* fila alternada en listas */

    /* Borders */
    --kf-border: #222222;
    --kf-border-subtle: #2E2E2E;
    --kf-border-focus: #FEC100;     /* accent corporativo como focus ring */

    /* Text */
    --kf-text-primary: #E0E0E0;
    --kf-text-secondary: #9E9E9E;
    --kf-text-disabled: #616161;
    --kf-text-accent: #FEC100;

    /* Semantic */
    --kf-color-success: #4CAF50;
    --kf-color-warning: #FFC107;
    --kf-color-error: #F44336;
    --kf-color-info: #2196F3;

    /* Interactive */
    --kf-interactive-primary: #FEC100;       /* botón principal */
    --kf-interactive-primary-hover: #FFD740;
    --kf-interactive-danger: #D32F2F;
    --kf-interactive-danger-hover: #EF5350;

    /* Spacing */
    --kf-space-xs: 4px;
    --kf-space-s: 8px;
    --kf-space-m: 12px;
    --kf-space-l: 16px;
    --kf-space-xl: 24px;
    --kf-space-2xl: 32px;

    /* Typography */
    --kf-font-size-xs: 10px;
    --kf-font-size-s: 11px;
    --kf-font-size-m: 12px;  /* base */
    --kf-font-size-l: 14px;
    --kf-font-size-xl: 16px;

    /* Radius */
    --kf-radius-s: 2px;
    --kf-radius-m: 4px;
    --kf-radius-full: 9999px;
}
```

---

## DECISIONES CONFIRMADAS (2026-04-12)

| # | Decisión | Detalle |
|---|----------|---------|
| A | Biblia corre **en paralelo** con PaletteKit | PaletteKit = banco de pruebas del design system |
| B | Scope: **Design + UX + Documentación** | Tokens + Componentes mínimos + Guía UX |
| C | Logo KITforge existe. Elemento visual: **bombilla (bulb)** como icono identificador | Confirmar path del asset |
| D | **#fec100 es accent color** — muy llamativo, usar con disciplina | No dominar la UI, solo acentuar |
| E | Biblia = documentación primero. `KF_Shared` si los componentes se repiten en 2+ productos | Agilidad sobre infraestructura |

## DECISIONES PENDIENTES (2026-04-12)

| # | Pregunta | Impacto |
|---|----------|---------|
| 1 | Accent stripe: ¿solo arriba, solo abajo, o ambos? | Implementación de KF_Header/KF_StatusBar |
| 2 | ¿El logo/bulb es un PNG en el proyecto o usamos EditorGUIUtility.IconContent? | Necesario para diseñar KF_Header |
| 3 | ¿La biblia es un doc nuevo `KF_UIBible.md` o enriquece `KF_DevEnv.md` + `KF_QARules.md`? | Estructura de Skills/ |

---

## RIESGO: Scope creep vs. producto

### El peligro
Diseñar la biblia ANTES de construir PaletteKit puede derivar en semanas de trabajo de diseño sin validación real. Las mejores "bible" de diseño se construyen a partir de patrones extraídos de productos reales, no de abstracciones a priori.

### Propuesta de balance
1. **AHORA (no bloquea BUILD)**: Documentar design tokens (colores, spacing) + los 3-4 componentes que PaletteKit necesita sí o sí (Header, StatusBar, EmptyState, PlayModeBanner)
2. **DURANTE BUILD-1**: PaletteKit ES el primer banco de pruebas del design system
3. **POST PaletteKit v1**: Extraer los patrones validados en producto real y formalizarlos como biblia completa
4. **Para cada producto siguiente**: Aplicar la biblia + enriquecerla con los nuevos patrones

Esto evita construir un design system para un producto imaginario.
