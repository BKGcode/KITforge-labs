# KF_BriefRules — Cómo escribir un Brief aprobable
Scope: KITforge labs only
Last updated: 2026-04-11

Un Brief no aprobado = no se empieza el producto. Este doc explica cuándo un Brief está listo.

---

## Criterios de aprobación por sección

### Sección 0 — Identidad
- **Aprobado si:** One-liner tiene ≤ 12 palabras, explica el valor sin ver el resto del doc.
- **Rechazado si:** One-liner es técnico ("Tool that hooks into Unity hierarchy callbacks") en vez de orientado a valor ("Clean visual hierarchy for any Unity project in seconds").

### Sección 1 — Objetivo
- **Aprobado si:** El problema está descrito como una situación concreta que le pasa a alguien real, no como una feature abstracta.
- **Rechazado si:** El objetivo empieza por "Una tool que..." en vez de "Los devs pierden tiempo porque...".
- **Test:** ¿Puedes describir a una persona real usando esto mal porque no tiene la tool? Si no → el problema no es concreto.

### Sección 2 — Solución
- **Aprobado si:** La sección "Fuera de scope" tiene al menos 3 entradas. El scope reducido es señal de madurez, no de limitación.
- **Rechazado si:** No hay nada en fuera de scope. Significa que el scope no está pensado.

### Sección 3 — Referencias
- **Aprobado si:** Hay al menos un competidor con análisis honesto de lo que hace BIEN. No queremos solo atacar, queremos entender.
- **Rechazado si:** La columna "Lo que hacen bien" está vacía o dice "nada".

### Sección 4 — Mejoras
- **Aprobado si:** Hay al menos un ángulo que no sea "más barato" o "más simple". Precio no es diferenciación duradera.
- **Rechazado si:** Todos los ángulos se reducen a precio o número de features.

### Sección 5 — Dificultades
- **Aprobado si:** Hay al menos un riesgo de compatibilidad de Unity versión identificado.
- **Rechazado si:** "Ninguno conocido" — en editor tools siempre hay riesgos de domain reload, versión, o API deprecation.

### Sección 6 — UI Design
- **Aprobado si:** Hay un sketch ASCII o descripción de la ventana con al menos los elementos principales nombrados.
- **Rechazado si:** "Será una ventana simple" sin descripción de contenido.

### Sección 7 — UX Flow
- **Aprobado si:** El "momento aha" está identificado y ocurre en < 2 minutos desde import.
- **Rechazado si:** El flow asume que el usuario ya sabe usar el producto. El primer uso es para alguien que no sabe nada.

### Sección 8 — Checkpoints
- **Aprobado si:** Cada checkpoint tiene un criterio observable sin interpretación ("ventana abre sin errores en consola" sí, "ventana funciona bien" no).
- **Rechazado si:** Checkpoints genéricos del tipo "implementar funcionalidad X".

### Sección 9 — Validador / QA
- **Aprobado si:** Hay al menos 2 casos en la lista "NUNCA debe ocurrir". Este lista protege contra reviews negativos.
- **Rechazado si:** No hay lista de casos bloqueantes explícitos.

### Sección 10 — Tests
- **Aprobado si:** Hay al menos 1 pieza de lógica que se puede testear como función pura (sin UI, sin Unity context).
- **Rechazado si:** "Todo requiere test manual". En toda editor tool hay al menos lógica de validación o parseo testeable.

### Sección 11 — Ejemplos
- **Aprobado si:** Los 3 ejemplos son diferentes entre sí (tipo de usuario diferente o contexto diferente) y cada uno menciona el resultado concreto.
- **Rechazado si:** Los ejemplos son variaciones del mismo escenario o no dicen qué obtiene el usuario.

---

## Kill criteria para un Brief completo

Aunque todas las secciones estén rellenas, el Brief se rechaza si:

1. **Imposible de demostrar en 30 segundos.** Si el demo requiere 5 minutos de setup, las ventas serán difíciles.
2. **Depende de APIs internas de Unity sin alternativa.** APIs que cambian entre minor versions son una bomba de soporte.
3. **Solo útil para un tipo de proyecto muy específico.** "Solo para RPGs con A* de cierta versión" → público demasiado pequeño.
4. **Scope que escala infinitamente.** Si la sección 2 no cierra el scope, cada feature pide otra. Síntoma de producto no maduro.
5. **El one-liner necesita 2 frases para explicarse.** Si en una frase no queda claro, el store page copy será imposible.

---

## Tier de complejidad — definición

| Tier | Criterio | Tiempo estimado |
|------|---------|-----------------|
| Simple | 1 clase principal + UI sencilla. Sin AssetDatabase ops destructivas. Sin domain reload state. | 1 semana |
| Moderate | 2-3 clases. UI compleja o múltiples features. AssetDatabase reads. Domain reload state. | 2-3 semanas |
| Complex | 4+ clases. Análisis de proyecto profundo. Operaciones destructivas con confirmación. URP shaders. | 3+ semanas |

Escalar el tier hacia arriba si: hay assets runtime completos, demo scene con muchos assets, o URP compatibility matrix requerida.

---

## Errores comunes en Briefs de editor tools

- Confundir EditorWindow con CustomEditor. Si el tool es para un asset específico → CustomEditor. Si es global → EditorWindow.
- Olvidar que el Play Mode cambia el estado del editor. ¿Qué hace la tool si el usuario la abre en Play Mode?
- No considerar proyectos con scripting backend IL2CPP. Algunas APIs de reflection no funcionan.
- Diseñar la UI para pantallas grandes. El Inspector tiene 300px de ancho. Las EditorWindows son redimensionables — diseñar para pequeño primero.
- No especificar si los datos de la tool van versionados con el proyecto (Assets/) o son locales al editor (EditorPrefs / Library/).
