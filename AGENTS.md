# Agent Guidelines

Instructions for all AI agents (GitHub Copilot, Claude, Cursor, etc.) working on this repository.

---

## Documentation Maintenance (MANDATORY)

When making changes to this codebase, **you MUST keep the documentation up to date**. This is not optional.

### Rules

1. **README.md must reflect the current state of the project.** After any change that affects the public API, architecture, configuration, project structure, or usage patterns, update the corresponding section(s) in `README.md`.

2. **What requires a README update:**
   - Adding, removing, or renaming public classes, interfaces, or methods
   - Adding or removing NuGet dependencies
   - Adding new Organization Request fakes
   - Changing configuration/environment variables
   - Modifying the builder APIs or fluent extensions
   - Adding new folders or restructuring the project
   - Changing build/test commands or CI/CD workflows
   - Adding new features or capabilities

3. **What does NOT require a README update:**
   - Internal refactoring that doesn't change the public API
   - Bug fixes that don't change behavior or usage
   - Test-only changes
   - Code style / formatting changes

4. **CHANGELOG.md is auto-generated** by semantic-release. Do NOT edit it manually.

5. **`baseline.sarif.json` is maintained by Qodana.** Do NOT edit it manually. If Qodana reports new findings, fix the code — never suppress findings by modifying the baseline file.

6. **Keep documentation in English.** All documentation in this repository is written in English.

### Documentation Style

- Use concise, technical language
- Include code examples for new public APIs
- Keep the table of contents in sync with the actual sections
- Use tables for listing related items (request fakes, config vars, etc.)
- Architecture diagrams use ASCII art (no external dependencies)

---

## Code Conventions

- **Language:** C# with latest LangVersion, nullable enabled, implicit usings
- **Target Framework:** net10.0
- **Naming:** Follow standard .NET naming conventions (PascalCase for public members)
- **File naming:** Each `.cs` file must contain exactly one top-level type, and the file name must match the type name (e.g. `MyService.cs` → `class MyService`). When renaming a type, rename the file too.
- **Licensing header:** All source files start with `// Copyright (c) DIGITALL Nature. All rights reserved`
- **Tests:** Use TUnit framework with TUnit.Assertions and TUnit.Mocks

### Async Migration Checklist (MANDATORY for Logic changes)

When **modifying or adding** any `PowerLogic<T>` subclass, **always check**:

> ❓ Can this class be migrated from `Task.FromResult(InvokeCore(...))` to a true `async/await` implementation using `IOrganizationServiceAsync2`?

- If **yes**: migrate in the same PR. Replace `((IOrganizationService)Connection).Execute(...)` with `await ((IOrganizationServiceAsync2)Connection).ExecuteAsync(...)`, replace LINQ `DataContext` queries with `QueryExpression` + `RetrieveMultipleAsync`.
- If **no** (e.g., touching unrelated logic, or migration is too large for the current scope): leave a `// TODO(async): migrate to IOrganizationServiceAsync2` comment and add an entry to `todo.md`.

**Pattern for truly async logic:**
```csharp
protected override async Task<bool> InvokeAsync(TVerb args, CancellationToken cancellationToken)
{
    var orgAsync = (IOrganizationServiceAsync2)Connection;
    var response = await orgAsync.ExecuteAsync(new RetrieveMultipleRequest { Query = query }, cancellationToken);
    // ...
}
```

See `todo.md` for full migration backlog and module-by-module scope.

### JSON Schema Maintenance (MANDATORY for Config changes)

When **modifying any configuration model class** that has a corresponding JSON schema in `schemas/`, **you MUST update the matching JSON schema file** in the same PR.

#### Rules

1. **Schema files live in `schemas/`** and are organized by module and version, e.g.:
   - `schemas/codegeneration/v1/schema.json` — V1 legacy config
   - `schemas/codegeneration/v2/dotnet.schema.json` — V2 .NET config
   - `schemas/codegeneration/v2/typescript.schema.json` — V2 TypeScript config
   - `schemas/analyzer/schema.json`, `schemas/push/schema.json`, `schemas/maintenance/…`

