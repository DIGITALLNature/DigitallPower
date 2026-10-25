// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using dgt.power.process.Configuration;

namespace dgt.power.process.tests.Configuration;

public class ProcessConfigurationTests
{
    [Test]
    public async Task Parse_ReadmeExampleFixture_AcceptsStrictV1AndSchema()
    {
        var json = await File.ReadAllTextAsync(
            Path.Combine(AppContext.BaseDirectory, "Resources", "ProcessConfiguration", "valid-v1.json"));
        var configuration = ProcessConfiguration.Parse(json);

        await Assert.That(configuration.Processes).Count().IsEqualTo(1);
        await Assert.That(configuration.Processes[0].ProcessId)
            .IsEqualTo(Guid.Parse("265a263f-958b-4d32-b89d-6b3fdeac0458"));
        await Assert.That(configuration.Processes[0].Name).IsEqualTo("Create additional Document Location");
        await Assert.That(configuration.Processes[0].Active).IsTrue();
        await Assert.That(configuration.Processes[0].Owner).IsEqualTo("integrationUser");

        using var schema = JsonDocument.Parse(await File.ReadAllTextAsync(
            Path.Combine(AppContext.BaseDirectory, "schemas", "process", "v1", "schema.json")));
        await Assert.That(schema.RootElement.GetProperty("properties").GetProperty("schemaVersion").GetProperty("const").GetInt32())
            .IsEqualTo(1);
        await Assert.That(schema.RootElement.GetProperty("$defs").GetProperty("process").GetProperty("properties")
            .GetProperty("processId").GetProperty("format").GetString()).IsEqualTo("uuid");
    }

    [Test]
    public async Task Parse_EmptyProcessesArray_IsValid()
    {
        var configuration = ProcessConfiguration.Parse("""{"schemaVersion":1,"processes":[]}""");

        await Assert.That(configuration.Processes).IsEmpty();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","name":"No intent"}]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","active":null}]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","owner":null}]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","owner":" integrationUser"}]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","unexpected":true,"active":true}]}""")]
    [Arguments("""{"schemaVersion":1,"owner":"default","processes":[]}""")]
    [Arguments("""{"schemaVersion":2,"processes":[]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"00000000-0000-0000-0000-000000000000","active":true}]}""")]
    [Arguments("""{"schemaVersion":1,"processes":[{"processId":"265a263f958b4d32b89d6b3fdeac0458","active":true}]}""")]
    public async Task Parse_InvalidV1Document_Rejects(string json)
    {
        await Assert.That(() => ProcessConfiguration.Parse(json)).Throws<ProcessConfigurationException>();
    }

    [Test]
    public async Task Parse_DuplicateProcessIds_Rejects()
    {
        const string json = """
        {"schemaVersion":1,"processes":[
          {"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","active":true},
          {"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","owner":"integrationUser"}
        ]}
        """;

        await Assert.That(() => ProcessConfiguration.Parse(json)).Throws<ProcessConfigurationException>();
    }

    [Test]
    public async Task Parse_DuplicateJsonProperty_Rejects()
    {
        const string json = """{"schemaVersion":1,"processes":[{"processId":"265a263f-958b-4d32-b89d-6b3fdeac0458","active":true,"active":false}]}""";

        await Assert.That(() => ProcessConfiguration.Parse(json)).Throws<ProcessConfigurationException>();
    }
}
