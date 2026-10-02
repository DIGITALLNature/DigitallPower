// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.common.Connections;
using dgt.power.common.Storage;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace dgt.power.connection.tests.Base;

public class ConnectionTestsBase<TCommand, TCommandSettings> : CommandTestsBase<TCommand, TCommandSettings>
    where TCommandSettings : CommandSettings
    where TCommand : class, ICommand<TCommandSettings>
{
    private readonly TestServiceCollection _services;
    private readonly ServiceProvider _serviceProvider;
    private readonly string _directory;

    protected ConnectionTestsBase()
    {
        _directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"dgtp-tests-{Guid.NewGuid():N}");
        var home = new DgtpHome(_directory);
        var store = new ConnectionStore(home);
        var secretStore = new TestSecretStore();
        var userTokenCache = new TestUserTokenCache();
        _services = new TestServiceCollection();
        _services.AddSingleton<IConnectionStore>(store);
        _services.AddSingleton<ISecretStore>(secretStore);
        _services.AddSingleton<IUserTokenCache>(userTokenCache);
        _services.AddSingleton(ConnectionInvocationOptions.FromArguments([]));
        _serviceProvider = _services.BuildServiceProvider();
        ConnectionStore = store;
        SecretStore = secretStore;
        UserTokenCache = userTokenCache;
    }

    protected override CommandTestContextBuilder<TCommand, TCommandSettings> GetBuilder() =>
        base.GetBuilder().WithServiceCollection(_services);

    protected IConnectionStore ConnectionStore { get; }

    protected TestSecretStore SecretStore { get; }

    protected TestUserTokenCache UserTokenCache { get; }

    protected static JsonElement CreateAuthenticationRecord(string homeAccountId)
    {
        var json = JsonSerializer.Serialize(new
        {
            version = "1.0",
            username = "user@contoso.com",
            authority = "https://login.microsoftonline.com/tenant-id",
            homeAccountId,
            tenantId = "tenant-id",
            clientId = "client-id"
        });
        using var document = JsonDocument.Parse(json);
        return document.RootElement.Clone();
    }

    public override void Dispose()
    {
        _serviceProvider.Dispose();
        Directory.Delete(_directory, recursive: true);
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
