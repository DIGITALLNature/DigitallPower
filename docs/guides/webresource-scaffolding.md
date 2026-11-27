# Scaffold a webresource project

`dgtp webresource init` creates a local project from the public
[`webresources-template`](https://github.com/DIGITALLNature/webresources-template) repository.
It customizes the project files but does not install dependencies, create a Git repository, generate
Dataverse models, or deploy resources.

## Create a project

```shell
dgtp webresource init ./contoso-webresources \
  --name contoso-webresources \
  --global Contoso
```

The destination must not exist or must be an empty directory. Hidden files count as content; the
command will not merge with or overwrite an existing project. The destination itself cannot be a
symbolic link, but its parent directories may contain symbolic links.

`--name` is an unscoped, lowercase npm package name. `--global` is a single, non-reserved JavaScript
identifier exposed by the generated bundles. The command reports the destination and exact template
commit, then prints the dependency-install and production-build commands. See `README.md` in the
generated project for additional configuration and setup steps.

## Choose a template revision

By default, initialization downloads the current `main` revision. This is a moving default, so
projects created at different times can use different template commits. The resolved commit SHA is
reported on success.

Use `--template-version` with an existing tag or a full 40-character commit SHA to select a specific
revision:

```shell
dgtp webresource init ./contoso-webresources \
  --name contoso-webresources \
  --global Contoso \
  --template-version v1.2.0
```

For reproducible scaffolding, save the reported SHA and pass it to `--template-version` on a later
run. Branch names are not accepted as explicit versions. A missing tag, inaccessible revision, rate
limit, or network error fails the command; it does not silently switch to another revision. Wait and
retry, or use a known compatible tag or SHA. GitHub network access is required at generation time;
Git, GitHub Releases, PAC, Node.js, pnpm, and Dataverse credentials are not required to generate files.

## Optional local workflow defaults

The essential options above create a buildable starter. To configure both model-generation and
local-deployment scripts, also supply a shared dgtp connection name, solution, and publisher prefix:

```shell
dgtp webresource init ./contoso-webresources \
  --name contoso-webresources \
  --global Contoso \
  --dgtp-connection dev \
  --solution ContosoCore \
  --publisher-prefix contoso
```

These independent values are defaults in the relevant generated scripts and examples; they are not
inferred from the package or global name. Solution and publisher prefix may contain letters, digits,
periods, underscores, and hyphens. Each omitted value becomes `REPLACE_ME` in the generated files.
Replace it in the project or override it with script arguments before running the relevant script;
configure pipeline values before running the pipeline. Scripts can pass `REPLACE_ME` to dgtp if it
is not replaced.

`--dgtp-connection` sets the shared connection name used by generated scripts; the inherited
`--connection` applies only to the current dgtp invocation and does not set that default.
Initialization neither checks nor creates the connection. Each developer must configure the named
connection locally; no connection string or credentials are embedded in the project.

Generation does not require deployment configuration. Install and build the frontend project with
the commands printed by `dgtp`; adapting models and example code, model-generation, local deployment,
and optional CI setup are described in the generated project `README.md`. Review and configure
the target before manually running a deployment workflow.

## Template compatibility errors

The template and `dgtp` share reserved `__DGTP_*__` markers. Initialization checks that every
required marker is present, replaces all occurrences in supported UTF-8 text files, and rejects any
unknown reserved marker before publishing the project. Ordinary `TODO` reminders are not scaffolding
markers. Binary files and unrelated text are left unchanged.

A missing or unsupported marker means the selected template revision and `dgtp` do not agree.
Choose a compatible revision or update the template and `dgtp` marker support together; do not
manually edit the generated project to conceal a compatibility error.
