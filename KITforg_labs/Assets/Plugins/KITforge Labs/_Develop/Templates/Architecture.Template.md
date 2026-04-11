# Architecture — KF_<ToolSlug>
Status: DRAFT
Phase: ARCHITECTURE
Parent Brief: Brief.md (must be APPROVED before this doc is created)
Date created: YYYY-MM-DD
Date approved: —

---

## Regla de este documento

Si la arquitectura no puede describirse con texto en 2 páginas, el producto está sobrediseñado.
KISS antes de SOLID. Una clase, una responsabilidad. Escalar solo si el uso lo justifica.

---

## 1. Assembly Definitions

| Assembly | Nombre | Plataformas | Referencia a | Ubicación |
|----------|--------|-------------|-------------|-----------|
| Editor | `KITforgeLabs.<Slug>.Editor` | Editor only | Runtime (si existe) | `KF_<Slug>/Editor/` |
| Runtime | `KITforgeLabs.<Slug>.Runtime` | All | — | `KF_<Slug>/Runtime/` |
| Tests | `KITforgeLabs.<Slug>.Tests` | Editor only | Editor + Runtime | `_Develop/Tests/KF_<Slug>_Tests/` |

*(Omitir Runtime si el producto es 100% editor-only)*

---

## 2. Namespaces

```
KITforgeLabs.Editor.<Slug>      ← clases de editor (EditorWindows, CustomEditors, Drawers)
KITforgeLabs.Runtime.<Slug>     ← componentes y utilidades runtime (si aplica)
KITforgeLabs.Editor.<Slug>.Tests
```

---

## 3. Inventario de clases

Listar TODAS las clases antes de escribir una sola. Si no sabes si necesitas una clase → no la añadas.

| Clase | Responsabilidad única | Base Unity | Namespace | Ubicación |
|-------|-----------------------|-----------|-----------|-----------|
| `KF_<Slug>Window` | Ventana principal del producto | `EditorWindow` | Editor | `Editor/` |
| `KF_<Slug>Scanner` | Lógica de escaneo/análisis (pura, sin UI) | — | Editor | `Editor/` |
| `KF_<Slug>SettingsSO` | Datos de configuración persistentes | `ScriptableObject` | Runtime | `Runtime/` |
| `KF_<Slug>Result` | Contenedor de datos de un resultado | — | Editor/Runtime | |
| ... | | | | |

**Clases que NO crearé** (explícito):
- *(Listar abstracciones tentadoras que YAGNI previene)*

---

## 4. API Surface pública

Qué métodos/propiedades son públicos y por qué necesitan ser públicos.
Si algo no necesita ser público, es privado. Punto.

```csharp
// KF_<Slug>Window
public static void Open()                         // MenuItem entry point
public void RunScan()                             // llamable desde tests

// KF_<Slug>Scanner  
public ScanResult Scan(string rootPath)           // retorna resultados sin efecto secundario
public bool IsSupported(string assetPath)         // validación previa

// KF_<Slug>SettingsSO
public string OutputFolder { get; set; }
public bool AutoRunOnBuild { get; set; }
```

*(Todo lo no listado aquí es privado o internal)*

---

## 5. Flujo de datos

Describe cómo fluye la información de entrada a salida. Sin código, solo conceptos.

```
Usuario trigger (MenuItem / Botón)
    ↓
KF_<Slug>Window recibe acción
    ↓
KF_<Slug>Scanner.Scan() — lógica pura, retorna List<ScanResult>
    ↓
KF_<Slug>Window renderiza resultados via UI Toolkit / IMGUI
    ↓
Usuario toma acción por resultado (fix / ignore / export)
    ↓
[Si acción destructiva] → KF_<Slug>Window muestra confirmación
    ↓
Operación ejecutada + ChangeLog entry si aplica
```

---

## 6. Persistencia de estado

| Dato | Scope | Mecanismo | Por qué |
|------|-------|-----------|---------|
| Preferencias del usuario (paths, toggles) | Por usuario | `EditorPrefs` | Sobrevive reinicios |
| Estado de sesión (scroll, selección) | Por sesión | `SessionState` | Se limpia al cerrar Unity |
| Configuración de proyecto | Por proyecto | `ScriptableObject` en `Assets/` | Versionable con el proyecto |
| Resultados de último scan | Por sesión | Memoria de la Window | No necesita persistir |

**PROHIBIDO:** campos estáticos para estado. Se pierden en domain reload.

---

## 7. Domain Reload safety

Listar qué debe sobrevivir un domain reload y cómo.

- La ventana puede cerrar y reabrir → el estado se recarga desde `EditorPrefs` / `SessionState`
- Los resultados de un scan se pierden en domain reload (aceptable / inaceptable → decidir)
- Si el producto subscribe a callbacks de editor → suscribirse en `[InitializeOnLoad]` o `OnEnable`

---

## 8. Dependencias

**Dependencias de Unity APIs:**
- `UnityEditor.AssetDatabase` — para X
- `UnityEditor.EditorApplication` — para Y
- (listar todas, incluso las obvias)

**Dependencias de packages de Unity** (si las hay):
- `com.unity.X` versión mínima X.X

**Dependencias de otros packages KITforge:**
- NINGUNA (por defecto) ← si hay alguna, justificarla aquí y en README

---

## 9. Estructura de carpetas del producto

```
KF_<Slug>/
  Editor/
    KF_<Slug>Window.cs
    KF_<Slug>Scanner.cs
    KF_<Slug>Window.uxml         ← si UI Toolkit
    KF_<Slug>Window.uss          ← si UI Toolkit
    KITforgeLabs.<Slug>.Editor.asmdef
  Runtime/                       ← omitir si no hay runtime
    KF_<Slug>SettingsSO.cs
    KITforgeLabs.<Slug>.Runtime.asmdef
  Demo/
    Demo_KF_<Slug>.unity
    DemoAssets/                  ← assets usados en la demo, incluibles en package
  Documentation/
    KF_<Slug>_Manual.pdf         ← opcional pero recomendado para paid tools
  README.md
  Third-Party Notices.txt        ← requerido si hay código de terceros
```

---

## 10. Riesgos técnicos confirmados

*(Refinamiento de los riesgos identificados en el Brief, ahora con context técnico real)*

| Riesgo | Cómo lo mitigamos en la arquitectura |
|--------|--------------------------------------|
| | |

---

## Approval checklist

- [ ] Todas las clases listadas con responsabilidad única clara
- [ ] API surface explícita — nada público sin razón
- [ ] Flujo de datos trazable de trigger a output
- [ ] Domain reload safety considerada
- [ ] Dependencias de Unity APIs listadas
- [ ] Estructura de carpetas definida
- [ ] Ninguna dependencia cross-product sin justificación
