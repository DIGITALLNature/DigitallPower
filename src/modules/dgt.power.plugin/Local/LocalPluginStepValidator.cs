// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;

namespace dgt.power.plugin.Local;

/// <summary>
/// Validates declared plugin steps/images against message-stage-image-type rules Dataverse itself
/// enforces (e.g. no post-image on a stage before the operation ran). Pure - depends only on the
/// declarative <see cref="LocalPluginType"/> model, so it can run immediately after parsing,
/// before any Dataverse round-trip.
/// </summary>
public static class LocalPluginStepValidator
{
    public static void Validate(LocalPluginType pluginType)
    {
        foreach (var step in pluginType.Steps)
        {
            ValidateStep(pluginType.TypeName, step);
            foreach (var image in step.Images)
            {
                ValidateImage(pluginType.TypeName, step, image);
            }
        }
    }

    private static void ValidateStep(string typeName, LocalPluginStep step)
    {
        if (step.Mode == SdkMessageProcessingStep.Options.Mode.Asynchronous && IsPreOperationStage(step.Stage))
        {
            throw new InvalidPluginStepException(
                $"Step '{step.Name}' on '{typeName}' is invalid: asynchronous mode is not allowed on a " +
                "pre-validation/pre-operation stage.");
        }
    }

    private static void ValidateImage(string typeName, LocalPluginStep step, LocalPluginStepImage image)
    {
        var isPreOperationStage = IsPreOperationStage(step.Stage);

        if (step.MessageName == "Create" &&
            image.ImageType == SdkMessageProcessingStepImage.Options.ImageType.PreImage &&
            isPreOperationStage)
        {
            throw new InvalidPluginStepException(
                $"Image '{image.Name}' on step '{step.Name}' ('{typeName}') is invalid: a pre-image is not " +
                "available for a 'Create' message on a pre-validation/pre-operation stage (the record does not " +
                "exist yet).");
        }

        var isMutationMessage = step.MessageName is "Create" or "Update" or "Delete";
        if (isMutationMessage &&
            image.ImageType == SdkMessageProcessingStepImage.Options.ImageType.PostImage &&
            isPreOperationStage)
        {
            throw new InvalidPluginStepException(
                $"Image '{image.Name}' on step '{step.Name}' ('{typeName}') is invalid: a post-image is not " +
                "available before the operation runs (pre-validation/pre-operation stage).");
        }

        if (step.MessageName == "Delete" &&
            image.ImageType == SdkMessageProcessingStepImage.Options.ImageType.PostImage &&
            step.Stage == SdkMessageProcessingStep.Options.Stage.PostOperation)
        {
            throw new InvalidPluginStepException(
                $"Image '{image.Name}' on step '{step.Name}' ('{typeName}') is invalid: a post-image is not " +
                "available for a 'Delete' message (the record no longer exists).");
        }
    }

    private static bool IsPreOperationStage(int stage) =>
        stage == SdkMessageProcessingStep.Options.Stage.PreValidation ||
        stage == SdkMessageProcessingStep.Options.Stage.PreOperation;
}
