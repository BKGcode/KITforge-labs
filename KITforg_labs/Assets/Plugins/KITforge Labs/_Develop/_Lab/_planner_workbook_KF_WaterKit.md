# Planner Workbook — KF_WaterKit
Session: 2026-04-12 → 2026-04-13 | Phase: DISCOVER ✅ ALIGN ✅ STRESS TEST ✅ PLAN 🔄
---

## ESSENCE (compressed)

**Draft proposition (unvalidated):**
> KF_WaterKit gives Unity 6 mobile developers production-ready stylized water with instant results via presets, zero extensions paywall, and explicit performance guarantees for low-spec devices.

**Critical functions (without which it has no reason to exist):**
1. Get great-looking stylized water in under 5 minutes from import
2. Ship on mobile without performance concerns (both low-end and mid-range hardware)
3. One price = everything (no extensions paywall)

**Scope red line (OUT for v1):**
- No underwater rendering (Staggart charges €23 for it — flag as future extension)
- No dynamic ripples/physics interaction (Staggart charges €18.40 — future extension)
- No HDRP
- No realistic/PBR water
- No 3D ocean simulation (no buoyancy physics, no vertex displacement at runtime)

---

## DISCOVER — Market Research

### Competitor map (research date: 2026-04-12)

| Asset | Price | Unity 6 | Reviews | Notes |
|---|---|---|---|---|
| **Stylized Water 3** (Staggart) | €45.08 base + €41.40 extensions | ✅ Render Graph native | 117 reviews, 856 ⭐ | Active (Apr 7 2026). The real competitor. |
| **Stylized Water 2** (Staggart) | €32.20 | ❌ No Unity 6 RG | 2,662 reviews | Legacy. Being sunsetted to SW3. |
| **Flat Kit** (Dustyroom) | €36.71 | ✅ | 198 reviews, 8,832 ⭐ | Full toon suite. Water is secondary. Not direct competitor. |

### Stylized Water 3 — Feature inventory

**Shader modes:** Unlit / Simple / Advanced (low → high-end scaling)
**Visual features:** Deep/shallow/horizon color, intersection foam, surface foam, GPU wave animations, animated caustics, translucency, flat shading (lowpoly), reflections (planar + SSR), refraction, sparkles, river mode, distance normals
**Technical:** Tessellation, C# API (wave height/normal), Align-to-Water component, Water Grid, planar reflections, SSR, Water Decals, Ocean mesh 8x8km, Height Pre-pass
**Presets included:** Clear, Frozen, Lava, Lowpoly, Murky, Ocean, Realistic, Swamp, Toon
**Platforms:** PC, macOS, Xbox, PS, Switch, Android/iOS, WebGL 2.0, WebGPU
**Extensions (sold separately):**
- Underwater Rendering: €23
- Dynamic Effects (ripples, shoreline waves): €18.40
**Total full cost: €86.48**

### SW3 — Explicit restrictions (= KF opportunities)
1. **"Built for 3D rendering. Can't be used with the 2D Renderer or Tilemaps"** → 2D games (top-down, side-scroller, isometric) are completely unserved.
2. **Extensions cost extra** → €86+ for full experience is steep for indie mobile devs.
3. **Only 117 reviews** → market position not yet consolidated at Unity 6 tier. Window is open.
4. **Minimum Unity 6000.0.60f1** → teams on earlier Unity 6 builds need to update first.
5. **"Not compatible with Apple Vision Pro"** → minor, not relevant for mobile.

### Demand signals (indirect)
- SW2 has 2,662 reviews → massive installed base. All those users must upgrade to SW3 or find alternatives.
- SW3 is 20% upgrade discount push → suggests active migration effort. Installed base = audience.
- Flat Kit water is not standalone → buyers who only want water have no clean option under €36.
- No strong contender in the "stylized water for 2D games" niche.

---

## ASSUMPTIONS LOG

| # | Assumption | Status | Risk if wrong |
|---|---|---|---|
| A1 | 2D Unity mobile game devs need stylized water and can't use SW3 | UNVALIDATED | If 2D devs use 2D shader workarounds or don't care about water, gap doesn't exist |
| A2 | Indie mobile devs find €45+ + extensions too expensive | UNVALIDATED | If they don't, price gap is not a real driver |
| A3 | "Preset-first" UX is a real unmet need vs SW3's material presets | UNVALIDATED | SW3 already has 9 presets — may be "good enough" |
| A4 | HLSL hand-written = better mobile perf than competitor | UNVALIDATED | SW3 is also HLSL hand-written. Need benchmark data. |
| A5 | Unity 6 is adopted by target audience | SEMI-VALIDATED | SW3 only targets Unity 6 and is selling. Signal = yes. |
| A6 | Full+Lite in one SKU is preferred over free+paid split | UNVALIDATED | user decision, no market data |

