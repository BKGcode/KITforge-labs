# Tests — KITforge labs Test Environment
Last updated: 2026-04-11

---

## Filosofía de tests para editor tools

Las editor tools no se prueban como apps de servidor. No hay CI con 300 unit tests aquí.
La estrategia es por capas — cada capa cubre lo que la anterior no puede.

| Capa | Cobertura | Herramienta | Cuándo ejecutar |
|------|-----------|-------------|----------------|
| 1 — Logic unit | Lógica pura C# sin Unity context | Unity Test Runner (Edit Mode) | En cada commit relevante |
| 2 — Editor integration | Window lifecycle, AssetDatabase ops | Unity Test Runner (Edit Mode) | En cada checkpoint |
| 3 — Manual QA | UX flow, edge cases, domain reload | TestPlan.md checklist | Antes de QA phase |
| 4 — Demo scene | End-to-end product experience | Manual + visual | Antes de STORE_PREP |

---

## Setup del Test Runner

1. Abrir `Window > General > Test Runner`
2. Seleccionar `EditMode`
3. Los tests de cada producto aparecen bajo su assembly

Para que Unity detecte los test assemblies, los ficheros `.asmdef` de tests deben:
- Tener `testPlatforms: ["Editor"]`
- Referenciar el assembly de editor del producto
- Estar en la carpeta `_Develop/Tests/KF_<Slug>_Tests/`

---

## Estructura de carpetas de tests

```
_Develop/Tests/
  README.md                        ← este archivo
  KF_HierarchyKit_Tests/
    KITforgeLabs.HierarchyKit.Tests.asmdef
    KF_HierarchyKitScannerTests.cs
    KF_HierarchyKitValidatorTests.cs
    Scenes/
      Test_KF_HierarchyKit.unity   ← escena de test con datos mock controlados
    MockData/
      SamplePrefabWithMissingRef.prefab  ← assets de test (solo para uso interno)
  KF_ProjectAudit_Tests/
    KITforgeLabs.ProjectAudit.Tests.asmdef
    ...
```

---

## Escenas de test

Cada producto que lo necesite tiene una escena de test en `Scenes/`:
- `Test_KF_<Slug>.unity`
- Contiene datos mock que representan el "estado problemático" que la tool detecta
- Ejemplos: prefabs con missing references, assets sobredimensionados, carpetas vacías, etc.
- **No se incluyen en el package final.** Solo en `_Develop/`.

---

## Datos mock (MockData/)

Datos de proyecto controlados para probar sin depender del estado del proyecto real:
- Prefabs con referencias rotas
- Texturas con configuraciones incorrectas
- Escenas con objetos desconectados
- Scripts vacíos o con errores específicos

Estos assets son internos al laboratorio y **no se venden ni distribuyen**.

---

## Template de test class

```csharp
using NUnit.Framework;
using KITforgeLabs.Editor.<Slug>;

namespace KITforgeLabs.Editor.<Slug>.Tests
{
    public class KF_<Slug>ScannerTests
    {
        [Test]
        public void Scan_EmptyPath_ReturnsEmptyResult()
        {
            var scanner = new KF_<Slug>Scanner();
            var result = scanner.Scan(string.Empty);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Scan_ValidPath_ReturnsResults()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
```

Reglas:
- Formato Arrange / Act / Assert
- Nombres de test: `[Método]_[Escenario]_[ResultadoEsperado]`
- Sin lógica de setup compleja — si el test necesita mucho setup, la arquitectura tiene un problema
- Test solo la lógica pura. La UI se prueba manualmente.

---

## Qué NO unit-testear

- El layout visual de la EditorWindow
- El comportamiento de domain reload (tests manuales)
- El comportamiento de deshacer (Undo) — Unity Test Runner no tiene soporte nativo limpio
- AssetDatabase operations destructivas (mock o test manual)
- Callbacks de editor (hierarchyWindowItemOnGUI, etc.) — probar visualmente

---

## Datos que vale la pena mantener en MockData

Para `KF_ProjectAudit`:
- Una carpeta con prefab con missing script
- Una textura configurada como 2048x2048 RGBA32 sin compresión
- Una escena no añadida a Build Settings con nombre que sugiere que debería estar

Para `KF_SafeClean`:
- Un asset referenciado por múltiples prefabs (para probar detección)
- Un asset completamente sin referencias (para probar safe delete)

Para `KF_HierarchyKit`:
- Una escena con jerarquía profunda (>5 niveles de nesting)
- Objetos con tags y layers variados
