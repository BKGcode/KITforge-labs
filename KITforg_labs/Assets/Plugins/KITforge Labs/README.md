# KITforge labs — Unity Store Product Lab

Purpose-built Unity tools for real workflows.

This Unity project is the internal lab for commercial Asset Store products published under **KITforge labs**. It is not a generic sandbox.

## Structure

- `_Lab/` — internal research, product briefs, templates, packaging checklists
- `_Shared/` — internal reusable building blocks used during development
- `KF_<ToolSlug>/` — one shippable product root per tool or shader

## Core rules

1. Every sellable package lives under its own root folder.
2. Every tool or shader ships with a simple, practical `README.md`.
3. Every package must show value quickly with a demo scene or sample workflow.
4. No hidden cross-package dependencies unless we intentionally publish a shared core.
5. Prefer focused products over mega-suites.

## Product folder template

```text
Assets/Plugins/KITforge Labs/KF_<ToolSlug>/
  Editor/
  Runtime/
  Shaders/
  Demo/
  Documentation/
  README.md
  Third-Party Notices.txt
```

## Working principle

Solve one repeated, awkward, or error-prone Unity workflow at a time.

Good examples:
- safer project cleanup
- scene/prefab validation
- inspector workflow improvements
- capture/export automation
- small but high-value URP shader packs

## Packaging target

Each product should be:
- self-contained
- understandable in under 5 minutes
- usable after a short setup
- clean enough for Asset Store review

See `_Lab/Research/MarketReference.md` for market signals and `_Lab/Templates/ToolREADME.Template.md` for the standard product README format.
