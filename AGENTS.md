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

3. **Major-version migration guides:** Every user-affecting breaking change in a major release
   must be documented in the corresponding `docs/migrations/<from>-to-<to>.md` guide (for example,
   `2.x-to-3.x.md`). Keep the README focused on current behavior and add a prominent link near the
   beginning to the migration guides directory. Internal-only breaking changes do not require
   migration guidance.

4. **What does NOT require a README update:**
   - Internal refactoring that doesn't change the public API
   - Bug fixes that don't change behavior or usage
   - Test-only changes
   - Code style / formatting changes

5. **CHANGELOG.md is auto-generated** by semantic-release. Do NOT edit it manually.

6. **`baseline.sarif.json` is maintained by Qodana.** Do NOT edit it manually. If Qodana reports new findings, fix the code — never suppress findings by modifying the baseline file.

7. **Keep documentation in English.** All documentation in this repository is written in English.

### Documentation Style

- Use concise, technical language
- Include code examples for new public APIs
- Keep the table of contents in sync with the actual sections
- Use tables for listing related items (request fakes, config vars, etc.)
- Architecture diagrams use ASCII art (no external dependencies)

---

## Fixing Qodana Issues (MANDATORY before merge)

Qodana runs on every PR. New findings (not in `baseline.sarif.json`) block the merge.

### Workflow

1. **Obtain Qodana SARIF** from the user or CI; downloading the relevant CI artifact is always allowed.

2. **Identify new-only findings** by filtering on `baselineState == "new"` in the SARIF — Qodana sets this authoritatively against the repo baseline. Do not diff fingerprints manually.

3. **Fix the code** — see rule patterns below.

4. **Never edit `baseline.sarif.json` manually.** The baseline is maintained by Qodana. Suppressing findings by modifying the baseline file is prohibited.

5. **Verify** with `dotnet build` — ensure 0 errors before committing.

### Common Fix Patterns

| Rule | Fix |
|------|-----|
| `CA1062` | Add `ArgumentNullException.ThrowIfNull(param);` at top of method |
| `AutoPropertyCanBeMadeGetOnly.Global` | Add `// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global` or suppress at class level if many properties are affected |
| `UnusedMember.Global` | Add `// ReSharper disable once UnusedMember.Global` above the member (test helpers, builder methods used indirectly) |
| `MergeIntoPattern` | Convert `if (x.A && x.B != null)` to `if (x is { A: true, B: not null })` |
| `BadControlBracesIndent` | Fix indentation of the flagged line to align with the surrounding block |
| `CA1056` / `S3996` | Change property type to `Uri`, or suppress with `#pragma warning disable CA1056, S3996` + comment if the property is a CLI argument (must stay `string`) |
| `S2302` | Replace string literal `"param"` with `nameof(param)` in `ArgumentException`/`ThrowIfNullOrWhiteSpace` calls |
| `S1135` | Resolve or remove the `TODO` comment; if deferring is intentional, move it to `todo.md` and delete the comment from code |

### Suppression Policy

- **Prefer fixing** over suppressing.
- **ReSharper inline comments** (`// ReSharper disable once …`) are acceptable for false positives in test helpers and DI-registered types that ReSharper cannot see are used.
- **`#pragma warning disable`** is acceptable when a rule conflicts with a framework constraint (e.g., CLI argument properties must be `string`, not `Uri`). Always include a brief comment explaining why.
- **Never suppress by editing `baseline.sarif.json`.**

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

### CLI Command-Tree & Settings-Parsing Tests (MANDATORY when changing commands)

The dgtp command tree is registered in `src/dgt.power/CommandTree.cs` and covered by two test
layers in `tests/dgt.power.cli.tests/`, independent from the `PowerLogic`/business-logic tests in
`CommandTestContext`:

- `CommandTreeTests.cs` — structural wiring test (`CommandTree.Register` + `ValidateExamples()`
  must build without throwing) and a data-driven smoke test over every top-level branch/command
  path (via `CommandAppTester`).
- `SettingsParsingTests.cs` — one test per distinct `CommandSettings` type, verifying that CLI
  arguments (positional arguments, options, aliases, comma-separated lists, defaults) are parsed
  correctly, using the dependency-free `NoOpCommand<TSettings>` test double.

**When you add, remove, or rename a top-level command/branch in `CommandTree.cs`**, add/update the
corresponding case in `CommandTreeTests.cs`.

**When you add a new `CommandSettings` subclass, or change `[CommandArgument]`/`[CommandOption]`
attributes (name, alias, position, default value) on an existing one**, add/update the
corresponding test in `SettingsParsingTests.cs`. Reuse an existing settings type's test if the new
command shares that settings class — do not duplicate tests per command.

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

## Spec-Driven Changes (OpenSpec)

OpenSpec is the source of truth for current, user-relevant capability requirements and significant change proposals. The repository-local CLI is installed with pnpm; always run it as `pnpm exec openspec ...`, never assume a global installation.

### When a proposal is required

Create and review an OpenSpec change for:

- Public CLI/API behavior or compatibility changes
- Configuration format or JSON schema changes
- Architecture or module-boundary changes
- Behavior changes that cross capabilities or affect multiple modules

Do not create a proposal for a localized bug fix, test-only or documentation-only change, or behavior-preserving refactor. Keep the process proportional to the change.

### Workflow

1. Read the relevant main specs with `pnpm exec openspec list --specs` and inspect the implementation, tests, schemas, and README that define current behavior.
2. For unclear or broad work, explore first. Otherwise create a proposal with `/opsx-propose`; include scope, affected capabilities, design constraints, and test/validation tasks.
3. Review the proposal before implementation. Implement only after the user authorizes the work, using `/opsx-apply` where available.
4. Update the main specs to match the implemented behavior, run focused tests and `pnpm exec openspec validate --all`, then archive the completed change with `/opsx-archive`.
5. Refresh generated Copilot instructions with `pnpm run openspec:update` so OpenSpec CLI examples continue to use the repository-local pnpm installation.

### Where knowledge belongs

- `openspec/specs/` describes current requirements and externally observable behavior. Keep specs concise, organized by capability, and grounded in code, tests, schemas, and README.
- `openspec/changes/` contains active proposals. Archived changes are history, not a substitute for current specs.
- `docs/architecture/decisions/` records only durable rationale that is not apparent from current behavior. Update its index when adding an ADR.
- Put implementation details and narrow caveats beside the relevant code or tests. Do not create session logs, duplicate API reference material, or migrate old notes solely to preserve them.
- The README remains the user-facing guide. Update it when public behavior, configuration, project structure, or build/test workflows change.

Do not add a knowledge file after every non-trivial edit. Persist information only when one of these canonical sources would otherwise be missing a durable requirement, decision, or actionable constraint.
