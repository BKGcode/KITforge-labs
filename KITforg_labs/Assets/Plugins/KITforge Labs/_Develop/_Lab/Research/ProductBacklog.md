# Product Backlog — KITforge labs
Last updated: 2026-04-11
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

---

## Products NOT in Wave 1 (rationale)

- `KF_SafeClean` — High value but high false-positive risk. Needs more careful design before building.
- `KF_BuildChecker` — Needs platform testing matrix. Better as Wave 2 after brand is established.
- `KF_MicroFX` — Shader work is higher variance. Better once tooling workflow is proven.
- `KF_InspectorKit` — Odin shadow is long. Needs a clear positioning angle before starting.
- `KF_DebugOverlay` — Solid product but crowded space at $0 (many free options).
- `KF_PaletteKit` — Niche. Good fit once artist audience is established.
- `KF_SceneExplorer` — Most valuable for large teams. Not the strongest first impression for a new solo studio.
