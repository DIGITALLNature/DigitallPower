// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.common.Ownership;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.process.Commands;

public sealed class ProcessInitSettings : BaseProgramSettings
{
    [CommandArgument(0, "<File>")]
    [Description("Process configuration output path.")]
    public string? File { get; init; }

    [CommandOption("--solution <UniqueName>")]
    [Description("Exact unique name of a solution to include; repeat to combine solution scopes.")]
#pragma warning disable CA1819 // Spectre.Console binds repeatable options through string arrays.
    public string[]? Solutions { get; init; }
#pragma warning restore CA1819

    [CommandOption("--capture <Scope>")]
    [Description("Capture scope: state, owner, or both.")]
    [DefaultValue("state")]
    public string Capture { get; init; } = "state";

    [CommandOption("--owner-mapping <File>")]
    [Description("Source environment owner mapping required when capturing ownership.")]
    public string? OwnerMapping { get; init; }

    [CommandOption("--unmapped-owners <Mode>")]
    [Description("Handling of owners without a source mapping key: fail or skip (omit the owner and warn).")]
    [DefaultValue(UnmappedOwnerHandlingOption.Fail)]
    public string UnmappedOwners { get; init; } = UnmappedOwnerHandlingOption.Fail;

    [CommandOption("--overwrite")]
    [Description("Replace an existing output file after capture and validation complete.")]
    public bool Overwrite { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("An output file path is required.");
        }

        var solutions = Solutions;
        if (solutions is not { Length: > 0 } ||
            Array.Exists(solutions, string.IsNullOrWhiteSpace) ||
            Array.Exists(solutions, solution => !string.Equals(solution, solution.Trim(), StringComparison.Ordinal)))
        {
            return ValidationResult.Error("Specify one or more exact, nonblank --solution unique names.");
        }

        if (Capture is not ("state" or "owner" or "both"))
        {
            return ValidationResult.Error("--capture must be state, owner, or both.");
        }

        if (!UnmappedOwnerHandlingOption.TryParse(UnmappedOwners, out _))
        {
            return ValidationResult.Error("--unmapped-owners must be fail or skip.");
        }

        if (Capture is "owner" or "both" && string.IsNullOrWhiteSpace(OwnerMapping))
        {
            return ValidationResult.Error("--owner-mapping is required for owner or both capture.");
        }

        if (!string.IsNullOrWhiteSpace(OwnerMapping))
        {
            try
            {
                OwnerMappingLoader.Load(OwnerMapping);
            }
            catch (Exception exception) when (exception is OwnerResolutionException or ArgumentException)
            {
                return ValidationResult.Error(exception.Message);
            }
        }

        var output = Path.GetFullPath(File, Environment.CurrentDirectory);
        if (!Overwrite && System.IO.File.Exists(output))
        {
            return ValidationResult.Error($"Output file '{output}' already exists. Use --overwrite to replace it.");
        }

        return ValidationResult.Success();
    }
}
