# Planner Workbook — KITforge Labs Strategic Review
Session: 2026-04-11
Objective: Revisar plan global de la fábrica. Dos bloques: (A) Tools & Shaders, (B) Marketing/Business/Comunicación.

---

## DISCOVER — Knowledge Inventory

### QUÉ EXISTE (bloque técnico/producto)

| Documento | Path | Estado | Cobertura |
|-----------|------|--------|-----------|
| `tools_lab_env.json` | Settings/KITforgeLabs/ | ✅ Completo | 10 productos, scores, fases, waves |
| `BOOTSTRAP.md` | _Develop/ | ✅ Completo | Protocolo AI, skill map, hard rules, lifecycle |
| `ProductBacklog.md` | _Lab/Research/ | ✅ Completo | 10 product cards con scoring 7-axis |
| `MarketReference.md` | _Lab/Research/ | ✅ Bueno | Market signals, competitor tables, opportunity filters |
| `ProductOpportunities.md` | _Lab/Research/ | ✅ Bueno | Wave 1 rationale, kill criteria |
| `UsefulReferences.md` | _Lab/Research/ | ✅ Bueno | Links reales categorizados |
| `KF_NamingConventions.md` | Skills/ | ✅ Completo | Namespace, assembly, clases, archivos, carpetas |
| `KF_BriefRules.md` | Skills/ | ✅ Completo | Criterios aprobación sección a sección |
| `KF_QARules.md` | Skills/ | ✅ Completo | Checklists compilación, editor, UX, package |
| `KF_StorePrepRules.md` | Skills/ | ✅ Parcial | Título, descripción, screenshots — falta más |
| `KF_DevEnv.md` | Skills/ | ✅ Completo | Entornos de test internos |
| `KF_Learnings.md` | Skills/ | ✅ Activo | Correcciones de sesiones anteriores |
| `Brief.Template.md` | Templates/ | ✅ Completo | 11 secciones + approval checklist |
| `Architecture.Template.md` | Templates/ | Existe | No leído aún |
| `ChangeLog.Template.md` | Templates/ | Existe | No leído aún |
| `TestPlan.Template.md` | Templates/ | Existe | No leído aún |
| `ToolREADME.Template.md` | _Lab/Templates/ | ✅ Completo | Template para README de producto |

### QUÉ NO EXISTE (bloque marketing/business/comunicación)

| Necesidad | Documento necesario | Prioridad |
|-----------|-------------------|-----------|
| Estrategia de marca / brand voice | `BrandStrategy.md` | ALTA |
| Segmentación de clientes | dentro de BrandStrategy o propio | ALTA |
| Estrategia de pricing global | `PricingStrategy.md` | ALTA |
| Modelo freemium (cuándo sí, cuándo no) | dentro de PricingStrategy | ALTA |
| Playbook de lanzamiento por producto | `LaunchPlaybook.md` | ALTA |
| Estrategia de soporte post-venta | `SupportStrategy.md` | ALTA |
| Plan de comunicación y canales | `ChannelStrategy.md` | MEDIA |
| SEO y copywriting para store pages | enriquecer `KF_StorePrepRules.md` | MEDIA |
| Estrategia de vouchers y reviews | dentro de LaunchPlaybook | MEDIA |
| Declaración AI (Unity policy) | dentro de LaunchPlaybook o SOP | MEDIA |
| Demo builds (APK/WebGL) como herramienta de venta | dentro de LaunchPlaybook | MEDIA |
| Calendario de contenido / cadencia de publicación | `ContentCalendar.md` | BAJA (futuro) |
| Financial model / revenue projections | `FinancialModel.md` | BAJA (futuro) |
| Community building plan | dentro de ChannelStrategy | BAJA (futuro) |

---

## DISCOVER — Contraste con el artículo proporcionado

### Puntos del artículo que YA cubrimos bien

