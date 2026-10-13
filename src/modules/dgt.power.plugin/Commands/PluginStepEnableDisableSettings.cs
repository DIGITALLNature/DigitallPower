// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.plugin.Base;
using dgt.power.plugin.Models;
using Spectre.Console.Cli;
using Spectre.Console;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.plugin.Commands;

/// <summary>
/// Settings for the <c>dgtp plugin step enable</c> and <c>dgtp plugin step disable</c> commands.
/// </summary>
public class PluginStepEnableDisableSettings : PluginSettings
{
    [CommandOption("--step-id")]
    [Description("SdkMessageProcessingStep id. Mutually exclusive with composite key options and --solution.")]
    public Guid? StepId { get; init; }

    [CommandOption("-s|--solution")]
    [Description("Unique name of the solution. Applies the state change to ALL plugin steps in that solution. Mutually exclusive with --step-id and composite key options.")]
    public string? Solution { get; init; }

    [CommandOption("--plugin-type")]
    [Description("Fully qualified type name of the plugin (e.g., MyNamespace.MyPluginClass).")]
    public string? PluginType { get; init; }

    [CommandOption("--message")]
    [Description("SDK message name (e.g., Create, Update, Delete).")]
    public string? Message { get; init; }

    [CommandOption("--stage")]
    [Description("Execution stage: PreValidation (10), PreOperation (20), MainOperation (30), or PostOperation (40).")]
    public PluginStepStage? Stage { get; init; }

    [CommandOption("--entity")]
    [Description("Primary entity logical name (e.g., account, contact).")]
    public string? Entity { get; init; }

    [CommandOption("--secondary-entity")]
    [Description("Secondary entity logical name (for messages like Associate).")]
    public string? SecondaryEntity { get; init; }

    [CommandOption("--execution-order")]
    [Description("Execution order / rank.")]
    public int? ExecutionOrder { get; init; }

    [CommandOption("--reason")]
    [Description("Optional reason for the state change. If provided, it replaces the step's description; steps already in the desired state are left untouched.")]
    public string? Reason { get; init; }

    [CommandOption("--dry-run")]
    [Description("Only report which steps would change; do not write anything to Dataverse.")]
    public bool DryRun { get; init; }

    /// <inheritdoc />
    public override ValidationResult Validate()
    {
        // A vs B vs C: step-id vs composite key vs solution
        var hasDirect = StepId.HasValue;
        var hasSolution = !string.IsNullOrWhiteSpace(Solution);
        var hasCompositeKeyOptions = PluginType != null || Message != null || Stage.HasValue || Entity != null;
        var hasCompositeDisambiguators = SecondaryEntity != null || ExecutionOrder.HasValue;
        var hasCompositeOptions = hasCompositeKeyOptions || hasCompositeDisambiguators;
        var hasCompositeKey = PluginType != null && Message != null && Stage.HasValue && Entity != null;

        if (Stage is { } stage && (stage == PluginStepStage.None || !Enum.IsDefined(stage)))
        {
            return ValidationResult.Error($"Invalid stage value: {(int)stage}.");
        }

        if (hasDirect && hasCompositeOptions)
        {
            return ValidationResult.Error("Cannot use --step-id together with composite key options (--plugin-type, --message, --stage, --entity, --secondary-entity, --execution-order).");
        }

        if (hasDirect && hasSolution)
        {
            return ValidationResult.Error("Cannot use --step-id together with --solution.");
        }

        if (hasSolution && hasCompositeOptions)
        {
            return ValidationResult.Error("Cannot use --solution together with composite key options (--plugin-type, --message, --stage, --entity, --secondary-entity, --execution-order).");
        }

        if (!hasDirect && !hasCompositeKey && !hasSolution)
        {
            return ValidationResult.Error("Either --step-id, all required composite key options (--plugin-type, --message, --stage, --entity), or --solution must be provided.");
        }

        return ValidationResult.Success();
    }
}
