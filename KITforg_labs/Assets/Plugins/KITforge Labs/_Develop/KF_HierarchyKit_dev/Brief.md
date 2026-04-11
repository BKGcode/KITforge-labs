# Brief — KF_HierarchyKit
Status: APPROVED
Phase: BRIEF
Score: 29/35
Tier: Simple
Wave: 1
Date created: 2026-04-11
Date approved: 2026-04-11
Global skills: unity-rules, unity-tools-skill, code-doctor-skill
KF skills: KF_NamingConventions, KF_BriefRules, KF_QARules

---

## 0. Identity
Slug: KF_HierarchyKit
Name: Hierarchy Enhancement Kit
One-liner: Color-coded headers and groups for Unity Hierarchy — zero scene pollution
Category: Tools > Utilities
Price: Freemium — free: colors+headers; paid($15): auto-color rules + team SO sync
Tier: Simple (1 week)
Wave: 1

---

## 1. Objective
PROBLEM: Unity Hierarchy = flat unstructured list. In scenes with 50+ objects → wrong selections, lost context, slow navigation. Native workaround = empty GameObjects as "--- UI ---" headers → scene pollution, breaks prefab workflows, adds useless objects to builds.
BUYER: any Unity dev with non-trivial scenes. Frequency: every session, every scene. Solo indie ↔ small studio ↔ educator. No level tech knowledge required.
VALUE: adds visual structure to any scene without modifying the scene at all.

---

## 2. Solution
CORE MECHANISM: `EditorApplication.hierarchyWindowItemOnGUI` hook → intercept every row draw → overlay colors, labels, icons. ZERO GameObjects created. ZERO Components added.
AUTO-ACTIVATION: `[InitializeOnLoad]` static class → subscribes on domain reload without any user action.
STATE: EditorPrefs (per-user, default) + optional Settings SO (team-shared, versionable).
CONTEXT MENU: right-click any row → "Add Header Above" → inline header definition.
SETTINGS WINDOW: optional EditorWindow for bulk config. Not required for feature to work.

APIS (exact):
- `EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyRow` (subscribe in [InitializeOnLoad])
- `EditorGUI.DrawRect(rect, color)` — background tints per row
- `GUI.Label(rect, content, style)` — header text rendering (IMGUI context, NOT UIToolkit)
- `EditorGUIUtility.IconContent(iconName)` — type indicators
- `EditorPrefs.GetString/SetString` — per-user config serialized as JSON
- `PrefabUtility.GetPrefabAssetType(go)` — prefab status indicators
- `SessionState.SetBool/GetBool` — group collapse state (session only)
- `[InitializeOnLoad] + static constructor` — auto-subscribe, domain reload safe

