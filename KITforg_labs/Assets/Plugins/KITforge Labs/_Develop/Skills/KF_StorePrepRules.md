# KF_StorePrepRules — Store Page & Submission Guide
Scope: KITforge labs only
Last updated: 2026-04-11

---

## Antes de preparar la store page

- QA completado y en estado PASSED
- Package exportado y limpio
- README.md final escrito

---

## 1. Título del producto

Fórmula: `[Nombre corto] — [Beneficio en una frase]`

Reglas:
- Máximo 60 caracteres total
- El beneficio debe ser para el comprador, no descripción técnica
- No usar "Unity" en el título (policy de Asset Store)
- No usar "Pro", "Plus", "Professional" a menos que sea la versión de pago de una con versión gratuita

Ejemplos:
```
✅ Hierarchy Kit — Visual Structure for Complex Scenes
✅ Project Auditor — Catch Problems Before They Ship
❌ Advanced Unity Hierarchy Enhancement Tool Professional
❌ Hierarchy Colors and Labels and Icons and Groups Manager
```

---

## 2. Descripción corta (Short Description)

Aparece en listados. Máximo 100 caracteres.

Fórmula: `[Qué hace] [Para quién] [Sin qué molestia]`

```
✅ Color-coded hierarchy labels and groups. Clean scenes without extra MonoBehaviours.
```

---

## 3. Descripción larga (Long Description)

Estructura estándar KITforge:

```
## The Problem
[1-2 frases describiendo el dolor. Sin soluciones todavía.]

## What It Does
[Bullet list de features. Cada uno empieza con verbo de acción.]
• Add color-coded section headers to any Hierarchy
• Group objects visually without changing scene structure
• Toggle visibility and lock state from the Hierarchy panel
• ...

## Quick Start
[3-5 pasos desde import hasta primer uso]
1. Import the package
2. Open KITforge Labs > Hierarchy Kit > Open Window
3. Right-click any Hierarchy row to add a header

## Compatibility
- Unity 6.0+
- Universal Render Pipeline (URP) — not required
- Windows and macOS Editor
- Works with any scripting backend

## Support
[Link a documentación, email de soporte, Discord si existe]
```

Reglas:
- Sin hyperbole ("the best", "most powerful", "revolutionary")
- Sin comparaciones directas que nombren competidores
- Sin capturas en la descripción (van en los screenshots separados)
- Todo en inglés

---

## 4. Screenshots

Formato requerido: 1920×1080 PNG

| # | Contenido | Propósito |
|---|-----------|-----------|
| 1 | Hero image — tool en acción, resultado más visual | Primera impresión en la lista |
| 2 | Feature principal — close-up del flujo principal | "Esto es lo que hace" |
| 3 | Feature secundaria o variante | Profundidad |
| 4 | Quick Start en 3 pasos (infographic) | Baja fricción de adopción |
| 5 | (Opcional) Comparación antes/después | Prueba de valor |

Reglas de screenshots:
- Sin UI de Unity fuera del producto (ocultar toolbars, project window si distrae)
- Tipografía legible a 400px de ancho (así se ven en listados)
- Sin texto en español (el store es global)
- Sin watermarks de herramientas de captura
- Usar la Demo scene del producto para consistencia

Nombre de archivos: `KF_<Slug>_Screenshot_01.png`, etc.
Hero: `KF_<Slug>_Hero.png`

---

## 5. Video (recomendado, no obligatorio para v1)

Reglas:
- Máximo 90 segundos
- Primeros 15 segundos: mostrar el valor principal. El comprador no ve más si no le engancha.
- Estructura: Problema (5s) → Demo en acción (60s) → Quick Start o call-to-action (15s)
- Sin música de librería genérica si es posible
- Subtítulos si hay narración en inglés no nativo
- Subir a YouTube y enlazar en la store page (Asset Store no hostea videos)

---

## 6. Categoría y keywords

Elegir la categoría más específica posible. En caso de duda entre dos, la más específica gana más tráfico cualificado.

Keywords: 5-10 palabras que el comprador buscaría. Pensar en verbos y problemas, no solo en features.

```
✅ hierarchy, editor tool, inspector, utility, workflow, color labels, scene organization
❌ unity, asset, tool, package, professional
```

---

## 7. Pricing

Referencia de mercado para cada tier (ver UsefulReferences.md para datos actualizados):

| Tier | Rango KITforge Wave 1 |
|------|----------------------|
| Herramienta de editor UX | $15-25 |
| Herramienta de seguridad/auditoría | $25-40 |
| Pack de shaders | $35-55 |

Regla: para la primera versión, errar hacia el precio BAJO. Es más fácil subir precio en v1.5 que perder reviews por expectativas no cumplidas a precio alto.

Versión gratuita (Freemium):
- Si hay plan de freemium, definirlo en el Brief antes de empezar el build
- La versión gratuita debe ser útil por sí misma, no un teaser de pago
- La línea de corte: features de productividad básica → gratuito. Features de automation, batch, et al. → pago.

---

## 8. README.md final (el que va en el package)

Diferente al README interno de desarrollo. Este es para el comprador.

Estructura:
```
# KF_<ToolName>

## What it does
[One-liner + problem description]

## Quick Start
1. 
2. 
3. 

## Features
- 
- 

## Requirements
- Unity X.X+
- URP X.X+ (si aplica)

## Compatibility
- Windows Editor: ✅
- macOS Editor: ✅
- IL2CPP: ✅ / ⚠️ Known limitation: ...

## Support
[Link o email]

## Version History
### 1.0.0
- Initial release
```

---

## 9. Submission checklist

| Item | Estado |
|------|--------|
| Package exportado sin archivos `_Develop/` | ⬜ |
| Todos los scripts en namespace `KITforgeLabs.*` | ⬜ |
| Sin Console errors en proyecto limpio | ⬜ |
| README.md presente y completo | ⬜ |
| Third-Party Notices.txt si aplica | ⬜ |
| Screenshots 1920x1080 preparados | ⬜ |
| Título y descripción escritos en inglés | ⬜ |
| Categoría y keywords seleccionados | ⬜ |
| Precio decidido y justificado | ⬜ |
| Demo scene incluida y funcional | ⬜ |
| Publisher Portal — publisher profile completo con logo y descripción | ⬜ |