---

## STRESS TEST — Pre-ALIGN

### Scenario 1: "SW3 adds 2D support next update"
→ Directly closes A1 gap. KF needs 2D support to be load-bearing or must rely on other differentiators.
→ KF's "2D as primary use case" would be destroyed. **A1 is NOT load-bearing differentiator unless we own it clearly and fast.**

### Scenario 2: "Staggart releases bundle at €45 all-in"
→ Closes the price gap. KF's only remaining advantage = UX + presets + mobile-first messaging.
→ Implication: pricing alone cannot be the core differentiator.

### Scenario 3: "KF_WaterKit is technically identical to SW3 but cheaper"
→ Historic anti-pattern from KF_PaletteKit: "imitate and improve" without different value = features without purpose.
→ This is exactly where we are unless we define a clearly different buyer moment.

### Scenario 4: "KF_WaterKit and all backlog editor tools compete for limited dev time"
→ Water shader = runtime product, completely different skillset and support burden vs editor tools.
→ Risk: KF loses brand coherence (editor tools lab vs shader lab). 

---

## OPEN QUESTIONS (for ALIGN phase)

1. Should KF_WaterKit target 3D water only (compete directly with SW3), 2D water only (uncontested niche), or both?
2. What is the measurable "preset-first" claim? 5 minutes to working water? What does this look like in the demo?
3. Is the KF brand (editor tools lab) compatible with launching a runtime shader as first product?
4. What is the actual mobile performance claim? "30fps on [specific device]" requires benchmark data.
5. What specific visual style does "stylized" mean here? Toon? Cell? Flat? Painterly? The answer determines the feature set.
6. What is the right price point? €20 full-in vs SW3's €45+ = clear value signal. Or €35 one-price?

---

## ALIGN — Session 2026-04-13

### New finding: Distant Lands = publisher/40676

**Thalassophobia** (Distant Lands, €27.60): underwater **environment pack** (660 prefabs — fish, coral, sea bases) with water shaders as secondary feature. Built-in only for caustic cookies. Uses Amplify Shader Editor (not HLSL). Last updated Sep 2024. Only 14 reviews. NOT Unity 6 native. NOT a standalone water shader. NOT a direct competitor.

**This clears the lane:** The Distant Lands reference is about COZY's UX philosophy, not their water product. COZY is what the user wants to replicate in terms of experience: preset-led, module-based, premium inspector, zero config to first result.

---

### COMPRESSED PROPOSITION (v1 — for validation)

> **KF_WaterKit** gives 3D indie mobile developers production-ready stylized water in under 5 minutes — one price, zero extensions, preset-first workflow inspired by COZY's UX philosophy.

**Subject:** 3D indie mobile developer with a scene that needs gorgeous water
**Verb:** gets production-ready stylized water instantly
**Object:** via preset-first workflow, no extensions paywall, mobile-first perf guarantee

---

### CRITICAL FUNCTIONS (without these = no reason to exist)

1. **Preset-led discovery** — pick from a curated library (Ocean Tropical, Lake Misty, River Autumn, Arctic, Lava, Swamp, Toon, Low-poly...) and get an immediately beautiful result
2. **Mobile performance guarantee** — Full and Lite variants, explicit benchmarks on real hardware
3. **One price = everything** — No underwater extension, no dynamics extension. All in.
4. **Sea of Thieves visual bar** at mobile scale — depth color, foam, wave movement, transparency/refraction, caustics, sparkles

---

### SCOPE RED LINE — v1 (explicitly OUT)

- No 2D Renderer / Tilemaps (could be v2 differentiator)
- No buoyancy / physics API (could be v2)
- No HDRP
- No Built-in RP
- No tessellation (mobile GPU hostile)
- No ocean mesh generation (static mesh provided)
- No underwater camera effects (could be v2)
- No dynamic ripple system (could be v2)

---

### UPDATED COMPETITOR MAP

| Asset | Price | Unity 6 | Standalone shader | Preset-first UX | Mobile-explicit |
|---|---|---|---|---|---|
| **SW3** (Staggart) | €45 + €41 ext | ✅ | ✅ | Partial (9 presets, parameter-first) | ✅ scalable |
| **Thalassophobia** (Distant Lands) | €27.60 | ❌ | ❌ (pack+shader) | ❌ | Not primary focus |
| **Flat Kit** (Dustyroom) | €36.71 | ✅ | ❌ (suite) | ❌ | ✅ |
| **KF_WaterKit** (hypothesis) | TBD | ✅ | ✅ | ✅ CORE | ✅ CORE |

**Positioning:** The only Unity 6 standalone stylized water shader where you start with a preset and get a beautiful result in minutes — not a blank material plus documentation.

