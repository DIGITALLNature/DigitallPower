# Plugin Scaffolding

## Purpose

Define independently usable native generation of DIGITALL-based Dataverse plugin projects and resource-oriented initialization through dgtp.

## ADDED Requirements

### Requirement: Native generation is independently usable

The official template SHALL be distributed as a standard NuGet template package and support documented native installation and generation with name, namespace, and output customization. Template delivery, generation, and building SHALL NOT depend on dgtp integration or PAC installation. The effective namespace SHALL default to the project name.

#### Scenario: Native first-use workflow

- **WHEN** a user installs the published template through `dotnet new install` and invokes its documented short name with valid project/output parameters
- **THEN** the customized plugin project is generated without dgtp, PAC, or Dataverse credentials
- **AND** it can be release-built using the documented SDK prerequisites

#### Scenario: Native namespace customization

- **WHEN** a user generates through native tooling with an explicit namespace different from the project name
- **THEN** generated source and project settings use the supplied namespace while retaining the requested project identity

### Requirement: Native template onboarding is complete

The generated README SHALL document native template installation and updates, generation parameters, build prerequisites, package location, optional native solution membership, and explicit deployment steps. It SHALL NOT require or advertise an unimplemented dgtp initialization command, embed credentials, or select an environment-specific deployment target.

#### Scenario: Template is delivered before a wrapper

- **WHEN** a user follows the published template's README without a dgtp initialization command available
- **THEN** the native installation, generation, and build instructions form a complete usable workflow
- **AND** updates use native tooling and existing-solution membership remains an explicit `dotnet sln ... add ...` step

### Requirement: Plugin initialization is resource-oriented

The CLI SHALL expose `dgtp plugin init <directory> --name <project-name> [--namespace <namespace>]`. The effective namespace SHALL default to the project name. Invalid project identities, namespaces, and destinations SHALL be rejected before template installation or network access.

#### Scenario: Explicit project identity

- **WHEN** a user initializes a new directory with `--name Contoso.Plugins --namespace Contoso.Dataverse`
- **THEN** the generated project uses `Contoso.Plugins` as its project identity and `Contoso.Dataverse` as its namespace

#### Scenario: Default namespace

- **WHEN** a user supplies a valid project name and omits `--namespace`
- **THEN** the project name is used as the namespace

#### Scenario: Invalid local input

- **WHEN** the project name introduces a path, the effective namespace is invalid, or the destination is invalid
- **THEN** initialization fails before network requests, template installation, or project publication

#### Scenario: Command discovery

- **WHEN** help or shell-completion metadata is generated
- **THEN** `plugin init` and its options are discoverable alongside existing plugin commands
- **AND** no template-management branch, template-update flag, or solution-integration option is introduced

### Requirement: Project identity applies to files and references

Initialization SHALL apply the supplied identity to template-owned content, filenames, namespaces, and internal project/solution references as applicable. The result SHALL contain no unresolved template identity placeholders or references to nonexistent renamed projects.

#### Scenario: Project files are renamed

- **WHEN** a project is generated with a different name from the template source
- **THEN** its generated project filename, identity, namespaces, and any template-owned references agree with the requested values

### Requirement: Generated projects build Dataverse plugin packages

The generated project SHALL reference `Digitall.Plugins` and `Digitall.Plugins.Registration`, include a minimal plugin with a supported registration declaration, and build a Dataverse plugin `.nupkg`. It SHALL NOT require a PAC-generated `PluginBase` or the standalone registration-source package.

#### Scenario: User builds the starter project

- **WHEN** the user explicitly runs the documented release build with the required SDK and dependencies available
- **THEN** it succeeds and produces a Dataverse plugin package containing the starter plugin
- **AND** its registration declaration is recognized by dgtp's local artifact inspection

### Requirement: Deployment packages contain runtime dependencies

The generated deployment package SHALL physically include the plugin and required non-platform runtime assemblies, including the DIGITALL runtime and registration assemblies. It SHALL NOT rely solely on NuGet dependency declarations or bundle platform-provided Dataverse SDK assemblies.

#### Scenario: Package contents are inspected

- **WHEN** the generated project's deployment package is opened after a release build
- **THEN** the required runtime DLLs are present and platform-provided SDK assemblies are excluded

### Requirement: Missing templates are installed transparently

When the official template package is missing, the dgtp initialization command SHALL announce and install its latest stable version into the normal shared per-user template installation. It SHALL generate with the resolved version, report that version, and make the template available to native .NET tooling. It SHALL NOT fall back to a prerelease or modify unrelated template packages.

#### Scenario: First initialization

