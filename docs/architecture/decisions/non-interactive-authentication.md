# Non-Interactive Authentication for Coding Agents

## Context

When a user token expires, Azure Identity may open a browser. Coding agents cannot observe this, so the CLI could block without a useful exit code.

## Decision

Offer an explicit non-interactive mode (`--non-interactive` / `DGTP_NON_INTERACTIVE`), a side-effect-free `connection status` pre-flight check, and an explicit `connection refresh` command, all using the dedicated exit code `2` (`ExitCode.AuthRequired`) and recognizable `AUTH_REQUIRED:` / `AUTH:` output markers. Behavior is specified in the `connection-management` capability.

## Rationale

The environment variable is preferred for agents because it also applies when the connection is resolved during DI setup, before command arguments are available. Separating status from refresh lets an agent ask the user to sign in instead of blocking.

## Consequences

Credentials use `DisableAutomaticAuthentication` in non-interactive mode. Connection-string connections always report success from `status`.
