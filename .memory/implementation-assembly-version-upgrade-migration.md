# Implementation: Assembly Version Upgrade Migration (Issue #91)

## Problem

When a standalone plugin assembly's major/minor version changes (e.g., `1.0.0.0` → `1.1.0.0`), the push command creates a **new** PluginAssembly record with a new GUID. This left references orphaned:

- **Plugin Steps** (SdkMessageProcessingStep) pointed to old PluginType GUIDs → lost on delete
- **Custom API** references pointed to old PluginType GUIDs → broken on delete

## Design Decisions

### Two Assemblies = Two Records (Correct)
In Dataverse, `Name + Version` defines assembly identity. A different major/minor version IS a different assembly. We keep the "create new record" semantics — this is correct and matches Dataverse platform behavior.

### Migration Strategy
The fix adds **reference migration** — updating FK pointers from old types to new types (matched by `TypeName`) before deletion:

| Scenario | Steps | Custom APIs | Old Assembly |
|----------|-------|-------------|--------------|
| Default | Stay on old assembly | Migrated to new | Remains |
| `--delete-on-upgrade` | Migrated to new | Migrated to new | Deleted |
| `--no-migrate-custom-apis` | Stay on old | Stay on old | Remains |

### PowerPlugin vs Non-PowerPlugin Distinction
- **PowerPlugins** (attribute-decorated): Steps are re-created from code attributes (source-of-truth is code). Custom APIs are linked via `CustomApiRegistrationAttribute` in `CreatePluginType()`.
- **Non-PowerPlugins** (manually configured): Steps MUST be migrated because there's no code source to re-create them. Custom APIs are migrated by TypeName matching.

### Packages Don't Need Changes
Plugin Packages (.nupkg) are platform-managed: Dataverse preserves assembly GUIDs when package content is updated. All PluginType and Step references survive automatically.

## Implementation

### New Methods in `AssemblyProcessor.cs`
- `MigratePluginSteps(outdatedAssemblies, newAssembly)` — Updates `SdkMessageProcessingStep.EventHandler` to point to matching new PluginType
- `MigrateCustomApis(outdatedAssemblies, newAssembly)` — Updates `CustomAPI.PluginTypeId` to point to matching new PluginType

### Migration Algorithm
Types are matched between old and new assembly by **fully qualified TypeName** (e.g., `MyNamespace.MyPlugin`). If a type was removed in the new version, a warning is logged and its references are left for the subsequent delete to clean up.

### New CLI Flag
`--no-migrate-custom-apis` — Opt-out from default Custom API migration (ignored when `--delete-on-upgrade` forces it).

## Edge Cases
- Type renamed between versions → no match → warning logged → old references lost on delete
- Type removed in new version → no migration target → warning logged
- `--delete-on-upgrade` + `--no-migrate-custom-apis` → Custom API migration forced anyway (deletion would break references)
