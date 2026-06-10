// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using dgt.power.dataverse;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

public class TslTemplateContractTests
{
    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    [Test]
    public async Task ShouldRenderAllTemplatesWithMinimalValidContractModels()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "true", isCiAgent: false, maxStepsValue: null);
        var cases = new[]
        {
            new RenderCase("Entity.liquid", CreateEntityModel(), "account", null, "account.entity.d.ts", "namespace XrmTable.Account"),
            new RenderCase("EntityForm.liquid", CreateFormModel(), "account", "account.main.default", "account.main.form.d.ts", "declare namespace XrmForm.Account"),
            new RenderCase("EntityFormTestHelper.liquid", CreateFormModel(), "account", "account.main.default", "account.main.testhelper.ts", "TestHelper"),
            new RenderCase("OptionSets.liquid", CreateOptionSetModel(), null, null, "optionsetvalues.d.ts", "declare namespace XrmEnum"),
            new RenderCase("SdkMessages.liquid", CreateSdkMessagesModel(), null, null, "sdkmessagenames.d.ts", "declare namespace XrmMetadata"),
            new RenderCase("CustomApi.liquid", CreateCustomApiModel(), "sample_customapi", null, "sample_customapi.customapi.d.ts", "declare namespace XrmCustomApi")
        };

        foreach (var renderCase in cases)
        {
            var template = LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, renderCase.TemplateName);
            var context = new TemplateContext(renderCase.Model, options);
            var output = TslRenderDiagnostics.Render(
                template,
                context,
                renderCase.TemplateName,
                entityKey: renderCase.EntityKey,
                formKey: renderCase.FormKey,
                artifact: renderCase.Artifact);

            await Assert.That(output).Contains(renderCase.ExpectedFragment);
        }
    }

    [Test]
    public async Task ShouldFailFastWhenContractFieldsAreMissingInStrictMode()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "true", isCiAgent: false, maxStepsValue: null);
        var cases = new[]
        {
            new MissingContractCase("Entity.liquid", new { LogicalName = "account", Attributes = Array.Empty<AttributeMetadata>() }),
            new MissingContractCase("EntityForm.liquid", new { Name = "accountmain", Attributes = Array.Empty<AttributeMetadata>(), BpfControls = Array.Empty<BpfControlDetail>() }),
            new MissingContractCase("EntityFormTestHelper.liquid", new { Name = "accountmain", Attributes = Array.Empty<AttributeMetadata>(), BpfControls = Array.Empty<BpfControlDetail>() }),
            new MissingContractCase("OptionSets.liquid", new { }),
            new MissingContractCase("SdkMessages.liquid", new { }),
            new MissingContractCase("CustomApi.liquid", new { InParameters = Array.Empty<WfParameter>(), OutParameters = Array.Empty<WfParameter>() })
        };

        foreach (var missingContractCase in cases)
        {
            var template = LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, missingContractCase.TemplateName);
            var context = new TemplateContext(missingContractCase.Model, options);

            try
            {
                _ = TslRenderDiagnostics.Render(template, context, missingContractCase.TemplateName);
                throw new InvalidOperationException("Expected strict-mode contract failure.");
            }
            catch (InvalidOperationException ex)
            {
                await Assert.That(ex.Message).Contains(missingContractCase.TemplateName);
                await Assert.That(ex.InnerException is not null).IsTrue();
                await Assert.That(ex.InnerException!.Message).Contains("Undefined liquid value");
            }
        }
    }

    private static EntityViewModel CreateEntityModel()
    {
        var attribute = new StringAttributeMetadata
        {
            LogicalName = "name",
            SchemaName = "Name",
            RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
        };

        return new EntityViewModel
        {
            SchemaName = "Account",
            LogicalName = "account",
            LanguageCode = 1033,
            Attributes = [attribute]
        };
    }

    private static FormViewModel CreateFormModel()
    {
        var attribute = new StringAttributeMetadata
        {
            LogicalName = "name",
            SchemaName = "Name",
            RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None)
        };

        var formDetail = new FormDetail
        {
            FormEntityName = "account",
            FormUniqueName = "account.main.default",
            FormTypeName = "main",
            FormType = SystemForm.Options.Type.Main,
            FormId = Guid.Empty.ToString()
        };
        formDetail.Attributes.Add(new FormAttributeData
        {
            DataFieldName = "name",
            IsOptionalAttribute = false
        });
        formDetail.FormControls.Add(new FormXmlControlData
        {
            ControlId = "name",
            DataFieldName = "name",
            ClassId = ControlClassNames.XrmClassId.TextBox,
            IsSubgrid = false,
            IsWebResource = false,
            IsVisible = true,
            IsDisabled = false,
            RepeatedControl = 0
        });

        return new FormViewModel
        {
            Name = "accountmaindefault",
            FormDetail = formDetail,
            Attributes = [attribute],
            BpfControls = []
        };
    }

    private static OptionSetViewModel CreateOptionSetModel() =>
        new()
        {
            OptionSets =
            [
                new KeyValuePair<string, List<Option>>(
                    "status",
                    [new Option(0, "Active"), new Option(1, "Inactive")])
            ]
        };

    private static SdkMessagesViewModel CreateSdkMessagesModel() =>
        new()
        {
            SdkMessages =
            [
                new SdkMessageViewModel(("Create", "Create"))
            ]
        };

    private static CustomApiViewModel CreateCustomApiModel() =>
        new()
        {
            Name = "sample_customapi",
            InParameters =
            [
                new WfParameter
                {
                    UniqueName = "Target",
                    Type = "EntityReference",
                    IsOptional = false,
                    IsOutput = false
                }
            ],
            OutParameters =
            [
                new WfParameter
                {
                    UniqueName = "Result",
                    Type = "Guid",
                    IsOptional = false,
                    IsOutput = true
                }
            ]
        };

    private sealed record RenderCase(
        string TemplateName,
        object Model,
        string? EntityKey,
        string? FormKey,
        string Artifact,
        string ExpectedFragment);

    private sealed record MissingContractCase(string TemplateName, object Model);
}
