// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Local;

namespace dgt.power.plugin.tests.Local;

public class LocalPluginStepValidatorTests
{
    [Test]
    [Arguments("create")]
    [Arguments("CREATE")]
    public async Task Validate_CreatePostOperationPreImageMessageIsCaseInsensitive(string messageName)
    {
        var pluginType = new LocalPluginType("Contoso.CreatePlugin", "Contoso.CreatePlugin", string.Empty, true, [
            new LocalPluginStep("Create step", SdkMessageProcessingStep.Options.Mode.Synchronous, messageName, SdkMessageProcessingStep.Options.Stage.PostOperation, "account", "none", null, null, [
                new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "preImage", "Target", null)
            ])
        ]);

        await Assert.That(() => LocalPluginStepValidator.Validate(pluginType)).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    public async Task Validate_CreatePostOperationPreImage_Throws()
    {
        var pluginType = new LocalPluginType("Contoso.CreatePlugin", "Contoso.CreatePlugin", string.Empty, true, [
            new LocalPluginStep("Create step", SdkMessageProcessingStep.Options.Mode.Synchronous, "Create", SdkMessageProcessingStep.Options.Stage.PostOperation, "account", "none", null, null, [
                new LocalPluginStepImage(SdkMessageProcessingStepImage.Options.ImageType.PreImage, "PreImage", "preImage", "Target", null)
            ])
        ]);

        await Assert.That(() => LocalPluginStepValidator.Validate(pluginType)).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Asynchronous, SdkMessageProcessingStep.Options.Stage.PreValidation)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Asynchronous, SdkMessageProcessingStep.Options.Stage.PreOperation)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Asynchronous, SdkMessageProcessingStep.Options.Stage.MainOperationForInternalUseOnly)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Synchronous, SdkMessageProcessingStep.Options.Stage.MainOperationForInternalUseOnly)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Synchronous, 99)]
    [Arguments(7, SdkMessageProcessingStep.Options.Stage.PostOperation)]
    public async Task Validate_UnsupportedModeOrStage_Throws(int mode, int stage)
    {
        var pluginType = new LocalPluginType("Contoso.Plugin", "Contoso.Plugin", string.Empty, true, [new LocalPluginStep("step", mode, "Update", stage, "account", "none", null, null, [])]);

        await Assert.That(() => LocalPluginStepValidator.Validate(pluginType)).ThrowsExactly<InvalidPluginStepException>();
    }

    [Test]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Synchronous, SdkMessageProcessingStep.Options.Stage.PreValidation)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Synchronous, SdkMessageProcessingStep.Options.Stage.PreOperation)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Synchronous, SdkMessageProcessingStep.Options.Stage.PostOperation)]
    [Arguments(SdkMessageProcessingStep.Options.Mode.Asynchronous, SdkMessageProcessingStep.Options.Stage.PostOperation)]
    public async Task Validate_SupportedModeAndStage_DoesNotThrow(int mode, int stage)
    {
        var pluginType = new LocalPluginType("Contoso.Plugin", "Contoso.Plugin", string.Empty, true, [new LocalPluginStep("step", mode, "Update", stage, "account", "none", null, null, [])]);

        await Assert.That(() => LocalPluginStepValidator.Validate(pluginType)).ThrowsNothing();
    }
}