1. **"No intrusividad" como principio** — Ya en README, ProductBacklog, product cards. Regla R3-R4 en BOOTSTRAP.
2. **Rendimiento / zero GC** — unity-rules cubre esto al 100%. code-doctor lo valida.
3. **Código fuente abierto** — Implícito (no hay DLLs, todo C# con source).
4. **Demo scene obligatoria** — Hard rule R8 en BOOTSTRAP. QA checklist.
5. **Quickstart / valor en 60s** — BriefRules §7 exige "aha moment <2 min".
6. **Score rubric multi-axis** — 7 factores ya bien calibrados.
7. **Kill criteria** — Presentes en ProductOpportunities y BriefRules.

### Puntos del artículo que cubrimos PARCIALMENTE

1. **Store page copy** — StorePrepRules tiene estructura pero falta:
   - Hook emocional (el artículo enfatiza "mitigación de fricción", no features)
   - SEO keywords strategy
   - A/B testing de títulos
2. **Segmentación de clientes** — Product cards mencionan "target buyer" pero no hay un perfil de cliente KITforge-wide
3. **Competitor analysis** — MarketReference tiene tabla básica, pero no analysis profundo del tipo "qué hace bien X y cómo nos diferenciamos structuralmente"
4. **Screenshots** — StorePrepRules tiene reglas de screenshots pero no strategy de "hero image como primer impacto"

### Puntos del artículo que NO cubrimos (GAPS)

1. **Psicología de compra** — "El cliente no busca features, busca mitigación de fricción." Esto debería informar TODO nuestro copywriting y comunicación. No está documentado.
2. **3 perfiles de cliente** (profesional, indie, estudiante) — Cada uno tiene prioridades distintas. Sin segmentación formal, nuestro messaging será genérico.
3. **Estrategia de soporte** — El artículo advierte sobre Discord como "cámara de eco" y la manipulación de reviews. No tenemos plan de soporte documentado.
4. **Vouchers** — Unity permite 16 cupones/año por producto. No hay estrategia de uso.
5. **Demo builds distribuibles** — APK/WebGL para shader products. No contemplado.
6. **Declaración de IA** — Unity obliga a declarar. No tenemos policy.
7. **Brand voice** — ¿Cómo hablamos? ¿Técnico pero accesible? ¿Directo? ¿Con humor? Sin definir.
8. **Estrategia anti-"asset flip" estigma** — Como publisher nuevo + uso de IA en desarrollo, necesitamos strategy consciente para establecer credibilidad.
9. **Modelo de pricing comparativo** — Productos como los nuestros: ¿dónde está el sweet spot? ¿$15? ¿$35? ¿$50? No hay framework.
10. **Cross-sell / portfolio effect** — ¿Bundle strategy? ¿Los productos se refuerzan entre sí? No documentado.

---

## DISCOVER — Análisis del bloque técnico/producto

### Estado actual de la fábrica

La infraestructura de producción está sorprendentemente madura para no haber producido todavía:

**Fortalezas:**
- Lifecycle claro (BACKLOG → PUBLISHED → MAINTAINED)
- Gate system sólido (no build sin brief aprobado, no code sin architecture)
- Skills y templates cuadriculados
- Scoring rubric de 7 ejes bien calibrado
- Learnings activos (se documenta lo que falla)
- Naming conventions rigurosas

**Gaps técnicos observados:**
- No hay ningún producto en fase BRIEF ni más allá. Todo está en BACKLOG.
- Wave 1 tiene 3 productos pero no hay un timeline ni commitment sobre cuándo empezar.
- Architecture.Template no lo he leído — podría necesitar review.
- No hay un plan de testing cross-version de Unity (6.0, 6.1, etc.)
- No hay strategy para mantener productos actualizados cuando Unity saca nueva LTS.

---

## DISCOVER — Brand Voice Analysis (from kitforgelabs.com)

### Raw extraction

**Hero tagline:** "Purpose-built Unity tools for real workflows."
**Core claim:** "Focused tools for the repeated tasks, awkward steps, and day-to-day work that keep slowing teams down."

### Tone signature

The copy is **deliberately anti-marketing**. Zero superlatives. Zero hype. Calm, direct, honest, understated. 
Reads like someone who has been burned by overpromising and chose the opposite path.

Key sentence that defines the voice:
> "No fluff, no inflated promises, no unnecessary complexity."

### Vocabulary DNA (words that repeat with intention)

| Word/phrase | Uses | Function |
|---|---|---|
| "Focused" | 12+ | Core positioning — everything is scoped, clear, intentional |
| "Day-to-day" | 8+ | Grounds value in daily reality, not theory |
| "Repeated tasks" | 6+ | The villain — recurrence is the enemy |
| "Awkward steps" | 5+ | Empathy — acknowledges the friction devs feel |
| "Quietly" | 3+ | Key insight — problems don't scream, they accumulate |
| "Should feel easier/lighter" | 5+ | Promise framing — aspirational but humble |
| "Unnecessary" | 4+ | Precision — not all effort is bad, only the wasteful kind |
| "Real" | 4+ | Authenticity signal — real workflows, real situations |
| "Clear scope" | 3+ | Anti-bloat commitment |
| "Useful beats bigger" | 1 (manifesto) | The one-line philosophy |

### Brand values (extracted, not invented)

1. **Focus over breadth** — "A clear scope is often more useful than more features"
2. **Honesty over hype** — "Keep it honest. No fluff, no inflated promises"
3. **Practical over impressive** — "Useful beats bigger"
4. **Respect for time** — "Small improvements matter when the same issue keeps coming back"
5. **Empathy for the mundane** — "The work people repeat every day deserves better support"
6. **Deliberate restraint** — "We are not trying to build everything"

### What the voice is NOT

- Not playful/fun (no humor, no emoji, no personality quirks)
- Not corporate (no "leverage", "solution", "enterprise-grade")
- Not aggressive (never attacks competitors, never claims superiority)
- Not technical (website avoids code jargon, speaks to the human not the coder)
- Not salesy (no urgency, no scarcity, no "limited time")

### Target audience (website segments)

| Segment | Trigger phrase | Matches article profile |
|---|---|---|
| Studios | "When process issues start multiplying across production" | Professional ≈ |
| Solo professionals | "When repeated tasks eat time and focus" | Indie dev ≈ |
| Prototype teams | "When speed matters, but so does keeping the workflow manageable" | Indie dev ≈ |

Note: the website does NOT speak to students/hobbyists at all. The brand is professional-first.

### Alignment with article insights

**Strong alignment:**
- Article says "client buys friction mitigation, not features" → website already does this naturally
- Article says "non-intrusive tools win" → website says "focused, clear scope"
- Article warns against hype → website is the opposite of hype

**Contradiction worth noting:**
- Article says students buy assets as learning material → website ignores this segment entirely
- Article emphasizes video tutorials → website is text-only, no learning content
- Article says "boca a boca" via YouTube is key → no video/content strategy visible

**Opportunity from alignment:**
- The website voice is unusually honest for a publisher. This IS the differentiator. If store page copy matches this tone, it will stand out massively against competitors who oversell.

---

## SESSION 2 — Primera Tool: Análisis de decisión
Session: 2026-04-11 (continuación)
Objetivo: Decidir cuál es el primer producto a desarrollar y entrar en fase BRIEF.

### Estado de la fábrica al entrar en esta sesión

- **0 productos fuera de BACKLOG.** La infraestructura está lista (templates, skills, BOOTSTRAP, env.json).
- Fase siguiente para cualquier producto: BRIEF → (Architecture si Moderate/Complex) → BUILD.
- `Assets/Scripts/` totalmente vacío. `KITforge Labs/` solo tiene `_Develop/`. Ningún código de producto existe.
- DOTween (Demigiant) está presente en Plugins — no relevante para editor tools.

### Los tres candidatos reales

| Candidato | Score | Tier | Gate antes de código | Riesgo técnico clave | Ventaja primera posición |
|-----------|-------|------|---------------------|---------------------|--------------------------|
| KF_HierarchyKit | 29 | Moderate | Brief APPROVED | `hierarchyWindowItemOnGUI` — estable pero IMGUI-based; en Unity 6 hay alternativa via UI Toolkit no documentada | Demo 5s GIF. Setup=5. Reconocimiento de marca rápido. |
| KF_ProjectAudit | 29 | Complex | Brief + Architecture | AssetDatabase scanning = lento en proyectos grandes (UX killer si no se asíncrona bien) | Expansion path = Custom Rules SDK. Diferenciación duradera. |
| KF_SafeClean | 28 | Complex | Brief + Architecture | false positives en dependency detection = reviews negativos. GetDependencies API tiene bugs conocidos. | Pain=5, historia antes/después clara. |

### Contradicción detectada en la documentación

ProductBacklog.md pone HierarchyKit y ProjectAudit empatados a #1 (29 pts).
ProductOpportunities.md dice: "Start with workflow safety before visual flash" → apunta a ProjectAudit/SafeClean primero.
La card de HierarchyKit dice explícitamente: "Why first: Visual impact is immediate. Demo is a 5-second GIF. Builds brand recognition fast."

**Son dos filosofías distintas:**
- A) "Brand first" → HierarchyKit. Primer producto impacta rápido, demos fáciles, se habla de la marca.
- B) "Value first" → ProjectAudit/SafeClean. Primer producto demuestra la seriedad del enfoque KITforge.

