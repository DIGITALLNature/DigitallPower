// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.documenttemplates.Logic;
using Spectre.Console.Testing;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateAuthenticationTests
{
    [Test]
    public async Task Export_InteractiveLoginRequiredPropagatesWithoutWriting()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var connection = Mock.Of<IDataverseConnection>();
            connection.GetServiceAsync().Throws(new InteractiveLoginRequiredException());
            using var console = new TestConsole();
            var engine = new DocumentTemplateExportEngine(connection.Object, console);
            await Assert.That(async () => await engine.ExportAsync(directory, null, null, false, CancellationToken.None))
                .Throws<InteractiveLoginRequiredException>();
            await Assert.That(Directory.GetFiles(directory)).IsEmpty();
            await Assert.That(console.Output).DoesNotContain("Export failed");
        }
        finally { Directory.Delete(directory, true); }
    }
}
