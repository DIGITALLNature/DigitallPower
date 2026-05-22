// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using System.Runtime.CompilerServices;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.tests.Extensions;
using FakeXrmEasy;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Enums;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using FakeXrmEasy.Abstractions.Middleware;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Middleware;
using FakeXrmEasy.Middleware.Crud;
using FakeXrmEasy.Middleware.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Xunit.Abstractions;

namespace dgt.power.tests;

/// <summary>
/// Builder for <see cref="WorkerTestContext{TWorker,TWorkerSettings}"/>. Mirrors
/// <see cref="CommandTestContextBuilder{TCommand,TCommandSettings}"/> but resolves a
/// <see cref="PowerWorker{TConfig}"/> instead of a Spectre command.
/// </summary>
public class WorkerTestContextBuilder<TWorker, TWorkerSettings>
    where TWorker : PowerWorker<TWorkerSettings>
    where TWorkerSettings : BaseProgramSettings
{
    private readonly ITestOutputHelper? _testOutputHelper;
    private readonly IMiddlewareBuilder _builder;
    private IServiceCollection _serviceCollection;
    private IEnumerable<Entity> _data = new List<Entity>();
    private IEnumerable<EntityMetadata> _metadata = new List<EntityMetadata>();

    private readonly IDictionary<string, XrmFakedRelationship> _relationships =
        new Dictionary<string, XrmFakedRelationship>();

    private Func<IXrmFakedContext, IEnumerable<Entity>>? _dataPreparer;

    public WorkerTestContextBuilder(ITestOutputHelper? testOutputHelper = null)
    {
        _testOutputHelper = testOutputHelper;
        _serviceCollection = new TestServiceCollection();
        _builder = MiddlewareBuilder
            .New()
            .SetLicense(FakeXrmEasyLicense.RPL_1_5)
            .AddFakeMessageExecutors(Assembly.GetAssembly(typeof(WhoAmIRequestExecutor)))
            .Add(context => context.EnableProxyTypes(Assembly.GetAssembly(typeof(DataContext))));
    }

    public WorkerTestContext<TWorker, TWorkerSettings> Build()
    {
        var fakedContext = _builder
            .Add(InitializeDataAndMetadata)
            .AddCrud()
            .UseMessages()
            .UseCrud()
            .Build();

        if (_testOutputHelper != null)
        {
            _serviceCollection
                .AddSingleton<ITestOutputHelper>(_ => _testOutputHelper);
        }

        var defaultConfiguration = new Dictionary<string, string?>
        {
            {"pollrate", TestFixtures.FakeCallDurations.ToString()}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(defaultConfiguration)
            .Build();

        var worker = _serviceCollection
            .AddScoped<IOrganizationService>(_ => fakedContext.GetAsyncOrganizationService2())
            .AddSingleton<TWorker>()
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider()
            .GetRequiredService<TWorker>();

        return new WorkerTestContext<TWorker, TWorkerSettings>(worker, fakedContext,
            _serviceCollection.BuildServiceProvider().GetRequiredService<IConfigResolver>());
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithRelationship(string schemaName,
        XrmFakedRelationship relationship)
    {
        _relationships[schemaName] = relationship;
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(IEnumerable<Entity> data)
    {
        _data = _data.Concat(data);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(Entity data) => WithData(new[] {data});

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(
        Func<IXrmFakedContext, IEnumerable<Entity>> dataPreparer)
    {
        _dataPreparer = dataPreparer;
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(ITuple tuple)
    {
        var data = Enumerable.Range(0, tuple.Length)
            .Select(i => tuple[i])
            .OfType<Entity>()
            .ToList();
        WithData(data);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithMetaData(EntityMetadata metadata) =>
        WithMetaData(new[] {metadata});

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithMetaData(IEnumerable<EntityMetadata> metadata)
    {
        _metadata = _metadata.Concat(metadata);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithFakeMessageExecutor<TRequestMessage>(
        IFakeMessageExecutor messageExecutor) where TRequestMessage : OrganizationRequest
    {
        _builder.AddFakeMessageExecutor<TRequestMessage>(messageExecutor);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithExecutionMock<TRequestMessage>(
        OrganizationRequestExecution executionHandler)
        where TRequestMessage : OrganizationRequest
    {
        _builder.AddExecutionMock<TRequestMessage>(executionHandler);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithServiceCollection(
        IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithCustomConfiguration(
        Action<IXrmFakedContext> contextHandler)
    {
        _builder.Add(contextHandler);
        return this;
    }

    private void InitializeDataAndMetadata(IXrmFakedContext context)
    {
        if (_metadata.Any())
        {
            context.InitializeMetadata(_metadata);
            context.InitializeRelationshipMetadata(_metadata.ToList());
            context.InitializeGlobalOptionsetMetadata(_metadata.ToList());
        }
        else
        {
            context.InitializeMetadata(Assembly.GetAssembly(typeof(DataContext)));
        }

        if (_relationships.Any())
        {
            foreach (var relationship in _relationships)
            {
                context.AddRelationship(relationship.Key, relationship.Value);
            }
        }

        if (_data.Any() || _dataPreparer != null)
        {
            if (_dataPreparer != null)
            {
                _data = _data.Concat(_dataPreparer(context));
            }

            context.Initialize(_data);
        }
    }
}
