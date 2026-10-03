// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Collections.Frozen;
using dgt.power.dataverse;

namespace dgt.power.plugin;

/// <summary>
/// Maps user-friendly stage names to Dataverse OptionSet values for <c>sdkmessageprocessingstep.stage</c>.
/// </summary>
public static class StageMapping
{
    private static readonly FrozenDictionary<string, int> StageNameToValue = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        ["PreValidation"] = SdkMessageProcessingStep.Options.Stage.PreValidation,
        ["PreOperation"] = SdkMessageProcessingStep.Options.Stage.PreOperation,
        ["PostOperation"] = SdkMessageProcessingStep.Options.Stage.PostOperation,
        ["PreCommitStage"] = SdkMessageProcessingStep.Options.Stage.PreCommitStageFiredBeforeTransactionCommitForInternalUseOnly,
        ["PostCommitStage"] = SdkMessageProcessingStep.Options.Stage.PostCommitStageFiredAfterTransactionCommitForInternalUseOnly
    }.ToFrozenDictionary();

    /// <summary>
    /// Gets the OptionSet value for a given stage name.
    /// </summary>
    /// <param name="stageName">The stage name (e.g., "PreOperation", "PostOperation").</param>
    /// <returns>The corresponding OptionSet value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stage name is not recognized.</exception>
    public static int GetStageValue(string stageName)
    {
        if (StageNameToValue.TryGetValue(stageName, out var value))
        {
            return value;
        }

        throw new InvalidOperationException(
            $"Invalid stage name: '{stageName}'. Valid values: {string.Join(", ", StageNameToValue.Keys)}");
    }

    /// <summary>
    /// Gets all valid stage names.
    /// </summary>
    public static IEnumerable<string> GetValidStageNames() => StageNameToValue.Keys;
}
