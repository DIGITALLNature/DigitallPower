# Plugin module end-to-end test plan

## Goal

Establish a Dev state that imports into downstream environments **without manual steps**
(no deleting steps, types or assemblies by hand). Every scenario therefore has two halves:

1. **Dev**: run `dgtp plugin push` (standalone or package) against the Dev environment.
2. **Import**: add the result to a solution, export it as **managed**, and import it into a
   clean-slate **Target** environment (and, for upgrades, into a Target that already holds the
   previous release).

Solution import does not run our pre-upload cleanup, so removals must work from the raw
Dataverse behaviour alone.

## Pass criteria (every case)

- Push preview and output match the expectation. A second push reports "up to date".
- Managed export and import succeed with no errors and no manual intervention.
- Target state equals Dev state: assemblies, plugin types, steps, images, Custom API links and
  data providers (compare by name and count).
- No orphans remain in Target (types, steps or assemblies that no longer exist in Dev).
- Re-importing the same managed solution is a no-op.

## Environments

| Env | Purpose |
|-----|---------|
| Dev | Unmanaged. All pushes run here. |
| Target-A | Clean. Receives the first release (fresh install). |
| Target-B | Seeded with release N-1 through managed import. Receives release N (upgrade). |

Reset Target-B to release N-1 before each upgrade case. Keep one solution per scenario family
to keep exports small.

## Procedure per case

1. Reset Dev and Target-B to release N-1 (managed import for Target-B).
2. Change the plugin code, then push to Dev. Record the preview and the result.
3. Run the push again and confirm "up to date".
4. Bump the solution version, then export managed.
5. Import into Target-B (upgrade) and, for create cases, into Target-A.
6. Compare the states and note the result. Record any Dataverse error code verbatim.

Status: `V` verified, `F` failed, `-` not run, `?` open.

## A. Standalone assemblies

| ID | Scenario (release N vs N-1) | Dev push expectation | Import expectation | Status |
|----|-----------------------------|----------------------|--------------------|--------|
| S1 | Create assembly with types and steps | Create assembly, types, steps | Fresh install in Target-A | V |
| S2 | Unchanged re-push | Up to date | n/a | V |
| S3 | Content change (build or revision bump) | Update assembly | Upgrade in place; steps survive | V |
| S4 | Add a type with step | Create type and step | Type and step appear | V |
| S5 | Change step (stage, mode, filtering attributes, rank) | Update step | Step updated | V (filtering attributes update in place; stage or rank change replaces the step) |
| S6 | Change image | Update image | Image updated | V |
| S7 | Remove a step, keep the type | Delete step | Step removed, no leftovers | V |
| S8 | Remove a type with steps, same version | Rejected at prepare with version bump hint | n/a | V |
| S9 | Remove a type, major or minor bump | New assembly created, old deleted with types and steps | Old assembly removed in Target-B without manual deletes | V |
| S10 | Major or minor bump, types unchanged | Replacement path: steps moved | Upgrade works; no duplicated steps | V |
| S11 | Remove the whole assembly | Not supported by the CLI for standalone DLLs; delete the assembly in Dev manually | Assembly and steps removed by import alone | F (Test import: assembly delete blocked by 2 steps and the Custom API link) |
| S11b | Staged removal: release N deletes steps and unlinks the Custom API, release N+1 deletes the assembly | Manual edits in Dev | Steps removed; Custom API link stays in Test (documented limitation, manual unlink needed); assembly removed by the next import after the manual unlink | V |
| S12 | Custom API link | Link handler type | Link present in Target | V |
| S13 | Custom API handler moves to another type (rename and bump) | Update link; old assembly deleted | Link points to new type | V |
| S14 | Custom API loses its handler | Unlink, then delete type | No dangling link in Target | F (Dev OK; Test import blocked until the link is removed manually, see README). With the manual unlink the import succeeds and Target matches Dev |
| S15 | Data provider add, change, remove | Matches preview | Matches Dev | ? (skipped: no working data provider example) |
| S16 | Retry after a partial failure | Resumes cleanly | n/a | V |

