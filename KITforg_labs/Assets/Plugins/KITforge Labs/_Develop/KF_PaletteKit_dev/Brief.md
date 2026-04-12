# Brief — KF_PaletteKit
Status: APPROVED
Phase: BRIEF
Score: —/35
Tier: Simple
Date created: 2026-04-11
Date approved: 2026-04-11

---

## 0. Identidad

| Campo | Valor |
|-------|-------|
| Slug | `KF_PaletteKit` |
| Nombre completo | PaletteKit — Color & Material Palette Manager |
| One-liner | Apply color themes across any project in one click. |
| Categoría Asset Store | Tools > Utilities |
| Precio estimado | $20 |
| Tier de complejidad | Simple (1 semana) |
| Wave | 1 |

---

## 1. Objetivo

**Problema concreto:**
Un TA o dev trabajando en un juego con 30+ materiales recibe este mensaje del art director: "el Primary ha cambiado a azul, actualiza todos los materiales." Sin herramienta, abre el Inspector, selecciona material 1, cambia `_BaseColor`, siguiente material... repite 30 veces. Si hay variaciones de paleta (summer/halloween/modo oscuro), el problema se multiplica.

El mismo escenario ocurre durante el prototipado: iterar sobre paletas de color es costoso, así que se itera menos. O se itera mal: algún material se queda con el color viejo.

No existe ninguna herramienta activa en la Asset Store (2026) que resuelva esto con el concepto de **roles semánticos** — asignar nombres de diseño (Primary, Accent, Danger) a propiedades de shader y propagar cambios a todos los materiales enlazados de golpe.

**Workaround actual:** Selección manual material a material en el Inspector. Algunos devs construyen scripts ad-hoc de un solo uso. Algunos adoptan MaterialPropertyBlock en runtime (no editor). Ninguna solución escala ni es reutilizable entre proyectos.

**Buyer profile:**
- Tipo de dev: TA en estudio pequeño / indie con varios materiales / cualquier dev que itera sobre paletas
- Momento de uso: Siempre que el art director cambia algo / durante iteración de prototipo / al preparar seasonal updates
- Nivel técnico: Mixto — funciona para devs Y para artistas que saben abrir una ventana de editor

**Una sola frase de valor:**
> *"PaletteKit permite a TAs y devs definir roles de color semánticos para que todo el proyecto actualice en un clic sin tocar cada material a mano."*

---

## 2. Solución propuesta

**Enfoque técnico:**
- `ScriptableObject` como contenedor de paleta: lista de `ColorRole` (nombre + color + propiedad objetivo)
- `EditorWindow` (UI Toolkit, UXML+USS) para gestión de paletas y asignación de materiales
- `AssetDatabase.FindAssets("t:Material")` + filtrado por scope para descubrir materiales
- `MaterialPropertyBlock` para live preview no destructivo en Scene/Game View
- `Material.SetColor(propertyName, color)` + `Undo.RecordObject` para Apply destructivo
- Parser JSON ligero para import desde Lospec / Coolors

**APIs de Unity principales involucradas:**
- `AssetDatabase.FindAssets`, `AssetDatabase.LoadAssetAtPath`
- `Material.SetColor`, `Material.GetColor`
- `Undo.RecordObject`, `Undo.RegisterCompleteObjectUndo`
- `MaterialPropertyBlock` (preview temporal, editor-only)
- `EditorWindow`, `CreateGUI` (UI Toolkit)
- `ScriptableObject`, `CreateAssetMenu`

**Explícitamente FUERA de scope:**
- Runtime: no hay código de runtime. Editor-only absoluto.
- Recoloring de texturas/sprites (manipulación de píxeles — eso es lo que hace Colorize, no esto)
- Auto-discovery de propiedades de shader (el mapeo rol→propiedad es manual por diseño — simple y seguro)
- Palette versioning/diffing (V2)
- Multi-user / cloud sync de paletas
- Integración con Shader Graph o Visual Effect Graph
- Modificación de shaders o creación de nuevos materiales

**Posibles extensiones futuras (V2):**
- Palette history con snapshots timestampeados
- Material browser con filtro visual por color (auditoría)
- Palette locking / aprobación de paleta de producción
- Export a CSS variables / Figma-compatible JSON
- Import directo desde URL de Lospec o Coolors

