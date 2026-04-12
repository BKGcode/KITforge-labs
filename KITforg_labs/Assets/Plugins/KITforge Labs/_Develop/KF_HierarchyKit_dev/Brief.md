# Brief — KF_HierarchyKit
Status: DRAFT
Phase: BRIEF
Score: 29/35
Tier: Moderate
Date created: 2026-04-11
Date approved: —

---

## 0. Identidad

| Campo | Valor |
|-------|-------|
| Slug | `KF_HierarchyKit` |
| Nombre completo | Hierarchy Enhancement Kit |
| One-liner | Visual structure for Unity's Hierarchy in any project. |
| Categoría Asset Store | Tools > Editor Extensions |
| Precio estimado | $TBD |
| Tier de complejidad | Moderate (2-3 semanas) |
| Wave | 1 |

---

## 1. Objetivo

**Problema concreto:**
Un dev trabajando en una escena con 50+ objetos abre el panel Hierarchy y ve una lista plana sin estructura. Buscar la cámara, el canvas de UI y los spawners requiere hacer scroll repetidamente. Para dar algo de estructura, muchos usan GameObjects vacíos con nombre `--- UI ---` como separadores visuales, lo que contamina la jerarquía real y complica la selección múltiple. No hay forma nativa de colorear por tipo, colapsar grupos visuales o bloquear objetos frágiles de un vistazo.

**Buyer profile:**
- Tipo de dev: cualquier desarrollador Unity, desde solo indie hasta TA de estudio
- Momento de uso: a diario, siempre que se trabaje en escenas con densidad media-alta
- Nivel técnico: todos los niveles — artistas técnicos y juniors también

**Una sola frase de valor:**
> *"KF_HierarchyKit permite estructurar visualmente el panel Hierarchy para que cualquier dev navegue escenas complejas sin perderse en listas planas."*

---

## 2. Solución propuesta

**Enfoque técnico:**
- Interceptar `EditorApplication.hierarchyWindowItemOnGUI` para dibujar overlays visuales por fila
- Separadores/cabeceras configurables (creación inline, sin GameObjects basura)
- Color de fila por tipo de componente, por tag, o asignación manual
- Iconos de toggle rápido de visibilidad y lock por fila (sin tocar la jerarquía real)
- Grupos colapsables visuales (no tocan parenting — TBD: ¿MVP o V2?)
- Persistencia de config en `EditorPrefs` global o JSON por escena (TBD: ¿qué scope?)

**APIs de Unity principales involucradas:**
- `EditorApplication.hierarchyWindowItemOnGUI` — callback principal del overlay
- `EditorGUIUtility.isProSkin` — para respetar tema dark/light del editor
- `EditorPrefs` o `SessionState` — para persistir datos entre recargas de dominio
- `SceneAsset` / `SceneManager` — si la config es por escena

**Explícitamente FUERA de scope:**
- Funcionalidad en runtime (cero impacto en builds)
- Modificar la jerarquía de GameObjects (no parenting, no reparenting)
- Integración con Prefab Mode
- Multi-scene editing
- Sincronización cloud/team de configuraciones
- URP, shaders o efectos visuales
- Cualquier funcionalidad de búsqueda/filtro de objetos (eso es KF_SceneExplorer)

**Posibles extensiones futuras** (no para v1):
- Auto-color basado en reglas (tag = color X, tipo Y = color Z)
- Export de presets visuales
- Colores en Project window (paralelo)
- Tema de colores compartido por equipo

---

## 3. Referencias

**Competidores directos:**

| Nombre | Precio | Rating | Lo que hacen bien | Nuestra diferencia |
|--------|--------|--------|------------------|-------------------|
| vHierarchy 2 | $20 | ⭐⭐⭐⭐⭐ | Setup cero, UX muy pulida, preset system maduro, soporte activo | Zero-setup también. Precio similar o menor. Scope más reducido y explícito. |
| Hierarchy 2 (Verpha) | $15 | ⭐⭐⭐⭐ | Colores + iconos + componentes visible | Menor complejidad de configuración |
| QuickHierarchy | Free | ⭐⭐⭐ | Gratis, simple | Más features, más pulido visualmente |

**Repos open source de referencia:**
- `Thundernerd/Unity-HierarchyHeader` — separadores simples, buena referencia de impl mínima
- Buscar en GitHub: unity "hierarchyWindowItemOnGUI" para ver patrones

**Documentación de Unity relevante:**
- `EditorApplication.hierarchyWindowItemOnGUI` (docs.unity3d.com)
- `EditorGUILayout` / `EditorGUI` — para dibujar en el overlay
- Unity 6 Hierarchy API changes (verificar si hay alternativa UI Toolkit)

**Inspiraciones de UX:**
- vHierarchy (el estándar a superar)
- VS Code file explorer (colores por tipo de archivo)

---

## 4. Mejoras vs competencia

