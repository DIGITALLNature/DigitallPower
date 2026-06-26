# Guide: Webresource Solution Membership — Lazy Add Pattern

## Problem

When pushing webresources with `--solution`, the original code called `AddSolutionComponent` for
**every** resource (Create, Update, and Up2Date states) on every push, even if the resource was
already part of the solution. This caused:

- Unnecessary API calls to Dataverse on each push run
- Higher risk of transient failures due to redundant `AddSolutionComponent` calls
- `DiscoverObsoleteWebresources` made a second separate DB query (JOIN) that was identical in scope
  to the upfront load needed for membership checks

## Solution

Solution contents (unmanaged webresources in the target solution) are loaded **once** at the start
of `Process()` via `LoadSolutionContents(solutionId)` — only when `--solution` is set — and passed
to both `UpsertResources` and `DiscoverObsoleteWebresources`.

### Key model

`SolutionWebresourceInfo` (`Model/SolutionWebresourceInfo.cs`) is a `record` carrying:
- `WebResourceId` (Guid) — primary key of the webresource
- `WebResourceType` (int) — OptionSetValue.Value for type comparison
- `Name` (string) — logical name used for obsolescence check

### Add guard logic

`IsAlreadyInSolution(Guid, IReadOnlyCollection<SolutionWebresourceInfo>?)` static helper.

| State | Behaviour |
|-------|-----------|
| Create | Always call `AddSolutionComponent` (new resource, not yet in solution) |
| Update | Only call `AddSolutionComponent` if NOT in `solutionContents` |
| Up2Date | Only call `AddSolutionComponent` if NOT in `solutionContents` |

### Obsolete resource discovery

`DiscoverObsoleteWebresources` previously re-queried `WebResourceSet JOIN SolutionComponentSet`.
After this change it iterates the pre-loaded `solutionContents` directly — no second round trip.

## Load condition

`LoadSolutionContents` is called when `solutionId.HasValue`, which is true whenever `--solution`
is specified. Both `--solution` and `--delete-obsolete` therefore share the same pre-fetched data.
