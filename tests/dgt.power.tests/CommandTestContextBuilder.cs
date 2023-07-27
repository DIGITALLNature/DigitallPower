// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using System.Runtime.CompilerServices;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.tests.Extensions;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Enums;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using FakeXrmEasy.Abstractions.Middleware;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Middleware;
using FakeXrmEasy.Middleware.Crud;
using FakeXrmEasy.Middleware.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console.Cli;
using Xunit.Abstractions;

namespace dgt.power.tests;

/// <summary>
/// This class serves as a base class for initializing command unit tests.
/// </summary>
public class CommandTestContextBuilder<TCommand, TCommandSettings>
    where TCommand : class, ICommand<TCommandSettings>
    where TCommandSettings : CommandSettings
{
    private readonly ITestOutputHelper? _testOutputHelper;
    private readonly IMiddlewareBuilder _builder;
    private IServiceCollection _serviceCollection;
    private IEnumerable<Entity> _data = new List<Entity>();
    private IEnumerable<EntityMetadata> _metadata = new List<EntityMetadata>();

    private readonly IDictionary<string, XrmFakedRelationship> _relationships =
        new Dictionary<string, XrmFakedRelationship>();

    private Func<IXrmFakedContext, IEnumerable<Entity>>? _dataPreparer;


    public CommandTestContextBuilder(ITestOutputHelper? testOutputHelper = null)
    {
        _testOutputHelper = testOutputHelper;
        _serviceCollection = new TestServiceCollection();
        _builder = MiddlewareBuilder
            .New()
            .SetLicense(FakeXrmEasyLicense.RPL_1_5)
            .AddFakeMessageExecutors(Assembly.GetAssembly(typeof(WhoAmIRequestExecutor)))
            .Add(context => context.EnableProxyTypes(Assembly.GetAssembly(typeof(DataContext))));
    }

    /// <summary>
    /// Builds the command test context to execute a command.
    /// </summary>
    /// <returns>The command test context.</returns>
    public CommandTestContext<TCommand, TCommandSettings> Build()
    {
        var fakedContext = _builder
            .Add(InitializeDataAndMetadata)
            .AddCrud()
            // It is important to useMessages() before useCrud because else you can't add execution mocks for crud messages.
            .UseMessages()
            .UseCrud()
            .Build();


        if (_testOutputHelper != null)
        {
            _serviceCollection
                .AddSingleton<ITestOutputHelper>(_ => _testOutputHelper);
        }

        var command = _serviceCollection
            .AddScoped<IOrganizationService>(_ => fakedContext.GetOrganizationService())
            .AddSingleton<TCommand>()
            .BuildServiceProvider()
            .GetRequiredService<TCommand>();

        return new CommandTestContext<TCommand, TCommandSettings>(command, fakedContext,
            _serviceCollection.BuildServiceProvider().GetRequiredService<IConfigResolver>());
    }

    public CommandTestContextBuilder<TCommand, TCommandSettings> WithRelationship(string schemaName,
        XrmFakedRelationship relationship)
    {
        _relationships[schemaName] = relationship;
        return this;
    }


    /// <summary>
    /// Adds the given entities as data to the xrm context
    /// </summary>
    /// <param name="data">The entities to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(IEnumerable<Entity> data)
    {
        _data = _data.Concat(data);
        return this;
    }

    /// <summary>
    /// Adds the given entity as data to the xrm context
    /// </summary>
    /// <param name="data">The entity to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(Entity data) => WithData(new[] {data});

    /// <summary>
    /// Adds the given entities as data to the xrm context.
    /// </summary>
    /// <param name="dataPreparer">The function to prepare context data.</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithData(
        Func<IXrmFakedContext, IEnumerable<Entity>> dataPreparer)
    {
        _dataPreparer = dataPreparer;
        return this;
    }

    /// <summary>
    /// Adds the entities in an tuple to the xrm context. Only tuple members that derive <see cref="Entity"/> class are
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
    /// Adds the given entity metadata to the xrm context
    /// </summary>
    /// <param name="metadata">The entity metadata to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithMetaData(EntityMetadata metadata)
    {
        return WithMetaData(new[] {metadata});
    }

    /// <summary>
    /// Adds the given entity metadata to the xrm context
    /// </summary>
    /// <param name="metadata">The entity metadata to add</param>
    /// <returns>self</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithMetaData(IEnumerable<EntityMetadata> metadata)
    {
        _metadata = _metadata.Concat(metadata);
        return this;
    }

    public CommandTestContextBuilder<TCommand, TCommandSettings> WithFakeMessageExecutor<TRequestMessage>(
        IFakeMessageExecutor messageExecutor) where TRequestMessage : OrganizationRequest
    {
        _builder.AddFakeMessageExecutor<TRequestMessage>(messageExecutor);
        return this;
    }

    /// <summary>
    /// Register an execution mock in the underlying <see cref="IXrmFakedContext"/>. See <see cref="XrmFakedContext."/>
    /// </summary>
    /// <param name="executionHandler"></param>
    /// <typeparam name="TRequestMessage"></typeparam>
    /// <returns></returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithExecutionMock<TRequestMessage>(
        OrganizationRequestExecution executionHandler)
        where TRequestMessage : OrganizationRequest
    {
        _builder.AddExecutionMock<TRequestMessage>(executionHandler);
        return this;
    }

    /// <summary>
    /// Specify the service collection that is used to generate the <see cref="PowerLogic{TConfig}"/> command.
    /// If no <see cref="IServiceCollection"/> is specified <see cref="TestServiceCollection"/> is used.
    /// If <see cref="IOrganizationService"/> is registered in the given service collection the registration will be
    /// overwritten with the organization service returned through <see cref="IXrmBaseContext.GetOrganizationService"/>.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithServiceCollection(
        IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
        return this;
    }

    /// <summary>
    /// Adds a custom configuration to the used <see cref="IXrmFakedContext"/>.
    /// </summary>
    /// <param name="contextHandler">A function containing the custom configuration which should be applied to the context.</param>
    /// <returns>The test context builder.</returns>
    public CommandTestContextBuilder<TCommand, TCommandSettings> WithCustomConfiguration(
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
