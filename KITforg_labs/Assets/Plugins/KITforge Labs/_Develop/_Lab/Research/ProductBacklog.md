# Product Backlog — KITforge labs
Last updated: 2026-04-11 (expanded 2026-04-11: +5 products from external candidate review)
Status: Wave 1 planning

---

## Scoring rubric

Each product is scored 1–5 on 7 axes. Higher is always better.

| Axis | What it measures |
|------|-----------------|
| **Pain** | How frequently and acutely Unity developers feel this problem |
| **Audience** | How broad the potential buyer base is |
| **Setup** | How quickly a buyer gets value after import (5 = instant, 1 = hours of config) |
| **Demo** | How easy it is to show the value in a GIF / video / screenshot |
| **Review safety** | How unlikely it is to collect bad reviews (5 = nearly impossible to dislike) |
| **Support ease** | How cheap and low-friction ongoing support will be |
| **Expansion** | Whether the product can grow naturally over time without re-architecture |

**Total max: 35**

---

## Ranked Backlog

| # | Slug | Name | Pain | Audience | Setup | Demo | Safety | Support | Expansion | **Total** |
|---|------|------|------|---------|-------|------|--------|---------|-----------|---------|
| 1 | `KF_HierarchyKit` | Hierarchy Enhancement Kit | 4 | 5 | 5 | 5 | 3 | 4 | 3 | **29** |
| 2 | `KF_ProjectAudit` | Project Health Auditor | 4 | 5 | 5 | 4 | 3 | 3 | 5 | **29** |
| 3 | `KF_SafeClean` | Safe Cleanup & Dependency Map | 5 | 5 | 4 | 4 | 3 | 3 | 4 | **28** |
| 4 | `KF_ScreenCapture` | Game Screenshot & Media Toolkit | 3 | 4 | 5 | 5 | 4 | 4 | 3 | **28** |
| 5 | `KF_BuildChecker` | Pre-Build Validator | 4 | 4 | 4 | 3 | 4 | 4 | 4 | **27** |
| 6 | `KF_MicroFX` | URP Micro FX Pack | 3 | 4 | 4 | 5 | 3 | 3 | 3 | **25** |
| 7 | `KF_PaletteKit` | Color & Material Palette Manager | 3 | 3 | 4 | 4 | 4 | 4 | 3 | **25** |
| 8 | `KF_DebugOverlay` | In-Game Debug & Stats Overlay | 4 | 3 | 3 | 4 | 3 | 3 | 4 | **24** |
| 9 | `KF_InspectorKit` | Inspector Productivity Pack | 4 | 4 | 4 | 3 | 2 | 2 | 4 | **23** |
| 10 | `KF_SceneExplorer` | Scene Cross-Reference Explorer | 3 | 3 | 4 | 3 | 3 | 2 | 3 | **21** |

---

## Product Cards

### 1. `KF_HierarchyKit` — Hierarchy Enhancement Kit

**One sentence:** Color-coded labels, section dividers, quick-toggle icons, and folding groups for the Unity Hierarchy window.

**Problem:** The default Unity Hierarchy is a flat list with no visual structure. In projects with 50+ objects per scene it becomes unreadable fast.

**Target buyer:** Any Unity developer working on non-trivial scenes. Solos, small studios, educators.

**Competition context:** `vHierarchy` is the market leader. Strategy: offer a free tier with core features, paid tier adds automation (auto-color by tag, batch tools, prefab indicators).

**Unique angle:** Zero-setup. Works by intercepting `OnGUI` hierarchy callbacks. No additional MonoBehaviours, no scene-level config asset required.

**MVP feature set:**
- Custom separator/header rows (drag-create inline)
- Row color by type, tag, or manual override
- Quick visibility & lock toggles per row
- Collapsible groups (visual only, not parenting)
- Theme presets (dark, light, pastel)

**Commercial tier:** Freemium — free with core separators/colors, paid for automation + export presets.

**Why first:** Visual impact is immediate. Demo is a 5-second GIF. Low support burden. Builds brand recognition fast.

---

### 2. `KF_ProjectAudit` — Project Health Auditor

**One sentence:** One-click editor window that scans your Unity project for common problems and generates a prioritized report.

