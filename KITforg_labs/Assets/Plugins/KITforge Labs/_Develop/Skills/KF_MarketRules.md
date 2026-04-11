# KF_MarketRules — Commercial & Brand Rules
Scope: KITforge labs only
Last updated: 2026-04-11
Source: BrandStrategy, PricingStrategy, LaunchPlaybook, SupportStrategy
Load when: BRIEF phase (pricing, segmentation) + STORE_PREP phase (all sections)

This skill contains ACTIONABLE RULES only. For context, rationale, and data → see `_Lab/Business/`.

---

## 1. Brand Voice Rules

Apply to ALL public copy: store pages, README files, documentation, social media, review responses.

### ALWAYS
- Lead with the problem, not the product. First line = what hurts. Second line = how this helps.
- Use plain verbs: find, clean, check, remove, show, save, organize.
- One promise per product. If the store page needs two paragraphs to explain what it does, the scope is wrong.
- Acknowledge limits honestly. If the tool doesn't do something, say so.
- English for all public copy.

### NEVER
- Never use superlatives: best, most powerful, revolutionary, ultimate, essential, game-changing.
- Never use corporate language: leverage, empower, unlock, seamless, enterprise-grade.
- Never name competitors directly in public copy.
- Never promise features that are planned but not shipped.
- Never use urgency tactics: "limited time", "only X left", "price going up".
- Never make AI a selling point or marketing angle.

### Vocabulary guide (use → avoid)
| Use | Avoid |
|-----|-------|
| focused | powerful |
| practical | professional-grade |
| useful | essential |
| clear scope | comprehensive |
| day-to-day | mission-critical |
| reduces effort | saves hours |
| finds / shows / cleans | detects / eliminates / eradicates |
| works with | seamlessly integrates |

---

## 2. Customer Segment Rules

Three segments. Each has different priorities. Write for the actual segment, not a generic "Unity developer".

| Segment | Trigger pain | Evaluation priority | Price sensitivity |
|---------|-------------|---------------------|-------------------|
| **Studios** | Process issues multiply across the team | Reliability, source code, non-intrusiveness | Low-moderate ($40–60) |
| **Solo Professionals** | Repeated tasks eat focus and flow state | Fast setup, native feel, clear docs | Moderate ($15–35) |
| **Prototype Teams** | Speed matters, can't afford workflow debt | Import → use immediately, reusable | High (free or <$20) |

KITforge does NOT target students or hobbyists. The brand speaks to people doing production work.

---

## 3. Pricing Rules

### Mandatory price floors and ceilings (Wave 1)

| Product tier | Price range |
|-------------|-------------|
| Simple | $15–25 |
| Moderate | $25–40 |
| Complex | $35–55 |

- Never price below $10 (signals hobby project).
- Never price above $55 until brand is established (Wave 2+).
- If price feels wrong → change the base price. Never run a "permanent sale".

### Freemium gating

Use freemium ONLY when ALL four conditions are true:
1. The free version is genuinely useful (not a crippled demo)
2. The paid upgrade is a natural "more" extension
3. The free version drives discovery
4. Support burden for free users stays low

If any condition fails → paid only.

### Wave 1 pricing (approved)

| Product | Model | Price |
|---------|-------|-------|
| `KF_HierarchyKit` | Freemium | $20 (paid tier) |
| `KF_ProjectAudit` | Paid | $30 |
| `KF_ScreenCapture` | Paid | $18 |
| `KF_SceneBackup` | Paid | $15 |

### Discount rules

- No discounts in the first month after launch.
- First sale: 30% max. Never train buyers to wait for deep discounts.
- No product below $10 after discount.
- Opt into Unity seasonal sales selectively — not every sale.

---

## 4. Voucher Rules (16 vouchers/year per product)

Standard allocation per product launch:

| Purpose | Codes | Rule |
|---------|-------|------|
| Pre-launch review seeding | 6–8 | Send 2–4 weeks before launch. Target users who have the exact problem the tool solves. |
| Influencer/YouTuber outreach | 2–3 | Personal pitch. No review requirement. |
| Community give-back | 2–3 | Reddit, forums, engaged contacts. |
| Reserve | 1–2 | Unexpected opportunities, bug reporter rewards. |

**NEVER** give a voucher in exchange for a specific rating.
**DO** say: "If you find it useful, an honest review would help us a lot."

---

## 5. Launch Rules

### Pre-launch gate (all must be TRUE before submitting to Unity)

- Brief checkpoints: all ✅
- QA checklist: PASSED
- Zero console errors in clean Unity 6 project
- README.md with real content
- Demo scene loads end-to-end
- Store page assets ready (title, description, 4+ screenshots, pricing)
- Brand compliance checked (no superlatives, problem-first copy, AI declared)
- 3+ pre-launch reviews ready (voucher seeding complete)

### Launch timing rules

- Launch Tuesday–Thursday (higher store traffic)
- Never launch in the first month of a Unity sale (your full-price product competes with discounted ones)
- Never launch with zero reviews
- Never launch with open QA blockers

### Post-launch week

- Monitor reviews daily. Respond to EVERY review within 48 hours.
- Monitor support email daily. Target < 24h first response.
- Fix S1/S2 bugs within 7–14 days and notify reporter proactively.

---

## 6. Support Rules

### Channels
- **Primary:** hello@kitforgelabs.com
- **Secondary:** Unity Publisher Forums (public responses)
- **Never:** Discord as the only support channel

### SLA (non-negotiable)
| Ticket type | First response | Resolution |
|------------|---------------|------------|
| S1 Critical (compile error, import broken) | 24h | Fix shipped ≤7 days |
| S2 Major (core feature broken) | 48h | Fix shipped ≤14 days |
| S3 Minor / Usage question | 72h | Resolved in reply or docs updated |
| Feature request | 72h | Acknowledged + logged |
| Review response | 48h | Public, always |

### Review response rules
- Every review gets a public response. No exceptions.
- Never be defensive. Never redirect to private channel to avoid public response.
- Never argue with reviewer's subjective experience.
- Template for negative/valid bug: "We've reproduced this — fix coming in next update."
- Template for negative/scope mismatch: "Apologies for the confusion. [Clarify scope]. We'll update the documentation."

### Absolute prohibitions
- Never require 5-star review for support
- Never ignore a negative review
- Never delete or flag legitimate complaints
- Never close support without resolution or honest explanation

### FAQ rule
If the same question is asked by two different users → add it to the product FAQ immediately.

---

## 7. AI Policy Rules

- Declare AI usage per Unity submission policy. Accurately and completely.
- Treat it as normal tooling. "We use modern development tools including AI-assisted testing."
- Never make AI a selling point ("AI-powered", "AI-built").
- Never hide usage beyond what policy requires.
- All shipped code must be readable, well-named, idiomatic C# regardless of how it was generated.

---

## 8. Anti-Pattern Checklist (run before any public output)

Before delivering any store page copy, README, or public communication, verify:

- [ ] No superlatives in the text?
- [ ] Leads with the problem, not the product?
- [ ] No competitor names mentioned?
- [ ] No promised unshipped features?
- [ ] Price matches tier framework?
- [ ] AI usage declared if required?
- [ ] Support channel is email, not Discord-only?
- [ ] Review response ready if product is live?
