# Launch Playbook — KITforge labs
Status: DRAFT
Created: 2026-04-11
Source: BrandStrategy, PricingStrategy, KF_StorePrepRules, article analysis, Unity submission guidelines

---

## 0. When This Document Applies

This playbook activates when a product reaches **STORE_PREP** phase (QA passed, package clean, README final). It covers everything from publisher account readiness to post-launch monitoring.

This is a chronological checklist. Work it top to bottom.

---

## 1. Publisher Account Readiness (one-time, before first product)

Must be done ONCE. Blocks all product launches until complete.

| Step | Detail | Status |
|------|--------|--------|
| Create Unity Publisher account | https://publisher.assetstore.unity.com | ☐ |
| Complete publisher profile | Studio name: KITforge labs. Logo, description, website link. | ☐ |
| Set payment information | Bank details for 70% revenue share payouts. $200 minimum threshold. | ☐ |
| Verify publisher identity | Unity may require identity verification. Allow 5–10 business days. | ☐ |
| Agree to publisher terms | Read and accept the Asset Store Provider Agreement. | ☐ |
| Prepare support email | Dedicated support address (not personal). e.g. support@kitforgelabs.com | ☐ |
| Prepare brand assets | Publisher logo (512x512 PNG min), banner if required. Use web brand identity. | ☐ |

**Timeline note:** Account approval can take 1–2 weeks. Start this process well before the first product is ready to submit.

---

## 2. Pre-Submission Checklist (per product)

Everything here must be TRUE before submitting to Unity review.

### 2.1 Product completeness

| Check | Source rule | Done |
|-------|------------|------|
| All Brief checkpoints at ✅ | Brief.md | ☐ |
| QA checklist fully passed | KF_QARules | ☐ |
| Zero console errors on import in clean Unity 6 project | KF_QARules §Compilation | ☐ |
| Zero console warnings (or documented intentional exceptions) | KF_QARules §Compilation | ☐ |
| README.md present with real content | BOOTSTRAP R8 | ☐ |
| Demo scene loads and works end-to-end | BOOTSTRAP R8 | ☐ |
| Third-Party Notices.txt present (if applicable) | BOOTSTRAP R8 | ☐ |
| Package contains ONLY `KF_<Slug>/` — no `_Develop/`, no `_Lab/` | KF_QARules §Package | ☐ |
| No missing references, scripts, or materials in package | KF_QARules §Package | ☐ |
| Domain reload survival tested | KF_QARules §Editor | ☐ |

### 2.2 Store page assets ready

| Asset | Spec | Source rule | Done |
|-------|------|------------|------|
| Title | ≤60 chars. Formula: `[Name] — [Benefit]` | KF_StorePrepRules §1 | ☐ |
| Short description | ≤100 chars. `[What] [For whom] [Without what pain]` | KF_StorePrepRules §2 | ☐ |
| Long description | Problem → Features → Quick Start → Compatibility → Support | KF_StorePrepRules §3 | ☐ |
| Hero screenshot (1920×1080 PNG) | Tool in action, most visual result | KF_StorePrepRules §4 | ☐ |
| Feature screenshots (3–4, 1920×1080 PNG) | Each shows one capability clearly | KF_StorePrepRules §4 | ☐ |
| Quick-start infographic (optional, recommended) | 3 steps from import to first result | KF_StorePrepRules §4 | ☐ |
| Key image (thumbnail) | Required by Unity. 420×280 or as specified. | Unity guidelines | ☐ |
| Category selected | Tools > Utilities / Tools > Editor Extensions / Shaders > ... | BrandStrategy §4 | ☐ |
| Price set | Per PricingStrategy framework | PricingStrategy | ☐ |
| Tags / keywords set | See §7 SEO below | — | ☐ |

### 2.3 Brand compliance

| Check | Source rule | Done |
|-------|------------|------|
| Store copy follows brand voice rules | BrandStrategy §2 | ☐ |
| No superlatives, no hype, no competitor names | BrandStrategy §6 | ☐ |
| Description leads with the problem, not the product | BrandStrategy §2 writing rule 1 | ☐ |
| AI usage declared per Unity policy | BrandStrategy §5 | ☐ |
| Price matches PricingStrategy tier framework | PricingStrategy §3 | ☐ |

---

## 3. Review Seeding (2–4 weeks before public launch)

Goal: Have 3–5 honest reviews live by launch day. Zero reviews on a paid product is a conversion killer.

### 3.1 Voucher allocation

From PricingStrategy §6 — allocate 6–8 of the 16 annual vouchers for pre-launch seeding.

| Target | How to find them | What to send | Codes |
|--------|-----------------|--------------|-------|
| Unity devs who publicly discuss the problem your tool solves | Reddit r/Unity3D, r/gamedev threads. Unity forums. X/Twitter searches. | DM: "We built a tool for [problem]. Would you try it and share honest feedback?" + voucher | 3–4 |
| YouTubers / content creators covering Unity tools | Code Monkey, Git-Amend, Tarodev, Sasquatch B Studios, smaller channels with engaged audiences | Email: professional pitch. Product key + screenshots + quick-start. No review requirement. | 2–3 |
| Community contacts or people who gave input during development | Contact form on kitforgelabs.com, Discord communities | Personal message: "You helped shape this. Here's a key. Honest feedback welcome." | 1–2 |

