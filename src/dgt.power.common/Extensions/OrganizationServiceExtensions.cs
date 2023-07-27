// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.common.Extensions;

public static class OrganizationServiceExtensions
{
    public static IEnumerable<TEntity> RetrieveMultiple<TEntity>(this IOrganizationService service, QueryBase query)
        where TEntity : Entity =>
        service.RetrieveMultiple(query)
            ?.Entities
            ?.Select(x => x.ToEntity<TEntity>())
            .ToList()
        ?? Enumerable.Empty<TEntity>();

    public static TEntity Retrieve<TEntity>(this IOrganizationService service, EntityReference entityReference, ColumnSet columns)
        where TEntity : Entity =>
        service.Retrieve<TEntity>(entityReference.LogicalName, entityReference.Id, columns);

    public static TEntity Retrieve<TEntity>(this IOrganizationService service, string logicalName, Guid id, ColumnSet columns)
        where TEntity : Entity =>
        service.Retrieve(logicalName, id, columns).ToEntity<TEntity>();

    public static bool TryAssign(this IOrganizationService service, EntityReference record, SystemUser Owner)
    {
        try
        {
            service.Execute(new AssignRequest
            {
                Target = record,
                Assignee = Owner.ToEntityReference()
            });
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"assign failed {record.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TrySetState(this IOrganizationService service, EntityReference reference, int state, int status)
    {
        try
        {
            service.Update(new Entity(reference.LogicalName, reference.Id)
            {
                ["statecode"] = new OptionSetValue(state),
                ["statuscode"] = new OptionSetValue(status)
            });
            AnsiConsole.Console.Log($"state set; state:{state}, status:{status}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"set state failed {reference.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TrySetStateDocumentTemplate(this IOrganizationService service, EntityReference reference, bool status)
    {
        try
        {
            service.Update(new DocumentTemplate(reference.Id)
            {
                Status = status
            });
            AnsiConsole.Console.Log($"state set; status:{status}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"set state failed {reference.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryCreate(this IOrganizationService service, Entity entity, out Guid id)
    {
        id = Guid.Empty;
        try
        {
            id = service.Create(entity);
            AnsiConsole.Console.Log("created", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"create failed {entity.LogicalName}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryUpdate(this IOrganizationService service, Entity entity)
    {
        try
        {
            service.Update(entity);
            AnsiConsole.Console.Log("updated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"update failed {entity.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryDelete(this IOrganizationService service, string name, Guid id)
    {
        try
        {
            service.Delete(name, id);
            AnsiConsole.Console.Log("deleted", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"delete failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryAssociate(this IOrganizationService service, string name, Guid id, Relationship relationship,
        EntityReferenceCollection references)
    {
        try
        {
            service.Associate(name, id, relationship, references);
            AnsiConsole.Console.Log("associated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"associate failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryDisassociate(this IOrganizationService service, string name, Guid id, Relationship relationship,
        EntityReferenceCollection references)
    {
        try
        {
            service.Disassociate(name, id, relationship, references);
            AnsiConsole.Console.Log("disassociated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"disassociate failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryExecute<TR, TO>(this IOrganizationService service, TR request, out TO response)
        where TR : OrganizationRequest where TO : OrganizationResponse
    {
        response = default!;
        try
        {
            response = (TO)service.Execute(request);
            AnsiConsole.Console.Log($"executed {request.RequestName}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"execute failed {request.RequestName}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryRetrieve<T>(this IOrganizationService service, string entityName, Guid entityId, ColumnSet columns, out T entity)
        where T : Entity
    {
        entity = default!;
        try
        {
            entity = service.Retrieve(entityName, entityId, columns).ToEntity<T>();
            AnsiConsole.Console.Log("retrieved", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            AnsiConsole.Console.Log($"retrieve failed {entityName}({entityId:D}): {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static TO Execute<TR, TO>(this IOrganizationService service, TR request)
        where TR : OrganizationRequest
        where TO : OrganizationResponse =>
        (TO)service.Execute(request);
}
