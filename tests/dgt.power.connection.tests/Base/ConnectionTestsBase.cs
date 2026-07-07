// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using dgt.power.common;
using dgt.power.common.Logic;
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
    private readonly IsolatedStorageFile _storage;

    public string IdentityFileName { get; } = $"{Guid.NewGuid():N}.dat";

    public ConnectionTestsBase()
    {
        _services = new TestServiceCollection();
        _services.AddSingleton<IsolatedStorageFile>(_ => IsolatedStorageFile.GetUserStoreForAssembly());
        _services.AddTransient<IProfileManager, ProfileManager>(provider =>
            new ProfileManager(provider.GetRequiredService<IsolatedStorageFile>(), IdentityFileName));
        _serviceProvider = _services.BuildServiceProvider();
        _storage = _serviceProvider.GetRequiredService<IsolatedStorageFile>();
        ProfileManager.Purge();
    }

    protected override CommandTestContextBuilder<TCommand, TCommandSettings> GetBuilder()
    {
        return base.GetBuilder().WithServiceCollection(_services);
    }

    protected IProfileManager ProfileManager => _serviceProvider.GetRequiredService<IProfileManager>();

    protected IIdentities GetIdentities() => ProfileManager.LoadIdentities();

    public override void Dispose()
    {
        _storage.Remove();
        _storage.Dispose();
        _serviceProvider.Dispose();
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