OUT OF SCOPE v1:
- Drag-reorder headers
- Per-component icon customization beyond type
- Multi-project settings sync
- Built-in palette library
- Undo for header placement (EditorPrefs ops aren't undoable by default)
- Play Mode-specific behavior (same as Edit Mode)

---

## 3. References
COMPETITORS:
| Name | Price | Good | Delta |
|------|-------|------|-------|
| vHierarchy 2 | free/paid | UX polish, mindshare | REQUIRES HeaderObject GOs in scene = scene pollution |
| Hierarchy 2 (Dmitry Edge) | free (open source) | Same hook approach, 1.2k stars | Unmaintained, no Unity 6 tests, no settings UI |
| Rainbow Hierarchy 2 | $10-15 | Color approach | Old codebase, IMGUI only, no context menu |
| QuickHierarchy | $15 | Icon focus | Different target, not direct competitor |

KEY OPEN SOURCE TO STUDY (architecture, not copy):
- https://github.com/nicloay/Unity-Hierarchy-Header (simple hook pattern)
- https://github.com/febucci/unitypackage-custom-hierarchy (context menu pattern)

UNITY APIS:
- https://docs.unity3d.com/ScriptReference/EditorApplication-hierarchyWindowItemOnGUI.html
- https://docs.unity3d.com/ScriptReference/EditorGUI.DrawRect.html
- https://docs.unity3d.com/Manual/RunningEditorCodeOnLaunch.html ([InitializeOnLoad])

---

## 4. Advantages
vs vHierarchy 2: NO HeaderObject GameObjects. Scene before = scene after install. CANNOT break prefab workflows or pollute transform hierarchy.
vs Hierarchy 2 (free): Unity 6 support, maintained, settings UI, proper package structure.
vs empty-GO workaround: zero performance cost (no GOs), configurable appearance, scales across all scenes.
UNIQUE ANGLE: "Team-syncable" — Settings SO is version-controlable. All teammates see same colors after git pull. vHierarchy uses EditorPrefs only (per-user unshared).

---

## 5. Risks
| Risk | P | Impact | Mitigation |
|------|---|--------|-----------|
| hierarchyWindowItemOnGUI perf | HIGH | HIGH | Cache ALL data outside callback. Zero LINQ, zero string alloc per frame. Profile with 200+ object scene. < 1ms/row target. |
| Domain reload state loss | HIGH | MEDIUM | [InitializeOnLoad] must re-read EditorPrefs into runtime cache. Explicit test: compile → verify still working. |
| Conflict with vHierarchy (same hook) | MEDIUM | MEDIUM | Both subscribe to same event. Unity calls both in undefined order. Must coexist: no GUIUtility.ExitGUI, no position assumptions. Test explicitly with both installed. |
| Settings SO deleted/moved | LOW | LOW | Graceful fallback to EditorPrefs. Try/catch SO load, never crash. |
| hierarchyWindowItemOnGUI deprecated | LOW | HIGH | Monitor Unity changelog. Wrap in version-conditioned compilation if needed. |
| IMGUI in UIToolkit world | MEDIUM | LOW | Hook context is IMGUI-only — can't use UIToolkit inside it. Settings WINDOW can use UIToolkit. Document this clearly. |

Unity min version: 6000.0.0f1
URP required: NO
IL2CPP safe: YES (no Reflection.Emit, no DynamicMethod)

---

## 6. UI Design
TYPES: [InitializeOnLoad] static hook (no window) + optional EditorWindow for settings
UI SYSTEM per context:
- Hierarchy hook: IMGUI (REQUIRED — hierarchyWindowItemOnGUI is IMGUI context)
- Settings window: UI Toolkit (UXML + USS) for Unity 6

HIERARCHY ROW rendering (per header object):
```
[FULL_WIDTH_COLOR_RECT][CENTERED_HEADER_TEXT in bold][optional type icon right-aligned]
```
HIERARCHY ROW rendering (normal object with color rule):
```
[4px LEFT BORDER in rule color][normal Unity hierarchy content unchanged]
```

SETTINGS WINDOW sketch:
```
┌──────────────────────────────────────────────┐
│ KF KITforge labs — Hierarchy Kit            ×│
├──────────────────────────────────────────────┤
│ [Headers]  [Color Rules]  [Display]          │
├──────────────────────────────────────────────┤
│ ▼ Headers                                    │
│   ■ [color▼] [icon▼]  [text__________] [×]  │
│   ■ [color▼] [icon▼]  [text__________] [×]  │
│   [+ Add Header]                             │
│ ▼ Color Rules (paid tier)                    │
│   Tag: [Untagged▼] → [color▼]  [×]          │
│   [+ Add Rule]                               │
│ ─────────────────────────────────────────── │
│ Storage: ●EditorPrefs  ○Project SO [Browse] │
│                    [Apply] [Reset Defaults]  │
└──────────────────────────────────────────────┘
```

---

## 7. UX Flow
FIRST USE (imports package, knows nothing):
1. Import .unitypackage
2. Open any existing scene → hierarchy rows already enhanced (default: subtle left border, auto type-icon)
3. **AHA MOMENT** ← visible in < 10 seconds, zero config
4. Right-click hierarchy row → "Add Header Above" → inline editor → type name → Enter
5. Header renders immediately

DAILY USE:
1. Building hierarchy → add headers as you go via context menu
2. Share project → if SO set up → teammates see same structure on git pull

POWER USE:
1. Open Settings window → bulk config all headers per scene
2. Auto-color rules: "all objects with tag 'UI' → blue background"

---

## 8. Checkpoints
| # | Checkpoint | Criterion |
|---|-----------|-----------|
| 1 | Package imports clean | Zero compile errors in clean Unity 6 project |
| 2 | Auto-activation | Any scene opened → default visual enhancements visible |
| 3 | Context menu | Right-click hierarchy row → "Add Header Above" option visible and works |
| 4 | Header renders | Header row shows colored background + bold text |
| 5 | Settings window | MenuItem "KITforge Labs/Hierarchy Kit/Settings" opens window, no console errors |
| 6 | EditorPrefs persistence | Change setting → domain reload → setting preserved |
| 7 | Optional SO | Create/assign Settings SO → config loads from SO, overrides EditorPrefs |
| 8 | Demo scene | Demo_KF_HierarchyKit.unity loads, shows 5+ headers, 3+ color rules |
| 9 | Zero idle errors | No console errors when no scene loaded, window closed |
| 10 | Performance gate | 200-object scene → no visible hierarchy lag |

---

## 9. Validator / QA
VERIFY: open complex scene → visual grouping renders. Open Settings window → modify config → changes persist after domain reload.

NEVER (auto-fail):
- Creates any GameObject in user's scene
- Modifies any user asset without explicit action
- Throws exceptions during normal hierarchy repaint
- Leaves console errors at idle
- Causes detectable lag in scenes with 200+ objects
- Fails to load in project with vHierarchy 2 also installed

TEST DATA NEEDED:
- Scene with 100+ objects (varied tags, layers, nesting depths)
- Scene with prefab instances (root, nested, variant)
- Project with vHierarchy 2 installed simultaneously
- Empty project (no scenes)

DEMO SCENE must show:
- 5+ custom headers with different colors
- 3 color rules applied (by tag, layer, manual)
- EditorPrefs and SO storage both demonstrable

---

## 10. Tests
PURE C# (unit-testable, no Unity context):
- `ColorRuleResolver.Resolve(objectData, rules) → Color` — deterministic, mockable
- `HeaderDataSerializer.Serialize(List<HeaderDef>) → string` — JSON round-trip
- `HeaderDataSerializer.Deserialize(string) → List<HeaderDef>` — parse + error handling
- `SettingsMerger.Merge(editorPrefs, so) → FinalSettings` — priority logic

EDITOR INTEGRATION (EditModeTests):
- Window opens without errors
- [InitializeOnLoad]: after domain reload → hook still subscribed → hierarchy renders enhanced
- Config round-trip: write EditorPrefs → read back → values match

MANUAL (TestPlan.md):
- Full first-use flow from import
- Context menu in 3 different hierarchy states (empty scene, prefab scene, complex scene)
- Settings window UX: all controls functional
- vHierarchy coexistence test
- 200-object perf gate

TEST ASSEMBLY: `_Develop/Tests/KF_HierarchyKit_Tests/`
ASSEMBLY NAME: `KITforgeLabs.HierarchyKit.Tests`

---

## 11. Examples
SCENARIO A — Solo indie dev (complex mobile game, 80+ objects/scene):
Situation: hierarchy is a flat wall of text, selects wrong objects constantly
Action: imports, adds headers to group UI/Gameplay/Audio via right-click
Result: "I can see my scene again. Fixed in 2 minutes."

SCENARIO B — Tech artist in 3-person team (shared Unity project):
Situation: every dev has different color preferences, hierarchy looks different per machine
Action: opens Settings, sets up color rules (green=gameplay, purple=audio, red=VFX), saves to SO in Assets/
Result: all devs see identical hierarchy colors after git pull. One config, all machines.

SCENARIO C — Unity educator creating tutorial scene (60 objects, students following along):
Situation: students can't tell which objects belong to which tutorial step
Action: adds numbered step headers "--- Step 1: Setup ---", "--- Step 2: UI ---"
Result: hierarchy becomes a visual guide. Students can follow scene structure without explanation.

---

## Approval checklist
- [x] All sections filled
- [x] One-liner ≤ 12 words, value-oriented not technical
- [x] Explicit out-of-scope list (6 items)
- [x] 4 competitors analyzed with honest "good" column
- [x] 5 risks with mitigation
- [x] ASCII wireframe present
- [x] AHA moment identified (< 10 seconds from import)
- [x] All 10 checkpoints have observable criteria
- [x] 6 NEVER cases listed
- [x] 4 pure-C# unit-testable functions identified
- [x] 3 examples with distinct user types and concrete results