---

### BUYER MOMENT (validated by user)

> "I'm making a 3D indie mobile game where visual quality matters. I have a lake/river/ocean in my scene. The default Unity water looks terrible. I find SW3 for €45 and realize I need another €41 for the full experience — OR I want something that's gorgeous by default, guided by presets, and works without reading 40 pages of docs."

**The emotional driver:** visual pride + launch confidence + not wanting to fight the tool.

---

### UPDATED ASSUMPTIONS LOG

| # | Assumption | Status | Risk if wrong |
|---|---|---|---|
| A1 | 3D indie mobile devs find SW3's parameter-first UX frustrating | UNVALIDATED | SW3 may be "good enough" for most |
| A2 | €45+€41 = too much for indie mobile segment | UNVALIDATED | They may budget for it if it's the best |
| A3 | Preset-first = meaningful differentiator vs SW3's 9 presets | PARTIALLY — SW3 has presets but workflow starts from params | |
| A4 | HLSL = measurably better mobile perf than SW3 | [ASSUMPTION] Both are HLSL. KF advantage = Lite variant explicitness | |
| A5 | "All-in-one price" is a real purchase barrier reducer | UNVALIDATED | Needs review mining of SW3 1-3★ |
| A6 | Full+Lite single SKU is preferred | User confirmed ✅ | — |
| A7 | Distant Lands UX philosophy translatable to a shader Inspector | [ASSUMPTION] COZY is a system, water is one shader. Inspector ≠ modular system. Need to define what "COZY-level" means concretely for a shader. | HIGH |

---

### NEXT: STRESS TEST

Key scenarios to run:
1. SW3 ships a "Quick Start Presets" update — what breaks in KF's positioning?
2. KF_WaterKit ships but developer reports 0.8ms on mid-range Android — how does that compare to SW3 benchmarks?
3. Buyer sees KF at €30 and SW3 at €45 — which do they pick if SW3 has 2,662 reviews and KF has 0?

Before STRESS TEST: need to mine SW3 1-3★ reviews for unmet needs. That's the only way to validate A1, A2, A5.

---

## STRESS TEST — Session 2026-04-13

### Review Mining (sources: SW3 107 reviews, SW2 166 reviews — scraped 2026-04-13)

**Rating distribution — SW3:**
5★=101 · 4★=10 · 3★=1 · 2★=4 · 1★=1 (117 total)
→ 96% positive. Complaints are rare BUT specific and actionable.

---

#### Pain Point 1 — Onboarding friction (VALIDATES A1, A3)

**GamerRealities (4★, 6m ago):**
> "I spent hours reading every fact on the asset store page and docs as part of my purchasing due diligence. Yet I found it confusing to get water into my scene. Even though I have 6 years of Unity experience, I still want to be taught to use your tools from the beginning. Show us which prefabs we should drag in."

**Staggart's own reply:**
> "Perhaps a 'Water wizard' would be a welcome addition, to properly preview and pick the type of water bodies that are possible. Currently it involves dragging in a water prefab and trying out the different materials, but that could definitely be streamlined!"

**Xaha2425 (5★ — early experience, contrast signal):**
> "Second shock was when I just pressed five buttons in one screen to setup and then just dropped a prefab and it worked! Most of the other assets welcome you with red errors or clunky setup."
→ Setup validator exists — but it's for renderer setup, NOT for "which water should I pick?"

**andersondesigner (SW2, 4★):**
> "Very good but I have difficulty with 2 things: making rivers and increasing the water mesh scale without making the water buggy. I didn't find a convincing and detailed tutorial."

**Assessment:** The setup validator in SW3 is a checkbox wizard (renderer configuration). What doesn't exist is a *visual preset picker* — "pick a mood, get a beautiful result, then dive into parameters." This is the exact gap. **A1 VALIDATED. A3 VALIDATED.**

---

#### Pain Point 2 — Extensions paywall (partially validates A2)

**baumxyz (5★, SW2→SW3 user):**
> "Sadly the underwater shader will be separate again. I would have paid €5–10 more for a complete package."

**Assessment:** The frustration is noticed but mild — user still gave 5★. No outright "too expensive" in low-star reviews. SW3's base price (€45) is not a barrier; the *total stack* (€86) is. The "no extensions" signal is real but not loud. **A2 PARTIALLY VALIDATED: €86 is the perceived frustration, not €45.**

---

#### Pain Point 3 — Docs & tutorial gaps

**Delcasda (initially low, then 5★ when docs updated):**
> "Now that the documentation has been updated I am giving the 5 stars"
→ Documentation quality is a purchase criterion. Users will punish missing docs.

**andersondesigner (SW2):** Tutorial gap for rivers + mesh scaling.

