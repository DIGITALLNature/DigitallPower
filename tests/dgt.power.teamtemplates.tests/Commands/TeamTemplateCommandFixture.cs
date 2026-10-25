// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.teamtemplates.Configuration;
using dgt.power.teamtemplates.tests.Logic;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.teamtemplates.tests.Commands;

public sealed class TeamTemplateCommandFixture : IDisposable
{
    public string DirectoryPath { get; } = Path.Combine(Path.GetTempPath(), $"teamtemplate-tests-{Guid.NewGuid():N}");
    public int AuthCalls { get; private set; }

    public string PathFor(string name) => Path.Combine(DirectoryPath, name);

    public string Write(string json)
    {
        Directory.CreateDirectory(DirectoryPath);
        var path = PathFor("input.json");
        File.WriteAllText(path, json);
        return path;
    }

    public string Write(params TeamTemplateEntry[] entries) => Write(TeamTemplateTransferTests.Config(entries).Serialize());

    public CommandTestContextBuilder<TCommand, TSettings> Builder<TCommand, TSettings>(IAnsiConsole console)
        where TCommand : class, ICommand<TSettings>
        where TSettings : CommandSettings
    {
        var connection = Mock.Of<IDataverseConnection>();
        var services = new TestServiceCollection().AddSingleton(connection.Object);
        return new CommandTestContextBuilder<TCommand, TSettings>()
            .WithServiceCollection(services)
            .WithAnsiConsole(console)
            .WithMetaData([TeamTemplateTransferTests.Metadata(), TeamTemplateTransferTests.Metadata("contact", 10001)])
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
            .WithCustomConfiguration(service => connection.GetServiceAsync().Returns(() =>
            {
                AuthCalls++;
                return service;
            }));
    }

    public void Dispose()
    {
        if (Directory.Exists(DirectoryPath))
        {
            Directory.Delete(DirectoryPath, recursive: true);
        }
    }
}