- **Vs vHierarchy:** Scope más pequeño y explicado en una frase. Sin feature bloat. Más fácil de entender y justificar la compra.
- **Vs workaround manual (empty GameObjects):** Cero contaminación de la jerarquía real. Los separadores no son GameObjects — no aparecen en builds, no afectan componentes.
- **Ángulo único:** Deliberate restraint. No intenta hacer todo. El README explica por qué ciertas cosas están fuera de scope — esto es un argumento de venta en sí mismo para equipos que sufren de feature bloat en sus herramientas.

---

## 5. Posibles dificultades

| Riesgo | Probabilidad | Impacto | Mitigación |
|--------|-------------|---------|------------|
| `hierarchyWindowItemOnGUI` deprecado o con cambios en Unity 6.x | Media | Alto | Verificar API status ANTES de empezar. Preparar fallback. |
| Rendimiento con escenas >500 objetos (callback fires per row per frame) | Alta | Alto | Usar GUIClip, cachear llamadas, no alocar en el callback |
| Domain reload borra estado en memoria | Seguro | Medio | Persistencia en EditorPrefs o JSON desde el día 1 |
| Conflicto con otros tools que usen el mismo callback (vHierarchy, etc.) | Media | Medio | Documentar incompatibilidades. Callback permite múltiples registros. |
| Grupos colapsables sin tocar jerarquía = UX confusa | Alta si en MVP | Medio | Mover a V2 si complica MVP |

**Compatibilidad de Unity:**
- Versión mínima: Unity 6.0 (LTS)
- ¿URP requerido? No
- ¿Algún package opcional que afecte funcionalidad? No

**Casos límite peligrosos:**
- Escena con prefab mode abierto — overlays pueden aparecer duplicados o en posición incorrecta
- Proyecto con otro hierarchy tool activo — comportamiento visual undefined

---

## 6. UI Design

**Tipo de interfaz:**
☑ CustomEditor / Inspector extension (Settings panel en Project Settings o EditorWindow)
☑ MenuItem / ContextMenu — para crear separadores inline
Overlay principal: directo en Hierarchy window via callback (no hay ventana propia)

**Sistema de UI:**
☐ UI Toolkit — la Hierarchy window callback es IMGUI. La settings UI puede ser UGUI/UITK.
☑ IMGUI — para el hierarchy overlay (técnicamente necesario aquí)

**Wireframe / sketch:**
```
Hierarchy window (augmented):
┌─────────────────────────────────────────┐
│  ▼ SampleScene                          │
│  ────── [ GAMEPLAY ] ──────── [color]   │ ← separador KF (no es GObject)
│    Main Camera               👁 🔒      │ ← iconos de toggle rápido
│    ■ GameManager             👁 🔒      │ ← color por tipo de componente
│  ────── [ UI ] ───────────── [color]    │
│  ▶ [UI Group colapsado]      👁 🔒      │ ← grupo colapsado (TBD V2?)
│    Canvas                    👁 🔒      │
│  ────── [ FX ] ─────────────            │
└─────────────────────────────────────────┘

Settings (EditorWindow o Project Settings):
┌─────────────────────────────────────────┐
│  KF HierarchyKit — Settings             │
├─────────────────────────────────────────┤
│  Separators   Colors   Toggles  Theme   │ ← tabs
│                                         │
│  [Lista de reglas de color activas]     │
│  [+ Add Rule]  [Remove]                 │
│                                         │
│  Theme: ○ Auto  ○ Dark  ○ Light         │
└─────────────────────────────────────────┘
```

**Notas de UI:**
- El overlay en Hierarchy es IMGUI puro — no hay alternativa en Unity 6 estable
- Settings window puede ser UI Toolkit (UXML+USS) — preferible
- Respetar siempre `EditorGUIUtility.isProSkin`

---

## 7. UX Flow

**Primer uso** (tras importar el package, sin saber nada):
1. El dev importa el package. No pasa nada automático — cero cambios en su escena.
2. Abre cualquier escena. Aparecen dos separadores de ejemplo en la parte superior de la Hierarchy (como "getting started" visual).
3. El dev hace clic en un separador de ejemplo y ve un tooltip: "Click para editar nombre y color."
4. *→ Primera percepción de valor aquí: el dev entiende en 10 segundos qué hace la tool sin leer nada.*

**Uso diario:**
1. Dev abre la escena. Los separadores y colores ya estaban guardados — se restauran inmediatamente.
2. Arrastra un separador con click derecho → "Insert Separator Here" desde el menú contextual.

**Uso avanzado:**
1. Dev abre KF_HierarchyKit Settings y crea una regla: "todos los objetos con tag `Enemy` → color rojo."
2. Exporta el preset de colores del equipo a un JSON para compartirlo con otros devs.

**Momento "aha":** Cuando el dev ve su Hierarchy con los dos separadores de ejemplo ya aplicados, sin haber configurado nada. Ocurre en < 30 segundos desde import.

---

## 8. Checkpoints

