# Repository Layout

The working specifications and active change proposals live in `openspec/`; concise records of durable architectural rationale live in `docs/architecture/decisions/`.

```
DigitallPower/
├── src/
│   ├── dgt.power/                # CLI host project (produces the `dgtp` tool)
│   ├── dgt.power.common/         # Shared infrastructure (typed connections, IO, command lifecycle)
│   ├── models/                   # Shared DTOs / data contracts
│   └── modules/
│       ├── dgt.power.analyzer/        # `analyze` commands
│       ├── dgt.power.codegeneration/  # `codegeneration` / `cg` command
│       ├── dgt.power.connection/      # `connection` commands (auth management)
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.plugin/          # `plugin` commands
│       ├── dgt.power.solution/        # `solution` commands (e.g. `solution lint`)
│       └── dgt.power.webresource/     # `webresource push` command
├── tests/                        # Unit and integration tests
├── samples/                      # Example inputs (configs, plugin samples)
├── schemas/                      # JSON schemas for configuration files
├── docs/                         # User docs, contributor docs, ADRs, design proposals, migration guides
├── openspec/                     # OpenSpec specs and change proposals
├── scripts/                      # Repository maintenance scripts
├── Directory.Build.props         # Common MSBuild properties
├── global.json                   # Pinned .NET SDK
└── DigitallPower.slnx            # Solution file
```

## Documentation structure

```
docs/
├── getting-started/              # Installation and quick start
├── guides/                       # How-to guides (CI/CD, tab completion, plugin deployment)
│   └── migrations/               # Version migration guides
├── reference/                    # Command and configuration reference
│   ├── commands/                 # Per-branch command docs
│   └── configuration.md
├── concepts/                     # Architecture, telemetry, connection model
├── development/                  # Contributor docs (build, layout, OpenSpec)
├── architecture/                 # Architecture Decision Records
│   └── decisions/
└── design/                       # Design proposals
```
