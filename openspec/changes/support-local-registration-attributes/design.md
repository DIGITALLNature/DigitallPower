# Design

## Context

See proposal.md - Why. The local reader inspects plugin metadata through `CustomAttributeData` in a `MetadataLoadContext`; it currently identifies supported attributes by namespace and defining assembly simple name, without loading or depending on the registration attribute package at compile time.

## Goals / Non-Goals

**Goals:**

- Recognize the official registration library and attribute definitions compiled into the inspected plugin assembly.
- Preserve rejection of same-named attributes defined by unrelated assemblies.
- Apply the same source-assembly rule consistently to plugin-level, Custom API, and data-provider registrations.

**Non-Goals:**

- Accept arbitrary attributes based only on namespace or name.
- Add a dependency on the registration attributes package to the plugin module.
- Change registration parsing or deployment reconciliation behavior.

## Decisions

- Pass the inspected target assembly to the attribute support check and accept a supported attribute only when its namespace is the registration namespace and its defining assembly simple name matches either the official registration library or the target assembly. This keeps the check reflection-only and independent of assembly version.
- Use the same check for attribute presence detection and attribute parsing, so local definitions are not detected by one path but ignored by another.
- Keep unrelated-assembly rejection covered by the existing foreign-attribute tests and add coverage for an attribute defined alongside its plugin type. Retain the official-library behavior as a regression case.

Alternatives considered:

- Accepting attributes by namespace and name alone would also admit unrelated or spoofed attributes, so it does not meet the isolation requirement.
- Comparing only assembly full names or versions would reject local definitions and unnecessarily constrain the supported official library versions.

## Risks / Trade-offs

- A target assembly can define a type with a supported registration name and thereby opt into registration parsing → Mitigate by requiring the exact supported namespace and a known plugin registration attribute name, and by limiting the additional accepted source to the target assembly itself.

## Migration Plan

No migration is required. Existing projects using the official registration library retain their behavior; plugin projects defining supported attributes locally can use `plugin push` after upgrading. Reverting the change restores the prior behavior and causes such local definitions to be treated as undeclared.
