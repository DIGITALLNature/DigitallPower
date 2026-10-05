// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using System.Globalization;
using dgt.power.dataverse;

namespace dgt.power.plugin;

/// <summary>
/// Custom type converter for plugin stage names (e.g., "PreOperation", "PostOperation") to Dataverse OptionSet values.
/// </summary>
public sealed class StageTypeConverter : TypeConverter
{
    private static readonly StringComparer s_comparer = StringComparer.OrdinalIgnoreCase;

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        var stageName = value?.ToString();
        if (stageName is null)
        {
            return null;
        }

        return GetStageValue(stageName);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is null)
        {
            return null;
        }

        if (destinationType == typeof(string) && value is int stageValue)
        {
            return GetStageDisplayName(stageValue);
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    private static int GetStageValue(string stageName)
    {
        return stageName switch
        {
            _ when s_comparer.Equals(stageName, "PreValidation") => SdkMessageProcessingStep.Options.Stage.PreValidation,
            _ when s_comparer.Equals(stageName, "PreOperation") => SdkMessageProcessingStep.Options.Stage.PreOperation,
            _ when s_comparer.Equals(stageName, "PostOperation") => SdkMessageProcessingStep.Options.Stage.PostOperation,
            _ when s_comparer.Equals(stageName, "PreCommitStage") => SdkMessageProcessingStep.Options.Stage.PreCommitStageFiredBeforeTransactionCommitForInternalUseOnly,
            _ when s_comparer.Equals(stageName, "PostCommitStage") => SdkMessageProcessingStep.Options.Stage.PostCommitStageFiredAfterTransactionCommitForInternalUseOnly,
            _ => throw new InvalidOperationException(
                $"Invalid stage name: '{stageName}'. Valid values: PreValidation, PreOperation, PostOperation, PreCommitStage, PostCommitStage")
        };
    }

    public static string GetStageDisplayName(int stageValue)
    {
        return stageValue switch
        {
            SdkMessageProcessingStep.Options.Stage.PreValidation => "PreValidation",
            SdkMessageProcessingStep.Options.Stage.PreOperation => "PreOperation",
            SdkMessageProcessingStep.Options.Stage.PostOperation => "PostOperation",
            SdkMessageProcessingStep.Options.Stage.PreCommitStageFiredBeforeTransactionCommitForInternalUseOnly => "PreCommitStage",
            SdkMessageProcessingStep.Options.Stage.PostCommitStageFiredAfterTransactionCommitForInternalUseOnly => "PostCommitStage",
            _ => stageValue.ToString(CultureInfo.InvariantCulture)
        };
    }
}