**Problem:** Project health issues (missing references, empty folders, inconsistent naming, leftover test scenes, unreferenced assets, oversized textures) accumulate invisibly until they cause real damage.

**Target buyer:** Solo devs and tech leads who care about delivery quality. Very broad audience.

**Competition context:** `ProjectAuditor` by Unity themselves exists but is developer-targeted and verbose. Gap: a simpler, opinionated version for non-auditors.

**Unique angle:** Opinionated rule catalog. Every flag has a plain-language explanation and a one-click fix or dismissal. Results save between sessions.

**MVP feature set:**
- Missing reference scanner (Prefabs, ScriptableObjects, Scenes)
- Empty folder sweeper (with safe delete)
- Texture size / compression rule checker
- Scene count and addressability audit
- Exportable HTML/markdown report
- Dismissible notices with project memory

**Commercial tier:** Paid standalone. Could offer a free "lite" with 3 rules only.

**Expansion path:** Custom rules SDK (let studios define their own rules as ScriptableObjects) — major differentiator.

---

### 3. `KF_SafeClean` — Safe Cleanup & Dependency Map

**One sentence:** Find every object that references an asset, safely reorganize or delete it, and never break your project again.

**Problem:** Devs are afraid to delete or move assets because Unity's reference tracking is opaque. This leads to stale, bloated projects.

**Competition context:** `UnityAssetUsageDetector` (open-source, 2k stars) proves the demand. Strategy: build a polished paid version with a visual dependency graph and guided cleanup flows.

**Unique angle:** "Confidence mode" — before any destructive operation, show a full impact list. Hard gate on operations that would break references.

**MVP feature set:**
- Asset dependency graph (who uses what)
- Reverse lookup (what uses this asset?)
- Safe delete with pre-flight validation
- Move/rename with automatic reference update
- Session log of all changes (undo history narrative)

**Commercial tier:** Paid. Premium positioning — "worth it once to save one broken build."

**Risk note:** False positives are the enemy. Must be conservative by default. Any incorrect "safe to delete" = guaranteed bad review.

---

### 4. `KF_ScreenCapture` — Game Screenshot & Media Toolkit

**One sentence:** Capture screenshots, record GIFs, export spritesheets, and organize media from inside Unity Editor with one shortcut.

**Problem:** Taking clean screenshots and capture media in Unity is still a manual, awkward process (PrintScreen, external recorders, janky paths). Devs need captures for devlogs, store pages, Reddit posts, team showcases.

**Target buyer:** Indie devs, small studios, educators, streamer/YouTubers building games.

**Competition context:** Several screen recorders exist. Gap: a Unity-native tool that captures at the exact game resolution with clean UI layer control, custom watermarks, and organized folder output.

**Unique angle:** Designed for store page production — bulk mode, predefined ratios (16:9, 9:16, 1:1), naming templates, batch export.

**MVP feature set:**
- Single screenshot with shortcut (editor or runtime trigger)
- Configurable resolution multiplier (1x, 2x, 4x)
- UI layer on/off per capture
- Auto-named output folder by date/project
- GIF capture (short runtime loop)
- Export to PNG/JPG/WebP

**Commercial tier:** Paid, low price point ($15-25). Strong demo via its own output.

---

### 5. `KF_BuildChecker` — Pre-Build Validator

**One sentence:** A configurable checklist that runs before every build to catch product-breaking mistakes before they reach the platform.

**Problem:** Build failures and platform rejections due to obvious, avoidable issues (wrong bundle ID, missing splash, wrong target version, uncompressed audio, test level in build settings) cost real time.

**Target buyer:** Mobile and console developers, anyone shipping to stores.

**Unique angle:** Pluggable rule system — studios can write their own rules using a simple interface. Shared company rule bundles.

**MVP feature set:**
- Built-in rules: bundle ID, app version, build scenes order, icon sizes, audio compression, PlayerSettings checklist
- Custom rule API (implement `IBuildRule`)
- Pre-build hook (auto-runs) + manual window
- Result summary with pass/fail/warning per rule
- Timed stamp and build report export

**Commercial tier:** Paid. Strong CI angle (also runs headless) = sells to studios.

---

### 6. `KF_MicroFX` — URP Micro FX Pack

