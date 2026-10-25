// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.process.Configuration;
using dgt.power.process.Logic;
using Microsoft.Xrm.Sdk;

namespace dgt.power.process.tests.Logic;

public class ProcessConfigurationWriterTests
{
    [Test]
    public async Task Serialize_StateCapture_OrdersIdsAndOmitsOwner()
    {
        var later = new Workflow(Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"))
        {
            Name = "Later",
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            OwnerId = new EntityReference("systemuser", Guid.NewGuid())
        };
        var earlier = new Workflow(Guid.Parse("00000000-0000-0000-0000-000000000001"))
        {
            Name = "Earlier",
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
            OwnerId = new EntityReference("systemuser", Guid.NewGuid())
        };

        var configuration = ProcessConfiguration.Parse(
            ProcessConfigurationWriter.Serialize([later, earlier], "state", new Dictionary<Guid, string>()));

        await Assert.That(configuration.Processes[0].ProcessId).IsEqualTo(earlier.Id);
        await Assert.That(configuration.Processes[0].Active).IsTrue();
        await Assert.That(configuration.Processes[0].Owner).IsNull();
        await Assert.That(configuration.Processes[1].Active).IsFalse();
    }

    [Test]
    public async Task Serialize_OwnerCaptureRequiresMappingForEveryProcess()
    {
        var process = new Workflow(Guid.NewGuid()) { Name = "Example" };

        await Assert.That(() => ProcessConfigurationWriter.Serialize([process], "owner", new Dictionary<Guid, string>()))
            .Throws<InvalidOperationException>();
    }

    [Test]
    public async Task Serialize_OwnerOnlyCaptureOmitsStateAndPreservesLogicalKey()
    {
        var process = new Workflow(Guid.NewGuid())
        {
            Name = "Informational only",
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated)
        };

        var configuration = ProcessConfiguration.Parse(
            ProcessConfigurationWriter.Serialize([process], "owner", new Dictionary<Guid, string>
            {
                [process.Id] = "integrationUser"
            }));

        await Assert.That(configuration.Processes[0].Owner).IsEqualTo("integrationUser");
        await Assert.That(configuration.Processes[0].Active).IsNull();
        await Assert.That(configuration.Processes[0].Name).IsEqualTo("Informational only");
    }

    [Test]
    public async Task WriteAtomicallyAsync_ProtectsExistingOutputWithoutOverwrite()
    {
        var directory = Path.Combine(Environment.CurrentDirectory, $"process-test-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "config.json");
        await System.IO.File.WriteAllTextAsync(path, "original");
        try
        {
            await Assert.That(async () => await ProcessConfigurationWriter.WriteAtomicallyAsync(path, "replacement", false, CancellationToken.None))
                .Throws<IOException>();
            await Assert.That(await System.IO.File.ReadAllTextAsync(path)).IsEqualTo("original");
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }
}