---

## 3. Referencias

**Competidores directos:**

| Nombre | Precio | Rating | Lo que hacen bien | Nuestra diferencia |
|--------|--------|--------|------------------|-------------------|
| Colorize (Smitesoft) | €55 | ⭐5.0 | Recolorea modelos low-poly con precisión de píxel via palette PNG. Familia completa de productos. UX muy pulida. | Nicho diferente: nosotros manejamos propiedades de shader (_BaseColor, _Color), no píxeles de textura. Funciona con ANY shader. No requiere formatos de textura específicos. |
| Palette Modifier (Nicrom, 2019) | N/A | N/A | Intentó resolver algo similar para low-poly. Tenía concepto de palette. | **404 — eliminado del store.** Abandonado o retirado. Dejó el mercado vacío. |
| Workaround manual | gratis | — | Siempre funciona. Sin dependencias. | No escala. 50 materiales = 50 operaciones. Sin undo masivo. Sin concepto de paleta reusable. Sin preview. |

**Repos open source de referencia:**
- [ProBuilder source (GitHub)](https://github.com/Unity-Technologies/com.unity.probuilder) — ProBuilder tiene un "Material Palette" para su workflow, útil como referencia de UX de panel de materiales
- [Lospec Palette API](https://lospec.com/palette-list/api) — API pública JSON para importar paletas de pixel art

**Documentación de Unity relevante:**
- [Material.SetColor](https://docs.unity3d.com/ScriptReference/Material.SetColor.html)
- [Undo.RecordObject](https://docs.unity3d.com/ScriptReference/Undo.RecordObject.html)
- [AssetDatabase.FindAssets](https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html)
- [MaterialPropertyBlock](https://docs.unity3d.com/ScriptReference/MaterialPropertyBlock.html)
- [EditorWindow](https://docs.unity3d.com/ScriptReference/EditorWindow.html)

**Inspiraciones de UX:**
- Adobe Swatch Libraries — convención de nombres de rol (Primary, Secondary, Accent)
- Figma / Sketch Global Styles — una fuente de verdad para colores; los componentes se actualizan solos
- Unity Lighting Settings — cómo una propiedad global se propaga a múltiples receptores

---

## 4. Mejoras vs competencia

Por qué alguien elegiría esto sobre lo que ya existe.

- **Vs Colorize:** Colorize manipula píxeles en texturas (bitmaps). PaletteKit toca propiedades de shader de materiales. Son herramientas ortogonales — un mismo proyecto podría usar ambas sin conflicto. Nuestro usuario objetivo ni siquiera compra Colorize porque no trabaja con low-poly sprite recoloring.
- **Vs workaround manual:** Un click para aplicar una palette a 50 materiales vs 50 ediciones en el Inspector. Con preview antes de commit y Undo de batch completo. El tiempo de setup (crear palette + asignar materiales) se amortiza en el primer uso.
- **Ángulo único:** Primera herramienta de la Asset Store con **roles semánticos** (Primary, Accent, Danger, Background) como abstracción. El concepto viene de los design systems de web/mobile (Figma, Material Design, Apple HIG) y no existe en ninguna herramienta de Unity. Esto no es un "mass color replace" — es un sistema de temas.

---

## 5. Posibles dificultades

| Riesgo | Probabilidad | Impacto | Mitigación |
|--------|-------------|---------|------------|
| Property names difieren por shader (_BaseColor en URP/Lit vs _Color en Standard) | Alta | Media | Property mapping: SO configurable por tipo de shader. Defaults inteligentes para los shaders comunes. |
| MaterialPropertyBlock no persiste tras domain reload | Media | Media | Documentado: preview es temporal. Window detecta domain reload y limpia preview state. |
| AssetDatabase.FindAssets lento en proyectos grandes (10k+ assets) | Baja | Alta | Scope filter obligatorio. Nunca escanear proyecto completo por defecto. |
| Undo de batch no revertir correctamente materiales compartidos entre prefabs | Baja | Alta | Test específico. `Undo.RegisterCompleteObjectUndo` sobre cada material individual antes del batch. |
| Materiales de Package Cache apareciendo en el scan | Media | Media | Filtrar paths que empiecen por "Packages/" en FindAssets. |

**Compatibilidad de Unity:**
- Versión mínima: Unity 6.0 (UI Toolkit en EditorWindow estable desde 2022.x, pero target es 6.0)
- ¿URP requerido? No — funciona con Standard, URP/Lit, URP/Unlit, HDRP, custom shaders
- ¿Algún package opcional? No

**Casos límite peligrosos:**
- Material eliminado del proyecto después de ser asignado a un rol — la referencia SO queda null. La window debe manejarlo con un warning visible, no con NullReferenceException.
- Prefabs con material overrides a nivel de instancia — `Material.SetColor` en el asset base no afecta overrides. El usuario debe saberlo. Añadir warning en Apply si hay instancias con overrides activos en la escena.
- Play Mode abierto: la window se desactiva o pone en modo read-only para evitar modificaciones de materiales durante runtime.

---

## 6. UI Design

**Tipo de interfaz:**
☑ EditorWindow standalone

**Sistema de UI:**
☑ UI Toolkit (UXML+USS) — Unity 6 target

**Wireframe:**
```
┌──────────────────────────────────────────────────────┐
│  KF PaletteKit                           [?]  [⋮]   │
├──────────────────────────────────────────────────────┤
│  Palette: [ Project Default ▼ ]  [+ New]  [Import]  │
│  ────────────────────────────────────────────────    │
│  ROLES                                               │
│  ┌──────────┐  ┌──────────────────────────────────┐  │
│  │ ■ Primary│  │ Property: _BaseColor       [+mat]│  │
│  │ #FF6B00  │  │  • Mat_Environment_Base           │  │
│  │  (click) │  │  • Mat_UI_Primary                 │  │
│  └──────────┘  │  • Mat_Player_Body                │  │
│                └──────────────────────────────────┘  │
│  ┌──────────┐  ┌──────────────────────────────────┐  │
│  │ ■ Accent │  │ Property: _BaseColor       [+mat]│  │
│  │ #2EC4B6  │  │  • Mat_UI_Button                  │  │
│  └──────────┘  └──────────────────────────────────┘  │
│  [+ Add Role]                                        │
│  ────────────────────────────────────────────────    │
│  Scope:  ◉ Project  ○ Active Scene  ○ Selection     │
│  ────────────────────────────────────────────────    │
│  [👁 Preview]              [✓ Apply]  [✗ Revert]    │
└──────────────────────────────────────────────────────┘
```

**Notas de UI:**
- Panel izquierdo del rol: swatch clickable abre Unity ColorPicker nativo
- Panel derecho del rol: lista de materiales asignados (drag-and-drop desde Project window)
- `[+mat]` abre selector de material (ObjectField o drag target)
- `Property: _BaseColor` es editable inline (TextField) para el mapeo shader
- Import: acepta JSON de Lospec o paste de hex codes separados por coma
- El botón "Revert" solo aparece si hay un preview activo
- Estado "Play Mode": toda la ventana muestra banner "Disabled in Play Mode"
- Mínimo viable de altura: ~350px. Diseñado para redimensionable.

---

## 7. UX Flow

**Primer uso** (tras importar el package, sin saber nada):
1. Menú `KITforge > PaletteKit` → ventana se abre
2. Click en `[+ New]` → se crea `KF_PaletteKit_Default.asset` en `Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings/`
3. Click en `[+ Add Role]` → rol "Role_01" creado, renombrar a "Primary"
4. Click en el swatch negro del rol → ColorPicker nativo → elegir #FF6B00
5. Click en `[+mat]` junto al rol → drag material desde Project window
6. Repetir con 2-3 materiales
7. Click `[👁 Preview]` → objetos en Scene View cambian al naranja
8. *→ "Vale, esto funciona" — todos los materiales cambian simultáneamente, sin Apply aún*
9. Click `[✓ Apply]` → cambios guardados. Un solo Ctrl+Z lo deshace todo.

**Uso diario** (usuario que la usa regularmente):
1. Abrir PaletteKit (o ya está abierta, docked)
2. Seleccionar palette del dropdown
3. Ajustar color del rol que cambió
4. Preview → Apply

**Uso avanzado** (power user):
1. Mantener 3 palettes: "Production", "Halloween", "Debug"
2. Click Import → seleccionar `.hex` file descargado de Lospec
3. Usar Scope = "Selection" para aplicar palette solo a un subset de materiales
4. Exportar swatch PNG para compartir en Notion/Figma del equipo

**Momento "aha":** Paso 7 del primer uso — el preview en tiempo real donde 3+ materiales cambian simultáneamente sin haber tocado el Inspector. Ocurre en < 2 minutos desde import.

---

## 8. Checkpoints

| # | Checkpoint | Criterio de done |
|---|-----------|-----------------|
| 1 | Ventana abre | `KITforge > PaletteKit` visible en menú; ventana se abre sin errores en consola |
| 2 | Palette SO crea | Click "New Palette" → .asset aparece en `Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings/` y en dropdown |
| 3 | Role add/rename | Click "+ Add Role" → rol aparece; doble click → nombre editable inline |
| 4 | Color change | Click swatch → Unity ColorPicker abre; cambio de color se refleja en swatch |
| 5 | Material assignment | Drag material desde Project → aparece en lista del rol (sin errores) |
| 6 | Preview funciona | Click Preview → MaterialPropertyBlock aplicado, escena actualiza; Revert revierte sin persistir |
| 7 | Apply funciona | Click Apply → `Material.SetColor` ejecutado; Ctrl+Z revierte TODOS los materiales de golpe |
| 8 | Scope Project | Scan con scope=Project encuentra todos los materiales asignados en la paleta |
| 9 | Scope Scene | Scope=Active Scene solo afecta materiales de objetos en la escena activa |
| 10 | Scope Selection | Scope=Selection solo afecta materiales de objetos seleccionados en Hierarchy |
| 11 | HEX Import | Lospec `.hex` file seleccionado → roles creados; nombre roles = `<filename> N`; Overwrite limpia bindings existentes; cap 64 colores |
| 12 | PNG Export | Click Export → swatch PNG generado en `Assets/Settings/KITforgeLabs/Exports/` |
| 13 | Demo scene funciona | `Demo_KF_PaletteKit.unity` carga, 5 objetos visibles, Apply cambia todos |
| 14 | Zero console errors | Todos los flujos anteriores sin errores ni warnings |

---

## 9. Validador / QA

**Cómo verificamos que funciona:**
- Tests unitarios para parsing JSON (puro C#, sin Unity context)
- EditMode tests: create SO → asignar material → apply → verificar `Material.GetColor` retorna nuevo valor
- Test de domain reload: reabrir Unity, state de la paleta persiste (SO en disco)
- Test manual con 3 tipos de shader: Standard, URP/Lit, URP/Unlit
- Test con proyecto limpio (compilación sin warnings)

**Casos que NUNCA deben ocurrir** (si ocurren → bloqueante):
- No debe modificar ningún material sin que el usuario haya clicado "Apply" explícitamente.
- No debe dejar materiales en estado de preview tras cerrar la ventana o recompilar — siempre revertir preview al detectar OnDestroy/domain reload.
- No debe causar NullReferenceException cuando un material asignado ha sido eliminado del proyecto.
- No debe ejecutar ninguna operación de escritura en Play Mode.
- No debe escanear ni modificar materiales dentro de `Library/` o `Packages/`.

**Datos de test necesarios:**
- Proyecto con 10+ materiales usando Standard, URP Lit y URP Unlit
- Al menos 1 prefab con material override activo en escena
- Proyecto completamente limpio (para test de compilación)

**Demo scene** — qué debe demostrar `Demo/Demo_KF_PaletteKit.unity`:
- 5+ objetos 3D con materiales asignados a roles Primary, Secondary, Accent
- Paleta "Demo_Default" preconfigurada con colores llamativos
- Workflow completo: abrir paleta, cambiar Primary, preview, apply, undo visible

---

## 10. Tests

**Lógica unit-testeable** (puro C#, sin Unity context):
- `PaletteParser.ParseLospecJson(string json) → List<Color>` — test con JSON válido, inválido, vacío
- `PaletteParser.ParseHexList(string hexCsv) → List<Color>` — test con hex válidos, inválidos, mezcla
- `PropertyMapper.FindBestPropertyMatch(string[] available, string[] candidates) → string` — test de fallback logic
- `PaletteValidator.ValidateRole(ColorRole role) → (bool valid, string error)` — test de rol vacío, sin nombre, sin materiales

**Tests de integración editor** (requieren AssetDatabase, EditModeTests):
- Crear paleta SO → verificar asset en path correcto
- Asignar material a rol → apply → `Material.GetColor("_BaseColor")` retorna color del rol
- Apply → Undo.PerformUndo() → `Material.GetColor` retorna color original
- Scope = Scene → apply → solo materiales activos en escena se ven afectados

**Tests manuales** (documentados en TestPlan.md):
- UX flow completo del primer uso (< 2 min)
- Survival test: domain reload no rompe SO ni assignments
- Play Mode: ventana muestra banner, no ejecuta Apply
- Proyecto grande: scan con 100+ materiales completa sin bloquear editor
- Null material: material eliminado del disco → warning en UI, sin excepción

**Localización del código de tests:**
`Assets/Plugins/KITforge Labs/_Develop/Tests/KF_PaletteKit_Tests/`

---

## 11. Ejemplos de uso

**Ejemplo A — Indie iterando paletas de prototipo:**
Un dev solo está probando 3 esquemas de color para su arcade game. Crea palettes "Neon", "Pastel" y "Dark" en PaletteKit. En su escena tiene 20 materiales. Cada palette tiene 4 roles mapeados. Para explorar las 3 opciones: selecciona palette, preview, evalúa → siguiente palette → preview. En 3 minutos ha visto las tres variantes. Sin PaletteKit: 20 materiales × 3 esquemas = 60 cambios en el Inspector, con riesgo de dejar alguno mal.

**Ejemplo B — TA de studio preparando seasonal update:**
El studio lanza eventos de Halloween cada año. El TA tiene una palette `Halloween_Orange` guardada en el repositorio. El día del update: abre PaletteKit, selecciona `Halloween_Orange`, Scope = Project, Apply. Los 80 materiales del juego actualizan al naranja. Un solo Ctrl+Z para revertir si hace falta. La palette está en el repo vía Git como un .asset más — el equipo entero puede replicar el cambio.

**Ejemplo C — Dev integrando guía de color de dirección de arte:**
El art director entrega un archivo JSON de 5 colores (Primary, Secondary, Accent, Background, Danger) exportado de Figma. El dev abre PaletteKit, click Import, pega el JSON, los 5 roles se crean automáticamente. Mapea cada rol a `_BaseColor` para los shaders del proyecto. Click Apply. Después exporta la misma paleta como PNG y la sube a Notion como referencia visual para el equipo no técnico.

---

## Approval Checklist

Completar con [x] cuando cada sección esté aprobada:

- [ ] §0 One-liner ≤ 12 palabras, orientado a valor
- [ ] §1 Problema descrito como situación concreta; buyer profile definido
- [ ] §2 Fuera de scope tiene ≥ 3 entradas
- [ ] §3 Al menos 1 competidor con "Lo que hace bien" honesto
- [ ] §4 Al menos 1 ángulo diferenciador no-precio
- [ ] §5 Al menos 1 riesgo de compatibilidad de Unity versionado
- [ ] §6 Sketch ASCII con elementos principales nombrados
- [ ] §7 Momento "aha" < 2 minutos, primer uso sin conocimiento previo
- [ ] §8 Checkpoints con criterios observables (no "funciona bien")
- [ ] §9 ≥ 2 casos en lista NUNCA-debe-ocurrir
- [ ] §10 ≥ 1 pieza de lógica unit-testeable puro sin Unity context
- [ ] §11 3 ejemplos distintos entre sí con resultado concreto

**Kill criteria:**
- [ ] Demostrable en < 30 segundos (preview en vivo, 3 materiales, 1 click)
- [ ] Scope cerrado: no escala infinitamente
- [ ] One-liner no necesita 2 frases para explicarse
