// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.documenttemplates.Commands;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateSettingsTests
{
    [Test]
    [Arguments(null, false)]
    [Arguments("", false)]
    [Arguments(" ", false)]
    [Arguments("nested/documenttemplates.json", true)]
    public async Task Import_ManifestPathRequired(string? manifest, bool valid)
    {
        var settings = new ImportDocumentTemplatesSettings { Manifest = manifest };
        await Assert.That(settings.Validate().Successful).IsEqualTo(valid);
    }

    [Test]
    [Arguments(null, null, null, false)]
    [Arguments(" ", null, null, false)]
    [Arguments("out", null, null, true)]
    [Arguments("out", "account", "word", true)]
    [Arguments("out", "account", "excel", true)]
    [Arguments("out", " account", null, false)]
    [Arguments("out", " ", null, false)]
    [Arguments("out", null, "WORD", false)]
    [Arguments("out", null, "pdf", false)]
    public async Task Export_FiltersAndDirectoryValidated(string? directory, string? entity, string? type, bool valid)
    {
        var settings = new ExportDocumentTemplatesSettings { Directory = directory, Entity = entity, Type = type };
        await Assert.That(settings.Validate().Successful).IsEqualTo(valid);
    }
}
