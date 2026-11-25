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
command will not merge with or overwrite an existing project.

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

## Optional deployment defaults

Solution and publisher-prefix defaults can be supplied independently:

```shell
dgtp webresource init ./contoso-webresources \
  --name contoso-webresources \
  --global Contoso \
  --solution ContosoCore \
  --publisher-prefix contoso
```

These values are used only as defaults in generated deployment scripts and examples; they are not
inferred from the package or global name. They may contain letters, digits, periods, underscores, and
hyphens. If either value is omitted, no sample deployment target is substituted. The generated
deployment script requires any missing value and a connection selection when the script is run.
Scaffolding itself does not select or persist a Dataverse connection.

Generation does not require deployment configuration. Install and build the frontend project with
the commands printed by `dgtp`; additional model-generation, local deployment, and CI setup are
described in the generated project `README.md`. Review and deliberately enable any deployment
workflow before using it.

## Template compatibility errors

The template and `dgtp` share reserved `__DGTP_*__` markers. Initialization checks that every
required marker is present, replaces all occurrences in supported UTF-8 text files, and rejects any
unknown reserved marker before publishing the project. Ordinary `TODO` reminders are not scaffolding
markers. Binary files and unrelated text are left unchanged.

A missing or unsupported marker means the selected template revision and `dgtp` do not agree.
Choose a compatible revision or update the template and `dgtp` marker support together; do not
manually edit the generated project to conceal a compatibility error.