**Assessment:** Buyers expect video tutorials and task-oriented guides ("how to set up a river"), not just API reference docs. KF must ship with a tutorial video and at minimum a Quick Start guide.

---

#### Pain Point 4 — Feature expectation mismatches (NOT a KF opportunity — note only)

- vplampinen (2★): Caustics not visible from underwater. Not a bug — physics limitation. User didn't read docs.
- Doxes (SW2, 2★): Bought SW2 the night before Unity 6, now needs SW3. Frustration = version fragmentation.
- brad_penbach (SW2, 2★): Angry about upgrade pricing (SW2→SW3). Long-term customer frustration.

**Assessment:** These are not unmet feature needs — they're expectation mismatches and business model frustration. KF single-SKU avoids version fragmentation anger entirely. That's a soft advantage.

---

#### Pain Point 5 — Feature gaps mentioned but not deal-breakers

- **Buoyancy physics**: Cyote (5★): "I do wish there were some included scripts for actual buoyancy instead of just the align to water component."
- **Wet surface contact**: SauraSMaker: "It would be nice if the water shader would create a wet effect on the ground when it comes into contact with it."
- **Waterfalls**: GamerRealities: "Waterfalls are not yet a feature."

**Assessment:** All three are "nice to have" from satisfied buyers. Not blockers. Note for v2 backlog.

---

### Assumption Log — Post-Mining Update

| # | Assumption | Status | Evidence |
|---|---|---|---|
| A1 | Onboarding frustration is real | ✅ VALIDATED | GamerRealities + Staggart's own "Water wizard" reply |
| A2 | €45+€41 price is a barrier | ⚠️ WEAKLY VALIDATED | baumxyz "5-10€ more for complete package"; no outright price complaints on base |
| A3 | Preset-first = real unmet need | ✅ VALIDATED | SW3 has 9 presets but no visual picker — workflow still starts from parameters |
| A4 | HLSL = measurably better mobile perf | ❌ NOT A GAP | SW3 Simple mode = excellent mobile perf. KF advantage = explicit benchmarks, not raw speed |
| A4b | Publishing explicit mobile benchmarks = differentiator | ✅ NEW | SW3 has no published benchmarks. KF publishing device+ms data = trust signal |
| A5 | Paywall reduces purchase intention | ⚠️ WEAKLY VALIDATED | Noticed and frustrating, but not a deal-breaker for most buyers |
| A7 | COZY-level Inspector translatable to a single shader | [STILL OPEN] | Needs design work in PLAN phase |

---

### Critical Scenarios

#### Scenario 1 — SW3 ships "Quick Start Preset Wizard" next update

**What Staggart said:** "Perhaps a 'Water wizard' would be a welcome addition."
→ He knows the gap. This update could ship any time.

**What breaks:** The primary UX differentiator narrows. If SW3 ships a visual preset picker, the "5 minutes to beautiful water" claim becomes contested.

**What holds:**
- KF's presets are a *named visual identity* (Ocean Tropical, Lake Misty, Arctic...) not just a shader setting bundle. Staggart's wizard would configure his existing 9 presets; KF's presets carry editorial identity and matched showcase renders.
- KF's "all-in price" and "no extensions" still stand.
- SW3 at €45 core still needs €41 for the full experience.

**Mitigation:** KF's preset-first philosophy must go deeper than a wizard. It must be the editorial experience: curated names, matched preview renders, scene-aware recommendations, mood suggestions. Wizard = tech answer. KF = product experience.

**Verdict:** Reduces but does NOT destroy KF's UX advantage. Speed up development. Preset quality and editorial identity must be exceptional from day one.

---

#### Scenario 2 — KF ships and benchmarks report 0.8ms on mid-range Android

**Context:** SW3 "Simple" mode gets "no frame rate impact" testimonials on Quest 3. That's hardware above mid-range mobile.

**Is 0.8ms bad?** At 60fps, total budget = 16.6ms. 0.8ms = ~5% of frame. That's reasonable for a water shader. SW3 "Simple" is likely in similar territory.

**The real issue:** SW3 has no published benchmark numbers anywhere. KF plans to publish them. But if KF publishes 0.8ms and users ask "is that good?", they need context. 

**Mitigation:** KF must publish benchmark *comparisons* (KF Lite vs KF Full vs SW3 Simple claimed equivalent) on defined hardware (e.g. Samsung Galaxy A54 — "2023 mid-range Android"). The comparison table itself is the differentiator, not the number.

**Verdict:** 0.8ms is a fine number — frame it correctly. The KF advantage is *transparency of benchmarks*, not the number itself. Ship with a performance table in the store description and README.

---

#### Scenario 3 — Buyer sees KF at €40 (0 reviews) vs SW3 at €45 (117 reviews)

