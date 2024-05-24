// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using dgt.power.common;
using dgt.power.common.Logic;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace dgt.power.profile.tests.Base;

public class ProfileTestsBase<TCommand, TCommandSettings> : CommandTestsBase<TCommand, TCommandSettings>, IDisposable
    where TCommandSettings : CommandSettings
    where TCommand : class, ICommand<TCommandSettings>
{
    private readonly TestServiceCollection _services;
    private readonly ServiceProvider _serviceProvider;
    private readonly IsolatedStorageFile _storage;

    public string IdentityFileName { get; } = $"{Guid.NewGuid():N}.dat";

    public ProfileTestsBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
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

    protected ProfileManager ProfileManager => _serviceProvider.GetRequiredService<IProfileManager>().As<ProfileManager>();

    protected Identities GetIdentities() => ProfileManager.LoadIdentities().As<Identities>();

    protected void AddIdentity(string name, string connectionString)
    {
        var profileManager = ProfileManager;
        profileManager.LoadIdentities().Upsert(name, new Identity {ConnectionString = connectionString});
        profileManager.Save();
    }

    public override void Dispose()
    {
        _storage.Remove();
        _storage.Dispose();
        base.Dispose();
    }
}
