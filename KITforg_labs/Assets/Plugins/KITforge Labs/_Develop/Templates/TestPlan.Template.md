# Test Plan — KF_<ToolSlug>
Version: 1.0
Phase: QA
Unity version tested: 6XXX.X.XXf1
URP version: X.X.X
Editor platform: Windows | macOS
Tester: —
Date: YYYY-MM-DD

---

## Pre-conditions

Antes de ejecutar este plan, verificar:
- [ ] Build compila sin errores
- [ ] Build compila sin warnings
- [ ] Package importado en un proyecto limpio (sin otros assets)
- [ ] Unity versión mínima soportada (ver Brief)
- [ ] URP configurado (si aplica)

---

## Capa 1 — Tests automáticos (Unity Test Runner, Edit Mode)

Ejecutar desde: Window > General > Test Runner > EditMode

| Test class | Tests | Estado |
|------------|-------|--------|
| `KF_<Slug>ScannerTests` | — | ⬜ No ejecutado |
| `KF_<Slug>ValidatorTests` | — | ⬜ No ejecutado |

**Criterio de paso:** Todos los tests en verde. Any red = bloqueante.

---

## Capa 2 — Tests de integración editor

Tests que requieren contexto de editor pero no UI visual.

| # | Test | Acción | Esperado | Resultado | Estado |
|---|------|--------|----------|-----------|--------|
| 2.1 | Window abre | Ejecutar MenuItem `KITforge Labs > <Nombre>` | Ventana aparece sin errores en consola | | ⬜ |
| 2.2 | Window sobrevive domain reload | Abrir ventana, forzar recompilación, volver | Ventana reabre correctamente | | ⬜ |
| 2.3 | Configuración persiste | Cambiar setting, reiniciar Unity, abrir ventana | Setting conservado | | ⬜ |
| 2.4 | | | | | ⬜ |

---

## Capa 3 — Happy Path Manual

El flujo exacto que haría un comprador nuevo el día 1.

| # | Paso | Acción del usuario | Resultado esperado | Resultado real | Estado |
|---|------|-------------------|--------------------|----------------|--------|
| 3.1 | Import | Importar package desde Package Manager / .unitypackage | Sin errores de compilación | | ⬜ |
| 3.2 | Primer acceso | Abrir Tool desde menú | Ventana abre, estado inicial correcto | | ⬜ |
| 3.3 | Acción principal | [Describir acción principal] | [Resultado esperado] | | ⬜ |
| 3.4 | Ver resultado | [Describir cómo se ve el output] | Output correcto y legible | | ⬜ |
| 3.5 | Tomar acción sobre resultado | [Click / fix / export] | Comportamiento esperado | | ⬜ |
| 3.6 | Resultado final | Estado del proyecto tras usar la tool | Proyecto en estado correcto | | ⬜ |

---

## Capa 3 — Edge Cases

| # | Escenario | Por qué es de riesgo | Acción | Resultado esperado | Resultado real | Estado |
|---|-----------|---------------------|--------|-------------------|----------------|--------|
| EC.1 | Proyecto vacío | Sin assets | Ejecutar scan | Mensaje "nada que escanear", sin crash | | ⬜ |
| EC.2 | Assets en readonly | Paquetes en cache de Library | Intentar modificar | Mensaje de error claro, sin excepción | | ⬜ |
| EC.3 | Ventana durante Play Mode | | Abrir tool en Play Mode | [Comportamiento esperado — disabled / functional] | | ⬜ |
| EC.4 | Múltiples ventanas | Abrir dos veces via MenuItem | Una sola instancia (focused) | | ⬜ |
| EC.5 | | | | | | ⬜ |

---

## Capa 3 — Casos NUNCA (bloqueantes)

Estos casos son fail automático si ocurren. No hay escala de grises.

| # | Caso | Verificación |
|---|------|-------------|
| B.1 | Modifica assets sin confirmación explícita del usuario | Intentar trigger de cualquier operación destructiva | ⬜ |
| B.2 | Causa errores de compilación en proyecto limpio | Importar en proyecto recién creado | ⬜ |
| B.3 | Deja errores en consola en estado idle | Abrir Unity con tool instalada, no hacer nada | ⬜ |
| B.4 | Crashea Unity | Ejecución normal de cualquier función | ⬜ |
| B.5 | | | ⬜ |

---

## Capa 4 — Demo Scene

Abrir `KF_<Slug>/Demo/Demo_KF_<Slug>.unity` y verificar:

| # | Lo que debe verse | Estado |
|---|------------------|--------|
| 4.1 | Demo scene carga sin errores | ⬜ |
| 4.2 | [Funcionalidad principal visible/demostrada] | ⬜ |
| 4.3 | README in-demo (si aplica) explica los pasos | ⬜ |
| 4.4 | Demo scene es reproducible por alguien que no conoce el producto | ⬜ |

---

## Resultado final

| Categoría | Estado |
|-----------|--------|
| Tests automáticos | ⬜ Pendiente |
| Integración editor | ⬜ Pendiente |
| Happy path | ⬜ Pendiente |
| Edge cases | ⬜ Pendiente |
| Bloqueantes | ⬜ Pendiente |
| Demo scene | ⬜ Pendiente |

**Decisión final:**
- [ ] ✅ QA PASSED → puede entrar en STORE_PREP
- [ ] ❌ QA FAILED → volver a BUILD con issues listados abajo

**Issues bloqueantes encontrados:**
1. 
2. 

**Issues no bloqueantes (Wave 1.1):**
1. 