**Reality check:** This is the primary launch risk. More damaging than any feature gap.

**The trust gap mechanics:**
- Asset Store buyers use review count as proxy for safety ("if 117 people bought it, it's debugged")
- Zero reviews = unproven, potential refund lottery
- Price gap (€40 vs €45) is only €5 — not compelling enough to overcome zero trust
- But €40 vs €86 (full stack) framing IS compelling IF clearly communicated on store page

**Mitigations:**
1. **Beta seeding strategy** — 5–10 developers test (with explicit "review requested" ask), launch with initial reviews
2. **Sample project quality** — high-quality demo scene with mobile showcase must be exceptional. Buyers trust their own preview more than other people's reviews.
3. **Publisher credibility ramp** — KF Labs needs at least 1 published product before WaterKIT to build publisher trust (or launch them close together)
4. **Store page video** — a 60-second "go from import to 5 beautiful scenes in 5 minutes" video converts better than any review
5. **Price framing on store page** — explicitly say "includes everything — no extensions" with a comparison call-out

**Verdict:** Trust gap is the **primary launch risk**. Plan Phase must account for beta seeding and store page quality as launch-critical deliverables — not afterthoughts.

---

### STRESS TEST — Conclusions

**Proposition holds. Three revisions:**

1. A1+A3 confirmed → "preset-first" is the core differentiator. Must be defended by *editorial identity* (named presets with visual showcase), not just a wizard.
2. A4 revised → Mobile performance differentiator is *benchmark transparency*, not raw speed. Ship with a device/ms table.
3. Trust gap → Primary launch risk. Beta seeding + exceptional demo scene + store video = non-negotiable pre-launch requirements.

**Updated compressed proposition (v2):**
> KF_WaterKit gives 3D indie Unity 6 developers production-ready stylized water in under 5 minutes via a curated preset library with editorial identity — one all-in price, explicit mobile benchmarks, no extensions paywall.

**What changed from v1:** "preset-first workflow inspired by COZY's UX" → "curated preset library with editorial identity" (more concrete, less brand-name dependent).

---

---

## PLAN — Session 2026-04-13

### System Decomposition

KF_WaterKit decomposes into 6 independent pieces. Build order and contracts defined per piece.

| Piece | What it is | Depends on | Produces |
|---|---|---|---|
| **P1 — Core Shader** | HLSL shader(s) with Full+Lite variants | Keyword architecture decision (D1) | `.shader` file(s) |
| **P2 — Preset Library** | Named `.mat` files, parameter bundles | P1 working | 8–12 `.mat` presets + preview renders |
| **P3 — Inspector UX** | `CustomEditor` + USS for the shader material | P1 + UX tier decision (D2) | One `Editor/` C# file + USS |
| **P4 — Demo Scene** | Showcase scene for store page + buyer evaluation | P2 + P3 | Unity scene + store screenshots/video |
| **P5 — Benchmark Suite** | Scene + device instructions for perf measurement | P1 (Full + Lite) | Documented ms table (device × variant) |
| **P6 — Store Package** | Store page copy, docs, Quick Start guide, video | All above | Uploaded package + store listing |

---

### Blocking Decisions (must be resolved before PLAN finalizes)

#### D1 — Shader architecture: single shader with keywords vs dual shaders

**Option A — One shader, two keyword paths (Full/Lite)**
- One `.shader` file, `#pragma multi_compile` or `#pragma shader_feature` to strip expensive features at build time
- Lite = keywords disabled → features stripped at build
- Pro: one codebase, SRP Batcher always compatible, variant count is controllable
- Con: shader variant explosion risk if keywords multiply, mobile stripped-variant cache size
- This is how SW3 works (Simple / Advanced modes = one shader, modes toggle features)

**Option B — Two separate shaders**
- `KF_WaterFull.shader` + `KF_WaterLite.shader`
- Pro: no variant complexity, each shader is exactly what it contains, Lite can be radically simpler
- Con: dual maintenance, preset system must handle two material types, Inspector must handle two shaders

**Option C — One shader, `LOD` groups**
- ShaderLab `SubShader` LOD for automatic quality fallback  
- Not recommended: LOD switching is Unity's legacy quality ladder, not the preset-first UX we want

**Planner recommendation:** **Option A**. SW3 uses it successfully. Variant count is manageable if keywords are budgeted from the start (planned maximum: 6 keywords = 64 variants, strip unused at build). Lite is then a material in the standard inspector with specific keywords disabled — which means the SAME preset system and inspector work for both tiers. Full→Lite is not a separate product; it's a keyword configuration.

→ **[DECISION NEEDED: D1]**

---

#### D2 — Inspector UX tier

