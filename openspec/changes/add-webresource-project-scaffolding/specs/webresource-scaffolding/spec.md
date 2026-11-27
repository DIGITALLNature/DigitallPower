# Spec Delta

## Purpose

Let users create a customized, buildable Dataverse webresource project from a selected DIGITALL template revision without manually copying and renaming template values.

## ADDED Requirements

### Requirement: Webresource initialization is a discoverable command

The CLI MUST expose `webresource init <directory>` with required `--name` and `--global` options and optional `--template-version`, `--dgtp-connection`, `--solution`, and `--publisher-prefix` options. Help and shell completion MUST expose the same command and options.

#### Scenario: Initialization help is requested

- **WHEN** a user invokes `dgtp webresource init --help`
- **THEN** help describes the destination, required package name and JavaScript global, optional template version, and optional local workflow defaults
- **AND** help succeeds without downloading a template or opening Dataverse

### Requirement: Template downloads default to main with explicit revision overrides

The command MUST download from `DIGITALLNature/webresources-template` at execution time. Omission of `--template-version` MUST select `main`; an explicit value MUST select a tag name or full commit SHA. The selection MUST resolve once to the commit downloaded and reported. The command MUST NOT require published tags, a per-dgtp-release template pin, or a version-specific adapter.

#### Scenario: No template version is supplied

- **WHEN** initialization is invoked without `--template-version`
- **THEN** the command resolves the current `main` commit and downloads that exact commit
- **AND** it reports the commit so that the same revision can later be selected explicitly
- **AND** no tags or GitHub Releases are required

#### Scenario: A user selects a template tag

- **WHEN** initialization is invoked with an existing template tag
- **THEN** the command resolves that tag to a commit and downloads that commit
- **AND** it reports the resolved SHA so the user can reproduce the selection even if the tag later moves

#### Scenario: A user selects a full commit

- **WHEN** initialization is invoked with a full commit SHA
- **THEN** the command downloads exactly that commit without substituting a default revision

#### Scenario: Selection cannot be resolved

- **WHEN** the selection is absent upstream, an explicit version names a branch rather than a tag or commit, or GitHub is unavailable
- **THEN** initialization fails with an actionable diagnostic and no generated project
- **AND** the command does not fall back to another revision

### Requirement: Project values are validated independently

The command MUST require an unscoped lowercase npm-compatible package name and a single non-reserved JavaScript identifier as the global. It MUST reject missing or invalid values before template acquisition or destination writes. It MUST NOT infer a Dataverse publisher prefix, solution, or connection from either value.

#### Scenario: Valid project values are supplied

- **WHEN** initialization receives `--name contoso-webresources --global Contoso`
- **THEN** those values are accepted as distinct package and global names

#### Scenario: An invalid project value is supplied

- **WHEN** the package name is invalid or the global is a dotted expression, reserved word, or other invalid identifier
- **THEN** initialization fails before any template download or destination write
- **AND** the diagnostic identifies the invalid option

### Requirement: Local workflow defaults are optional and explicit

The command MUST accept `--dgtp-connection`, `--solution`, and `--publisher-prefix` independently as generated local workflow defaults. Scripts and examples MUST use supplied values consistently; omitted values MUST become `REPLACE_ME` placeholders rather than sample targets. Scripts MUST allow overrides. Omitting defaults MUST NOT prevent generation or frontend builds.

#### Scenario: All local workflow defaults are supplied

- **WHEN** initialization receives `--dgtp-connection dev --solution ContosoCore --publisher-prefix contoso`
- **THEN** the generated model-generation and push scripts use the supplied connection name by default and the push script uses the supplied deployment defaults
- **AND** script arguments can override those defaults at execution time
- **AND** no value is derived from the package name or JavaScript global
- **AND** scaffolding does not resolve, create, or select a local connection

#### Scenario: Some or all local workflow defaults are omitted

- **WHEN** initialization omits any of the three local workflow defaults
- **THEN** generation succeeds with `REPLACE_ME` for each omitted connection, solution, or publisher-prefix value rather than a sample target
- **AND** the project remains buildable without deployment setup
- **AND** users must replace or override placeholders before using the affected workflow
- **AND** scripts do not treat the placeholder as a special validation error and can pass it to dgtp

### Requirement: Connection defaults are separate from invocation credentials

`--dgtp-connection` MUST supply only the shared connection name. The inherited `--connection` MUST NOT populate that default, and connection strings and credentials MUST NOT be embedded.

#### Scenario: The inherited connection option is supplied

- **WHEN** initialization receives `--connection` without `--dgtp-connection`
- **THEN** the inherited option does not become a generated project default
- **AND** initialization does not resolve or select that connection

### Requirement: Only compatible templates are customized

The command MUST customize reserved `__DGTP_*__` markers in supported text files independently of their filenames or bundler. It MUST replace every occurrence of recognized markers without changing unrelated content or binary files. The generated project MUST retain its license and dependency lockfile and MUST NOT misidentify the template repository as its own repository.

#### Scenario: A supported template is generated

- **WHEN** a compatible template is initialized with valid project values
- **THEN** `package.json` uses the requested package name and bundled scripts expose the requested global
- **AND** unrelated source content and the dependency lockfile remain unchanged
- **AND** the project retains its license without claiming that the template repository is the generated project's repository

#### Scenario: A marker moves during a bundler migration

- **WHEN** the global marker moves from a Webpack configuration to a Rolldown configuration while retaining its insertion convention
- **THEN** initialization still replaces the marker without a dgtp change or filename-specific adapter

#### Scenario: A marker occurs in several files