**One sentence:** 10 focused, drag-and-drop URP post-processing effects with strong visual results and zero shader graph dependency.

**Problem:** URP post-processing is limited. Adding custom effects via Volume framework requires shader expertise most devs don't have.

**Competition context:** `Beautify 3` dominates. Strategy: a focused, cheaper pack of 8-10 effects that solve specific aesthetic gaps vs a massive all-in-one.

**Unique angle:** Each effect targets one specific visual problem. "Just add Scanlines. Just add Chromatic Burst on hit. Just add Retro Dither." No bloat.

**MVP feature set (proposed effects):**
- Dithering / retro palette reduction
- Chromatic aberration burst (event-driven)
- Edge glow (Sobel-based)
- Pixelate (resolution reduction, per-frame)
- VHS noise + color grading preset
- CRT scanlines
- Color LUT bake helper
- Vignette + lens distortion combo preset
- Depth haze (distance fog with color tint)
- Film grain (animated)

**Commercial tier:** Paid, mid-range ($40-60). Visual tools command more.

**Risk note:** URP version compatibility. Must test on URP 14, 16, 17. Setup instructions critical.

---

### 7. `KF_PaletteKit` — Color & Material Palette Manager

**One sentence:** A project-wide palette manager that lets you define color sets, apply them to materials in bulk, and switch visual themes in one click.

**Problem:** Maintaining consistent color palettes across hundreds of materials is painful. "Change the primary green across the whole game" becomes a slow manual hunt.

**Target buyer:** Artists, TAs, indie devs working on polished visual projects.

**MVP feature set:**
- Palette ScriptableObject (named swatches + roles)
- Material browser with palette filter
- Bulk apply swatch to material property
- Theme switch (swap entire palette, materials update live)
- Export palette as texture atlas / CSS / JSON

**Commercial tier:** Paid, low-mid range ($20-35).

---

### 8. `KF_DebugOverlay` — In-Game Debug & Stats Overlay

**One sentence:** A runtime overlay panel showing FPS, memory, custom values, and a dev command console that disappears in release builds.

**Problem:** Hardcoding debug HUDs is tedious and error-prone. Devs re-write the same debug overlay in every project.

**Unique angle:** Zero MonoBehaviour required — register values as lambdas. Automatic stripping via `#if UNITY_EDITOR || DEVELOPMENT_BUILD`.

