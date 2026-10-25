# Documentation

This directory contains all documentation for the DigitallPower CLI (`dgtp`).

## For users

| Section | Description |
|---------|-------------|
| [Getting Started](getting-started/installation.md) | Install dgtp and verify your setup |
| [Quick Start](getting-started/quick-start.md) | Five commands to get running |
| [Guides](guides/) | How-to guides for common tasks |
| ├─ [CI/CD Integration](guides/ci-cd.md) | Azure Pipelines, Workload Identity Federation |
| ├─ [Tab Completion](guides/tab-completion.md) | Shell autocompletion setup |
| ├─ [Plugin Deployment](guides/plugin-deployment.md) | End-to-end plugin workflow |
| └─ [Migration Guides](guides/migrations/) | Upgrading between major versions |
| [Reference](reference/) | Command and configuration reference |
| ├─ [Commands](reference/commands/) | Per-branch command documentation |
| │  ├─ [connection](reference/commands/connection.md) | Authentication & environments |
| │  ├─ [export](reference/commands/export.md) | Export Dataverse artifacts |
| │  ├─ [import](reference/commands/import.md) | Import Dataverse artifacts |
| │  ├─ [maintenance](reference/commands/maintenance.md) | Operational tasks |
| │  ├─ [analyze](reference/commands/analyze.md) | Solution analysis |
| │  ├─ [solution](reference/commands/solution.md) | version, lint, copy-components, codegeneration |
| │  ├─ [plugin](reference/commands/plugin.md) | push, step enable/disable, config, state |
| │  ├─ [webresource](reference/commands/webresource.md) | Deploy webresources |
| │  ├─ [telemetry](reference/commands/telemetry.md) | Manage telemetry settings |
| │  └─ [complete](reference/commands/complete.md) | Shell tab completion |
| └─ [Configuration](reference/configuration.md) | Environment variables, connection settings, schemas |
| [Concepts](concepts/) | Understanding how dgtp works |
| ├─ [Architecture](concepts/architecture.md) | Solution architecture and design principles |
| ├─ [Telemetry and Privacy](concepts/telemetry-and-privacy.md) | What data is collected and how to control it |
| └─ [Connection Model](concepts/connection-model.md) | Connections, authentication, credential storage |

## For contributors

| Section | Description |
|---------|-------------|
| [Build & Test](development/build-and-test.md) | Build commands, test commands, local packaging |
| [Repository Layout](development/repository-layout.md) | Project structure and module organization |
| [Spec-Driven Development](development/spec-driven-development.md) | OpenSpec workflow, Graphify pilot |
| [Contributing](../CONTRIBUTING.md) | How to contribute (root-level) |
| [Architecture Decisions](architecture/decisions/) | ADRs — durable design rationale |
| [Design Proposals](design/) | Design documents and proposals |