### Mi análisis — para resolver la contradicción

**Argumento para HierarchyKit como primero:**
1. Menor complejidad técnica → menos bugs → menos reviews negativos → mejor de cara al estreno
2. Demo en 5 segundos = mejor TFA (top-of-funnel asset) cuando no tienes brand recognition aún
3. Competitive validation: vHierarchy valida que hay mercado activo. No estamos adivinando.
4. Build time ~1-2 semanas vs 3+ para ProjectAudit. Primer ship rápido = aprendizaje real del pipeline store.
5. Freemium (core gratis + automation paid) es modelo menos arriesgado para debut.

**Argumento para ProjectAudit como primero:**
1. Diferenciación más duradera. HierarchyKit vive a la sombra de vHierarchy desde el día 1.
2. Mejor alineación con el claim del website: "focused tools for day-to-day work that keep slowing teams down."
3. Expansion=5 significa que el primer producto puede evolucionar mucho → mayor lifetime value.
4. Complejidad más alta, pero la fábrica (skills, templates) está diseñada precisamente para manejarla.

**Argumento en contra de SafeClean como primero:**
- Review safety = 3/5 + tech risk alto = combinación peligrosa para el primer producto publicado. Un falso positivo puede destruir la rating inicial y no hay portfolio detrás que amortigüe.