**MVP feature set:**
- FPS, CPU ms, GPU ms, memory
- Custom value registration API
- Dev command console (type commands, execute C# lambdas)
- Screenshot button in overlay
- Toggle shortcut (configurable)
- Theme: minimal / detailed / compact

---

### 9. `KF_InspectorKit` — Inspector Productivity Pack

**One sentence:** A set of custom property attributes and inspector extensions that make MonoBehaviour components easier to read and edit in the Inspector.

**Problem:** Default Unity Inspector shows every field as a plain box. No grouping, no previews, no validation feedback.

**Competition context:** `Odin Inspector` is dominant and expensive. Gap: a lightweight, low-footprint alternative for devs who don't want the Odin overhead.

**Risk note:** Odin has network effects. Must be pitched as "Odin-free" complement, not replacement. Or a "dip your toe in" version.

**MVP feature set:**
- `[ReadOnly]`, `[Required]`, `[MinMax]`, `[PreviewTexture]` attributes
- Section headers and dividers in Inspector
- Inline SO editor (expand a ScriptableObject field inline)
- Field validation with inline warning icons
- Reorderable tagged lists

---

### 10. `KF_SceneExplorer` — Scene Cross-Reference Explorer

**One sentence:** An editor panel showing all cross-scene and cross-prefab object references with one-click navigation.

**Problem:** In multi-scene setups, breakage between scene references is invisible. "Find who references this component" requires a project-wide search that Unity doesn't natively provide clearly.

**Target buyer:** Medium-to-large teams working with multi-scene architectures.

**Risk note:** Most valuable for larger projects. Solo devs may not see pain clearly.

**MVP feature set:**
- Cross-scene reference scanner
- Prefab reference graph
- Navigate-to buttons
- Export reference map as JSON / CSV
- Highlight broken references

---

## Wave 1 recommendation

Start with the **top 3 by score AND strategic fit for a new brand:**

| Priority | Product | Rationale |
|----------|---------|-----------|
| **First** | `KF_HierarchyKit` | Fastest build, highest visual impact, cheapest to support. Gets first reviews. |
| **Second** | `KF_ProjectAudit` | Broader value prop, establishes "safety & quality" brand voice. |
| **Third** | `KF_ScreenCapture` | Different segment (media/capture), fun to demo, very low risk. |

These three cover three different buyer motivations:
- ergonomics (`HierarchyKit`)
- safety (`ProjectAudit`)
- output / publishing (`ScreenCapture`)

And they share zero code. Each is independently shippable.

**Note (2026-04-11):** `KF_SceneBackup` (score 26, Simple tier) is a strong Wave 1 candidate. Unity 6 has no native auto-save. Could replace or join `KF_ScreenCapture` depending on team capacity.

---

## Products NOT in Wave 1 (rationale)

- `KF_SafeClean` — High value but high false-positive risk. Needs more careful design before building.
- `KF_BuildChecker` — Needs platform testing matrix. Better as Wave 2 after brand is established.
- `KF_MicroFX` — Shader work is higher variance. Better once tooling workflow is proven.
- `KF_InspectorKit` — Odin shadow is long. Needs a clear positioning angle before starting.
- `KF_DebugOverlay` — Solid product but crowded space at $0 (many free options).
- `KF_PaletteKit` — Niche. Good fit once artist audience is established.
- `KF_SceneExplorer` — Most valuable for large teams. Not the strongest first impression for a new solo studio.
- `KF_DataBridge` — Promising (27/35) but Moderate tier. Wave 2 after brand established.
- `KF_TexturePacker` — Valid pain. Needs clear differentiation vs Unity Sprite Atlas. Wave 2.
- `KF_AutoCull` — Solid for mobile devs. Runtime component increases support burden. Wave 2.
- `KF_SpriteSlicer` — 2D-only audience. Narrower reach. Wave 3.

---

## New Product Cards (added 2026-04-11)

### 11. `KF_SceneBackup` — Scene Auto-Backup

**One sentence:** Automatically saves timestamped backup copies of your scenes at configurable intervals — so a crash never means lost work again.

**Problem:** Unity has no native auto-save. Crashes, accidental overwrites, or "I accidentally deleted that" moments cause real lost work. Devs either save obsessively or lose progress.

**Target buyer:** Every Unity developer. One of the broadest possible audiences.

**Competition context:** A few free open-source scripts exist (GitHub) but are unsupported and require manual setup. No polished paid solution in this space.

**Unique angle:** Zero setup — works immediately on import. Configurable interval, max backups count, one-click restore from the backup list. Transparent and non-intrusive.

**MVP feature set:**
- Auto-save on interval (5/10/15/30 min, configurable)
- Backup stored in `Library/SceneBackups/` (not in Assets — not committed to version control)
- Backup list window with timestamps and restore button
- Manual save shortcut
- Notify in status bar when backup completes

**Commercial tier:** Paid, $15. Impulse buy. "Worth it the first time Unity crashes."

**Wave:** 1 (Simple tier, 1 week build, universal audience)

**Score breakdown:** Pain 5 | Audience 5 | Setup 5 | Demo 4 | Safety 4 | Support 5 | Expansion 3 = **31/35**

---

### 12. `KF_DataBridge` — Spreadsheet to ScriptableObject Importer

**One sentence:** Import data from Excel or Google Sheets into Unity ScriptableObjects in one click — no boilerplate, no manual copy-paste.

**Problem:** Game designers maintain balance data in spreadsheets. Importing it into Unity means either writing custom parsers every project or copying values by hand. Both are slow and error-prone.

**Target buyer:** Studios and solo devs working with data-heavy games (RPGs, strategy, card games). Designers who can't write C#.

**Competition context:** Odin Serializer handles some of this. A few free CSV importers exist but are limited and unsupported. No polished, opinionated paid solution targeting this exact pain.

**Unique angle:** Schema detection — the tool reads the spreadsheet and proposes the ScriptableObject layout. No-code mapping UI. Incremental re-import (only changed rows update).

**MVP feature set:**
- Import from .xlsx (no Google Sheets API — avoid external deps)
- Auto-detect column types (string, int, float, bool, enum)
- Visual column-to-field mapper
- Generate ScriptableObject class from schema
- Re-import with change detection
- Progress bar for large sheets

**Commercial tier:** Paid, $35. Moderate complexity but strong ROI is easy to demonstrate.

**Wave:** 2

**Score breakdown:** Pain 4 | Audience 4 | Setup 3 | Demo 4 | Safety 4 | Support 4 | Expansion 4 = **27/35**

---

### 13. `KF_TexturePacker` — UI Texture Atlas Builder

**One sentence:** Combine UI sprites into a single texture atlas from inside Unity Editor — reducing SetPass calls without touching your folder structure.

**Problem:** UI batching in Unity requires sprites to be in the same atlas. Unity's Sprite Atlas setup is manual, opaque, and easy to misconfigure. Developers either tolerate excessive draw calls or spend time debugging atlas misses.

**Target buyer:** Any Unity dev with a non-trivial UI — mobile games especially where SetPass calls matter.

**Competition context:** Unity Sprite Atlas is the native solution but poorly documented and manual. TexturePacker (external app) is the standard but costs money and requires leaving Unity.

**Unique angle:** Fully inside Unity Editor. Visual preview of atlas packing. One-click rebuild. Respects existing folder structure — no files moved.

**MVP feature set:**
- Select sprites → pack into atlas asset
- Preview packing efficiency (utilization %)
- Rebuild atlas on demand or on asset change
- Report: which sprites are NOT in any atlas (draw call risk)
- Integration with Unity Sprite Atlas (generates the .spriteatlas asset)

**Commercial tier:** Paid, $25.

**Wave:** 2

**Score breakdown:** Pain 4 | Audience 4 | Setup 4 | Demo 4 | Safety 4 | Support 4 | Expansion 3 = **27/35** → adjusted to **25** given Unity Sprite Atlas competition risk.

---

### 14. `KF_AutoCull` — Scene Object Visibility Optimizer

**One sentence:** Automatically disables distant GameObjects based on configurable distance rules — with zero manual setup per object.

**Problem:** Mobile devs need to cull far objects for performance, but setting up LOD groups or custom scripts per object is tedious. Most devs either skip it or write the same distance-check script every project.

**Target buyer:** Mobile Unity developers building open or semi-open scenes.

**Competition context:** Unity LOD Groups exist but require manual setup per prefab. Occlusion Culling is bake-only. No simple "just disable things past this distance" tool with a clean UI.

**Unique angle:** Works at the scene level, not per-object. One component on a manager — all objects in range registered via tags or layers. No touching individual prefabs.

**MVP feature set:**
- Distance-based disable/enable per layer or tag
- Configurable distance per category
- Hysteresis (re-enable threshold different from disable threshold to avoid flickering)
- Editor visualization (Scene View overlay showing culling zones)
- Optional: LOD-style multi-distance tiers

**Commercial tier:** Paid, $20.

**Wave:** 2

**Score breakdown:** Pain 4 | Audience 3 | Setup 4 | Demo 4 | Safety 3 | Support 4 | Expansion 4 = **26/35** → adjusted to **24** (mobile-only audience narrowing).

---

### 15. `KF_SpriteSlicer` — Sprite Sheet Auto-Slicer

**One sentence:** Automatically slice and name sprites from large sprite sheets using configurable grid or smart-edge detection — with batch export to organized folders.

**Problem:** Unity's manual Sprite Editor is tedious for large sheets. Naming each sprite manually is error-prone. Studios working with 2D assets waste significant time on sprite setup.

**Target buyer:** 2D game developers. Narrower audience than editor tools.

**Competition context:** Unity's Sprite Editor exists but is manual. No polished batch-naming tool in this space.

**MVP feature set:**
- Grid slicer with naming templates (`hero_idle_0`, `hero_idle_1`, etc.)
- Smart edge detection (alpha-based bounds)
- Batch rename with preview
- Export to organized subfolder structure
- Pivot presets (center, bottom-center, custom)

**Commercial tier:** Paid, $15.

**Wave:** 3

**Score breakdown:** Pain 3 | Audience 3 | Setup 4 | Demo 4 | Safety 4 | Support 4 | Expansion 3 = **25/35** → adjusted to **22** (2D-only audience).

