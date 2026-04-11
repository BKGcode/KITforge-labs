# Brief — KF_<ToolSlug>
Status: DRAFT
Phase: BRIEF
Score: XX/35
Tier: Simple | Moderate | Complex
Date created: YYYY-MM-DD
Date approved: —

---

## 0. Identidad

| Campo | Valor |
|-------|-------|
| Slug | `KF_<ToolSlug>` |
| Nombre completo | |
| One-liner | (≤ 12 palabras. Debe poder leerse en un GIF de 3 segundos.) |
| Categoría Asset Store | Tools > Utilities \| Shaders > ... |
| Precio estimado | $XX |
| Tier de complejidad | Simple (1 semana) \| Moderate (2-3 semanas) \| Complex (>3 semanas) |
| Wave | 1 \| 2 \| 3 |

---

## 1. Objetivo

**Problema concreto:**
¿Qué situación frustrante o peligrosa vive el comprador? Quién es exactamente. Con qué frecuencia ocurre. Qué hace ahora para resolverlo (workaround actual).

**Buyer profile:**
- Tipo de dev: (solo indie / TA / pequeño estudio / cualquier studio)
- Momento de uso: (en cada proyecto / ocasionalmente / al buildear / a diario)
- Nivel técnico: (junior / senior / artista / no programador)

**Una sola frase de valor:**
> *"___ permite ___ para que ___ sin tener que ___."*

---

## 2. Solución propuesta

**Enfoque técnico** (máximo 6 bullets):
- 
- 
- 

**APIs de Unity principales involucradas:**
- 

**Explícitamente FUERA de scope:**
- *(Lo que NO hace este producto, aunque parezca relacionado)*

**Posibles extensiones futuras** (no para v1):
- 

---

## 3. Referencias

**Competidores directos:**

| Nombre | Precio | Rating | Lo que hacen bien | Nuestra diferencia |
|--------|--------|--------|------------------|-------------------|
| | | | | |

**Repos open source de referencia** (para estudiar arquitectura, no copiar):
- 

**Documentación de Unity relevante:**
- 

**Inspiraciones de UX** (herramientas ajenas, incluso fuera de Unity):
- 

---

## 4. Mejoras vs competencia

Por qué alguien elegiría esto sobre lo que ya existe. Una frase por entrada.

- **Vs [Competidor A]:** 
- **Vs workaround manual:** 
- **Ángulo único:** 

---

## 5. Posibles dificultades

Lista de riesgos técnicos identificados ANTES de empezar a codear.

| Riesgo | Probabilidad | Impacto | Mitigación |
|--------|-------------|---------|------------|
| | Media | Alto | |

**Compatibilidad de Unity:**
- Versión mínima: Unity X.X
- ¿URP requerido? Sí / No / Opcional
- ¿Algún package opcional que afecte funcionalidad? 

**Casos límite peligrosos:**
- 
- 

---

## 6. UI Design

**Tipo de interfaz:**
☐ EditorWindow standalone  
☐ CustomEditor / Inspector extension  
☐ PropertyDrawer(s)  
☐ MenuItem / ContextMenu only  
☐ Overlay (Scene View)  
☐ Runtime HUD / MonoBehaviour  
☐ Post-process Volume component  

**Sistema de UI:**
☐ UI Toolkit (UXML+USS) — preferido para Unity 6  
☐ IMGUI — solo si hay razón técnica concreta  

**Wireframe / sketch (ASCII o descripción):**
```
┌─────────────────────────────────────────┐
│  KF_<Slug> — Título de ventana          │
├─────────────────────────────────────────┤
│  [Acción principal]    [Acción 2]       │
│                                         │
│  ┌─────────────────────────────────┐   │
│  │ Lista / contenido principal     │   │
│  │                                 │   │
│  └─────────────────────────────────┘   │
│                                         │
│  Estado: ____  [Botón primario]         │
└─────────────────────────────────────────┘
```

**Notas de UI:**
- 

---

## 7. UX Flow

Describe el recorrido real del comprador, no el técnico.

**Primer uso** (tras importar el package, sin saber nada):
1. 
2. 
3. *→ Primera percepción de valor aquí*

**Uso diario** (usuario que la usa regularmente):
1. 
2. 

**Uso avanzado** (usuario power):
1. 
2. 

**Momento "aha":** *(Cuándo el usuario dice "vale, esto es útil" — debe ocurrir en < 2 minutos desde import)*

---

## 8. Checkpoints

Lista de hitos medibles durante BUILD. Cada uno tiene criterio de done observable.

Cuando todos los checkpoints están en ✅ → el producto entra en QA.

| # | Checkpoint | Criterio de done |
|---|-----------|-----------------|
| 1 | Ventana/panel abre | MenuItem visible, ventana se abre sin errores |
| 2 | | |
| 3 | | |
| 4 | | |
| 5 | Demo scene funciona | Demo scene carga, herramienta funciona end-to-end |
| N | Zero console errors | Sin errores ni warnings en consola en estado idle |

---

## 9. Validador / QA

**Cómo verificamos que funciona:**
- 

**Casos que NUNCA deben ocurrir** (si ocurren → bloqueante):
- No debe modificar assets sin confirmación explícita del usuario.
- No debe causar errores de compilación en proyectos limpios.
- No debe hacer nada en Play Mode que no haga en Edit Mode (a menos que sea intencional).
- 

**Datos de test necesarios** (qué "estado de proyecto" se necesita para probar):
- 

**Demo scene** — qué debe demostrar `Demo/Demo_KF_<Slug>.unity`:
- 

---

## 10. Tests

**Lógica unit-testeable** (puro C#, sin Unity context):
- *(funciones de parsing, formateo, validación de rutas, lógica de filtros, etc.)*

**Tests de integración editor** (requieren AssetDatabase, EditModeTests):
- 

**Tests manuales** (documentados en TestPlan.md):
- UX flow completo del primer uso
- Survival test: domain reload no rompe estado
- Survival test: compilación en proyecto limpio
- 

**Localización del código de tests:**
`Assets/Plugins/KITforge Labs/_Develop/Tests/KF_<Slug>_Tests/`

---

## 11. Ejemplos de uso

Tres escenarios concretos. No hipotéticos — escenarios que un comprador real viviría el día 1.

**Escenario A — [Tipo de usuario]:**
> *Situación:* 
> *Acción:* 
> *Resultado:* 

**Escenario B — [Tipo de usuario]:**
> *Situación:* 
> *Acción:* 
> *Resultado:* 

**Escenario C — [Tipo de usuario]:**
> *Situación:* 
> *Acción:* 
> *Resultado:* 

---

## Approval checklist

Antes de cambiar Status a APPROVED, confirmar:

- [ ] Todas las secciones rellenas (ninguna vacía o con placeholder)
- [ ] One-liner tiene ≤ 12 palabras y explica el valor sin ver el resto
- [ ] Scope fuera está explícitamente listado
- [ ] Al menos 1 competidor directo comparado
- [ ] Al menos 2 riesgos técnicos identificados con mitigación
- [ ] Wireframe sketch presente (aunque sea ASCII)
- [ ] UX flow tiene el "momento aha" identificado
- [ ] Todos los checkpoints tienen criterio de done observable
- [ ] Al menos 2 casos bloqueantes en la sección Validador
- [ ] Los 3 ejemplos de uso son escenarios reales, no genéricos