- **WHEN** no official template package is installed and a stable version is available
- **THEN** initialization announces and installs the resolved stable version and generates the project with it
- **AND** native .NET tooling can discover the installed template

#### Scenario: Initial installation fails

- **WHEN** stable-version resolution or first-use installation fails
- **THEN** initialization fails with an actionable diagnostic and no published project

### Requirement: Existing template installations are not updated implicitly

The dgtp initialization command SHALL reuse an unambiguous usable installed stable version of the official template without upgrading or downgrading it. It SHALL recognize native installation and updates on subsequent runs. An unusable, prerelease-only, ambiguous, or concurrently changed selection SHALL fail with guidance instead of being silently replaced.

#### Scenario: Native installation is reused

- **WHEN** the official template has been installed through native .NET tooling
- **THEN** initialization uses and reports that installed stable version without reinstalling it

#### Scenario: Native update is picked up

- **WHEN** the user updates the shared template through native tooling and starts another initialization
- **THEN** the new run uses and reports the updated installed stable version

#### Scenario: Installed template is incompatible

- **WHEN** the selected installation cannot satisfy the required template identity or parameters
- **THEN** initialization fails with native remediation guidance and does not replace the installation or publish a partial project

### Requirement: Stable updates produce native actionable guidance

For a usable installed template, each dgtp initialization run SHALL attempt a bounded check for newer stable versions. When one exists, it SHALL report installed and available versions and print `dotnet new install <actual-template-package-id>`. It SHALL continue generating with the installed version and SHALL NOT execute the suggested update.

#### Scenario: An update is available

- **WHEN** a newer stable template version exists
- **THEN** initialization prints the targeted native update command with the actual package ID
- **AND** the current project is generated with the reported installed version

#### Scenario: Only a newer prerelease exists

- **WHEN** no newer stable version exists but a newer prerelease does
- **THEN** initialization does not advertise that prerelease as a stable update

#### Scenario: Update lookup fails

- **WHEN** a usable stable template is installed but its update check times out or fails
- **THEN** initialization warns that the check could not complete and continues with the installed version
- **AND** it does not claim that version is up to date

### Requirement: Existing project files are protected

The dgtp initialization command SHALL accept only an absent or empty destination, counting hidden entries as content. It SHALL reject linked destinations or ancestors that defeat containment, prevent output outside the destination, and never merge or overwrite user content. Handled failure or cancellation SHALL leave no partial project and clean only owned temporary output.

#### Scenario: Hidden content exists

- **WHEN** the destination contains a hidden file
- **THEN** initialization rejects it without modifying its content

#### Scenario: Generation fails or is cancelled

- **WHEN** an error or cancellation occurs before project publication
- **THEN** no partial project is published and pre-existing user content remains unchanged

#### Scenario: Destination changes during generation

- **WHEN** another process creates content at the destination before publication
- **THEN** initialization fails rather than overwriting that content

#### Scenario: A newly installed template survives generation failure

- **WHEN** first-use template installation succeeds but project generation subsequently fails
- **THEN** the shared template remains installed and that persistent side effect is reported
- **AND** project cleanup does not uninstall it or affect unrelated templates

### Requirement: Initialization does not execute development or deployment work

The dgtp initialization command SHALL NOT authenticate to Dataverse, deploy, execute template post-actions, restore or build the generated project, or initialize Git. It SHALL require neither PAC nor Dataverse credentials. Template installation and update checking SHALL remain distinct from restoring the generated project's dependencies.

#### Scenario: Initialization without an environment

- **WHEN** a user initializes a project without PAC, a Dataverse connection, or Dataverse credentials
- **THEN** generation can succeed without authentication or deployment
- **AND** build, restore, Git, and template post-actions are not executed

### Requirement: Onboarding leaves solution integration explicit

The dgtp command's success output SHALL identify the template package/version, generated project path, verified build command, and generated README. It SHALL provide native `dotnet sln <solution> add <generated-project>` guidance with the actual project path and clearly identify the solution argument as user-supplied. The command SHALL NOT discover or modify existing `.sln`/`.slnx` files.

#### Scenario: Successful onboarding

- **WHEN** initialization succeeds
- **THEN** the output includes the template version used, a release-build command, and native solution-add guidance
- **AND** it prints `See README.md in the generated project for additional configuration and setup steps.`

#### Scenario: An existing solution is nearby

- **WHEN** a solution file exists outside the new project directory
- **THEN** initialization leaves it unchanged and does not select it automatically

#### Scenario: User follows generated documentation

- **WHEN** the user reads the generated README
- **THEN** it explains build prerequisites, package location, optional solution membership, and explicit deployment steps
- **AND** it contains no credentials or preselected environment-specific deployment target
