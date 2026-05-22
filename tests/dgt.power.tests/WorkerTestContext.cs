// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Linq.Expressions;
using dgt.power.common;
using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.tests;

/// <summary>
/// Test context for <see cref="PowerWorker{TConfig}"/> based components. Built via
/// <see cref="WorkerTestContextBuilder{TWorker,TWorkerSettings}"/>.
/// </summary>
public class WorkerTestContext<TWorker, TWorkerSettings>
    where TWorker : PowerWorker<TWorkerSettings>
    where TWorkerSettings : BaseProgramSettings
{
    private readonly TWorker _worker;
    public DataContext DataContext { get; }
    public FakeOrganizationServiceAsync FakedService { get; }
    public IConfigResolver ConfigResolver { get; }

    internal WorkerTestContext(TWorker worker, FakeOrganizationServiceAsync fakedService, IConfigResolver configResolver)
    {
        _worker = worker;
        DataContext = new DataContext(fakedService);
        FakedService = fakedService;
        ConfigResolver = configResolver;
    }

    public bool Execute(TWorkerSettings settings) => _worker.Invoke(settings);

    public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        query == null
            ? DataContext.CreateQuery<TEntity>().ToList()
            : DataContext.CreateQuery<TEntity>().Where(query).ToList();

    public TEntity GetById<TEntity>(Guid id) where TEntity : Entity, new() => FakedService
        .Retrieve(new TEntity().LogicalName, id, new ColumnSet(true)).ToEntity<TEntity>();

    public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        Get(query).Single();

    public TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>>? query = null) where TEntity : Entity =>
        Get(query).First();
}
