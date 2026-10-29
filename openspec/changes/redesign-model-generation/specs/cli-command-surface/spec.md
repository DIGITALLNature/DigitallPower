# CLI Command Surface Delta

## ADDED Requirements

### Requirement: Model generation is resource-oriented
The CLI SHALL expose `model generate` with required named `--language`, `--config/-c`, and `--output/-o` options and existing connection options. The `--language` option SHALL select the target (`dotnet` or `typescript`). The command SHALL NOT accept positional destinations, target subcommands, `--folder`, or an overwrite confirmation flag/prompt.

#### Scenario: Generate through either option spelling
- **WHEN** a user invokes `model generate` with `--language`, a supported recipe, and the long or short config/output options
- **THEN** the selected language determines the target and output is rooted directly at the supplied destination
- **AND** runtime help and shell completion expose the same options

#### Scenario: Required options are missing
- **WHEN** any of `--language`, `--config`, or `--output` is omitted
- **THEN** parsing fails without authenticating or changing output
- **AND** no conventional config or destination is inferred

#### Scenario: Unsupported language value is supplied
- **WHEN** `--language` is set to a value other than `dotnet` or `typescript`
- **THEN** parsing fails with an actionable diagnostic without authenticating or changing output

#### Scenario: Recipe does not match the selected language
- **WHEN** the recipe contains settings that are incompatible with the selected `--language` (for example, a TypeScript recipe with `forms` supplied with `--language dotnet`, or a .NET recipe with `namespace`/`target` supplied with `--language typescript`)
- **THEN** recipe loading fails with an actionable diagnostic identifying the mismatch
- **AND** no authentication or output changes occur

#### Scenario: Unsupported command shapes are supplied
- **WHEN** a user supplies a positional destination, a target subcommand, or a removed generation-setting option
- **THEN** parsing fails with an actionable diagnostic without changing output

### Requirement: Legacy code generation remains available with deprecation guidance
The CLI SHALL retain `codegeneration` and `cg` with their existing settings and generation behavior. Invocation SHALL display a deprecation hint directing users to `model generate`, without redirecting legacy recipes or output to the new contract.

#### Scenario: Invoke a legacy alias
- **WHEN** a user invokes `codegeneration` or `cg` with a supported legacy recipe
- **THEN** the existing generator executes with its existing paths and output behavior
- **AND** a deprecation hint identifies `model generate` as the replacement

#### Scenario: Inspect registered command paths
- **WHEN** command-tree smoke tests or shell-completion metadata are generated
- **THEN** `model generate`, `codegeneration`, and `cg` remain represented
- **AND** structural and settings-parsing coverage requires no live Dataverse environment
