# Custom API handler unlink does not propagate through solution import

## Finding

Clearing the plugin type link of a Custom API in Dev (via `plugin push` or manually) and
importing the managed solution into a downstream environment leaves the old link in place.
Solution import only writes attributes present in the exported component, and a cleared lookup is
omitted. Importing as a single-step upgrade and publishing all in Dev do not change this.

## Consequences

- Deleting the linked plugin type or assembly in a later release fails on import with
  "cannot be deleted because it is referenced by 1 other components". The dependency list
  (`RetrieveDependenciesForDelete`) names the assembly's steps and the Custom API.
- Removing steps by import alone works; removing the handler link does not.
- Users must unlink the handler manually in each downstream environment before the release that
  removes the type or assembly. This is documented in the README section "Removals and solution
  import".

## Related e2e observations

- Package assemblies are updated in place: the `pluginassemblyid` stayed identical across assembly
  versions 1.1.0.0, 1.2.0.0 and 2.0.0.0 in Dev. Unlike standalone assemblies, a major or minor
  bump never replaces the assembly, so it does not help removals.
- Removing a package type that still has a step downstream fails on managed import with "Unable to
  delete plugintype due to 1 step(s) registered on it". The step must be removed in an earlier
  release or manually downstream.
- Standalone assembly removal is not supported by the CLI; deleting the assembly in Dev and
  importing fails while steps or the Custom API link still exist downstream.
- Changing a step's identity fields (stage, mode, rank) creates a new step and deletes the old one;
  changing filtering attributes or images updates in place.
- The preview's destructive count includes the Custom API relink in addition to the listed
  deletions.

See `docs/design/plugin-e2e-test-plan.md` for the case matrix.
