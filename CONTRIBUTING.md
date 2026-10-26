# Contributing

DigitallPower CLI is a **community-driven open source project** backed by DIGITALL. We are committed to a fully transparent development process and **highly appreciate any contributions**. Whether you are helping us fix bugs, propose new features, improve our documentation, or spread the word — **we would love to have you as part of the DigitallPower community**.

## Questions and discussions

We are happy to answer your questions via [GitHub Discussions](https://github.com/DIGITALLNature/DigitallPower/discussions).

## Bugs and feature requests

Feel free to **file a new issue** with a descriptive title on the [DigitallPower issues](https://github.com/DIGITALLNature/DigitallPower/issues) page.

## Development setup

See [Build & Test](docs/development/build-and-test.md) for prerequisites and build commands.

## Coding standards

- **Language:** C# with latest LangVersion, nullable enabled, implicit usings
- **Target Framework:** net10.0
- **Naming:** Standard .NET naming conventions (PascalCase for public members)
- **File naming:** Each `.cs` file contains exactly one top-level type; the file name matches the type name
- **Licensing header:** All source files start with `// Copyright (c) DIGITALL Nature. All rights reserved`

## Testing

Every code change that modifies behavior **must** be accompanied by tests using the TUnit framework. See [Build & Test](docs/development/build-and-test.md) for test commands.

## Commit messages

This project uses [Conventional Commits](https://www.conventionalcommits.org/) enforced by commitlint + Husky.

```
<type>(<scope>): <short description>
```

| Type | When to use |
|------|-------------|
| `feat` | New feature or capability |
| `fix` | Bug fix |
| `docs` | Documentation only |
| `refactor` | Code change that neither fixes a bug nor adds a feature |
| `perf` | Performance improvement |
| `test` | Adding or updating tests only |
| `chore` | Tooling, CI, dependencies, config |
| `style` | Formatting, white-space, etc. |

Subject line: imperative mood, lowercase, no period at end, max 100 characters.
Breaking changes: add `!` after type/scope or a `BREAKING CHANGE:` footer.

## Documentation

When making changes that affect the public API, configuration, project structure, or usage patterns, update the corresponding documentation:

- **Command changes** → update `docs/reference/commands/<branch>.md`
- **Configuration changes** → update `docs/reference/configuration.md` and the matching JSON schema in `schemas/`
- **New guides** → add to `docs/guides/`
- **Architecture changes** → add an ADR to `docs/architecture/decisions/`
- **Breaking changes** → add a migration guide to `docs/guides/migrations/`
- **README** → keep as a landing page; link to detailed docs rather than duplicating content

## Pull requests

1. Fork the repository and create a feature branch
2. Make your changes with tests and documentation
3. Run `dotnet test` and ensure all tests pass
4. Commit using [conventional commits](#commit-messages)
5. Open a pull request with a clear description

## Spec-driven development

For significant changes to public CLI behavior, configuration, or architecture, create an [OpenSpec](https://github.com/Fission-AI/OpenSpec) change proposal. See [Spec-Driven Development](docs/development/spec-driven-development.md) for the workflow.

## License

By contributing, you agree that your contributions will be licensed under the [MS-RL License](LICENSE.md).
