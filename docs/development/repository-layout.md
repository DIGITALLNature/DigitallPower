# Repository Layout

The working specifications and active change proposals live in `openspec/`; concise records of durable architectural rationale live in `docs/architecture/decisions/`.

```
DigitallPower/
├── src/
│   ├── dgt.power/                # CLI host project (produces the `dgtp` tool)
│   ├── dgt.power.common/         # Shared infrastructure (typed connections, IO, command lifecycle)
│   └── modules/
│       ├── dgt.power.analyzer/        # `analyze` commands
│       ├── dgt.power.codegeneration/  # `codegeneration` / `cg` command
│       ├── dgt.power.connection/      # `connection` commands (auth management)
│       ├── dgt.power.export/          # `export` commands
│       ├── dgt.power.import/          # `import` commands
│       ├── dgt.power.maintenance/     # `maintenance` commands
│       ├── dgt.power.plugin/          # `plugin` commands
│       ├── dgt.power.solution/        # `solution` commands (e.g. `solution lint`)
│       ├── dgt.power.telemetry/       # `telemetry` commands (status, enable, disable)
│       └── dgt.power.webresource/     # `webresource push` command
├── tests/                        # Unit and integration tests (one project per module)
├── schemas/                      # JSON schemas for configuration files
├── docs/                         # Documentation (see below)
├── openspec/                     # OpenSpec specs and change proposals
├── scripts/                      # Repository maintenance scripts
├── AGENTS.md                     # AI agent guidelines
├── CONTRIBUTING.md               # Contribution guidelines
├── LICENSE.md                    # License (MS-PL)
├── SECURITY.md                   # Security policy
├── README.md                     # Project overview and quick links
├── Directory.Build.props         # Common MSBuild properties
├── global.json                   # Pinned .NET SDK
└── DigitallPower.slnx            # Solution file
```

## Documentation structure

```
docs/
├── README.md                     # Docs landing page
├── getting-started/              # Installation and quick start
│   ├── installation.md
│   └── quick-start.md
├── guides/                       # How-to guides
│   ├── ci-cd.md                  # CI/CD pipeline integration
│   ├── plugin-deployment.md      # Deploying plugins
│   ├── tab-completion.md         # Shell tab completion
│   └── migrations/               # Version migration guides
├── reference/                    # Command and configuration reference
│   ├── commands/                 # One file per command branch
│   └── configuration.md          # Configuration file reference
├── concepts/                     # Architecture and design concepts
│   ├── architecture.md
│   ├── connection-model.md
│   └── telemetry-and-privacy.md
├── development/                  # Contributor docs
│   ├── build-and-test.md
│   ├── repository-layout.md
│   └── spec-driven-development.md
├── architecture/                 # Architecture Decision Records
│   └── decisions/
└── design/                       # Design proposals
```
