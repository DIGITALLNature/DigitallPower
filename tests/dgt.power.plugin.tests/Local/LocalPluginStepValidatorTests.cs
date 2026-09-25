// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Local;

namespace dgt.power.plugin.tests.Local;

public class LocalPluginStepValidatorTests
{
    [Test]
    public async Task Validate_CreatePostOperationPreImage_Throws()
    {
        var pluginType = new LocalPluginType(
            "Contoso.CreatePlugin",
            "Contoso.CreatePlugin",
            string.Empty,
            true,
            [
                new LocalPluginStep(
                    "Create step",
                    SdkMessageProcessingStep.Options.Mode.Synchronous,
                    "Create",
                    SdkMessageProcessingStep.Options.Stage.PostOperation,
                    "account",
                    "none",
                    null,
                    null,
                    null,
                    [
                        new LocalPluginStepImage(
                            SdkMessageProcessingStepImage.Options.ImageType.PreImage,
                            "PreImage",
                            "preImage",
                            "Target",
                            null)
                    ])
            ]);

        await Assert.That(() => LocalPluginStepValidator.Validate(pluginType))
            .ThrowsExactly<InvalidPluginStepException>();
    }
}
