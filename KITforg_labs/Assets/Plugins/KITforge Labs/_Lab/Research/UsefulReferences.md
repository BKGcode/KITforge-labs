# Useful References — KITforge labs
Last updated: 2026-04-11

A curated reference list for tool building, Asset Store publishing, and Unity editor extension development.
These are working links, not decoration — each one has a reason listed.

---

## Asset Store Strategy & Publishing

| Resource | URL | Why useful |
|----------|-----|-----------|
| Asset Store Submission Guidelines | https://assetstore.unity.com/publishing/submission-guidelines | Must-read before submitting any product. Covers folder rules, namespace, documentation, demo requirements. |
| Publisher Portal | https://publisher.assetstore.unity.com | Where you manage products, check sales, respond to reviews. |
| Asset Store Affiliate FAQ | https://assetstore.unity.com/affiliates | Understand the royalty model (70% to publisher) and payment thresholds. |
| Package Validation Tool | https://docs.unity3d.com/Manual/cus-asmdef.html | Assembly definitions are required for proper package isolation. |
| Asset Store Best Practices (Unity blog) | https://blog.unity.com/engine-platform/best-practices-for-asset-store-publishers | Covers store page copy, screenshots, pricing, and category choice. |

---

## Unity Editor Extension Reference

| Resource | URL | Why useful |
|----------|-----|-----------|
| Editor Scripting Manual | https://docs.unity3d.com/Manual/editor-EditorWindows.html | Base reference for EditorWindow, CustomEditor, PropertyDrawer. |
| UIToolkit in Editor | https://docs.unity3d.com/Manual/UIE-support-for-editor-ui.html | UI Toolkit is the preferred UI approach for new Editor tools in Unity 6. |
| EditorPrefs Reference | https://docs.unity3d.com/ScriptReference/EditorPrefs.html | Per-user persistent editor settings (not per-project). Use for UI state, toggles, shortcuts. |
| Hierarchy GUI Callbacks | https://docs.unity3d.com/ScriptReference/EditorApplication-hierarchyWindowItemOnGUI.html | How `KF_HierarchyKit` hooks into the Hierarchy window. Critical. |
| OnGUI Inspector | https://docs.unity3d.com/Manual/editor-CustomEditors.html | CustomEditor + CreateInspectorGUI patterns. |
| IPreprocessBuildWithReport | https://docs.unity3d.com/ScriptReference/Build.IPreprocessBuildWithReport.html | How `KF_BuildChecker` intercepts pre-build events. |
| AssetDatabase Reference | https://docs.unity3d.com/ScriptReference/AssetDatabase.html | Central tool for scanning, loading, finding assets programmatically. |
| Selection & SceneView | https://docs.unity3d.com/ScriptReference/Selection.html | Useful for context menus, picked objects, and scene-level tools. |

---

## URP Shader Reference

| Resource | URL | Why useful |
|----------|-----|-----------|
| URP Volume Renderer Feature | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/manual/renderer-features.html | How `KF_MicroFX` custom effects plug into URP Volume framework. |
| ScriptableRendererFeature | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/api/UnityEngine.Rendering.Universal.ScriptableRendererFeature.html | C# entry point for each custom renderer feature. |
| URP HLSL Includes | https://github.com/Unity-Technologies/Graphics/tree/master/Packages/com.unity.render-pipelines.universal/ShaderLibrary | The real URP ShaderLibrary source. Look here before writing any HLSL include. |
| Blit Pattern (URP 17+) | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/manual/renderer-features/how-to-fullscreen-blit.html | Fullscreen blit changed in URP 14+. This is the correct pattern for Unity 6. |
| ShaderLab Reference | https://docs.unity3d.com/Manual/SL-Reference.html | Properties, Tags, Blend modes, Stencil, ZWrite. |

---

## Open Source Projects to Study (Not Copy)

| Repo | Stars | Relevance to KITforge |
|------|-------|----------------------|
| MyBox | 2.2k | Understanding which attributes/inspector utilities devs actually use daily |
| UnityAssetUsageDetector | 2k | Reference architecture for `KF_SafeClean` — how to safely scan asset references |
| Kino (Keijiro) | 2.4k | Minimal URP effect pack structure — one file per effect, clean Volume integration |
| NaughtyAttributes | 5k | Lightweight inspector attributes approach. Shows what Odin doesn't cover cheaply |
| Hierarchy 2 (Dmitry Edge) | 1.2k | Free hierarchy icons/colors — study UX approach for `KF_HierarchyKit` differentiation |
| Unity-Toolbar-Extender | 1.5k | Shows ToolbarExtender pattern — useful for `KF_ProjectAudit` quick-access button |
| Console Pro (martindale) | - | Community alternative inspector console patterns |

