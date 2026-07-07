# Decision: Generic Command Deprecation Mechanism

## Context

The `profile` branch is deprecated in favor of `connection` (see
`decision-non-interactive-auth-for-agents.md`). The first implementation was an
`ICommandInterceptor` that inspected `CommandContext.Arguments[0]` (the raw, unparsed argv) to guess
which branch was invoked. That approach was fragile: any global option preceding the branch (e.g. a
future `dgtp --verbose profile list`) would make the detection silently miss the deprecated branch,
and every future deprecation would need its own bespoke argument-scanning logic.

## Decision

Deprecation is now driven by an attribute on the command's `CommandSettings` type, read by a single
generic interceptor - not by inspecting raw arguments.

### `DeprecatedCommandAttribute` (`dgt.power.common/Commands/DeprecatedCommandAttribute.cs`)

```csharp
[DeprecatedCommand("connection")]   // with a replacement hint
public class ProfileSettings : BaseProgramSettings;

[DeprecatedCommand]                 // deprecated with no direct replacement
public class SomeOldSettings : CommandSettings;
```

- `AttributeUsage(AttributeTargets.Class, Inherited = true)` - applying it to a branch-level settings
  class (e.g. `ProfileSettings`) deprecates every command under that branch at once, because all leaf
  settings classes (`NamedProfileSettings`, `CreateProfileSettings`, ...) derive from it and inherit the
  attribute. Apply it to a single leaf settings class instead to deprecate just that one command.

### `DeprecationInterceptor` (`src/dgt.power/DeprecationInterceptor.cs`)

`ICommandInterceptor.Intercept(CommandContext context, CommandSettings settings)` receives the actual
**bound** settings instance for the invocation - not raw argv. The interceptor just reflects on
`settings.GetType()` for `DeprecatedCommandAttribute` (inherited) and prints a warning if found. This
is reliable regardless of argument order, global options, or nesting depth, since Spectre has already
resolved which command/settings type matched before invoking interceptors.

Registered once in `Program.cs` alongside `TelemetryInterceptor` / `VersionCheckInterceptor` via
`CompositeInterceptor` (Spectre only supports a single interceptor).

## How to deprecate a new command or branch

1. Add `[DeprecatedCommand("replacement-name")]` (or no argument if there is no replacement) to the
   branch's or command's `CommandSettings` class.
2. Nothing else - no changes to `Execute`/`ExecuteAsync`, no per-command wiring, no test scaffolding
   beyond an attribute-presence guard test (see `ProfileSettingsDeprecationTests` in
   `dgt.power.profile.tests` for the pattern).

## Caveats

- Because `CommandTestContext<TCommand, TCommandSettings>` (`tests/dgt.power.tests`) calls
  `ICommand<T>.ExecuteAsync` directly, it bypasses Spectre's `CommandApp` pipeline entirely - including
  `ICommandInterceptor`. Command-level tests built on that harness cannot observe the deprecation
  warning. Instead, test `DeprecationInterceptor` directly (see `DeprecationInterceptorTests` in
  `dgt.power.cli.tests`) and add a lightweight attribute-presence guard test per deprecated
  branch/command to catch accidental attribute removal.
