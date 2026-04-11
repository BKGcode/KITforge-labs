# KF_DevEnv — Entornos de Desarrollo y Test Internos

Referencia para crear y mantener entornos de desarrollo interno en KITforge Labs.
Para uso exclusivo del equipo de desarrollo. No se distribuye al usuario final.

---

## Principio

Cada producto incluye su propio entorno de test regenerable. El objetivo es:
- Empezar a probar en segundos, sin setup manual
- El entorno es determinista: siempre produce el mismo resultado
- Si la escena se corrompe o pierde: un clic la reconstruye

---

## Estructura por producto

```
KF_<Slug>/
  Editor/
    KF_<Slug>_DevSetup.cs     ← MenuItem que construye la escena

_Develop/
  KF_<Slug>_dev/
    Scenes/
      KF_<Slug>_DevScene.unity  ← Generada por DevSetup, commiteada
```

### Reglas de la carpeta `_Develop/<Slug>_dev/`
- Todos los artefactos de desarrollo interno de un producto viven aquí
- Brief.md, ChangeLog.md, Architecture.md, escenas de test → todo aquí
- Nunca dentro de la carpeta del producto (`KF_<Slug>/`) — esa carpeta es lo que recibe el comprador
- `Scenes/` nunca contiene assets de producción

### Reglas de `KF_<Slug>_DevSetup.cs`
- Vive en `Editor/` del producto (cubierto por el asmdef Editor existente)
- Un único `[MenuItem]` con path `KITforge/Dev/<Slug> — Rebuild Dev Scene`
- Priority `100` (siempre al final del submenú Dev)
- Siempre pide guardar la escena actual antes de proceder (`SaveCurrentModifiedScenesIfUserWantsTo`)
- Crea la scene con `NewSceneSetup.EmptyScene` y la guarda en `_Dev/Scenes/`
- Crea el directorio si no existe (no asumir que existe)
- Loguea confirmación con `Debug.Log("[KF_<Slug>] Dev scene rebuilt at: <path>")`

---

## Contenido mínimo de toda dev scene

Toda escena de desarrollo incluye SIEMPRE estos elementos base:

| GameObject | Componentes | Notas |
|---|---|---|
| `Main Camera` | `Camera` | tag=MainCamera, clearFlags=SolidColor, bg #1F1F1F |
| `Directional Light` | `Light` | type=Directional, intensity=1, rot=(50,-30,0) |
| `Canvas` | `Canvas` + `CanvasScaler` + `GraphicRaycaster` | Ver config Canvas Mobile abajo |
| `EventSystem` | `EventSystem` + `StandaloneInputModule` | Siempre junto al Canvas |

### Config Canvas Mobile (estándar del proyecto)
- Render Mode: `Screen Space - Overlay`
- UI Scale Mode: `Scale With Screen Size`
- Reference Resolution: `1080 × 1920`
- Screen Match Mode: `Match Width Or Height`
- Match: `1` (Height)

---

## Fixtures específicos por tipo de producto

Después de los elementos base, cada DevSetup crea los fixtures que su producto necesita.

### Editor tools (HierarchyKit, ProjectAudit…)
GameObjects con estructura y nombres representativos del caso de uso del tool.
No necesitan componentes — solo la jerarquía importa.

### URP Shaders (futurible)
- Primitivos: Sphere, Capsule, Quad, Plane (un objeto por mesh)
- Materiales: slot vacío asignado al shader en test
- Luces: Directional (key) + Point (fill) + sin luz (test de unlit)
- Fondo: sólido neutro #1F1F1F

### UI Tools/UGUI (futurible)
- Canvas con la config mobile estándar (ya incluida en base)
- Panel de test con Image + TMP Label corto + TMP Label largo (>80 chars)
- ScrollRect vacío para tests de scroll

---

## Convención de nombres en fixtures

- Headers de Hierarchy: `--- Nombre ---`, `=== Nombre ===`, `[Nombre]`
- Objetos normales de test: `NormalObject_A`, `NormalObject_B`
- Containers de grupo: `Group_<Tipo>` (ej. `Group_Primitives`)
- Objetos de referencia neutral: `Ref_<Uso>` (ej. `Ref_Backdrop`)

---

## Nice-to-haves (futuros, no implementar hasta necesitar)

- **DevHUD overlay**: Scene View overlay con FPS, DrawCalls, stats por contexto
- **Test runner hooks**: DevSetup como fixture de tests EditMode
- **Capture tool**: screenshot antes/después para comparativa visual de shaders
- **Multi-resolution preview**: switch rápido entre 1080×1920, 1284×2778 (iPhone), 1440×3088
- **Shader test materials**: MaterialPropertyBlock presets para testear variantes

---

## Qué NO va en la dev scene

- Assets de producción (sprites, prefabs, SOs de configuración del juego)
- Código de gameplay
- Configuración de build o Player Settings
- Cualquier cosa que el usuario final pueda ver o tocar