### 3.2 Outreach rules

- **Never ask for a specific rating.** Ask for "honest feedback" or "honest review if you find it useful."
- **Send the pitch personally.** No mass mail. Reference something specific about the person's work.
- **Include everything they need to try it:** voucher code, quick-start link, what Unity version to use.
- **Follow up once** (1 week later) if no response. Then stop. Respect their time.
- **Track outreach** in a simple log:

```
| Date | Person | Channel | Product | Code sent | Response | Review posted |
|------|--------|---------|---------|-----------|----------|---------------|
```

---

## 4. Unity Submission Process

### 4.1 Timeline expectations

| Step | Expected duration |
|------|------------------|
| Prepare and upload package draft | 1–2 days |
| Fill store page (title, description, screenshots, metadata) | 1 day |
| Submit for review | Click "Submit" |
| Unity review queue | 5–15 business days (can vary) |
| Feedback / revision cycle (if rejected) | 2–5 days per round |
| Published | After approval |

**Total realistic timeline: 2–4 weeks from submission to public.** Plan accordingly.

### 4.2 Submission checklist (Unity side)

| Field | What to enter |
|-------|--------------|
| Package file | .unitypackage exported from Unity. Contains ONLY `KF_<Slug>/` folder tree. |
| Unity version | Minimum version the package supports. Use Unity 6.0 (or current LTS). |
| Render pipeline | Declare: Any / URP / HDRP. For tools: "Any". For shaders: specific pipeline. |
| Dependencies | List any required packages (e.g., URP, TextMeshPro). Prefer zero dependencies. |
| AI declaration | Declare AI usage per Unity policy. See BrandStrategy §5. |
| Documentation format | Include URL to web docs (kitforgelabs.com) + offline README.md in package. |

### 4.3 Common rejection reasons (prevent these)

| Reason | How we prevent it |
|--------|------------------|
| Files outside root folder | KF_QARules §Package — only `KF_<Slug>/` ships |
| Missing namespace declarations | KF_NamingConventions §1 — everything namespaced |
| Console errors on import | KF_QARules §Compilation — tested in clean project |
| Missing documentation | BOOTSTRAP R8 — README.md mandatory |
| Screenshots don't match product | Screenshots taken from actual product, not mockups |
| Duplicate GUIDs with other packages | KF_QARules §Package — check for GUID conflicts |

---

## 5. Launch Week Actions

### Day 0 — Product goes live

| Action | Detail |
|--------|--------|
| Verify store page is live and correct | Check title, screenshots, description rendering. Fix typos immediately. |
| Test purchase flow | Use a second Unity account if possible. Verify download, import, first use. |
| Announce on X/Twitter | Short post from @kitforgelabs. Problem statement + link. Match brand voice. No hype. |
| Post on Reddit (r/Unity3D, r/gamedev) | Honest post: "We built X because Y. Here's what it does." Follow subreddit rules. No spam. |
| Post on Unity forums | Asset Store subforum. Brief description + link. |
| Update kitforgelabs.com | Product page or link to store listing. |

### Days 1–7

| Action | Frequency |
|--------|-----------|
| Monitor reviews | Daily. Respond to every review (positive: thank. negative: investigate + respond publicly). |
| Monitor support email | Daily. Target < 24h first response. |
| Monitor Reddit/forums for mentions | Daily. Respond helpfully. Don't be defensive. |
| Fix any reported bugs | ASAP. A bug fix in week 1 shows responsiveness. |
| Thank voucher recipients who posted reviews | Personal message. Genuine. |

### Days 8–30

| Action | Frequency |
|--------|-----------|
| Continue review monitoring | Every 2–3 days |
| Collect feedback themes | Weekly. What do users like? What's missing? What's confusing? |
| Plan first update (if feedback warrants) | After 2+ weeks of data. Don't rush patches for non-critical issues. |
| Write a "first month" retrospective (internal) | What sold, what didn't, what feedback says. Input for next product decisions. |

---

## 6. Post-Launch Maintenance Cadence

Products don't end at launch. A dead product (no updates for 12+ months) signals abandonment.

| Cadence | Action |
|---------|--------|
| **Monthly (months 1–3)** | Bug fixes if reported. Minor UX improvements from feedback. |
| **Quarterly (months 4–12)** | Compatibility check with latest Unity LTS. Update if needed. |
| **Per Unity LTS release** | Test in new LTS. Update minimum version if breaking changes. Document in changelog. |
| **Annually** | Review pricing. Evaluate expansion features. Decide: maintain, expand, or sunset. |

### Update changelog rule

Every public update gets a changelog entry. Users must see what changed. Format:

```
## [1.1.0] — 2026-MM-DD
### Added
- Feature X

### Fixed
- Bug Y reported by user Z

### Changed
- Improved behavior of W
```

---

## 7. SEO and Discoverability

With 0€ marketing budget, organic discoverability is everything.

### 7.1 Store page SEO

Unity Asset Store has internal search. Optimize for it:

