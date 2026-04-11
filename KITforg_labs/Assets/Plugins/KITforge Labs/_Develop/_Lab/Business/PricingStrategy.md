# Pricing Strategy — KITforge labs
Status: DRAFT
Created: 2026-04-11
Source: Competitor data (UsefulReferences, MarketReference), article analysis, BrandStrategy segments

---

## 1. Revenue Model

### Unity Asset Store economics

| Factor | Value |
|--------|-------|
| Publisher revenue share | 70% of net sale |
| Payment threshold | $200 minimum payout |
| Refund policy | Unity handles. Rare but possible. |
| Sale events | Unity runs seasonal sales (publisher opts in per product, sets discount %) |
| Vouchers | Up to 16 free voucher codes per product per year |
| Price floor | No official minimum, but < $10 signals low value |

### KITforge revenue target

Not projecting revenue yet. The goal for Wave 1 is:
1. Establish the publisher account with rated products
2. Collect honest reviews (target: 10+ per product in first 6 months)
3. Validate that the product/market fit and brand voice work
4. Generate enough revenue to clear the $200 payout threshold consistently

Revenue optimization comes after brand establishment.

---

## 2. Market Pricing Map

### Editor tools — what the market charges

| Product | Type | Price | Rating | Reviews | Notes |
|---------|------|-------|--------|---------|-------|
| Odin Inspector | Inspector mega-suite | $65 | 4.9 | 2847 | Price anchor for "premium tools" |
| vHierarchy 2 | Hierarchy enhancement | Free + paid | 4.9 | — | Freemium. Direct competitor to KF_HierarchyKit |
| vInspector 2 | Inspector enhancement | Free + paid | 4.8 | — | Freemium. Direct competitor to KF_InspectorKit |
| Hot Reload | Workflow (code reload) | ~$80 | 4.6 | 278 | High price justified by massive daily time savings |
| Editor Console Pro | Enhanced console | $30 | 4.7 | 642 | Mid-price utility. Basic but essential tool. |
| Easy Save | Serialization | $55 | 4.8 | 1035 | Infrastructure tool. Long lifecycle. |
| Feel | Feedback/juice framework | $50 | 4.9 | 800+ | Gold standard of documentation and polish |
| A* Pathfinding Pro | Pathfinding | $100 | 4.7 | 828 | Specialized, deep. Highest price bracket. |

### Shader/VFX packs — what the market charges

| Product | Type | Price | Rating | Reviews |
|---------|------|-------|--------|---------|
| Beautify 3 | Post-processing suite | $50 | 4.8 | 420 |
| Toony Colors Pro 2 | Toon shading | $65 | 5.0 | 547 |
| Flat Kit | Stylized shading | $50 | 5.0 | 198 |
| Highlight Plus 2 | Object highlight FX | $35 | 5.0 | 176 |
| ProPixelizer | Pixel art post-FX | $45 | 5.0 | 118 |
| Volumetric Fog & Mist 2 | Fog VFX | $50 | 5.0 | 240 |

### Price bands observed

| Band | Range | What lives here | Buyer expectation |
|------|-------|-----------------|-------------------|
| **Budget** | $10–20 | Single-purpose utilities, small packs | Works immediately. No setup. |
| **Mid** | $25–45 | Focused tools, shader packs, quality utilities | Polished UX, demo scene, docs. |
| **Premium** | $50–80 | Feature-rich tools, proven studios, deep systems | Comprehensive. Regular updates. Community. |
| **Pro** | $80–150 | Infrastructure tools, frameworks, deep specializations | "Pays for itself." Mature product. |

---

## 3. Pricing Framework for KITforge

### Principle: Price by value delivered, not by effort invested

A tool that saves 5 minutes per day is worth more than a tool that took 3 months to build but is used once a year.

### Tier-to-price mapping

| Product tier | Price range | Rationale |
|-------------|-------------|-----------|
| **Simple** (1 class, fast build) | $15–25 | Budget-to-mid band. Low barrier, impulse-buy territory. |
| **Moderate** (2-3 classes, complex UI) | $25–40 | Mid band. Must justify with clear polish and demo. |
| **Complex** (4+ classes, deep analysis, shaders) | $35–55 | Mid-to-premium floor. Must feel professional. |

### Why NOT go lower

- Below $10 signals "hobby project" or disposable tool
- Below $15 makes the $200 payout threshold a moving target (need 14+ sales to cash out)
- Low price invites less scrutiny upfront but more disappointment if quality isn't high — the "it's $5 but it doesn't even work" review

### Why NOT go higher (yet)

- New publisher, zero track record, zero reviews
- Premium pricing requires proven trust (established brands command premium)
- Higher price → higher expectations → bigger support burden
- KITforge values: "fair in scope and pricing"

### The trust ladder

```
Wave 1: $15–30 range     → Prove quality. Earn reviews. Build brand recognition.
Wave 2: $25–45 range     → Leverage reputation. Slightly more complex products.
Wave 3: $35–55+ range    → Established publisher. Premium justified by track record.
```

Price goes up as trust goes up. Not before.

---

## 4. Freemium Decision Framework

### When to offer a free tier

Use freemium ONLY when ALL of these are true:

1. **The free version is genuinely useful** — not a crippled demo
2. **The paid upgrade is a natural "more" extension** — not a paywall on basics
3. **The free version drives discovery** — it makes people find KITforge and consider paid products
4. **Support burden stays low** — free users won't flood support with demands

### When to go paid-only

