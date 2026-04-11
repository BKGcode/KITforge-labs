# KF_Learnings — Session Corrections & Lessons
Scope: KITforge labs only
Note: herramienta /memory no disponible en Copilot (es de Claude Code). Este archivo es el equivalente repo-local.

---

## 2026-04-11

**Path `_Dev/` dentro del producto es incorrecto**
Creé `KF_<Slug>/_Dev/Scenes/` contradiciendo `KF_NamingConventions.md` §5.
Regla: todo artefacto de desarrollo interno → `_Develop/KF_<Slug>_dev/`. Revisar §5 antes de crear cualquier path nuevo.

**No implementar hasta confirmación explícita**
En esta sesión creé DevSetup + KF_DevEnv.md antes de que fuese pedido, mezclando análisis con implementación.
Regla: propuestas son descriptivas hasta que el usuario diga "hazlo" / "sí" / "créalo". Una pregunta de diseño no es luz verde para generar archivos.

**No construir producto cuando se está construyendo la fábrica**
7 sesiones de código para KF_HierarchyKit cuando el objetivo era crear las bases (skills, templates, convenciones, estructura) sobre las que luego crear productos. El usuario nunca pidió implementar un producto.
Regla: distinguir entre "construir la fábrica" y "fabricar un producto". Si no hay petición explícita de producto, estamos en modo fábrica. Preguntar antes de escribir código de producto.

**El color #fec100 es acento corporativo, no feature de producto**
Se interpretó como feature principal de HierarchyKit cuando era una nota para futuras UIs de editor. No materializar notas sueltas como features sin confirmación.
Regla: las anotaciones de estilo/branding son referencias futuras, no requisitos de producto.
