// Copyright (c) DIGITALL Nature. All rights reserved

using System.ComponentModel;
using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.process.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.process.Commands;

public sealed class ProcessApplySettings : BaseProgramSettings
{
    [CommandArgument(0, "<File>")]
    [Description("Process configuration input path.")]
    public string? File { get; init; }

    [CommandOption("--owner-mapping <File>")]
    [Description("Target environment owner mapping.")]
    public string? OwnerMapping { get; init; }

    [CommandOption("--activate-as <OwnerKey>")]
    [Description("Run every state transition as the mapped Dataverse user.")]
    public string? ActivateAs { get; init; }

    [CommandOption("--activate-as-owner")]
    [Description("Run each process state transition as its configured target owner.")]
    public bool ActivateAsOwner { get; init; }

    [CommandOption("--dry-run")]
    [Description("Resolve and report transitions without writing to Dataverse.")]
    public bool DryRun { get; init; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(File))
        {
            return ValidationResult.Error("A process configuration file path is required.");
        }

        try
        {
            _ = ProcessConfiguration.Load(File);
        }
        catch (Exception exception) when (exception is ProcessConfigurationException or ArgumentException)
        {
            return ValidationResult.Error(exception.Message);
        }

        if (ActivateAsOwner && ActivateAs is not null)
        {
            return ValidationResult.Error("--activate-as and --activate-as-owner are mutually exclusive.");
        }

        if (ActivateAs is not null &&
            (string.IsNullOrWhiteSpace(ActivateAs) ||
             !string.Equals(ActivateAs, ActivateAs.Trim(), StringComparison.Ordinal)))
        {
            return ValidationResult.Error("--activate-as must be a nonblank, unpadded owner key.");
        }

        if ((ActivateAs is not null || ProcessHasOwnerIntent(File)) &&
            string.IsNullOrWhiteSpace(OwnerMapping))
        {
            return ValidationResult.Error("--owner-mapping is required for owner changes and --activate-as.");
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

        return ValidationResult.Success();
    }

    private static bool ProcessHasOwnerIntent(string file)
    {
        try
        {
            return ProcessConfiguration.Load(file).Processes.Any(process => process.Owner is not null);
        }
        catch (ProcessConfigurationException)
        {
            return false;
        }
    }
}
