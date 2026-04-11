# Support Strategy — KITforge labs
Status: DRAFT
Created: 2026-04-11
Source: BrandStrategy, LaunchPlaybook, article analysis (Unity Asset Store patterns)
Support email: hello@kitforgelabs.com

---

## 0. Philosophy

Support is where reputation is built or destroyed. The article analysis is clear: bad support destroys publishers faster than bad products. A buggy product with transparent, responsive support gets a second chance. A good product with dismissive or absent support does not.

KITforge's support philosophy is an extension of the brand voice:

> **Honest, responsive, and public. Handle problems openly — they become proof of quality.**

---

## 1. Support Channels

### Primary channel — Email

**Address:** hello@kitforgelabs.com  
**Purpose:** All official support requests, bug reports, license questions.  
**SLA:** First response within **72 hours** on business days.  
**Tone:** Same brand voice as everything else. Direct, helpful, no bureaucratic language.

### Secondary channel — Unity Publisher Forums / Asset Store Reviews

**Purpose:** Public-facing responses to reviews and forum questions.  
**Why public matters:** Forum responses are indexed. A public "we fixed this in v1.1" is worth 10 private replies — it shows future buyers that issues get resolved.  
**Rule:** Every review (positive or negative) gets a public response.

### No Discord as primary support

Discord is fine as a community space but NEVER as the only support channel. Reasons:
- Conversations are unsearchable by future buyers
- Perceived as a mechanism to hide negative feedback
- Puts support burden on async real-time communication
- Forces users to join yet another server for basic help

If Discord is eventually set up (community, not support), it links back to email for real issues.

### Documentation as self-service support

The best support request is the one that never gets sent. Every product's documentation must answer the top 5 questions a new user will have:

1. How do I install / set up?
2. How do I use the main feature?
3. Why isn't it working in my project?
4. What Unity versions are supported?
5. How do I reach support?

If these 5 questions keep appearing in support, the documentation is failing.

---

## 2. Response SLA

| Ticket type | First response | Resolution target |
|------------|---------------|-------------------|
| **Critical bug** (product broken on import, compile error) | 24 hours | Fix shipped within 7 days |
| **Functional bug** (feature not working as described) | 48 hours | Fix shipped within 14 days |
| **Usage question** (how do I do X) | 72 hours | Resolved in reply or docs updated |
| **Feature request** | 72 hours | Acknowledged + added to backlog |
| **Review response** (public, Asset Store) | 48 hours | Always public, never defensive |

**What "first response" means:** A real answer, not an acknowledgement template. If investigation takes time, say so explicitly: "We can reproduce this — investigating now, will update by [date]."

---

## 3. Bug Triage Protocol

### Severity levels

| Level | Description | Example |
|-------|-------------|---------|
| **S1 — Critical** | Product unusable. Compile error, import error, crash on open. | "Importing the package causes 50 compile errors in my project" |
| **S2 — Major** | Core feature broken. User cannot use the main promised feature. | "The backup never saves anything" |
| **S3 — Minor** | Secondary feature broken or UX issue. Workaround exists. | "The backup count doesn't update in the UI until restart" |
| **S4 — Cosmetic** | Visual glitch, typo, minor inconsistency. | "The button label is cut off at small window sizes" |

### Triage steps

1. **Reproduce first.** Never promise a fix without reproducing the issue.
2. **Ask for a minimal reproduction case if needed.** "Can you share the repro steps in a new empty Unity project?" — but only ask if genuinely needed, not as a deflection tactic.
3. **Be honest about timelines.** If a fix is complex and will take 2 weeks, say so.
4. **Ship a hotfix for S1/S2.** Don't batch S1 bugs into the next planned update.
5. **Acknowledge in the changelog.** Credit the reporter if they want it: "Fixed X (reported by user Y)."

---

## 4. Review Response Protocol

### Every review gets a response

No exceptions. Positive reviews: thank genuinely (not copy-paste thanks). Negative reviews: investigate, respond publicly, fix if valid.

### Response templates (use as starting point, always personalize)

**Positive review:**
> "Thank you for taking the time to leave a review — glad it's working well for you. If there's anything we can improve, you know where to find us."

**Negative review — valid bug:**
> "Thank you for the detailed report. We've reproduced the issue and a fix is on the way in the next update. We'll post here when it ships."

**Negative review — usage issue:**
> "Apologies for the confusion. [Specific explanation of the issue]. We'll also update the documentation to make this clearer. Feel free to reach us at hello@kitforgelabs.com if anything else comes up."