### Valoración de KF_ProjectAudit — riesgos técnicos a resolver en Brief

1. ¿La scan se hace asíncrona o bloquea el editor? (AssetDatabase.FindAssets es síncrono)
2. ¿Cómo se persisten las "dismissals" entre sesiones? (SessionState vs EditorPrefs vs .json)
3. El range de Unity versions a soportar. ProjectAuditor de Unity está desde 2020.x.
4. ¿"Custom Rules SDK" es MVP o V2? Si es MVP, scope explota. Si es V2, es una promesa sin cumplir.

### Valoración de KF_HierarchyKit — riesgos técnicos a resolver en Brief

1. `EditorApplication.hierarchyWindowItemOnGUI` — usar correctamente para no impactar perf del editor con escenas grandes (muchos objetos).
2. Estado persistence: ¿dónde se guarda la config de colores/secciones? ¿Por escena? ¿Global?
3. Collapsible groups "visual only" — UI tricky. ¿Cómo funciona sin tocar la jerarquía real?
4. Tema dark/light: respetar `EditorGUIUtility.isProSkin`.
5. Freemium: ¿qué feature ≥ paid? Definir el tier gate es fundamental para no dar todo gratis.

### DECISIÓN TOMADA ✅

**Primer producto: KF_HierarchyKit**
Razón: Aprender el pipeline. Tier Moderate. Demo en 5s. Bajo support risk. 

