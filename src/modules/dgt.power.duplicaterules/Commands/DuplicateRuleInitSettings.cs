// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.common.Ownership;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.duplicaterules.Commands;

public sealed class DuplicateRuleInitSettings : BaseProgramSettings
{
    [CommandArgument(0, "<File>")]
    [Description("Duplicate-rule configuration output path.")]
    public string? File { get; init; }

    [CommandOption("--capture <Scope>")]
    [Description("Capture scope: state, owner, or both.")]
    [DefaultValue("state")]
    public string Capture { get; init; } = "state";

    [CommandOption("--entity <BaseTable>")]
    [Description("Exact logical name of the rule base table to restrict capture to.")]
    public string? Entity { get; init; }

    [CommandOption("--rule-id <Guid>")]
    [Description("Duplicate rule ID to capture; repeat to select several rules.")]
#pragma warning disable CA1819 // Spectre.Console binds repeatable options through string arrays.
    public string[]? RuleIds { get; init; }
#pragma warning restore CA1819

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

    public IReadOnlyList<Guid> GetParsedRuleIds() =>
        (RuleIds ?? []).Select(id => Guid.ParseExact(id, "D")).Distinct().ToArray();

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("An output file path is required.");
        }

        if (Capture is not ("state" or "owner" or "both"))
        {
            return ValidationResult.Error("--capture must be state, owner, or both.");
        }

        if (!UnmappedOwnerHandlingOption.TryParse(UnmappedOwners, out _))
        {
            return ValidationResult.Error("--unmapped-owners must be fail or skip.");
        }

        if (Entity is not null &&
            (string.IsNullOrWhiteSpace(Entity) || !string.Equals(Entity, Entity.Trim(), StringComparison.Ordinal)))
        {
            return ValidationResult.Error("--entity must be a nonblank base-table logical name without surrounding whitespace.");
        }

        if (RuleIds is not null &&
            Array.Exists(RuleIds, id => !Guid.TryParseExact(id, "D", out var parsed) || parsed == Guid.Empty))
        {
            return ValidationResult.Error("Each --rule-id must be a single nonempty GUID.");
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
