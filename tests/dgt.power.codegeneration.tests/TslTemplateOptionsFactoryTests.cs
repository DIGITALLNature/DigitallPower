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

public class TslTemplateOptionsFactoryTests
{
    private const string TslResourcePrefix = "dgt.power.codegeneration.Templates.tsl";

    [Test]
    public async Task ShouldUseDefaultMaxStepsWhenNoOverrideIsProvided()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: null, isCiAgent: false, maxStepsValue: null);
        await Assert.That(options.MaxSteps).IsEqualTo(TslTemplateOptionsFactory.DefaultTemplateMaxSteps);
    }

    [Test]
    public async Task ShouldUseMaxStepsOverrideWhenProvided()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: null, isCiAgent: false, maxStepsValue: "321");
        await Assert.That(options.MaxSteps).IsEqualTo(321);
    }

    [Test]
    public async Task ShouldRejectInvalidMaxStepsOverride()
    {
        try
        {
            _ = TslTemplateOptionsFactory.Create(strictModeValue: null, isCiAgent: false, maxStepsValue: "invalid");
            throw new InvalidOperationException("Expected invalid max-steps configuration to fail.");
        }
        catch (InvalidOperationException ex)
        {
            await Assert.That(ex.Message).Contains(TslTemplateOptionsFactory.MaxStepsEnvironmentVariable);
        }
    }

    [Test]
    public async Task ShouldFailFastInStrictModeWhenUndefinedValueIsReferenced()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "true", isCiAgent: false, maxStepsValue: null);
        var template = ParseTemplate("{{ Missing.Value }}");
        var context = new TemplateContext(new { }, options);

        try
        {
            _ = template.Render(context);
            throw new InvalidOperationException("Expected strict-mode render failure.");
        }
        catch (InvalidOperationException ex)
        {
            await Assert.That(ex.Message).Contains("Undefined liquid value");
        }
    }

    [Test]
    public async Task ShouldFailWhenTemplateStepsExceedConfiguredLimit()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "false", isCiAgent: false, maxStepsValue: "10");
        var template = ParseTemplate("{% for i in (1..200) %}{{ i }}{% endfor %}");
        var context = new TemplateContext(new { }, options);

        try
        {
            _ = template.Render(context);
            throw new InvalidOperationException("Expected render to exceed configured max steps.");
        }
        catch (InvalidOperationException ex)
        {
            await Assert.That(ex.Message.Length > 0).IsTrue();
        }
    }

    [Test]
    public async Task ShouldCreateReusableTemplateProfileForEntityAndFormRendering()
    {
        var options = TslTemplateOptionsFactory.Create(strictModeValue: "true", isCiAgent: false, maxStepsValue: null);
        var entityTemplate = LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, "Entity.liquid");
        var formTemplate = LiquidTemplateEngine.LoadTemplate(TslResourcePrefix, "EntityForm.liquid");

        var entityOutput = entityTemplate.Render(new TemplateContext(CreateEntityModel(), options));
        var formOutput = formTemplate.Render(new TemplateContext(CreateFormModel(), options));

        await Assert.That(entityOutput).Contains("namespace XrmTable.Account");
        await Assert.That(formOutput).Contains("declare namespace XrmForm.");
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

    private static IFluidTemplate ParseTemplate(string templateSource)
    {
        var parser = new FluidParser(new FluidParserOptions { AllowParentheses = true });
        if (!parser.TryParse(templateSource, out var template, out var error))
        {
            throw new InvalidOperationException($"Failed to parse test template: {error}");
        }

        return template ?? throw new InvalidOperationException("Template parser returned no template for test case.");
    }
}