**Modelo comercial:** Paid simple (freemium descartado para v1 — añade complejidad innecesaria al aprender el pipeline).
**Siguiente paso:** Brief DRAFT → completar secciones → Brief APPROVED → BUILD.

---

## ASSUMPTIONS LOG

| # | Supuesto | Estado | Fuente |
|---|---------|--------|--------|
| A1 | Publicaremos bajo cuenta publisher "KITforge Labs" | Sin confirmar | README menciona la marca |
| A2 | La web kitforgelabs.com será el hub de documentación | Sin confirmar | La web existe, pero no hay docs section aún |
| A3 | Equipo (no solo dev) | Confirmado | User: "El equipo de KITforge labs" |
| A4 | El presupuesto de marketing es 0€ | Confirmado | User: "0€", herramienta Buffer para posts es proyecto aparte |
| A5 | Los primeros productos serán Wave 1 del backlog | Solo referencia | User: "Solo son referencias" (scores no son commitment) |
| A6 | URP es el pipeline principal de los shaders | Confirmado | MarketReference + skills |
| A7 | La IA se usa activamente en desarrollo | Confirmado | Todo el ecosistema AI sessions |
| A8 | Publisher account no creada aún, pero en breve | Confirmado | User respuesta 1 |
| A9 | Web kitforgelabs.com activa | Confirmado | User + copy analizado |

---

## OPEN QUESTIONS (para el usuario)

→ Ver chat message

---

## DECISION — Organización del bloque Business

### El problema
Tenemos/tendremos 4-5 documentos de business en `_Lab/Business/`. ¿Cómo se asegura un AI session que los consulta en el momento correcto? ¿Y un humano?

### Infraestructura existente de referencia (bloque técnico)

```
Capa 1 — BOOTSTRAP.md        → Protocolo de arranque. Dice QUÉ cargar y CUÁNDO.
Capa 2 — Skills (KF_*.md)    → Reglas accionables. Dice CÓMO hacer las cosas.
Capa 3 — Templates (*.md)    → Plantillas. Dice QUÉ format tiene cada entregable.
Capa 4 — Research (*.md)     → Referencia. Datos para consulta, no instrucciones.
```

Los business docs son mezcla de capas 2 y 4:
- BrandStrategy → Referencia (consultar) + reglas (voice rules, anti-patterns)
- PricingStrategy → Referencia (datos mercado) + reglas (framework de decisión)
- LaunchPlaybook → Procedural/checklist → se parece más a una SKILL
- SupportStrategy → Referencia + reglas

### Opciones evaluadas

