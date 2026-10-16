# Custom API Unlink and Solution Import

Observed behavior from the plugin end-to-end runs (see `plugin-e2e-test-plan.md`). User-facing guidance is in the README section "Removals and solution import".

## Finding

Clearing the plugin type link of a Custom API in Dev, through `plugin push` or manually, and importing the managed solution downstream leaves the old link in place. Solution import writes only attributes present in the exported component, and a cleared lookup is omitted. A single-step upgrade import and publishing all in Dev do not change this.

## Consequences

- Deleting the linked plugin type or assembly in a later release fails on import with "cannot be deleted because it is referenced by 1 other components". `RetrieveDependenciesForDelete` names the assembly's steps and the Custom API.
- Removing steps by import alone works; removing the handler link does not. The link must be cleared manually in each downstream environment before the release that removes the type or assembly.
- Removing a package type that still has a step downstream fails on managed import with "Unable to delete plugintype due to 1 step(s) registered on it". Remove the step in an earlier release or manually downstream.
- Package assemblies are updated in place: `pluginassemblyid` stayed identical across versions 1.1.0.0, 1.2.0.0 and 2.0.0.0, so a major or minor bump never replaces a package assembly and does not help removals.
- Standalone assembly removal is not supported by the CLI: deleting the assembly in Dev and importing fails while steps or the Custom API link still exist downstream.
- Changing a step's identity fields (message, entity, stage, mode) creates a new step and deletes the old one; changing rank, filtering attributes or images updates in place.
- The preview's destructive count includes the Custom API relink in addition to the listed deletions.
