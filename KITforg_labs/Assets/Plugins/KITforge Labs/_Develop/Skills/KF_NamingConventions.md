# KF_NamingConventions — KITforge labs Naming Rules
Scope: KITforge labs only
Complements: unity-rules (global), project-organization-skill (global)
Last updated: 2026-04-11

---

## 1. Namespaces

Formato: `KITforgeLabs.<Area>.<Slug>`

| Contexto | Namespace |
|----------|-----------|
| Editor tools, windows, drawers | `KITforgeLabs.Editor.<Slug>` |
| Runtime components, utilities | `KITforgeLabs.Runtime.<Slug>` |
| Código compartido cross-tool | `KITforgeLabs.Shared` |
| Tests | `KITforgeLabs.Editor.<Slug>.Tests` |

Ejemplos:
```csharp
namespace KITforgeLabs.Editor.HierarchyKit { }
namespace KITforgeLabs.Runtime.ScreenCapture { }
namespace KITforgeLabs.Editor.HierarchyKit.Tests { }
```

---

## 2. Assembly Definitions

Formato: `KITforgeLabs.<Slug>.<Area>`

| Assembly | Nombre | Ejemplo |
|----------|--------|---------|
| Editor | `KITforgeLabs.<Slug>.Editor` | `KITforgeLabs.HierarchyKit.Editor` |
| Runtime | `KITforgeLabs.<Slug>.Runtime` | `KITforgeLabs.HierarchyKit.Runtime` |
| Shared | `KITforgeLabs.Shared` | (único, cross-tool) |
| Tests | `KITforgeLabs.<Slug>.Tests` | `KITforgeLabs.HierarchyKit.Tests` |

Nombre del archivo `.asmdef`: igual que el nombre del assembly.

---

## 3. Clases

Formato: `KF_<Slug><Rol>`

| Rol | Sufijo | Ejemplo |
|-----|--------|---------|
| Ventana EditorWindow | `Window` | `KF_HierarchyKitWindow` |
| Lógica central / procesador | `Scanner` / `Processor` / `Engine` | `KF_ProjectAuditScanner` |
| Configuración de proyecto (SO) | `SettingsSO` | `KF_HierarchyKitSettingsSO` |
| Datos de resultado | `Result` / `Report` | `KF_ProjectAuditResult` |
| Componente runtime | `Component` (o rol explícito) | `KF_DebugOverlayComponent` |
| PropertyDrawer | `Drawer` | `KF_ReadOnlyDrawer` |
| CustomEditor | `Editor` | `KF_SettingsSOEditor` |
| Renderer Feature (URP) | `Feature` | `KF_ScanlineFeature` |
| Volume Component (URP) | `Volume` | `KF_ScanlineVolume` |
| MenuItem/ContextMenu class | `MenuItems` | `KF_HierarchyKitMenuItems` |
| Utilidad estática | `Utils` / `Helper` | `KF_PathUtils` |

**Regla:** Nunca omitas el prefijo `KF_`. Guarantees no naming collision con proyectos del comprador.

---

## 4. Archivos

| Tipo | Formato | Ejemplo |
|------|---------|---------|
| Script C# (clase) | `KF_<Slug><Rol>.cs` | `KF_HierarchyKitWindow.cs` |
| UXML (UI Toolkit) | `KF_<Slug><Rol>.uxml` | `KF_HierarchyKitWindow.uxml` |
| USS (UI Toolkit) | `KF_<Slug><Rol>.uss` | `KF_HierarchyKitWindow.uss` |
| Shader HLSL/ShaderLab | `KF_<Slug><Efecto>.shader` | `KF_MicroFX_Scanline.shader` |
| ScriptableObject asset | `KF_<Slug>Settings.asset` | `KF_HierarchyKitSettings.asset` |
| Scene demo | `Demo_KF_<Slug>.unity` | `Demo_KF_HierarchyKit.unity` |
| Scene test | `Test_KF_<Slug>.unity` | `Test_KF_HierarchyKit.unity` |
| Screenshot store | `KF_<Slug>_Screenshot_XX.png` | `KF_HierarchyKit_Screenshot_01.png` |
| Hero image store | `KF_<Slug>_Hero.png` | `KF_HierarchyKit_Hero.png` |
| README | `README.md` | (en raíz del producto) |

---

## 5. Carpetas (estructura por producto)

```
KF_<Slug>/
  Editor/       ← Scripts de editor únicamente
  Runtime/      ← Scripts runtime (omitir si 100% editor-only)
  Shaders/      ← Solo para productos de shaders
  Demo/         ← INCLUIDO en package (el comprador lo recibe)
    Demo_KF_<Slug>.unity
    DemoAssets/
  Documentation/
    KF_<Slug>_Manual.pdf   ← Opcional pero recomendado
  README.md     ← Obligatorio
  Third-Party Notices.txt  ← Obligatorio si hay código de terceros
```

**PROHIBIDO** en la carpeta del producto:
- Archivos `_dev/`, `Brief.md`, `Architecture.md`, tests internos
- Cualquier asset de otra tool KITforge
- Escenas `Test_*.unity`

---

## 6. MenuItems

Formato: `KITforge/<NombreTool>/<Accion>` — top-level, al mismo nivel que File, Edit, Assets, Tools.

```csharp
[MenuItem("KITforge/Hierarchy Kit/Open Window %#h")]
[MenuItem("KITforge/Project Audit/Run Scan")]
[MenuItem("KITforge/Project Audit/Settings")]
```

Todos los MenuItems del mismo producto bajo el mismo submenu. El shortcut (si hay) va al final entre `%`, `#`, `&` y la tecla.

---

## 7. ScriptableObject instances creadas por el comprador

Si el usuario crea instancias de SOs:
- Nombre del asset: `KF_<Slug>Settings.asset` por defecto
- Location: `Assets/` por defecto (el usuario puede moverlo)
- Siempre `[CreateAssetMenu]` con `menuName = "KITforge Labs/..."` para descubrimiento (Assets menu, no Tools — convención Unity estándar para SOs)

```csharp
[CreateAssetMenu(menuName = "KITforge Labs/Hierarchy Kit Settings", fileName = "KF_HierarchyKitSettings")]
public class KF_HierarchyKitSettingsSO : ScriptableObject { }
```

---

## 8. Constantes de versión

Cada producto expone su versión como constante interna:

```csharp
public static class KF_HierarchyKitVersion
{
    public const string Current = "1.0.0";
}
```

Formato: `MAJOR.MINOR.PATCH` — semver estricto.