**Opción A — Solo index en _Lab/Business/**
- Un `_index.md` que lista docs y cuándo consultarlos
- Pro: Simple, sin overhead
- Con: Depende de que alguien (AI o humano) recuerde ir a leerlo
- Veredicto: Insuficiente solo

**Opción B — Nueva skill `KF_BusinessRules.md`**
- Una skill como KF_QARules que compila las reglas accionables de TODOS los business docs
- Se carga en STORE_PREP (y parcialmente en BRIEF para pricing)
- Pro: Se integra en el sistema existente. AI lo carga automáticamente.
- Con: Duplica info si solo copia-pega de los docs de referencia
- Veredicto: ✅ Buena opción si la skill es REGLAS, no copia

**Opción C — Enriquecer BOOTSTRAP + StorePrepRules**
- BOOTSTRAP referencia los business docs en su skill map por fase
- StorePrepRules absorbe las reglas accionables de brand voice y pricing
- Los docs en _Lab/Business/ quedan como referencia de profundidad
- Pro: No crea nueva capa. Usa infraestructura existente.
- Con: StorePrepRules crece mucho
- Veredicto: ✅ Buena opción, más orgánica

**Opción D — B + C combinadas (recomendada)**
- Business docs en `_Lab/Business/` → REFERENCIA (datos, análisis, contexto profundo)
- Nueva skill `KF_MarketRules.md` → REGLAS accionables extraídas de los business docs
  - Cuándo se carga: BRIEF (pricing, segmentación) + STORE_PREP (todo)
  - Qué contiene: voice rules, pricing framework, launch checklist, support rules
- BOOTSTRAP actualizado para incluir la nueva skill en su mapa
- Index en `_Lab/Business/` para navegación humana
- Pro: Cada capa hace su trabajo. No duplica, referencia.
- Con: Un archivo más en Skills/
- Veredicto: ✅✅ Recomendada — escala bien, sigue el patrón existente

### Decisión propuesta: Opción D

```
_Lab/Business/                    ← REFERENCIA (profundidad, contexto, datos)
  _index.md                       ← Mapa de navegación para humanos
  BrandStrategy.md                ← ✅ Creado
  PricingStrategy.md              ← Pendiente
  LaunchPlaybook.md               ← Pendiente
  SupportStrategy.md              ← Pendiente

Skills/                           ← REGLAS ACCIONABLES (lo que AI carga)
  KF_MarketRules.md               ← NUEVO — compila reglas de brand, pricing, launch, soporte
                                     Se carga en fases BRIEF + STORE_PREP

BOOTSTRAP.md                      ← Actualizar skill map con KF_MarketRules
```

La skill KF_MarketRules NO copia los documentos — extrae las reglas binarias y checklists:
- "Nunca uses superlatives en store copy" (de BrandStrategy)
- "Precio mínimo $15, máximo $60 para Wave 1" (de PricingStrategy)
- "Vouchers: máximo 16/año, usar para reviews pre-launch" (de LaunchPlaybook)
- "Responder bugs públicamente en < 72h" (de SupportStrategy)

Los docs en Business/ son "por qué" y "con qué datos". La skill es "qué hacer y qué no hacer".

---

## ANALYSIS — 20 External Product Candidates

### Evaluación criteria
- **Brand fit**: ¿Es un tool de workflow/producción, no un game system? ¿No-intrusivo?
- **Overlap**: ¿Ya tenemos algo equivalente en el backlog?
- **Kill criteria**: API externa, plataforma-específico, pure game system, alta varianza de soporte

### Tabla de evaluación

| # | Producto | Brand fit | Overlap backlog | Kill reason | Recomendación |
|---|---------|-----------|-----------------|-------------|---------------|
| 1 | PSX Retro Shader Pack | ✅ URP shader pack | Parcial (KF_MicroFX tiene efectos retro) | — | **Considerar como efecto dentro de KF_MicroFX**, no producto standalone |
| 2 | AI Localization Bridge | ❌ | — | Dependencia API externa (DeepL/OpenAI). Claves, costes, rate limits del usuario. Muy alto soporte. | **Descartado** |
| 3 | Mobile Stylized Water (URP) | ✅ URP shader | — | Mercado brutal (Crest €220, muchos competidores). Muy complejo de mantener correctamente. | **Descartado** — demasiado competido para Wave 1 |
| 4 | Save System Lite | ❌ parcial | — | Easy Save domina. Muchas alternativas gratis. Runtime game system. No encaja brand. | **Descartado** |
| 5 | Hierarchy Organizer Pro | ✅ Editor tool | **KF_HierarchyKit (backlog #1)** — exactamente lo mismo | — | **Duplicado**. Refuerza que HierarchyKit tiene demanda. |
| 6 | Easy Footstep System | ❌ | — | Game mechanic específico. Audience pequeña. No es workflow tool. | **Descartado** |
| 7 | Simple Grid Inventory | ❌ | — | Pure game system. No es workflow/producción. Completamente fuera de brand. | **Descartado** |
| 8 | Mobile Performance Overlay | ✅ Editor/runtime tool | **KF_DebugOverlay (backlog #8)** — equivalente directo | — | **Duplicado**. Valida backlog item. |
| 9 | VFX Hit Flash Pack | ✅ parcial URP shader | Parcial (KF_MicroFX) | Muy niche (solo combat games). Demo bueno pero audience limitada. | **Posible efecto dentro de KF_MicroFX**, no standalone |
| 10 | Dialogue Node Lite | ❌ | — | Yarn Spinner existe y es maduro. Editor visual complejo. Game system, no workflow tool. | **Descartado** |
| 11 | Auto-Cull Tool | ✅ Editor+Runtime tool | Relacionado con KF_ProjectAudit pero diferente | — | **CANDIDATO NUEVO** — Ver card abajo |
| 12 | Ads & IAP Wrapper | ❌ | — | SDKs cambian constantemente. Alta deuda de mantenimiento. Platform-specific. Kill. | **Descartado** |
| 13 | Cloud Save Bridge | ❌ | — | Dependencia Firebase/PlayFab. Kill por misma razón que #2. | **Descartado** |
| 14 | 2D Sprite Slicer Tool | ✅ Editor tool | — | Automates tedious repetitive task. Fits KITforge brand. Demostrable en GIF. | **CANDIDATO NUEVO** — Ver card abajo |
| 15 | Top-Down Mobile Controller | ❌ | — | Game mechanic. Completamente fuera de brand. | **Descartado** |
| 16 | Scene Auto-Backuper | ✅ Editor tool | Relacionado con KF_SafeClean pero diferente | — | **CANDIDATO NUEVO** — Ver card abajo |
| 17 | Health Bar VFX Pack | ✅ parcial URP/UGUI shader | Relacionado con KF_MicroFX | Muy niche (UI VFX). Audience limitada a combat games. | **Posible dentro de KF_MicroFX**, no standalone |
| 18 | Quest Tracker UI | ❌ | — | Pure game system UI. No encaja brand. | **Descartado** |
| 19 | Texture Packer Lite | ✅ Editor tool | Relacionado con KF_ProjectAudit (texture audit) | Toca URP/texture pipeline. Audience amplia. | **CANDIDATO NUEVO** — Ver card abajo |
| 20 | Excel to Unity Bridge | ✅ Editor tool | — | Real pain point de equipos. Data import tedioso. Fits brand. | **CANDIDATO NUEVO** — Ver card abajo |

### Candidatos nuevos — mini-cards

**`KF_AutoCull` — Scene Object Visibility Optimizer**
Pain: Devs móviles desactivan objetos uno a uno. Nadie tiene un sistema automático no-intrusivo.
Unique: Work at edit-time + optional runtime component. No GameObject parenting requirements.
Tier: Moderate. Audience: mobile devs (narrower than otros). Score estimado: ~24/35.
Risk: Runtime component = soporte más complejo que pure editor tools.

**`KF_SpriteSlicer` — Sprite Sheet Slicer & Auto-Namer**
Pain: Cortar y nombrar spritesheets grandes es lento y error-prone en Unity's manual slicer.
Unique: Batch naming templates, pivot presets, export a folder structure predecible.
Tier: Simple-Moderate. Audience: 2D devs (narrower). Score estimado: ~22/35.
Risk: Audience más estrecha que tools generales. Solo útil para proyectos 2D.

**`KF_SceneBackup` — Scene Auto-Backup**
Pain: Unity no tiene auto-save. Perder trabajo por un crash es una realidad frecuente.
Unique: Timestamped backups, configurable interval, one-click restore, zero setup.
Tier: Simple. Audience: TODOS los Unity devs. Muy amplia. Score estimado: ~26/35.
Risk: Unity añadió auto-save en versiones recientes — verificar si esto lo hace redundante.

**`KF_TexturePacker` — UI Texture Atlas Builder**
Pain: Reducir SetPass calls combinando texturas UI manualmente es tedioso y error-prone.
Unique: Focused on UI atlases specifically (Sprite Atlas es manual y confuso). Smart naming.
Tier: Moderate. Audience: cualquier dev con UI. Score estimado: ~25/35.
Risk: Unity Sprite Atlas existe. Debemos ser MEJOR o más fácil, no diferente por ser diferente.

**`KF_DataBridge` — Spreadsheet to ScriptableObject Importer**
Pain: Importar balanceo de juego o datos de localización desde Excel/Sheets es siempre boilerplate.
Unique: Schema detection automático, mapping visual, zero-code import, re-import incremental.
Tier: Moderate-Complex. Audience: studios y equipos con diseñadores no-coder. Score estimado: ~27/35.
Risk: Varios competitors existen (Odin tiene serialización, algunos importers libres). Diferenciar en UX.

### Decisión final sobre los 20

| Categoría | Productos | Acción |
|-----------|----------|--------|
| **Duplicados del backlog** (refuerzan) | #5 HierarchyOrganizer, #8 PerfOverlay | No hacer nada. Backlog ya los tiene. |
| **Candidatos nuevos a evaluar** | #11 AutoCull, #14 SpriteSlicer, #16 SceneBackup, #19 TexturePacker, #20 DataBridge | Scorer y considerar añadir al backlog |
| **Candidatos MicroFX** (no standalone) | #1 PSX, #9 HitFlash, #17 HealthBarVFX | Agregar como feature ideas a KF_MicroFX card |
| **Descartados** (12/20) | #2,3,4,6,7,10,12,13,15,18 + parcialmente #3 | Fuera de brand o kill criteria |

**El más prometedor de los 5 candidatos: `KF_SceneBackup`**
- Audience universal (todos los Unity devs)
- Tier Simple → build en 1 semana
- Zero API dependencies, zero runtime requirements
- Demo inmediato (muestra el archivo guardado en carpeta)
- Kill risk: verificar que Unity auto-save no lo haga redundante antes de decidir

---

## TODO — ESTADO FINAL (sesión cerrada 2026-04-11)

### Bloque Marketing/Business ✅ COMPLETO

- [x] Confirmar assumptions A1-A4 con usuario ✅
- [x] Definir qué documentos crear para bloque marketing ✅
- [x] Decidir dónde viven los docs de marketing → `_Lab/Business/` ✅
- [x] Decidir estructura de organización → Opción D implementada ✅
- [x] Crear BrandStrategy.md ✅ APPROVED por usuario
- [x] Crear PricingStrategy.md ✅
- [x] Crear LaunchPlaybook.md ✅
- [x] Crear SupportStrategy.md ✅
- [x] Crear _Lab/Business/_index.md ✅
- [x] Crear Skills/KF_MarketRules.md ✅
- [x] Actualizar BOOTSTRAP.md skill map + sección BUSINESS FILES ✅
- [x] Evaluar 20 productos externos → 5 nuevos candidatos identificados ✅
- [x] tools_lab_env.json expandido de 10 a 15 productos ✅
- [x] ProductBacklog.md expandido con 5 product cards nuevas ✅

### Pendiente para próximas sesiones

- [ ] Revisar y enriquecer KF_StorePrepRules con voice rules del artículo (SEO, hero image strategy)
- [ ] Revisar bloque técnico: Architecture.Template, TestPlan.Template, ChangeLog.Template
- [ ] Validar KF_SceneBackup como primer producto a activar (score más alto del backlog: 31/35)
- [ ] Definir cuándo comenzar la primera Brief (Wave 1 commitment)
- [ ] Crear publisher account en Unity Asset Store

---

## SESIÓN CERRADA — 2026-04-11

**Objetivo:** Construir el bloque marketing/business de KITforge Labs desde cero.
**Resultado:** Completado al 100%.

**Documentos creados:**
| Archivo | Path |
|---------|------|
| BrandStrategy.md | _Lab/Business/ |
| PricingStrategy.md | _Lab/Business/ |
| LaunchPlaybook.md | _Lab/Business/ |
| SupportStrategy.md | _Lab/Business/ |
| _index.md | _Lab/Business/ |
| KF_MarketRules.md | Skills/ |

**Archivos modificados:**
| Archivo | Cambio |
|---------|--------|
| tools_lab_env.json | 10 → 15 productos |
| ProductBacklog.md | +5 product cards, Wave 1 actualizado |
| BOOTSTRAP.md | KF_MarketRules en skill map + sección BUSINESS FILES |
