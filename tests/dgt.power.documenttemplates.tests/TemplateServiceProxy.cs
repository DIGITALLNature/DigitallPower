// Copyright (c) DIGITALL Nature. All rights reserved

using System.Reflection;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.documenttemplates.tests;

public class TemplateServiceProxy : DispatchProxy
{
    public List<Entity> Targets { get; } = [];
    public List<OrganizationRequest> Writes { get; } = [];
    public List<(int Page, string? Cookie)> Pages { get; } = [];
    public Dictionary<string, EntityMetadata> Metadata { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Func<Entity, bool>? RejectWrite { get; set; }
    public Action<Entity>? AfterWrite { get; set; }
    public Func<QueryExpression, EntityCollection>? ReadPage { get; set; }
    public List<string> MetadataReads { get; } = [];

    public static (IOrganizationServiceAsync2 Service, TemplateServiceProxy State) Create()
    {
        var service = Create<IOrganizationServiceAsync2, TemplateServiceProxy>();
        // DispatchProxy generates a runtime type deriving from this proxy and implementing the service interface.
        // ReSharper disable once SuspiciousTypeConversion.Global
        return (service, (TemplateServiceProxy)service);
    }

    protected override object Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        if (args?.LastOrDefault() is CancellationToken token)
            token.ThrowIfCancellationRequested();
        if (targetMethod?.Name == "RetrieveMultipleAsync")
        {
            var query = (QueryExpression)args![0]!;
            Pages.Add((query.PageInfo.PageNumber, query.PageInfo.PagingCookie));
            return Task.FromResult(ReadPage?.Invoke(query) ?? new EntityCollection(Targets));
        }
        if (targetMethod?.Name == "ExecuteAsync")
        {
            var request = (OrganizationRequest)args![0]!;
            if (request is RetrieveEntityRequest retrieve)
            {
                MetadataReads.Add(retrieve.LogicalName);
                if (!Metadata.TryGetValue(retrieve.LogicalName, out var entityMetadata))
                    throw new InvalidDataException($"Missing metadata {retrieve.LogicalName}");
                return Task.FromResult<OrganizationResponse>(new RetrieveEntityResponse
                {
                    Results = new ParameterCollection { ["EntityMetadata"] = entityMetadata }
                });
            }
            if (request is not (CreateRequest or UpdateRequest))
                throw new InvalidOperationException($"Forbidden request {request.RequestName}");
            Writes.Add(request);
            var entity = (Entity)request.Parameters["Target"];
            if (RejectWrite?.Invoke(entity) == true)
                throw new InvalidOperationException("Deliberate server failure");
            if (request is CreateRequest)
            {
                if (Targets.Any(target => target.Id == entity.Id))
                    throw new InvalidOperationException("Duplicate ID");
                var created = new Entity(entity.LogicalName, entity.Id);
                foreach (var (key, value) in entity.Attributes)
                    created[key] = value;
                Targets.Add(created);
            }
            else
            {
                var target = Targets.Single(value => value.Id == entity.Id);
                foreach (var (key, value) in entity.Attributes)
                    target[key] = value;
            }
            AfterWrite?.Invoke(entity);
            return Task.FromResult<OrganizationResponse>(request is CreateRequest
                ? new CreateResponse { Results = new ParameterCollection { ["id"] = entity.Id } }
                : new UpdateResponse());
        }
        throw new InvalidOperationException($"Unexpected service call {targetMethod?.Name}");
    }
}
