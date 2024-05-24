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
#pragma warning disable S3242
    public static bool TryAssign(this IOrganizationService service, EntityReference record, SystemUser owner)
#pragma warning restore S3242
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(owner);

        try
        {
            service.Execute(new AssignRequest
            {
                Target = record,
                Assignee = owner.ToEntityReference()
            });
        }
        catch (Exception e)
        {
            LogToConsole($"assign failed {record.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TrySetState(this IOrganizationService service, EntityReference reference, int state, int status)
    {
        ArgumentNullException.ThrowIfNull(reference);

        try
        {
            service.Update(new Entity(reference.LogicalName, reference.Id)
            {
                ["statecode"] = new OptionSetValue(state),
                ["statuscode"] = new OptionSetValue(status)
            });
            LogToConsole($"state set; state:{state}, status:{status}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"set state failed {reference.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TrySetStateDocumentTemplate(this IOrganizationService service, EntityReference reference,
        bool status)
    {
        ArgumentNullException.ThrowIfNull(reference);

        try
        {
            service.Update(new DocumentTemplate(reference.Id)
            {
                Status = status
            });
            LogToConsole($"state set; status:{status}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"set state failed {reference.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryCreate(this IOrganizationService service, Entity entity, out Guid id)
    {
        Debug.Assert(service != null, nameof(service) + " != null");
        ArgumentNullException.ThrowIfNull(entity);

        id = Guid.Empty;
        try
        {
            id = service.Create(entity);
            LogToConsole("created", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"create failed {entity.LogicalName}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryUpdate(this IOrganizationService service, Entity entity)
    {
        Debug.Assert(service != null, nameof(service) + " != null");
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            service.Update(entity);
            LogToConsole("updated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"update failed {entity.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryDelete(this IOrganizationService service, string name, Guid id)
    {
        Debug.Assert(service != null, nameof(service) + " != null");

        try
        {
            service.Delete(name, id);
            LogToConsole("deleted", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"delete failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryAssociate(this IOrganizationService service, string name, Guid id, Relationship relationship,
        EntityReferenceCollection references)
    {
        Debug.Assert(service != null, nameof(service) + " != null");

        try
        {
            service.Associate(name, id, relationship, references);
            LogToConsole("associated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"associate failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryDisassociate(this IOrganizationService service, string name, Guid id,
        Relationship relationship,
        EntityReferenceCollection references)
    {
        Debug.Assert(service != null, nameof(service) + " != null");

        try
        {
            service.Disassociate(name, id, relationship, references);
            LogToConsole("disassociated", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"disassociate failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryExecute<TR, TO>(this IOrganizationService service, TR request, out TO response)
        where TR : OrganizationRequest where TO : OrganizationResponse
    {
        Debug.Assert(service != null, nameof(service) + " != null");
        ArgumentNullException.ThrowIfNull(request);

        response = default!;
        try
        {
            response = (TO)service.Execute(request);
            LogToConsole($"executed {request.RequestName}", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"execute failed {request.RequestName}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static bool TryRetrieve<T>(this IOrganizationService service, string entityName, Guid entityId,
        ColumnSet columns, out T entity)
        where T : Entity
    {
        Debug.Assert(service != null, nameof(service) + " != null");


        entity = default!;
        try
        {
            entity = service.Retrieve(entityName, entityId, columns).ToEntity<T>();
            LogToConsole("retrieved", TraceEventType.Verbose);
        }
        catch (Exception e)
        {
            LogToConsole($"retrieve failed {entityName}({entityId:D}): {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    private static void LogToConsole(string message, TraceEventType type) =>
        AnsiConsole.Console.MarkupLine($"[underline red]{type}:[/]  {message}");
}
