// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.CompilerServices;
using dgt.power.common;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console.Cli;

namespace dgt.power.tests;

/// <summary>
/// This class serves as a base class for initializing command unit tests.
/// </summary>
public class CommandTestContextBuilder<TCommand, TCommandSettings>
    where TCommand : class, ICommand<TCommandSettings>
    where TCommandSettings : CommandSettings
{
    private IServiceCollection _serviceCollection;
    private IEnumerable<Entity> _data = new List<Entity>();
    private IEnumerable<EntityMetadata> _metadata = new List<EntityMetadata>();
    private readonly List<RelationshipMetadataBase> _relationships = new();
    private readonly List<IOrganizationRequestFake> _requestFakes = new();
    private readonly List<Action<FakeOrganizationServiceAsync>> _customConfigurations = new();
    private Func<FakeOrganizationServiceAsync, IEnumerable<Entity>>? _dataPreparer;

    public CommandTestContextBuilder()
    {
        _serviceCollection = new TestServiceCollection();
    }

    /// <summary>
    /// Builds the command test context to execute a command.
    /// </summary>
    /// <returns>The command test context.</returns>
    public CommandTestContext<TCommand, TCommandSettings> Build()
    {
        var service = new FakeOrganizationServiceAsync();

        // Merge project-specific fakes with custom fakes (custom fakes win on conflict)
        var allFakes = new Dictionary<Type, IOrganizationRequestFake>
        {
            [typeof(Microsoft.Crm.Sdk.Messages.RetrieveCurrentOrganizationRequest)] = new FakeExecutor.RetrieveCurrentOrganizationExecutor()
        };

        foreach (var fake in _requestFakes)
        {
            allFakes[fake.ForType] = fake;
        }

        service.AddRequests(allFakes.Values);

        // Add defaults (internally uses AddRequestIfNecessary, skips already registered)
        service.AddDefaultRequests();

        // Initialize metadata
        if (_metadata.Count() > 0)
        {
            service.AddMetadata(_metadata);
        }

        // Add relationships
        if (_relationships.Count() > 0)
        {
            service.AddRelationships(_relationships);
        }

        // Apply custom configurations
        foreach (var config in _customConfigurations)
        {
            config(service);
        }

        // Initialize data
        if (_data.Count() > 0 || _dataPreparer != null)
        {
            if (_dataPreparer != null)
            {
                _data = _data.Concat(_dataPreparer(service));
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

        var command = _serviceCollection
            .AddScoped<IOrganizationService>(_ => service)
            .AddSingleton<TCommand>()
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider()
            .GetRequiredService<TCommand>();

        return new CommandTestContext<TCommand, TCommandSettings>(command, service,
            _serviceCollection.BuildServiceProvider().GetRequiredService<IConfigResolver>());
    }

    public CommandTestContextBuilder<TCommand, TCommandSettings> WithRelationship(
        RelationshipMetadataBase relationship)
    {
        _relationships.Add(relationship);
        return this;
    }

    /// <summary>
    /// Adds the given entities as data to the service
    /// </summary>
    /// <param name="data">The entities to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(IEnumerable<Entity> data)
    {
        _data = _data.Concat(data);
        return this;
    }

    /// <summary>
    /// Adds the given entity as data to the service
    /// </summary>
    /// <param name="data">The entity to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(Entity data) => WithData(new[] {data});

    /// <summary>
    /// Adds the given entities as data to the service.
    /// </summary>
    /// <param name="dataPreparer">The function to prepare context data.</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(
        Func<FakeOrganizationServiceAsync, IEnumerable<Entity>> dataPreparer)
    {
        _dataPreparer = dataPreparer;
        return this;
    }

    /// <summary>
    /// Adds the entities in a tuple to the service. Only tuple members that derive <see cref="Entity"/> class are
    /// added. Additional members are ignored.
    /// </summary>
    /// <param name="tuple">The tuple containing the entities to add.</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(ITuple tuple)
    {
        var data = Enumerable.Range(0, tuple.Length)
            .Select(i => tuple[i])
            .OfType<Entity>()
            .ToList();
        WithData(data);
        return this;
    }

    /// <summary>
    /// Adds the given entity metadata to the service
    /// </summary>
    /// <param name="metadata">The entity metadata to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithMetaData(EntityMetadata metadata)
    {
        return WithMetaData(new[] {metadata});
    }

    /// <summary>
    /// Adds the given entity metadata to the service
    /// </summary>
    /// <param name="metadata">The entity metadata to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithMetaData(IEnumerable<EntityMetadata> metadata)
    {
        _metadata = _metadata.Concat(metadata);
        return this;
    }

    public CommandTestContextBuilder<TCommand, TCommandSettings> WithFakeMessageExecutor(
        IOrganizationRequestFake requestFake)
    {
        _requestFakes.Add(requestFake);
        return this;
    }

    /// <summary>
    /// Register an execution mock for the given request type.
    /// </summary>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithExecutionMock<TRequestMessage>(
        Func<OrganizationRequest, OrganizationResponse> executionHandler)
        where TRequestMessage : OrganizationRequest
    {
        _requestFakes.Add(new LambdaRequestFake<TRequestMessage>(executionHandler));
        return this;
    }

    /// <summary>
    /// Specify the service collection that is used to generate the command.
    /// If no <see cref="IServiceCollection"/> is specified <see cref="TestServiceCollection"/> is used.
    /// </summary>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithServiceCollection(
        IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
        return this;
    }

    /// <summary>
    /// Adds a custom configuration to the fake service.
    /// </summary>
    /// <param name="contextHandler">A function containing the custom configuration which should be applied to the service.</param>
    /// <returns>The test context builder.</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithCustomConfiguration(
        Action<FakeOrganizationServiceAsync> contextHandler)
    {
        _customConfigurations.Add(contextHandler);
        return this;
    }
}