2. **What requires a schema update:**
   - Adding, removing, or renaming a config property
   - Changing a property type (e.g. `int` → `int?`, `bool` → `string`)
   - Changing a default value
   - Adding or removing enum values
   - Adding or modifying nested objects / `$defs`
   - Changing validation constraints (required fields, allowed values, patterns)

3. **Keep schema and C# model in sync.** Property names in the schema use camelCase (JSON convention) matching the serialized output of the C# model.

4. **Versioning:** Breaking schema changes (removing properties, changing types in incompatible ways, renaming properties) require a new schema version folder (e.g. `v3/`). Additive changes (new optional properties, relaxing constraints) can be made in-place within the current version.

5. **Validate after changes.** Ensure existing sample/test config files still validate against the updated schema.

---

## Commit Messages

This project uses [Conventional Commits](https://www.conventionalcommits.org/) enforced by commitlint + Husky.

### Format

```
<type>(<scope>): <short description>

[optional body]

[optional footer(s)]
```

### Types

| Type | When to use | Version bump |
|------|-------------|--------------|
| `feat` | New feature or capability | minor |
| `fix` | Bug fix | patch |
| `docs` | Documentation only | none |
| `refactor` | Code change that neither fixes a bug nor adds a feature | none |
| `perf` | Performance improvement | patch |
| `test` | Adding or updating tests only | none |
| `chore` | Tooling, CI, dependencies, config | none |
| `style` | Formatting, white-space, etc. (no logic change) | none |

### Rules

- **Subject line:** imperative mood, lowercase, no period at end, max 100 chars
- **Breaking changes:** Add `!` after type/scope (e.g. `feat!: remove deprecated API`) or add `BREAKING CHANGE:` footer
- **Scope:** optional, use the affected component (e.g. `feat(query): add fiscal year grouping`)

### Examples

```
feat: add BulkUpsert organization request fake
fix(query): correct paging cookie generation for empty results
docs: update README with relationship management section
refactor: extract condition parsing into dedicated class
test: add coverage for FetchXml aggregate queries
chore: bump Microsoft.PowerPlatform.Dataverse.Client to 1.2.10
feat!: remove deprecated ModelAssemblies property
```

---

## Testing (MANDATORY)

Every code change that modifies behavior **must** be accompanied by tests.

### Rules

1. **New features:** Write tests that cover the happy path and relevant edge cases.
2. **Bug fixes:** Write a test that reproduces the bug before fixing it (test-first when feasible).
3. **Refactoring:** Ensure existing tests still pass. Add tests if coverage gaps are discovered.
4. **Deleted functionality:** Remove or update tests that cover the removed code.

### Test Location

- Tests live in `tests/dgt.power.*.tests/` — one project per source module, e.g.:
  - `tests/dgt.power.maintenance.tests/`
  - `tests/dgt.power.export.tests/`
  - `tests/dgt.power.import.tests/`
  - `tests/dgt.power.analyzer.tests/`
  - `tests/dgt.power.codegeneration.tests/`
  - `tests/dgt.power.push.tests/`
  - `tests/dgt.power.profile.tests/`
  - `tests/dgt.power.telemetry.tests/`
  - `tests/dgt.power.tests/` (shared test helpers / base classes)
- Mirror the source folder structure within each test project (e.g. `Logic/` tests go in `tests/dgt.power.<module>.tests/`)
- Test class naming: `<ClassUnderTest>Tests.cs`

### Test Style

```csharp
[Test]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange
    // Act
    // Assert
}
```

---

## Build & Test Commands

```bash
dotnet restore              # Restore dependencies (uses lock files)
dotnet build                # Build the solution
dotnet test                 # Run all tests
dotnet test --project tests/dgt.power.<module>.tests/dgt.power.<module>.tests.csproj  # Run single module
dotnet test --project tests/dgt.power.<module>.tests/dgt.power.<module>.tests.csproj --treenode-filter "/<assembly>/<namespace>/<Class>/<Method>"  # Run specific test
```

---

## RTK — Token-Optimized CLI

**rtk** is a CLI proxy that filters and compresses command outputs, saving 60-90% tokens.

**Always prefix shell commands with `rtk`** when available. It passes through unchanged if no filter exists — always safe to use.

```bash
rtk git status              # Compact status
rtk git diff                # Compact diff
rtk dotnet build            # Filtered build output
rtk dotnet test             # Failures only
```

Even in command chains:
```bash
rtk git add . && rtk git commit -m "msg" && rtk git push
```

---

## Knowledge Persistence (.memory/)

Agents **must** persist valuable findings, decisions, and context in the `.memory/` directory so that knowledge survives across sessions and is available to other agents and developers.

> ⚠️ **This is not optional.** Skipping `.memory/` updates is a failure to complete the task.
> Future agents and developers depend on this knowledge. Treat it with the same discipline as code.

### Core Principle: Timeless, Not Transient

**`.memory/` is a durable knowledge base — not a sprint log, not a branch diary.**

It records **why** decisions were made, **what** patterns apply, and **which** caveats exist in the codebase. This knowledge remains correct regardless of which branch, PR, or release is currently active.

**The merge test:** Before writing anything into `.memory/`, ask: _"Would this still be accurate and useful after the branch is merged?"_ If the answer is no, it does not belong in `.memory/`.

### Explicitly Forbidden in .memory/

❌ **Commit hashes** — stale the moment history is amended
❌ **Branch names as content** (e.g. "Implemented on branch `chore-analysis`") — irrelevant after merge
❌ **"Next Steps" for a PR or merge** — use the PR description for that
❌ **Sprint / task progress tracking** — use session tools or a task tracker
❌ **"Status: Implemented / In Progress"** headers tied to a branch — state facts about the *codebase*, not the *git graph*

### Rules

1. **Read first — always.** Read `.memory/summary.md` at the start of every task to understand current architecture, key decisions, and known caveats. Do not start work without it.

2. **Write at the end — always.** After any non-trivial investigation, design decision, or fix:
   - Create or update the relevant `.memory/<type>-<title>.md` file.
   - Update `.memory/summary.md` to reflect the new architectural state.

3. **Co-stage `.memory/` with code.** Changes to `.memory/` MUST be committed together with the code change that triggered them — either in the same commit or as an immediate follow-up in the same PR. Never let `.memory/` lag behind the code.

4. **Filename convention:** `.memory/<type>-<title>.md`
   - `<type>` is one of: `research`, `guide`, `decision`, `implementation`
   - `<title>` is a short kebab-case descriptor

5. **Exception:** `.memory/summary.md` does not follow the naming convention — it is the index file.

6. **`summary.md` describes the codebase, not the branch.** It must always reflect:
   - Architecture overview (modules, frameworks, key patterns)
   - Key decisions (with links to decision files)
   - Known caveats and technical debt (timeless observations)
   - Memory files index

7. **Prune stale information.** When caveats are resolved or decisions are superseded, update or remove them. `summary.md` must stay current and accurate — not grow into a historical archive.

### Mandatory Write Triggers

These situations **require** a `.memory/` update — no exceptions:

| Situation | What to write |
|-----------|--------------|
| Architecture or design decision made | `decision-<title>.md` + update `summary.md` |
| Non-obvious API behavior discovered | `research-<title>.md` + add caveat to `summary.md` |
| Recurring anti-pattern found and fixed | `guide-<title>.md` + add to `summary.md` index |
| Implementation spec agreed upon | `implementation-<title>.md` + update `summary.md` |

### Types

| Type | Purpose |
|------|---------|
| `research` | Investigation results, API behavior findings, library evaluations |
| `guide` | How-to instructions, reusable patterns, and recurring anti-pattern fixes |
| `decision` | Architecture/design decisions with rationale and alternatives considered |
| `implementation` | Implementation plans, technical specs, or post-implementation notes |

### When to Write

- After discovering non-obvious behavior or caveats
- After making a design/architecture decision
- When findings would save a future agent significant research time

### When NOT to Write

- Trivial or self-evident facts already in the code
- Temporary debugging notes (use session memory instead)
- Information already covered in `README.md` or code comments
- Sprint/workflow state that belongs in a PR description or ticket

### Example Filenames

```
.memory/summary.md
.memory/research-fetchxml-paging-behavior.md
.memory/decision-tunit-over-xunit.md
.memory/guide-bulk-operation-patterns.md
.memory/implementation-audit-export-logic.md
```