What does "COZY-level" mean concretely for a single water shader Inspector?

COZY Weather 3 has a module system (because it IS a multi-system package). KF_WaterKit is one shader. The equivalent is:

**Option A — Preset strip at top + foldout sections** (minimal, ~2 days work)
- Preset dropdown + "Apply" button at the top
- Below: foldout groups (Appearance / Waves / Foam / Performance)
- Matches SW2's inspector philosophy. Easy to build.

**Option B — Tabbed interface: Presets | Appearance | Performance** (moderate, ~4 days)
- Tab 1: curated preset grid with thumbnail previews (visual picking)
- Tab 2: full parameter control
- Tab 3: performance tier selection (Full / Lite) with estimated cost per feature
- This is probably closest to "COZY-level" for a shader product

**Option C — Contextual adaptive inspector** (complex, ~2 weeks)
- Inspector detects selected performance tier and hides parameters irrelevant to that tier
- "Beginner mode" shows only 8 parameters; "Advanced mode" reveals all
- Staggart himself doesn't do this — it's genuinely novel

**Planner recommendation:** **Option B**. It's the minimum that delivers editorial identity (preset thumbnails = the differentiator made visible), without the complexity of Option C. Tab 3 (Performance) is the benchmark transparency made inspectable. This is what makes it feel like a product, not a shader.

→ **[DECISION NEEDED: D2]**

---

#### D3 — Public product name

User liked "WaterKIT". Candidates:

| Name | Feeling | Risk |
|---|---|---|
| **WaterKIT** | Clean, KITforge family, memorable | Generic "Kit" suffix is crowded on AS |
| **FlowKit** | Evokes movement, broader (could do rivers too) | Too abstract |
| **TideKit** | Evokes ocean specifically | Too narrow for lake/river use |
| **WaveKit** | Specific, clean | Missing the "water" word — buyer needs to guess |
| **WaterForge** | Uses FORGE family, stronger | Might imply creation tool, not shader |
| **KIT Water** / **LABS Water** | Label style | Too dry, no personality |

**Planner recommendation:** **WaterKIT**. Clean, KITforge-family read, descriptive. Put it on the table for final lock.

→ **[DECISION NEEDED: D3]**

---

#### D4 — Target benchmark device

A concrete mobile benchmark claim requires a named device + a baseline number. Without a device, "runs on mobile" is not a claim, it's a hope.

**Proposed baseline family:**
- Low-end: **Samsung Galaxy A14** (2023, Dimensity 700) — represents entry Android
- Mid-range: **Samsung Galaxy A54** (2023, Exynos 1380) — target mid-range Android
- High-end: **iPhone 13** or **iPhone 15** — Apple Metal URP baseline

Claim format: `KF Lite — A54: <X>ms | A14: <Y>ms | Nexus iPhone comparison`

**Planner note:** Actual numbers only come from P5 (Benchmark Suite), which runs after P1. But the *device family* must be decided now so P5 is built against the right targets.

→ **[CAN DEFAULT: Samsung A54 mid-range, A14 low-end. Confirm or replace?]**

---

### Preset Library (candidate list — 10 presets)

Each preset = a `.mat` file + a 512×512 preview render + a narrative name that communicates the mood instantly. Names are final store-facing identity.

| # | Name | Mood | Key visual signals |
|---|---|---|---|
| 1 | **Ocean — Tropical** | Clean, bright, Sea of Thieves hero | Cyan-deep gradient, white foam, sparkles, caustics |
| 2 | **Ocean — Stormy** | Dark, dramatic, overcast | Navy-grey, tall foam, strong normals, no sparkles |
| 3 | **Lake — Misty** | Calm, atmospheric, morning | Grey-green, subtle foam, soft refraction, no waves |
| 4 | **Lake — Clear** | Alpine, transparent, summer | Pale blue-green, visible bottom depth, minimal foam |
| 5 | **River — Autumn** | Fast-moving, warm tones | Amber-brown tint, directional foam, river mode |
| 6 | **Arctic — Ice** | Cold, pale, frozen edge | Pale blue, sharp intersection foam, static/slow normals |
| 7 | **Swamp — Dark** | Murky, oppressive, horror-adjacent | Deep green-black, heavy opaque foam, low refraction |
| 8 | **Toon — Cartoon** | Flat stylized, mobile-casual friendly | Hard-edge color bands, thick foam outlines, flat normals |
| 9 | **Lowpoly — Flat** | No normals, faceted mesh style | Solid color, flat shading mode, foam as color only |
| 10 | **Lava** | Special / dramatic | Orange-red, animated foam as embers, emissive glow |

**Total: 10 presets.** Covers the 4 terrain types (ocean, lake, river, special) across the stylization spectrum (realistic-ish → toon → lowpoly). SW3 ships 9 presets — this matches and adds #10 and a second ocean.