- **WHEN** a recognized marker occurs in both configuration and README examples
- **THEN** every occurrence is replaced consistently without an exact-count restriction

### Requirement: Marker validation checks presence and completion

Before replacement, the command MUST check that every required marker configured in dgtp exists in the template's supported text files. Before publishing, it MUST reject any remaining reserved `__DGTP_*__` marker. It MUST NOT require markers in particular filenames or impose exact occurrence counts. Ordinary TODOs for manual configuration MUST NOT be treated as scaffolding markers.

#### Scenario: A required marker is absent

- **WHEN** a required marker is not present anywhere in the supported template text files
- **THEN** initialization fails before publishing a project
- **AND** the diagnostic identifies the missing marker

#### Scenario: The template introduces an unsupported marker

- **WHEN** an unrecognized reserved marker remains after replacement
- **THEN** initialization fails before publishing a project
- **AND** the diagnostic identifies the unresolved marker and the need to coordinate template and dgtp support

#### Scenario: Local workflow defaults are not supplied

- **WHEN** optional connection, solution, or publisher-prefix values are omitted
- **THEN** their recognized markers are replaced with `REPLACE_ME` rather than left unresolved
- **AND** explicitly supplied defaults are retained independently

#### Scenario: Manual setup reminders remain

- **WHEN** the template contains ordinary TODOs for CI or model configuration
- **THEN** those reminders can remain without causing marker validation to fail

### Requirement: Existing destination content is protected

Initialization MUST accept only an absent destination or an existing empty directory. It MUST reject files, nonempty directories including hidden content, and symbolic-link destinations. It MUST NOT overwrite or merge project files. A destination that becomes nonempty during generation MUST also be protected.

#### Scenario: An empty destination is used

- **WHEN** a valid project is initialized into an absent or empty destination directory
- **THEN** the generated project appears directly in that directory without a repository-archive wrapper folder

#### Scenario: An ancestor is a symbolic link

- **WHEN** an absent or empty destination is inside a directory reached through a symbolic link
- **THEN** initialization can create the project in that destination
- **AND** a symbolic link used as the destination itself is rejected

#### Scenario: Existing content is present

- **WHEN** the destination contains a file, including only a hidden file
- **THEN** initialization fails without changing existing content
- **AND** the template is not downloaded when the conflict is detectable during initial validation

#### Scenario: Destination content appears during acquisition

- **WHEN** another process creates destination content before the generated project is published
- **THEN** initialization fails without replacing that content

### Requirement: Acquisition failures do not leave partial projects

Download, extraction, customization, and cancellation failures MUST NOT publish a partial project or modify pre-existing destination content. Without concurrent destination changes, the destination MUST remain absent or originally empty. Command-owned temporary artifacts MUST be cleaned up on handled failures. Diagnostics MUST distinguish version, connectivity, template-compatibility, and filesystem failures.

#### Scenario: A download is interrupted

- **WHEN** the template download fails or execution is cancelled before publication
- **THEN** no partial project remains in the destination
- **AND** existing content outside command-owned temporary paths is untouched

### Requirement: Downloaded archives cannot write outside the project

The command MUST reject archive entries that escape the staging root, represent links, collide after destination-platform path normalization, or exceed bounded extraction limits. It MUST NOT execute template-provided hooks or scripts during generation.

#### Scenario: An archive contains an unsafe entry

- **WHEN** an archive contains traversal paths, absolute paths, links, or colliding entries
- **THEN** initialization fails before publication
- **AND** no entry is written outside command-owned staging storage

#### Scenario: An archive exceeds extraction limits

- **WHEN** the download or expanded archive exceeds the supported size or entry-count limits
- **THEN** initialization fails with a diagnostic identifying the exceeded limit
- **AND** no partial project is published

### Requirement: Generation requires no development-tool execution or Dataverse access

Initialization MUST NOT require Git, PAC, Node.js, pnpm, or a Dataverse connection to generate files. It MUST NOT install dependencies, initialize version control, create remote repositories, alter stored connections, generate live Dataverse models, or deploy resources.

#### Scenario: Initialization runs without a configured connection

- **WHEN** valid initialization is invoked without Dataverse credentials or frontend tooling installed
- **THEN** generation can succeed using only dgtp and access to the template download
- **AND** no authentication, package-manager execution, or Dataverse operation occurs

### Requirement: Generated projects have actionable onboarding

Successful initialization MUST report the destination and resolved commit, show install and build commands, and direct users to the generated `README.md`. That README MUST distinguish the essential starter from the local workflow configured with all three optional defaults and explain local prerequisites and optional CI setup. Automatic CI triggers MUST be disabled by default.

#### Scenario: A project is generated successfully

- **WHEN** initialization completes
- **THEN** output explains how to enter the project, install dependencies, and run its production build
- **AND** output explicitly directs the user to the generated `README.md` for additional configuration and setup steps
- **AND** that README documents supplied local workflow defaults, script overrides, local connection prerequisites, and remaining model-scope and CI setup without treating them as prerequisites for a frontend build

#### Scenario: A user considers the optional pipeline

- **WHEN** a user configures or manually runs the generated pipeline
- **THEN** the README explains that automatic triggers are disabled by default
- **AND** it warns that a manual run on `main` attempts deployment after a successful build, so the target must be configured and reviewed first

#### Scenario: The prepared template is validated end to end

- **WHEN** the marker-based template is prepared for the initial command release or its customization convention changes
- **THEN** a project generated with representative custom values is verified by frozen-lockfile dependency installation and a production build
- **AND** its sample form and ribbon bundles expose the requested global
- **AND** supplied dgtp scripts and examples use the supported command surface
