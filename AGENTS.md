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

5. **Keep documentation in English.** All documentation in this repository is written in English.

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
- **Licensing header:** All source files start with `// Copyright (c) DIGITALL Nature. All rights reserved`
- **Tests:** Use TUnit framework with TUnit.Assertions and TUnit.Mocks

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

### Rules

1. **Read first.** Always read `.memory/summary.md` at the start of a task to understand current status and avoid redundant work.
2. **Store findings** in `.memory/` directory. All notes must be in markdown format.
3. **Filename convention:** `.memory/<type>-<title>.md`
   - `<type>` is one of: `research`, `phase`, `guide`, `decision`, `implementation`
   - `<title>` is a short kebab-case descriptor
4. **Exception:** `.memory/summary.md` does not follow the naming convention — it is the index file.
5. **Keep `.memory/summary.md` up to date** with current status, active tasks, and key findings. Prune incorrect or outdated information.
6. **Committed to git.** The `.memory/` directory is shared knowledge — do not add it to `.gitignore`.

### Types

| Type | Purpose |
|------|---------|
| `research` | Investigation results, API behavior findings, library evaluations |
| `phase` | Progress tracking for multi-step work (e.g. migration phases) |
| `guide` | How-to instructions, patterns, and reusable approaches |
| `decision` | Architecture/design decisions with rationale and alternatives considered |
| `implementation` | Implementation plans, technical specs, or post-implementation notes |

### When to Write

- After discovering non-obvious behavior or caveats
- After making a design/architecture decision
- When starting multi-step work that spans sessions
- When findings would save future agents significant research time

### When NOT to Write

- Trivial or self-evident facts already in the code
- Temporary debugging notes (use comments or session memory instead)
- Information already covered in README.md or code comments

### Example Filenames

```
.memory/summary.md
.memory/research-fetchxml-paging-behavior.md
.memory/decision-tunit-over-xunit.md
.memory/phase-net10-migration.md
.memory/guide-bulk-operation-patterns.md
.memory/implementation-audit-export-logic.md
```