**Negative review — feature expectation mismatch:**
> "We appreciate the honest feedback. [Explain what the product does and doesn't do]. We'll look at whether the store description could be clearer about scope."

### What to NEVER do in a review response

- Never be defensive ("we tested this thoroughly, it works fine")
- Never redirect to a private channel to avoid the public response
- Never argue with the reviewer's subjective experience
- Never promise features as a response to a review (publicly committing to unplanned scope)
- Never delete or flag reviews as inappropriate unless they genuinely violate Unity's terms

---

## 5. Update and Communication Policy

### When a fix ships

Users who reported the bug should be notified proactively:
1. Reply to their support email: "The fix is live in v1.1.0. You can update from [Package Manager / Asset Store]."
2. Update the public forum/review response if applicable.
3. Add to changelog.

### Changelog as communication

Every update published includes a clear changelog. Format defined in `LaunchPlaybook.md §6`.

The changelog must be:
- In the package (CHANGELOG.md or inside README.md)
- Posted as a publisher update on the Asset Store page
- Written in plain language (not commit messages)

### Version support policy

KITforge supports the **current Unity LTS** and **one previous LTS** for all packages.

| Unity version | Support status |
|--------------|----------------|
| Current LTS (Unity 6.x) | Full support |
| Previous LTS (Unity 2022.3) | Fix critical bugs only |
| Older versions | Best effort — no guarantees |

When dropping support for a version, announce it in the changelog at least one release before dropping it.

---

## 6. Feature Request Handling

Feature requests are valuable intelligence, not promises.

### How to handle them

1. **Acknowledge every request.** "Thanks for the suggestion — noted and added to our backlog."
2. **Never promise.** Don't say "we'll add this in the next update" unless it's already decided.
3. **Track internally.** Maintain a feature request log per product in `_Develop/KF_<Slug>_dev/FeatureRequests.md`.
4. **Use patterns to prioritize.** If 5 users request the same feature independently, it's a signal.
5. **Close the loop.** When a requested feature ships, email the requester: "You asked for X — it's in v1.2.0."

---

## 7. Anti-Patterns (things KITforge NEVER does)

These are extracted from documented bad practices in the Unity publisher ecosystem and explicitly banned:

| Anti-pattern | Why it's banned |
|-------------|----------------|
| Requiring 5-star review for support | Violates trust, likely violates Unity policy, destroys community reputation |
| Discord-only support | Unsearchable, excludes users, can suppress criticism |
| Ignoring negative reviews | Future buyers read them. Silence = confirmation the review is accurate. |
| Closing support without resolution | "Closing as won't fix" is acceptable if stated honestly; silent closure is not |
| Copy-paste acknowledgements that never lead to action | Users notice. Kills trust faster than slow responses. |
| Blaming Unity for the problem in public | Even when true, it reads as deflection. Acknowledge, find workaround, fix what you can. |
| Deleting or flagging legitimate complaints | Nuclear option for reputation. Never. |

---

## 8. Knowledge Base / FAQ

Each product ships with a FAQ section in its documentation (on kitforgelabs.com, once docs site is live). The FAQ should be updated after every support interaction that reveals a documentation gap.

**FAQ update rule:** If the same question is asked twice by different users → add it to the FAQ. No exceptions.

Minimum FAQ per product:

```
Q: Does this work with Unity 2022 LTS?
A: [Answer]

Q: Does this work in Play Mode?
A: [Answer]

Q: How do I update to the latest version?
A: [Answer]

Q: Does this affect build size?
A: [Answer]

Q: Is the source code included?
A: Yes. All KITforge products ship with full C# source code.
```

---

## 9. Support Load Estimation

For planning capacity:

| Product tier | Expected support tickets / month (first 6 months) |
|-------------|--------------------------------------------------|
| Simple (HierarchyKit, SceneBackup) | 2–5 / month |
| Moderate (ProjectAudit, DataBridge) | 5–15 / month |
| Complex (SafeClean, SceneExplorer) | 10–25 / month |

These are rough estimates. Reality depends heavily on documentation quality. Good docs can reduce tickets by 50–70%.

**Wave 1 total estimated load:** Under 20 tickets/month across all products. Manageable without dedicated support staff.

---

## Approval checklist

- [x] Support channels defined (email primary, forum secondary, no Discord-as-primary)
- [x] SLA table for all ticket types
- [x] Bug severity levels with examples
- [x] Review response protocol with templates
- [x] Anti-patterns explicitly listed
- [x] Update and communication policy
- [x] Version support policy
- [x] Feature request handling
- [x] FAQ update rule
- [x] Support load estimation for planning
- [ ] Team review and approval
