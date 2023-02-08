using System.Linq.Expressions;
using dgt.power.common;
using dgt.power.dataverse;
using FakeItEasy;
using FakeXrmEasy.Abstractions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.tests;

/// <summary>
/// This context can be used to test <see cref="PowerLogic{TConfig}"/> commands and is build with <see cref="CommandTestContextBuilder{TCommand,TCommandSettings}"/>
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TCommandSettings"></typeparam>
public class CommandTestContext<TCommand, TCommandSettings> where TCommand : ICommand<TCommandSettings>
    where TCommandSettings : CommandSettings
{
    private readonly TCommand _command;
    public DataContext DataContext { get; }
    public IXrmFakedContext FakedContext { get; }
    public IConfigResolver ConfigResolver { get; }

    internal CommandTestContext(TCommand command, IXrmFakedContext fakedContext, IConfigResolver configResolver)
    {
        _command = command;
        DataContext = new DataContext(fakedContext.GetOrganizationService());
        FakedContext = fakedContext;
        ConfigResolver = configResolver;
    }

    public bool Execute(TCommandSettings settings)
    {
        return _command.Execute(GetCommandContext(), settings).GetAwaiter().GetResult() == 0;
    }

    public ValidationResult Validate(TCommandSettings settings) => _command.Validate(GetCommandContext(), settings);

    private static CommandContext GetCommandContext()
    {
        var commandContext = new CommandContext(A.Dummy<IRemainingArguments>(), "test", null);
        return commandContext;
    }

    public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        query == null
            ? DataContext.CreateQuery<TEntity>().ToList()
            : DataContext.CreateQuery<TEntity>().Where(query).ToList();

    public TEntity GetById<TEntity>(Guid id) where TEntity : Entity, new() => FakedContext.GetOrganizationService()
        .Retrieve(new TEntity().LogicalName, id, new ColumnSet(true)).ToEntity<TEntity>();

    public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        Get(query).Single();

    public TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        Get(query).First();
}