---

### Full vs Lite — Feature Split (keyword budget)

**Design rule:** Lite must run on Samsung Galaxy A14 without frame-rate impact. Full targets mid-range and above. Both are in the same shader (D1 Option A). Expensive features are behind keywords.

| Feature | Full | Lite | Keyword |
|---|---|---|---|
| Depth-based color gradient | ✅ | ✅ | — (always on; cheap depth sample) |
| Intersection foam | ✅ | ✅ | — (depth diff; cheap) |
| Normal map waves | ✅ | ✅ | — (UV anim; cheap) |
| Surface foam texture | ✅ | ✅ | — (texture sample; cheap) |
| Translucency | ✅ | ✅ | — (cheap, analytical) |
| Refraction (opaque copy) | ✅ | ❌ | `KF_REFRACTION` |
| Caustics (animated texture) | ✅ | ❌ | `KF_CAUSTICS` |
| Sparkles / sun scatter | ✅ | ❌ | `KF_SPARKLES` |
| Planar Reflections | ✅ | ❌ | `KF_REFLECTIONS` |
| Flat shading (lowpoly mode) | ✅ | ✅ | `KF_FLAT_SHADING` (free — no cost) |
| Emissive foam (lava) | ✅ | ❌ | `KF_EMISSIVE_FOAM` |
| River mode (directional flow) | ✅ | ✅ | `KF_RIVER_MODE` (cheap UV remap) |

**Keyword count: 6 active feature switches.** Max theoretical variants: 2^6 = 64. In practice < 20 after stripping (most are full-only, Lite disables 5 of 6).

**Lite = always**: depth, foam, normal waves, translucency, flat mode, river mode.
**Full adds**: refraction, caustics, sparkles, reflections, emissive foam.

**Lava preset in Lite?** No. Lava requires `KF_EMISSIVE_FOAM` which is Full-only. Lite gets 8 presets, Full gets all 10. This is a valid tiering.

---

### Build Order

**Milestone 0 — Architecture** (before any code)
- Decide D1, D2, D3, D4
- Define shader file structure: folders, naming, assembly definitions
- Define keyword list (locked — adding keywords after is expensive)
- Define preset naming (locked — renaming after store launch = confusion)
- Done when: decisions documented, folder scaffold created, keywords listed in a `KF_WaterKit_Keywords.md`

**Milestone 1 — Shader MVP (P1)** 
- HLSL shader: all Full features active, no keywords yet — just get them all working
- Target: Ocean Tropical preset looks like Sea of Thieves on desktop URP
- No Inspector, no presets — raw material tuning in Unity Inspector
- Done when: "Ocean Tropical" screenshot is indistinguishable from the visual bar in a test scene, running at acceptable framerate on desktop

**Milestone 2 — Full/Lite split (P1 complete)**
- Introduce the 6 keywords, gate features behind them
- Verify SRP Batcher compatibility survives keyword addition (CBUFFER layout must stay consistent)
- Build P5 (Benchmark Suite) here: measure both Full and Lite on target devices
- Done when: Full and Lite are verifiably different in GPU cost on Samsung A54; benchmark table has numbers

**Milestone 3 — Preset Library (P2)**
- Create all 10 preset `.mat` files (8 Lite-compatible, 10 Full)
- Take 512×512 preview renders for each (used in Inspector and store page)
- Done when: all 10 presets load and look correct with no console errors in a clean project

**Milestone 4 — Inspector UX (P3)**
- Build CustomEditor: Tab 1 (preset grid + thumbnails), Tab 2 (full params), Tab 3 (perf toggle)
- Preset thumbnails display in the Inspector grid (128×128 textures embedded as addressables or Resources)
- Tab 3 shows: current variant cost, which keywords are enabled, "Switch to Lite / Full" button
- Done when: user can go from Import → pick preset → working water WITHOUT opening Tab 2 once

**Milestone 5 — Demo Scene (P4)**
- One scene: 5 bodies of water, each using a different preset, real-time demo loop (camera path or cinematic)
- Demonstrates mobile performance in the Play Mode title (show ms counter overlay)
- Done when: 60-second store capture video can be recorded directly from Play Mode

**Milestone 6 — Launch Package (P6)**
- Quick Start guide (1 page: import → pick preset → done in 5 steps) 
- Full reference docs (all parameters, all presets, Full/Lite decision guide)
- Beta seeding: 5–10 developers receive free key, asked to review post 1 week test
- Store page: video (60s), screenshots (6 minimum), description with "no extensions" and benchmark table
- Done when: submission checklist (Unity AS) passes, 3+ beta reviews exist at submission moment

---

### Success Criteria per Milestone

