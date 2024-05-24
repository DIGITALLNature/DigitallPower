// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.push.Model;

namespace dgt.power.push.Logic;

internal static class AssemblyValidator
{
    public static void ValidateImage(string step, string message, int stage, int imageType)
    {
        if (message == "Create" &&
            imageType == SdkMessageProcessingStepImage.Options.ImageType.PreImage &&
            (stage == SdkMessageProcessingStep.Options.Stage.PreValidation || stage == SdkMessageProcessingStep.Options.Stage.PreOperation))
        {
            throw new AssemblyException($"Invalid step image setup! Check: {step}");
        }

        if ((message == "Create" || message == "Update" || message == "Delete") &&
            imageType == SdkMessageProcessingStepImage.Options.ImageType.PostImage &&
            (stage == SdkMessageProcessingStep.Options.Stage.PreValidation || stage == SdkMessageProcessingStep.Options.Stage.PreOperation))
        {
            throw new AssemblyException($"Invalid step image setup! Check: {step}");
        }

        if (message == "Delete" &&
            imageType == SdkMessageProcessingStepImage.Options.ImageType.PostImage &&
            stage == SdkMessageProcessingStep.Options.Stage.PostOperation)
        {
            throw new AssemblyException($"Invalid step image setup! Check: {step}");
        }
    }

    public static void Validate(SdkMessageProcessingStep step)
    {
        if (step.Mode!.Value == SdkMessageProcessingStep.Options.Mode.Asynchronous &&
            (step.Stage!.Value == SdkMessageProcessingStep.Options.Stage.PreValidation || step.Stage.Value == SdkMessageProcessingStep.Options.Stage.PreOperation))
        {
            throw new AssemblyException($"Invalid step setup! Check: {step.Name}");
        }
    }
}