| # | Checkpoint | Criterio de done |
|---|-----------|-----------------|
| 1 | Package importa sin errores | Cero errores/warnings en consola tras import en proyecto limpio Unity 6 |
| 2 | Overlay básico funciona | Los 2 separadores de ejemplo aparecen en Hierarchy en cualquier escena abierta |
| 3 | Crear separador | Click derecho en Hierarchy → "KF / Add Separator" → separador aparece como fila visual |
| 4 | Color de fila manual | Click en fila de objeto → selector de color → fila cambia de color |
| 5 | Toggles visibilidad/lock | Iconos 👁 y 🔒 visibles por fila → click togglea sin romper selección de Unity |
| 6 | Persistencia | Separadores y colores sobreviven domain reload y reapertura del editor |
| 7 | Settings window | MenuItem "KITforge/HierarchyKit/Settings" abre ventana sin errores |
| 8 | Demo scene funciona | Demo_KF_HierarchyKit.unity carga y muestra la tool en acción |
| 9 | Zero console errors | Sin errores ni warnings en consola en estado idle en proyecto limpio y en proyecto con escenas densas |

---

## 9. Validador / QA

**Cómo verificamos que funciona:**
- Importar en proyecto limpio (Unity 6, URP, sin nada más) → cero errores
- Abrir escena con 200+ objetos → verificar que no hay frame drops perceptibles en el Editor

**Casos que NUNCA deben ocurrir:**
- Nunca modificar GameObjects en la escena (ni nombre, ni transform, ni nada)
- Nunca aparecer en la build del juego — cero rastro de editor code en runtime
- Nunca causar NullReferenceException al cambiar de escena
- Nunca bloquear el Domain Reload o alargar compilación
- Nunca conflicto de asmdef que impida compilar proyectos que usen Odin / vHierarchy / otros

**Datos de test necesarios:**
- Escena vacía (smoke test)
- Escena con 50-100 objetos reales (typical case)
- Escena con 300+ objetos (stress test de rendimiento del callback)
- Proyecto con otro hierarchy tool activo (compatibility test)

**Demo scene** — `Demo/Demo_KF_HierarchyKit.unity`:
- Escena con ~30 objetos organizados en 4 secciones con separadores KF
- Cada sección con color diferente
- Objetos con colores manuales asignados
- Panel de instrucciones en Game View explicando cómo reproducir (TextMeshPro)

---

## 10. Tests

**Lógica unit-testeable:**
- Parser de reglas de color (tag → color, tipo de componente → color)
- Serialización/deserialización de config a JSON
- Validación de nombre de separador (vacío, demasiado largo, caracteres especiales)

**Tests de integración editor:**
- Separador se crea, persiste tras domain reload, se elimina correctamente
- Config se guarda y se carga correctamente desde EditorPrefs/JSON

**Tests manuales:**
- UX flow completo del primer uso (los 2 separadores de ejemplo aparecen)
- Survival test: domain reload no borra separadores ni colores
- Survival test: compilación en proyecto limpio sin Odin ni otros tools
- Survival test: compilación en proyecto con vHierarchy activo

**Localización:**
`Assets/Plugins/KITforge Labs/_Develop/Tests/KF_HierarchyKit_Tests/`

---

## 11. Ejemplos de uso

**Escenario A — Dev solo en un RPG:**
> *Situación:* Lleva 3 meses en el proyecto. La Hierarchy tiene 80 objetos. FindEnemy() ya no funciona rápido.
> *Acción:* Añade 5 separadores de color: GAMEPLAY, UI, FX, AUDIO, DEBUG. Colorea en rojo los objetos con tag Enemy.
> *Resultado:* Encuentra cualquier objeto en 2 segundos en vez de 15. Nunca más hace scroll perdido.

**Escenario B — TA en un estudio mediano:**
> *Situación:* Tiene que entregar la escena a un artista para que ajuste los FX. El artista no sabe la estructura.
> *Acción:* Los separadores ya están puestos. El artista abre la escena y ve "── FX ──" en verde de inmediato.
> *Resultado:* El artista no interrumpe al TA con preguntas. El handoff es visual.

**Escenario C — Dev que hereda un proyecto ajeno:**
> *Situación:* Recibe un proyecto con 120 objetos en la jerarquía, sin estructura, sin comentarios.
> *Acción:* Sin tocar nada de la escena, crea separadores y asigna colores para mapear lo que encuentra.
> *Resultado:* En 15 minutos tiene el proyecto visualmente navegable sin haber modificado nada real.

---

## Approval checklist

- [ ] Todas las secciones rellenas (ninguna vacía o con placeholder)
- [ ] One-liner tiene ≤ 12 palabras y explica el valor sin ver el resto
- [ ] Scope fuera está explícitamente listado
- [ ] Al menos 1 competidor directo comparado
- [ ] Al menos 2 riesgos técnicos identificados con mitigación
- [ ] Wireframe sketch presente (aunque sea ASCII)
- [ ] UX flow tiene el "momento aha" identificado
- [ ] Todos los checkpoints tienen criterio de done observable

### Pendiente de decisión antes de APPROVE:
- [ ] Precio confirmado ($TBD)
- [ ] Modelo comercial: ¿paid simple o freemium?
- [ ] Grupos colapsables: ¿MVP o V2?
- [ ] Scope de persistencia: ¿config global (EditorPrefs) o por escena?