| Milestone | Measurable done-condition |
|---|---|
| M0 | D1–D4 documented, shader scaffold committed, keyword list locked in .md |
| M1 | Screenshot reviewed by user and confirmed "this is the visual bar" |
| M2 | Benchmark table: Lite ≤ 0.8ms on A54, Full ≤ 2ms on A54, both verified with Unity Profiler |
| M3 | All 10 presets load in a clean Unity 6 URP project, zero console errors |
| M4 | Full user flow (Import → Preset selected → water in scene) completed in < 5 minutes without opening any docs |
| M5 | 60-second video captured, shows 5 water types, ms counter visible |
| M6 | Beta: 3+ reviews exist; store submission passes first review round |

---

### Launch Risk Mitigation Plan (from STRESS TEST)

**Trust gap (zero reviews at launch):**
1. Launch candidate list in M6: find 5 indie Unity devs (Discord, Twitter/X, forum). Offer free key in exchange for honest post-testing review. Do NOT offer "exchange for 5-star" — that violates AS terms.
2. Demo scene quality = primary conversion asset. Make it so good that buyers forget to look for reviews.
3. Store video = 60s "from import to 5 beautiful scenes in 5 minutes" — the Water wizard we validated as the unmet need IS the video.
4. Store page copy explicitly says: "Everything included. No extensions. Ever." with a comparison callout vs SW3 pricing.

**SW3 ships "Water wizard" (Scenario 1 mitigation):**
- KF's advantage must be editorial identity (named presets with showcase renders), not just "wizard exists"
- Preset names and preview renders must be the visual brand. They must feel curated, like Distant Lands' scene packs.
- If Staggart ships a wizard: KF is still the product where you pick "Ocean Tropical" and get exactly that. Not presets that need to be understood — presets that are immediately recognizable.

---

### DECISIONS LOG (locked 2026-04-13)

| # | Decision | Choice | Rationale |
|---|---|---|---|
| D1 | Shader architecture | **Single shader + keywords (6 max)** | One codebase, SRP Batcher safe, Full→Lite = keyword config, same Inspector+presets for both tiers |
| D2 | Inspector UX tier | **Option B — 3-tab Inspector (Presets / Appearance / Performance)** | Premium product = no cut corners. Tab 1 = preset grid with thumbnails. Tab 3 = cost visibility. |
| D3 | Public product name | **WaterKIT** | KITforge family read, descriptive, clean |
| D4 | Benchmark device family | **Samsung Galaxy A54 (mid) + Samsung Galaxy A14 (low-end)** | Targets set in M2 success criteria |
| D5 | M4 — Setup Window | **Included in M4** | Materializes "no docs needed". Checks URP config + Unity version minimum, offers Quick Showcase Scene on first import. |
| D6 | Tab 1 — Apply behavior | **Apply Button + confirmation dialog if material was modified** | `EditorUtility.DisplayDialog`: "Applying this preset will overwrite your current settings. Continue?" Unity Undo stack does not cover material property changes. |
| D7 | Tab 2 — Internal structure | **Collapsible sections — required, not stretch** | ~33 params in Full without sections = unreadable. Sections: Appearance / Waves & Flow / Foam / Special Effects / Performance. |
| D8 | Full ↔ Lite toggle behavior | **Preserves all values, only toggles keywords** | Full→Lite hides Full-only params but does NOT reset values. Tab 3 tooltip: "Switching modes hides parameters but does not reset them." Fully reversible. |
| D9 | River flow mechanism | **Both: float2 global direction + optional Flow Map texture slot** | float2 = out-of-the-box for straight rivers. Flow Map = advanced curves/complex flow. Example flow map included in P2. |

### Surviving Assumptions / Open Questions

| # | Assumption or Question | Blocking | Phase |
|---|---|---|---|
| Q5 | Lava preset = Full-only (requires `KF_EMISSIVE_FOAM`). Assumed OUT of Lite. Unconfirmed. | Non-blocking | M3 |
| Q6 | Beta seeding: free key strategy + who to contact — to be defined at M6 | Non-blocking until M6 | M6 |
| Q7 | Duration estimates per milestone — not defined. Executor establishes in M0. | Non-blocking | M0 |
| Q8 | M1 visual reference: **LOCKED 2026-04-13**. Sea of Thieves water screenshot provided by user. Visual targets: (1) cian brillante superficial #35E5D5→teal oscuro #030F14 en profundidad, (2) destellos dorados #FFD700 en agua poco profunda (sparkles/caustics — firma SoT), (3) foam blanca suave en crestas de ola, (4) translucencia en canto de olas, (5) refracción de fondo visible. M1 done = screenshot de Ocean Tropical aprobado contra esta referencia. | **RESOLVED** | M1 |