---

## Competitor Analysis

### Tools

| Product | Price | Rating | Reviews | Notes |
|---------|-------|--------|---------|-------|
| Odin Inspector | $65 | 4.9 | 2847 | Dominant inspector tool. Avoid direct competition on attributes. |
| vHierarchy 2 | $0-paid | 4.9 | - | Direct competitor to `KF_HierarchyKit`. Study their free/paid split. |
| vInspector 2 | $0-paid | 4.8 | - | Direct competitor to `KF_InspectorKit`. Free core, paid advanced. |
| Hot Reload | ~$80 | 4.6 | 278 | Workflow tool. Not a competitor but a benchmark for "pays for itself" positioning. |
| Unity Asset Usage Detector | free/paid | 4.7 | - | Competitor to `KF_SafeClean`. Paid version benchmarks the market. |
| Editor Console Pro | $30 | 4.7 | 642 | Enhanced console. Study UX patterns for log tools. |
| ProjectAuditor (Unity) | free | - | - | Unity's own auditor. Our `KF_ProjectAudit` must be simpler and more guided. |

### Shaders

| Product | Price | Rating | Reviews | Notes |
|---------|-------|--------|---------|-------|
| Beautify 3 | $50 | 4.8 | 420 | Dominant fullscreen post-fx. All-in-one. Our `KF_MicroFX` is focused, not competing. |
| Toony Colors Pro 2 | $65 | 5.0 | 547 | Toon shading leader. Not our target. |
| Flat Kit | $50 | 5.0 | 198 | Stylized shading pack. Not our target. |
| Highlight Plus 2 | $35 | 5.0 | 176 | Object outline/highlight. Study focus + demo quality. |
| ProPixelizer | $45 | 5.0 | 118 | Specialized pixel art post-fx. Study how a focused shader pack sells. |

---

## Community Channels to Monitor

| Channel | URL | Signal type |
|---------|-----|-------------|
| r/Unity3D | https://www.reddit.com/r/Unity3D/ | Bug complaints, tool recommendations, "what do you use for X" threads |
| r/gamedev | https://www.reddit.com/r/gamedev/ | Broader game dev signal, tool usage patterns |
| Unity Discord | https://discord.gg/unity | Real-time dev conversation, frustration signals |
| Asset Store Review Feed | (publisher portal) | Competitor review tone, common complaints |
| Unity Discussions Forum | https://discussions.unity.com | Bug reports, changelog reactions, API frustration |
| @kitforgelabs X/Twitter | - | Brand channel to monitor mentions and replies once active |

---

## Pricing Strategy Notes

Based on market observation:

| Category | Common range | Notes |
|----------|-------------|-------|
| Hierarchy tools | $0 free / $10-20 paid | Low-price bracket. Volume game. |
| Inspector tools | $10-30 | Unless it's Odin. Odin is in a class alone. |
| Safety/cleanup tools | $20-40 | "Peace of mind" framing supports higher price. |
| Capture/media tools | $15-30 | Utility perception, keeps expectation moderate. |
| Shader packs | $35-65 | Visual impact = premium expectation. |
| Full workflow suites | $60-150 | Requires established reputation and large demo. |

**KITforge labs positioning for Wave 1:** Start at the lower-mid range ($20-40). Focus on reviews and reputation over revenue. Raise prices in v2 with added features.

---

## Key API Docs Bookmarks Per Product

### KF_HierarchyKit
- `EditorApplication.hierarchyWindowItemOnGUI`
- `EditorGUI`, `EditorGUIUtility`
- `SessionState` (UI state per session), `EditorPrefs` (persistent preferences)

### KF_ProjectAudit
- `AssetDatabase.FindAssets`, `AssetDatabase.GetDependencies`
- `PrefabUtility.GetPrefabAssetType`
- `EditorBuildSettings.scenes`
- `PlayerSettings`

### KF_ScreenCapture
- `ScreenCapture.CaptureScreenshot`
- `Camera.targetTexture`, `RenderTexture`, `Texture2D.ReadPixels`
- `Application.persistentDataPath`
- `UnityEditor.RecorderFrameRate` (Unity Recorder integration path)

### KF_BuildChecker
- `IPreprocessBuildWithReport`, `BuildReport`
- `PlayerSettings`, `BuildPlayerOptions`
- `AssetDatabase` (scene asset validation)