## B. Packages

| ID | Scenario | Dev push expectation | Import expectation | Status |
|----|----------|----------------------|--------------------|--------|
| P1 | Create package | Upload creates assembly and types; tool registers steps | Fresh install in Target-A | V |
| P2 | Unchanged re-push | Up to date | n/a | V |
| P3 | Update step or image | Update | Updated in Target-B | V |
| P4 | Add a type with step | Create | Appears | V |
| P5 | Remove a type with steps | Steps deleted, then upload removes type | Import alone removes type and steps | F (Dev OK; Test import fails: "Unable to delete plugintype due to 1 step(s) registered on it"; with the step deleted manually in Test first, the import succeeds and the type is removed; a major or minor assembly version bump in the package does not change this; the assembly ID stays the same across 1.1.0.0, 1.2.0.0 and 2.0.0.0, so Dataverse updates the package assembly in place) |
| P6 | Remove a stepless type | Upload removes type | Import alone removes type | F (Dev OK; Test import fails: "Unable to delete plugintype due to 1 step(s) registered on it", caused by the Custom API link; with a manual unlink in Test first, the import succeeds and the type is removed) |
| P7 | Add a second assembly | Create | Appears | V (Dev) |
| P8 | Remove an assembly with steps | Steps deleted, upload removes assembly | Import alone removes assembly and steps | V (Dev) |
| P9 | Remove an assembly without steps | Upload removes assembly | Import alone removes assembly | - |
| P10 | New package version | Rejected by Dataverse (verify message) | Import of a new package version as upgrade | ? |
| P11 | Custom API link and unlink in a package | Link, update, unlink match preview | Same in Target | - |
| P12 | Type moved between assemblies in one package | Verify behaviour | Verify | ? |

## C. Managed import without our cleanup (decision cases)

These isolate raw platform behaviour. For each, remove the object in Dev **without** running
the tool's pre-cleanup (use the maintenance UI or an unmanaged solution edit), then import.
Alternatively compare to the tool path in A and B.

| ID | Removal | Expected question | Status |
|----|---------|-------------------|--------|
| M1 | Standalone type without steps (version bump) | Does import delete the old assembly? | - |
| M2 | Package type with steps | Does import fail with `0x80040265`? | F (observed in P5: import fails with "Unable to delete plugintype due to 1 step(s) registered on it"; no error code captured) |
| M3 | Package assembly with steps | Does Dataverse cascade the steps? | ? |
| M4 | Package type without steps | Removed cleanly? | - |
| M5 | Package assembly without steps | Removed cleanly? | - |
| M6 | Build or revision bump of a standalone assembly | In-place upgrade keeps steps? | - |
| M7 | Major or minor bump of a standalone assembly | New assembly beside old; are old steps left? | ? |
| M8 | Custom API handler change | Link updated, no dangling reference? | - |

**Decision rule:** if M2, M3 or M5 fail, removals need a staged rollout. Release N removes the
steps, release N+1 removes the type or assembly. Document this as a user rule rather than
changing the tool.

## D. Cross-cutting

| ID | Scenario | Expectation | Status |
|----|----------|-------------|--------|
| X1 | Several targets in one run | One preview, one confirmation, one apply | - |
| X2 | Two targets conflict on the same assembly or package | Preflight reports the conflict | - |
| X3 | Declined confirmation | No writes | - |
| X4 | Destructive count in the preview | Counts steps, types and assemblies | - |
| X5 | CI mode (no prompt) | Applies without prompting | - |
| X6 | Error paths (missing file, invalid package) | Clear message, no writes | - |

## Reporting

After the run, update the status columns here, the design doc, and `.memory\summary.md`.
Record every Dataverse error code verbatim, and the exact import behaviour for C cases.
