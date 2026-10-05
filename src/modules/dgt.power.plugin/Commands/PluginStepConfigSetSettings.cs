// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.plugin.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.plugin.Commands;

/// <summary>
/// Settings for the <c>dgtp plugin step config set</c> command.
/// </summary>
public class PluginStepConfigSetSettings : PluginSettings
{
    // ==================== Step Resolution ====================

    [CommandOption("--step-id")]
    [Description("SdkMessageProcessingStep id. Mutually exclusive with composite key options.")]
    public Guid? StepId { get; init; }

    [CommandOption("--plugin-type")]
    [Description("Fully qualified type name of the plugin (e.g., MyNamespace.MyPluginClass).")]
    public string? PluginType { get; init; }

    [CommandOption("--message")]
    [Description("SDK message name (e.g., Create, Update, Delete).")]
    public string? Message { get; init; }

    [CommandOption("--stage")]
    [Description("Execution stage: PreValidation, PreOperation, PostOperation, PreCommitStage, PostCommitStage.")]
    [TypeConverter(typeof(StageTypeConverter))]
    public int? Stage { get; init; }

    [CommandOption("--entity")]
    [Description("Primary entity logical name (e.g., account, contact).")]
    public string? Entity { get; init; }

    [CommandOption("--secondary-entity")]
    [Description("Secondary entity logical name (for messages like Associate).")]
    public string? SecondaryEntity { get; init; }

    [CommandOption("--execution-order")]
    [Description("Execution order / rank.")]
    public int? ExecutionOrder { get; init; }

    // ==================== Configuration Values ====================

    [CommandOption("--unsecure")]
    [Description("Inline unsecure configuration value. Use empty string to clear.")]
    public string? UnsecureConfig { get; init; }

    [CommandOption("--unsecure-file")]
    [Description("Path to a UTF-8 file containing the unsecure configuration. Mutually exclusive with --unsecure.")]
    public FileInfo? UnsecureConfigFile { get; init; }

    [CommandOption("--secure")]
    [Description("Inline secure configuration value. Use empty string to clear.")]
    public string? SecureConfig { get; init; }

    [CommandOption("--secure-file")]
    [Description("Path to a UTF-8 file containing the secure configuration. Mutually exclusive with --secure.")]
    public FileInfo? SecureConfigFile { get; init; }

    // ==================== Validation ====================

    /// <inheritdoc />
    public override ValidationResult Validate()
    {
        // A vs B: step-id vs composite key
        var hasDirect = StepId.HasValue;
        var hasComposite = PluginType != null || Message != null || Stage != null || Entity != null;

        if (hasDirect && hasComposite)
        {
            return ValidationResult.Error("Cannot use --step-id together with composite key options (--plugin-type, --message, --stage, --entity).");
        }

        if (!hasDirect && !hasComposite)
        {
            return ValidationResult.Error("Either --step-id or composite key options (--plugin-type, --message, --stage, --entity) must be provided.");
        }

        // Unsecure: inline vs file
        if (UnsecureConfig != null && UnsecureConfigFile != null)
        {
            return ValidationResult.Error("Cannot use both --unsecure and --unsecure-file.");
        }

        // Secure: inline vs file
        if (SecureConfig != null && SecureConfigFile != null)
        {
            return ValidationResult.Error("Cannot use both --secure and --secure-file.");
        }

        // At least one config value
        if (UnsecureConfig == null && UnsecureConfigFile == null && SecureConfig == null && SecureConfigFile == null)
        {
            return ValidationResult.Error("At least one of --unsecure, --unsecure-file, --secure, or --secure-file must be provided.");
        }

        // File existence validation
        if (UnsecureConfigFile is { Exists: false })
        {
            return ValidationResult.Error($"File not found: {UnsecureConfigFile.FullName}");
        }

        if (SecureConfigFile is { Exists: false })
        {
            return ValidationResult.Error($"File not found: {SecureConfigFile.FullName}");
        }

        return ValidationResult.Success();
    }
}
