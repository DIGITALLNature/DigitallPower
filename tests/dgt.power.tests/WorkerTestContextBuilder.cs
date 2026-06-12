// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.CompilerServices;
using dgt.power.common;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

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
    private IServiceCollection _serviceCollection = new TestServiceCollection();
    private List<Entity> _data = [];
    private List<EntityMetadata> _metadata = [];
    private readonly List<RelationshipMetadataBase> _relationships = [];
    private readonly List<IOrganizationRequestFake> _requestFakes = [];
    private readonly List<Action<FakeOrganizationServiceAsync>> _customConfigurations = [];
    private Func<FakeOrganizationServiceAsync, IEnumerable<Entity>>? _dataPreparer;
    private IAnsiConsole? _console;

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithAnsiConsole(IAnsiConsole console)
    {
        _console = console;
        return this;
    }

    public WorkerTestContext<TWorker, TWorkerSettings> Build()
    {
        if (_console != null)
        {
            _serviceCollection.AddSingleton(_console);
        }

        var service = new FakeOrganizationServiceAsync();

        // Merge project-specific fakes with custom fakes (custom fakes win on conflict)
        var allFakes = new Dictionary<Type, IOrganizationRequestFake>
        {
            [typeof(RetrieveCurrentOrganizationRequest)] = new RetrieveCurrentOrganizationExecutor()
        };

        foreach (var fake in _requestFakes)
        {
            allFakes[fake.ForType] = fake;
        }

        service.AddRequests(allFakes.Values);

        // Add defaults (internally uses AddRequestIfNecessary, skips already registered)
        service.AddDefaultRequests();

        // Initialize metadata
        if (_metadata.Count != 0)
        {
            service.AddMetadata(_metadata);
        }

        // Add relationships
        if (_relationships.Count > 0)
        {
            service.AddRelationships(_relationships);
        }

        // Apply custom configurations
        foreach (var config in _customConfigurations)
        {
            config(service);
        }

        // Initialize data
        if (_data.Count != 0 || _dataPreparer != null)
        {
            if (_dataPreparer != null)
            {
                _data.AddRange(_dataPreparer(service));
            }

            // Auto-register metadata for entity types used in data that aren't already registered
            var registeredTypes = service.State.EntityMetadata.Keys.ToHashSet(StringComparer.OrdinalIgnoreCase);
            var missingTypes = _data
                .Select(e => e.LogicalName)
                .Where(name => !string.IsNullOrEmpty(name) && !registeredTypes.Contains(name))
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var entityName in missingTypes)
            {
                var meta = new EntityMetadata();
                meta.GetType().GetProperty(nameof(EntityMetadata.LogicalName))!.SetValue(meta, entityName);
                service.AddMetadata(meta);
            }

            service.AddRange(_data);
        }

        var defaultConfiguration = new Dictionary<string, string?>
        {
            {"pollrate", TestFixtures.FakeCallDurations.ToString()}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(defaultConfiguration)
            .Build();

        var worker = _serviceCollection
            .AddScoped<IOrganizationService>(_ => service)
            .AddSingleton<TWorker>()
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider()
            .GetRequiredService<TWorker>();

        return new WorkerTestContext<TWorker, TWorkerSettings>(worker, service,
            _serviceCollection.BuildServiceProvider().GetRequiredService<IConfigResolver>());
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithRelationship(
        RelationshipMetadataBase relationship)
    {
        _relationships.Add(relationship);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(IEnumerable<Entity> data)
    {
        _data.AddRange(data);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(Entity data) => WithData([data]);

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithData(
        Func<FakeOrganizationServiceAsync, IEnumerable<Entity>> dataPreparer)
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
        WithMetaData([metadata]);

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithMetaData(IEnumerable<EntityMetadata> metadata)
    {
        _metadata.AddRange(metadata);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithFakeMessageExecutor(
        IOrganizationRequestFake requestFake)
    {
        _requestFakes.Add(requestFake);
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithExecutionMock<TRequestMessage>(
        Func<OrganizationRequest, OrganizationResponse> executionHandler)
        where TRequestMessage : OrganizationRequest
    {
        _requestFakes.Add(new LambdaRequestFake<TRequestMessage>(executionHandler));
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithServiceCollection(
        IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
        return this;
    }

    public WorkerTestContextBuilder<TWorker, TWorkerSettings> WithCustomConfiguration(
        Action<FakeOrganizationServiceAsync> contextHandler)
    {
        _customConfigurations.Add(contextHandler);
        return this;
    }
}