When ANY of these are true:

1. **The core function can't be meaningfully split** — stripping features would make the free version an ad, not a tool
2. **The product targets studios** — studios expect to pay for tools and distrust free tools
3. **The product involves risk** (cleanup, deletion, validation) — free tools that break projects destroy reputation
4. **Support for free users would eat all capacity**

### Per-product recommendation (Wave 1)

| Product | Model | Rationale |
|---------|-------|-----------|
| `KF_HierarchyKit` | **Freemium** | Core separators/colors free → drives discovery. Automation + export = paid. Matches vHierarchy proven model. |
| `KF_ProjectAudit` | **Paid** | Risk-bearing tool. Free version with wrong results = guaranteed bad review. Studios expect to pay for quality assurance tools. |
| `KF_ScreenCapture` | **Paid** | Hard to split meaningful free/paid. Single screenshot free + GIF/batch paid feels artificial. Better to price low ($15–20) and be honest. |

---

## 5. Discount and Sales Strategy

### Unity seasonal sales

Unity runs 2-3 major sales per year (typically summer, Black Friday/winter, spring). Publishers opt in per product and set discount %.

**Rules for KITforge:**

| Rule | Detail |
|------|--------|
| Never discount on launch month | First month = full price establishes anchor |
| First sale: 30% max | Don't train buyers to wait for deeper discounts |
| No product under $10 after discount | Preserve value perception |
| No "permanent sale" | If a price feels wrong, change the base price instead |
| Opt into major Unity sales selectively | Not every sale. Scarcity of discounts increases perceived value. |

### Discount ramp over product lifecycle

| Age | Max discount | Frequency |
|-----|-------------|-----------|
| 0–3 months | 0% | No discounts |
| 3–6 months | 20–30% | 1 sale max |
| 6–12 months | 30–40% | 2 sales max |
| 12+ months | Up to 50% | Major seasonal only |

---

## 6. Voucher Strategy

Unity allows **16 free voucher codes per product per year**. These are the most valuable marketing tool at 0€ budget.

### Allocation framework

| Purpose | Codes | When to use | Expected return |
|---------|-------|-------------|-----------------|
| **Pre-launch reviews** | 6–8 | 2–4 weeks before public launch | Honest reviews from real users. Target: 3–5 reviews before day 1. |
| **Influencer/YouTuber outreach** | 3–4 | Around launch or major update | Coverage, backlinks, "recommended by" credibility |
| **Community give-back** | 2–3 | Reddit threads, Unity forums, Twitter engagement | Organic word-of-mouth, community goodwill |
| **Reserve** | 1–2 | Keep unused for unexpected opportunities | Bug reporter reward, key community member, cross-promotion |

### Voucher rules

- **Never give a voucher in exchange for a specific rating.** This violates trust and Unity policy.
- **Do ask:** "If you find it useful, an honest review would help us a lot."
- **Target real users:** prioritize people who have the exact problem the tool solves. They're more likely to give detailed, genuine reviews.
- **Track who received vouchers:** maintain a simple list (name, channel, date, product). Don't lose track.

---

## 7. Bundle / Cross-Sell Strategy

### Not yet — but planned

Bundles make sense after 3+ products exist. Premature bundling sends the wrong signal ("buy more stuff") before the first product has proven itself.

### When to introduce bundles

- After Wave 1 ships (3 products minimum)
- Only if products share an audience but solve different problems
- Bundle price = 15–25% savings vs buying separately
- Never force bundles. Each product must stand alone.

### Cross-sell via documentation

The cheapest cross-sell: mentioning other KITforge tools naturally in documentation.

Example in KF_HierarchyKit README:
> "Looking for project-wide health checks? See KF_ProjectAudit."

Not advertising. Helpful references. Consistent with the brand voice.

---

## 8. Pricing Table (Wave 1 — Proposed)

| Product | Tier | Base price | Freemium | Launch discount | Target segment |
|---------|------|-----------|----------|-----------------|----------------|
| `KF_HierarchyKit` | Simple | $20 (paid upgrade) | Yes — core free | None | Solo + Prototype |
| `KF_ProjectAudit` | Moderate | $30 | No | None | Studios + Solo |
| `KF_ScreenCapture` | Simple | $18 | No | None | Solo + Prototype |

Notes:
- HierarchyKit free tier builds funnel. Paid at $20 is impulse-buy for anyone already using the free version.
- ProjectAudit at $30 positions as serious quality tool without entering premium territory.
- ScreenCapture at $18 is the lowest-friction entry point. "Worth it for the first time you need clean store screenshots."

### Price validation check

| Product | Competitor price | Our price | Are we credible here? |
|---------|-----------------|-----------|----------------------|
| HierarchyKit | vHierarchy: free/paid (~$15–20?) | $20 paid | ✅ Yes — in range, need strong differentiation |
| ProjectAudit | Unity's free + paid alternatives ~$20–40 | $30 | ✅ Yes — mid-range, must deliver on opinionated UX |
| ScreenCapture | Various $10–30 | $18 | ✅ Yes — budget-friendly for clear utility |

---

## Approval checklist

- [x] Revenue model and Asset Store economics documented
- [x] Market pricing map with real competitor data
- [x] Tier-to-price framework defined
- [x] Freemium decision framework with per-product decisions
- [x] Discount and sales rules established
- [x] Voucher allocation strategy with rules
- [x] Bundle strategy noted (future, not premature)
- [x] Wave 1 pricing table proposed
- [ ] Team review and approval
