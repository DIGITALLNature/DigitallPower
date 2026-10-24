# Tasks

## 1. Extract Shared Telemetry Code to dgt.power.common

- [x] 1.1 Move `TelemetryNotice` from `src/dgt.power/Telemetry/TelemetryNotice.cs` to `src/dgt.power.common/Telemetry/TelemetryNotice.cs`, change `internal` to `public`, and update the namespace; verify `dotnet build src/dgt.power.common` and `dotnet build src/dgt.power` succeed
- [x] 1.2 Update the `using` in `src/dgt.power/Program.cs` to reference the new namespace; verify the first-run notice still works
- [x] 1.3 Create `src/dgt.power.common/Telemetry/TelemetryEnvironment.cs` as `public static class` with an `IsOptedOut` property that checks `DGTP_TELEMETRY_OPTOUT` and `DO_NOT_TRACK` via `ExecutionEnvironment.IsTruthy`; verify it compiles
- [x] 1.4 Update `TelemetryConfig.IsOptedOut` in `src/dgt.power/Telemetry/TelemetryConfig.cs` to delegate to `TelemetryEnvironment.IsOptedOut`; verify existing `TelemetryConfigTests` still pass

## 2. Create Module Project

- [x] 2.1 Create `src/modules/dgt.power.telemetry/dgt.power.telemetry.csproj` referencing `dgt.power.common`, and verify `dotnet build src/modules/dgt.power.telemetry` succeeds
- [x] 2.2 Create `src/modules/dgt.power.telemetry/Base/TelemetrySettings.cs` deriving from `BaseProgramSettings`, and verify it compiles
- [x] 2.3 Add the new project to `DigitallPower.slnx` under the `/Modules/` folder and to `src/dgt.power/dgt.power.csproj` project references, and verify `dotnet build src/dgt.power` succeeds

## 3. Implement Telemetry Commands

- [x] 3.1 Implement `TelemetryStatusCommand` in `src/modules/dgt.power.telemetry/Commands/TelemetryStatusCommand.cs` deriving from `PowerCommand<TelemetrySettings>`, injecting `StateStore` and `IAnsiConsole`; display status line with colored circle indicator (green `●` enabled / red `●` disabled), how-to line, optional env opt-out explanation, and the telemetry notice panel; verify `dgtp telemetry status --help` works
- [x] 3.2 Implement `TelemetryEnableCommand` in `src/modules/dgt.power.telemetry/Commands/TelemetryEnableCommand.cs` deriving from `PowerCommand<TelemetrySettings>`, calling `StateStore.SetTelemetryEnabled(true)` and printing confirmation (with env opt-out warning if applicable); verify `dgtp telemetry enable` persists `telemetry.enabled: true` in `state.json`
- [x] 3.3 Implement `TelemetryDisableCommand` in `src/modules/dgt.power.telemetry/Commands/TelemetryDisableCommand.cs` deriving from `PowerCommand<TelemetrySettings>`, calling `StateStore.SetTelemetryEnabled(false)` and printing confirmation; verify `dgtp telemetry disable` persists `telemetry.enabled: false` and preserves the install ID

## 4. Register Commands in Command Tree

- [x] 4.1 Add `telemetry` branch registration in `src/dgt.power/CommandTree.cs` with `status`, `enable`, and `disable` subcommands, including descriptions and examples; verify `dgtp telemetry --help` lists all three subcommands
- [x] 4.2 Add `using dgt.power.telemetry.Commands;` and `using dgt.power.telemetry.Base;` to `CommandTree.cs` as needed, and verify `dotnet build src/dgt.power` succeeds with 0 errors

## 5. Create Test Project

- [x] 5.1 Create `tests/dgt.power.telemetry.tests/dgt.power.telemetry.tests.csproj` referencing `dgt.power.common`, `dgt.power.telemetry`, `dgt.power.tests`, and TUnit; verify `dotnet build tests/dgt.power.telemetry.tests` succeeds
- [x] 5.2 Add the new test project to `DigitallPower.slnx` under the `/Tests/` folder, and verify the solution builds

## 6. Write Command Tests

- [x] 6.1 Write `TelemetryStatusCommandTests.cs` covering: enabled state shows green circle and "To disable" hint, disabled state shows red circle and "To enable" hint, env opt-out shows red circle with "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set." line, telemetry notice panel is displayed in all cases; verify all tests pass with `dotnet test --project tests/dgt.power.telemetry.tests`
- [x] 6.2 Write `TelemetryEnableCommandTests.cs` covering: persists enabled=true, confirmation message, environment opt-out warning, exit code 0; verify all tests pass
- [x] 6.3 Write `TelemetryDisableCommandTests.cs` covering: persists enabled=false, confirmation message, install ID preservation, exit code 0; verify all tests pass

## 7. Update CLI Tests

- [x] 7.1 Add `telemetry` to the `TopLevelPath_HelpInvocation_Succeeds` arguments in `tests/dgt.power.cli.tests/CommandTreeTests.cs`, and verify the test passes
- [x] 7.2 Add a `TelemetrySettings_ParsesOptions` test in `tests/dgt.power.cli.tests/SettingsParsingTests.cs` verifying `--no-telemetry` and other inherited options parse correctly, and verify the test passes

## 8. Update Documentation

- [x] 8.1 Update the README telemetry section: replace the manual `state.json` editing instructions with documentation for `dgtp telemetry status`, `dgtp telemetry enable`, and `dgtp telemetry disable` with examples; keep a brief note that `state.json` can still be edited directly for advanced use; verify the commands work as documented
- [x] 8.2 Add the `telemetry` branch to the command overview table in README if one exists, and verify the table is accurate

## 9. Final Verification

- [x] 9.1 Run `dotnet build` on the full solution and verify 0 errors
- [x] 9.2 Run `dotnet test` on the full solution and verify all tests pass
- [x] 9.3 Run `pnpm exec openspec validate --all` and verify the change validates
