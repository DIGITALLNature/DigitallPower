// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Nodes;
using dgt.power.common.Storage;

namespace dgt.power.cli.tests;

public class StateStoreTests
{
    [Test]
    public async Task MissingState_HasDefaultsWithoutWriting()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            var store = new StateStore(home);
            await Assert.That(store.TelemetryEnabled).IsTrue();
            await Assert.That(store.LastVersionCheckOn).IsEqualTo(DateTime.MinValue);
            await Assert.That(File.Exists(home.StatePath)).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Mutations_WriteNestedStateAndRetainInstallIdWhenReenabled()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            var store = new StateStore(home);
            var id = store.GetOrCreateTelemetryInstallId(out var created);
            store.SetTelemetryEnabled(false);
            store.SetLastVersionCheckOn(new DateTime(2026, 10, 7, 0, 0, 0, DateTimeKind.Utc));
            var reloaded = new StateStore(home);
            await Assert.That(created).IsTrue();
            await Assert.That(reloaded.TelemetryEnabled).IsFalse();
            await Assert.That(reloaded.LastVersionCheckOn).IsEqualTo(new DateTime(2026, 10, 7));
            reloaded.SetTelemetryEnabled(true);
            await Assert.That(reloaded.GetOrCreateTelemetryInstallId(out var createdAgain)).IsEqualTo(id);
            await Assert.That(createdAgain).IsFalse();

            var state = JsonNode.Parse(await File.ReadAllTextAsync(home.StatePath))!;
            await Assert.That(state["schemaVersion"]!.GetValue<int>()).IsEqualTo(1);
            await Assert.That(state["telemetry"]!["enabled"]!.GetValue<bool>()).IsTrue();
            await Assert.That(state["telemetry"]!["installId"]!.GetValue<string>()).IsEqualTo(id);
            await Assert.That(state["updates"]!["lastCheckOn"]).IsNotNull();
            await Assert.That(state.AsObject().ContainsKey("firstRunOn")).IsFalse();
            await Assert.That(state.AsObject().ContainsKey("TelemetryNoticeShownOn")).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task LegacyState_PreservesIdAndUpdateCheckWhenMigrated()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            const string id = "0cd02469-3c53-4102-b9c3-a9c3742a1128";
            await File.WriteAllTextAsync(home.StatePath, """
                {
                  "TelemetryInstallId": "0cd02469-3c53-4102-b9c3-a9c3742a1128",
                  "TelemetryNoticeShownOn": "2026-10-01T00:00:00Z",
                  "LastVersionCheckOn": "2026-10-07T00:00:00Z",
                  "future": { "value": 42 }
                }
                """);
            var store = new StateStore(home);
            await Assert.That(store.GetOrCreateTelemetryInstallId(out var created)).IsEqualTo(id);
            await Assert.That(created).IsFalse();
            await Assert.That(store.LastVersionCheckOn).IsEqualTo(new DateTime(2026, 10, 7));
            store.SetTelemetryEnabled(false);
            var state = JsonNode.Parse(await File.ReadAllTextAsync(home.StatePath))!;
            await Assert.That(state["telemetry"]!["installId"]!.GetValue<string>()).IsEqualTo(id);
            await Assert.That(state["future"]!["value"]!.GetValue<int>()).IsEqualTo(42);
            await Assert.That(state.AsObject().ContainsKey("TelemetryNoticeShownOn")).IsFalse();
            await Assert.That(state.AsObject().ContainsKey("TelemetryInstallId")).IsFalse();
            await Assert.That(state.AsObject().ContainsKey("LastVersionCheckOn")).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Mutations_PreserveUnknownPropertiesAtEveryLevel()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            await File.WriteAllTextAsync(home.StatePath, """
                {
                  "schemaVersion": 1,
                  "future": { "value": 42 },
                  "telemetry": { "future": ["keep", "me"] },
                  "updates": { "future": true }
                }
                """);
            var store = new StateStore(home);
            store.GetOrCreateTelemetryInstallId();
            store.SetTelemetryEnabled(false);
            store.SetLastVersionCheckOn(DateTime.UtcNow);
            var state = JsonNode.Parse(await File.ReadAllTextAsync(home.StatePath))!;
            await Assert.That(state["future"]!["value"]!.GetValue<int>()).IsEqualTo(42);
            await Assert.That(state["telemetry"]!["future"]!.ToJsonString()).IsEqualTo("""["keep","me"]""");
            await Assert.That(state["updates"]!["future"]!.GetValue<bool>()).IsTrue();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    [Arguments("""{"schemaVersion":2}""")]
    [Arguments("""{"schemaVersion":null}""")]
    [Arguments("""{"schemaVersion":"1"}""")]
    [Arguments("""{"telemetry":{"enabled":false}}""")]
    [Arguments("""{"schemaVersion":1,"telemetry":null}""")]
    [Arguments("""{"schemaVersion":1,"updates":null}""")]
    [Arguments("""{"schemaVersion":1,"telemetry":{"installId":"invalid"}}""")]
    [Arguments("""{"schemaVersion":1,"telemetry":{"enabled":"false"}}""")]
    [Arguments("""{"schemaVersion":1,"updates":{"lastCheckOn":"invalid"}}""")]
    [Arguments("null")]
    [Arguments("[]")]
    [Arguments("{")]
    public async Task InvalidState_ThrowsWithoutRewriting(string json)
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            await File.WriteAllTextAsync(home.StatePath, json);
            var store = new StateStore(home);
            await Assert.That(() => store.SetTelemetryEnabled(false)).ThrowsException();
            await Assert.That(await File.ReadAllTextAsync(home.StatePath)).IsEqualTo(json);
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task ConcurrentInitialization_CreatesOnlyOneInstallId()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-state-");
        try
        {
            var home = new DgtpHome(directory.FullName);
            var results = await Task.WhenAll(Enumerable.Range(0, 8).Select(_ => Task.Run(() =>
            {
                var store = new StateStore(home);
                var id = store.GetOrCreateTelemetryInstallId(out var created);
                return (Id: id, Created: created);
            })));
            await Assert.That(results.Select(result => result.Id).Distinct().Count()).IsEqualTo(1);
            await Assert.That(results.Count(result => result.Created)).IsEqualTo(1);
        }
        finally
        {
            directory.Delete(true);
        }
    }
}