| Element | Strategy |
|---------|----------|
| **Title** | Include the primary keyword naturally. "Hierarchy Kit" is searchable. "HK Pro Max Ultra" is not. |
| **Tags** | Use all available tag slots. Mix specific ("hierarchy colors", "hierarchy icons") with broad ("editor tool", "workflow", "productivity"). |
| **Description keywords** | Naturally include terms users search for. "hierarchy", "organize", "labels", "scene structure", "editor extension". Don't keyword-stuff. |
| **Category** | Choose the most specific correct category. Broader category = more competition. |

### 7.2 External discoverability (0€ budget)

| Channel | Action | Effort | Expected impact |
|---------|--------|--------|-----------------|
| **Reddit** | Genuine posts in r/Unity3D, r/gamedev. Help in threads about the problem you solve. Link naturally, not spam. | Medium | High — Reddit recommendations drive significant Asset Store purchases |
| **X/Twitter** | Regular posts from @kitforgelabs. Dev process, tips, tool demos. Engage with Unity community. | Low-medium | Medium — builds presence over time |
| **YouTube** | Short demo videos (60–90s). Can be unlisted and linked from store page. Or approach creators. | Medium-high | High — video demos dramatically increase conversion |
| **Unity Forums** | Post in Asset Store subforum. Respond to relevant threads. | Low | Low-medium — less traffic than Reddit but indexed well |
| **kitforgelabs.com** | Product pages with more detail than store allows. Blog posts about the problems you solve (not ads for your tools). | Medium | Medium-long term — SEO builds over months |
| **GitHub presence** | If free tier exists, host it on GitHub. Stars and forks = visibility. | Low | Medium — devs discover tools via GitHub trending |

### 7.3 Content ideas (organic, brand-voice-aligned)

Content that helps = content that sells. Not ads. Not promos. Actual value.

| Content type | Example | Where |
|-------------|---------|-------|
| "Problem + solution" post | "5 things that quietly slow your Unity project down" | Blog, Reddit |
| Tool demo GIF/video | 15-second GIF of tool in action | X, Reddit, store page |
| Dev log | "How we designed X to not break your project" | Blog, X |
| Tip thread | "3 Unity editor shortcuts most devs don't know" | X, Reddit |
| Comparison (honest) | "When to use X vs doing it manually" (never name competitors) | Blog |

---

## 8. Demo Builds (for visual products)

Applies to shader packs and visual tools. NOT needed for editor-only tools.

### When to provide a demo build

| Product type | Demo build? | Format |
|-------------|-------------|--------|
| Editor tools (HierarchyKit, ProjectAudit) | No — screenshots and GIFs are sufficient | — |
| Shader/VFX packs (MicroFX) | Yes — buyers need to see real rendering | WebGL build hosted on itch.io or kitforgelabs.com |
| Runtime tools (DebugOverlay) | Optional but helpful | WebGL or APK |

### Demo build rules

- Must represent the actual product accurately (same shaders, same settings)
- Include a visible "Made with KITforge labs — [Product Name]" watermark
- Host on a platform that's stable and fast (itch.io WebGL, kitforgelabs.com)
- Link from the store page description
- Test the demo build on mobile browser (for shader products targeting mobile)

---

## 9. Launch Timing

### When to launch

| Do | Don't |
|----|-------|
| Tuesday–Thursday (higher store traffic during work week) | Weekend (lower traffic, slower support response) |
| 2+ weeks before a known Unity sale event | During or just before a Unity sale (your full-price product competes with discounted ones) |
| When you have 3+ pre-launch reviews ready to post | When zero reviews exist (empty review section kills conversion) |
| After a major Unity version release has stabilized | On the day of a new Unity release (users are dealing with upgrade issues, not shopping) |

### When NOT to launch

- December 20–January 5 (holiday dead zone for business purchases)
- Same week as a major Unity conference (GDC, Unite) — attention is elsewhere
- If QA still has open blockers — never. No exceptions.

---

## 10. First-Month Retrospective Template

After 30 days, write a brief internal retrospective in `_Develop/KF_<Slug>_dev/Retrospective_v1.md`:

```
## 30-Day Retrospective — KF_<Slug>

### Numbers
- Units sold: X
- Revenue (gross): $X
- Reviews: X (avg rating: X.X)
- Support tickets: X
- Refunds: X

### What went well
- 

### What users complained about
-

### Most requested feature
-

### What we'd do differently
-

### Decision: next action
☐ Maintain as-is (no changes needed)
☐ Ship update addressing feedback
☐ Plan expansion features
☐ Adjust pricing
☐ Sunset (if product clearly failed)
```

---

## Approval checklist

- [x] Publisher account setup documented
- [x] Pre-submission checklist comprehensive (product + store + brand)
- [x] Review seeding strategy with voucher allocation
- [x] Unity submission process with timeline and rejection prevention
- [x] Launch week day-by-day actions
- [x] Post-launch maintenance cadence
- [x] SEO and discoverability strategy (0€ budget)
- [x] Demo build guidelines for visual products
- [x] Launch timing guidance
- [x] Retrospective template
- [ ] Team review and approval
