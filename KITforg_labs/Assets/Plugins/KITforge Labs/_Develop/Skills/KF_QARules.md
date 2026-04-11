# KF_QARules — QA Checklist para Asset Store Tools
Scope: KITforge labs only
Last updated: 2026-04-11

---

## Regla de oro

Un producto KITforge labs que pasa QA cumple ONE condition without exception:

> **El comprador puede importar el package, abrir la tool, usarla para su caso principal, y salir sin ningún error en su consola y sin ninguna modificación involuntaria en su proyecto.**

Todo lo demás es optimización sobre esta baseline.

---

## Checklist de compilación

| Check | Cómo verificar | Bloqueante |
|-------|---------------|-----------|
| Sin errores de compilación | Importar en proyecto limpio Unity 6 | ✅ SÍ |
| Sin warnings de compilación | Revisión en Console (filtro Warnings) | ⚠️ Warn |
| Sin obsolete API usage | `#pragma warning disable` no oculta errores reales | ✅ SÍ |
| Compatible con IL2CPP | Verificar reflection usage, no usar `Type.GetType(string)` sin fallback | ✅ SÍ |

---

## Checklist de Unity Editor

| Check | Cómo verificar | Bloqueante |
|-------|---------------|-----------|
| Sin errores al abrir Unity con la tool instalada | Iniciar Unity, no hacer nada, revisar consola | ✅ SÍ |
| Manuel abre sin errores | MenuItem o window open | ✅ SÍ |
| Domain reload survival | Recompilar mientras la ventana está abierta | ✅ SÍ |
| Cierre y reapertura de Unity | Cerrar Unity, reabrir, verificar estado | ✅ SÍ |
| No código en Update/tick innecesario | Revisar que no hay hooks globales activos cuando la tool está cerrada | ⚠️ Warn |
| No static fields con estado | Buscar `static` mutable en clases de editor | ✅ SÍ |

---

## Checklist de operaciones sobre el proyecto

Aplica solo a tools que modifican assets, prefabs, o escenas.

| Check | Bloqueante |
|-------|-----------|
| Toda operación destructiva tiene confirmación (`EditorUtility.DisplayDialog`) antes de ejecutar | ✅ SÍ |
| El usuario puede cancelar cualquier operación antes de que empiece | ✅ SÍ |
| Si la operación falla a mitad → el proyecto queda en estado válido (no parcialmente modificado) | ✅ SÍ |
| Las operaciones tienen `AssetDatabase.StartAssetEditing()` / `StopAssetEditing()` para batch ops | ✅ SÍ |
| Undo funciona para operaciones que modifican assets | ✅ SÍ |

---

## Checklist de UX

| Check | Bloqueante |
|-------|-----------|
| El estado inicial de la tool es claro (instrucciones o estado visible) | ✅ SÍ |
| Los botones deshabilitados se ven claramente deshabilitados | ⚠️ Warn |
| Los errores se muestran en la UI, no solo en consola | ⚠️ Warn |
| Los procesos largos tienen progress bar o feedback visual | ⚠️ Warn |
| La tool funciona en pantallas pequeñas (1280x720 Editor) | ⚠️ Warn |
| No hay texto cortado en labels a menos que sea por scrollview intencional | ⚠️ Warn |

---

## Checklist de compatibilidad

| Check | Cómo verificar |
|-------|---------------|
| Unity versión mínima declarada en README | Ver que coincide con Unity version usada para build |
| URP dependency declarada (si aplica) | package dependencies en .asmdef |
| Il2CPP: no Reflection.Emit, no DynamicMethod | Code review |
| No `#if UNITY_EDITOR` faltante en código que accede a APIs de Editor | Code review + compilación en modo Build |

---

## Checklist del package final

| Check | Bloqueante |
|-------|-----------|
| Package exportado solo contiene `KF_<Slug>/` (sin `_Develop/`, sin `_Lab/`) | ✅ SÍ |
| `README.md` presente y con contenido real | ✅ SÍ |
| `Third-Party Notices.txt` presente si se usa código de terceros | ✅ SÍ |
| Demo scene carga sin errores | ✅ SÍ |
| Ningún asset roto (missing script, missing material) | ✅ SÍ |
| No hay GUIDs duplicados con packages populares (verificar Assets Store common conflicts) | ✅ SÍ |

---

## Checklist de Asset Store compliance

Basado en las submission guidelines oficiales:

| Requisito | Check |
|-----------|-------|
| Todo el contenido está bajo una única carpeta raíz (`KF_<Slug>/`) | ✅ |
| Se declara namespace (no contaminar espacio global) | ✅ |
| Si hay shaders: guía de uso incluida | ✅ |
| Sin DRM restrictivo | ✅ |
| Sin llamadas de red no declaradas en descripción | ✅ |
| Contenido apto para audiencia general | ✅ |
| Descripción del producto en inglés | ✅ |

---

## Definición de severidades

| Símbolo | Significado |
|---------|-------------|
| ✅ SÍ | Bloqueante. No pasa QA sin esto. |
| ⚠️ Warn | No bloqueante para v1.0, pero se registra en issues Wave 1.1 |
| ℹ️ Info | Mejora de calidad para versiones futuras |

---

## QA en versiones de parche (v1.X.X)

Para patches: ejecutar solo los checks afectados por el cambio + el full checklist de compilación y el checklist del package.

Para minor versions (v1.1): ejecutar el plan completo.
