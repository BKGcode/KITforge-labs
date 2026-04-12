# BOOTSTRAP — KITforge labs
v1.0.0 | Factory v1 | 2026-04-11
AI-ONLY DOCUMENT. No human reader. Dense instruction set.

---

## EXEC PROTOCOL (strict order)

```
1. READ  → tools_lab_env.json           (activeProduct, phase per product)
2. READ  → _dev/{slug}/Brief.md         (if activeProduct != null)
3. READ  → _dev/{slug}/ChangeLog.md     (last 30 lines, if exists)
4. LOAD  → global skills (map below)
5. LOAD  → KF skills (map below)
6. OUTPUT → ready state block
7. WAIT  → user instruction
```

If `activeProduct == null` → ask user which product to activate before continuing.

---

## SKILL MAP

### Global (ecosystem — paths exact)
```
ALWAYS:           c:\Users\Joan\.claude\skills\unity-rules\SKILL.md
TOOLS product:    c:\Users\Joan\.claude\skills\unity-tools-skill\SKILL.md
SHADER product:   c:\Users\Joan\.claude\skills\urp-shader-skill\SKILL.md
CODE REVIEW:      c:\Users\Joan\.claude\skills\code-doctor-skill\SKILL.md   ← activate during any code generation or review
COMPLEX tier:     c:\Users\Joan\.claude\skills\design-patterns-skill\SKILL.md
```

### KITforge-specific (paths relative to ROOT)
```
ROOT = d:\_AI_PROJECTS_\KITforge Labs\Unity\KITforge-labs\KITforg_labs\Assets\Plugins\KITforge Labs\_Develop\Skills

ALWAYS:                {ROOT}/KF_NamingConventions.md
ALWAYS:                {ROOT}/KF_UIBible.md
BRIEF phase:           {ROOT}/KF_BriefRules.md
BUILD/QA phase:        {ROOT}/KF_QARules.md
STORE_PREP:            {ROOT}/KF_StorePrepRules.md
BRIEF + STORE_PREP:    {ROOT}/KF_MarketRules.md
```

---

## READY STATE OUTPUT FORMAT
```
=== KITforge labs ===
Product : KF_{slug} — {name}
Phase   : {PHASE}
Last CP : {last ChangeLog entry or "pre-build"}
Skills  : unity-rules ✓ | unity-tools-skill ✓ | code-doctor ✓ | KF_Naming ✓
===
```

---

## HARD RULES (enforce always, no exceptions)
```
R1: No código antes de Architecture.md APPROVED (Moderate/Complex tier only. Simple tier: Brief APPROVED = enough gate)
R2: No BUILD sin Brief.md APPROVED
R3: _Develop/ = INTERNAL ONLY. Never included in shipped packages.
R4: KF_{slug}/ = SHIPPING ONLY. Never dev files here.
R5: Namespace = KITforgeLabs.Editor.{Slug} | KITforgeLabs.Runtime.{Slug}
R6: No cross-product deps without explicit _Shared/ module + doc justification
R7: Zero console errors at idle before QA PASSED
R8: Every product ships: README.md + Demo scene + Third-Party Notices.txt (if needed)
R9: NEVER build product code without explicit user request. Building the factory ≠ building a product.
R10: Read KF_Learnings.md at session start — avoid repeating past mistakes.
```

---

## LIFECYCLE
```
BACKLOG → BRIEF → ARCHITECTURE → BUILD → QA → STORE_PREP → PUBLISHED → MAINTAINED
         Brief.APPROVED  Arch.APPROVED  all CPs ✅  TestPlan PASSED
```

---

## FILE INDEX (all paths absolute)
```
JSON:        d:\_AI_PROJECTS_\KITforge Labs\Unity\KITforge-labs\KITforg_labs\Assets\Settings\KITforgeLabs\tools_lab_env.json
ROOT:        d:\_AI_PROJECTS_\KITforge Labs\Unity\KITforge-labs\KITforg_labs\Assets\Plugins\KITforge Labs
_Develop:    {ROOT}/_Develop
Skills:      {ROOT}/_Develop/Skills/
Templates:   {ROOT}/_Develop/Templates/
Tests:       {ROOT}/_Develop/Tests/
Research:    {ROOT}/_Lab/Research/
_dev/slug:   {ROOT}/_Develop/KF_{slug}_dev/
Product:     {ROOT}/KF_{slug}/
```

## RESEARCH FILES (load when writing Briefs or scoring products)
```
{ROOT}/_Lab/Research/ProductBacklog.md      ← 15 products, scores, product cards
{ROOT}/_Lab/Research/MarketReference.md     ← market signals, competitor data
{ROOT}/_Lab/Research/UsefulReferences.md    ← API links, GitHub repos, pricing
/memories/repo/kitforge-tools-lab-core-reference.md
/memories/repo/kitforge-tools-market-notes.md
```

## BUSINESS FILES (load during STORE_PREP or when writing positioning / pricing)
```
{ROOT}/_Lab/Business/_index.md              ← navigation map + key decisions table
{ROOT}/_Lab/Business/BrandStrategy.md       ← brand DNA, voice rules, customer segments
{ROOT}/_Lab/Business/PricingStrategy.md     ← pricing tiers, freemium framework, Wave 1 prices
{ROOT}/_Lab/Business/LaunchPlaybook.md      ← launch process, submission checklist, seeding
{ROOT}/_Lab/Business/SupportStrategy.md     ← SLA, channels, review protocol, anti-patterns
```

## _KFL COMMANDS AVAILABLE
```
/KFL_start          → this bootstrap protocol
/KFL_status         → lab overview, all products phases
/KFL_brief [slug]   → create/fill/approve Brief
/KFL_arch           → create Architecture doc (needs Brief APPROVED)
/KFL_build          → start BUILD, load skills, show checkpoint progress
/KFL_checkpoint     → log completed checkpoint in ChangeLog
/KFL_qa             → execute QA protocol
/KFL_phase [slug]   → advance product phase in lifecycle
/KFL_close          → close session: writes ChangeLog entry + outputs plain-text handoff block for next session
```

NOTE: Run /KFL_close at the END of every session. The output handoff block is pasted as the first message of the next session before running /KFL_start.
