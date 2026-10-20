// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.common.Extensions;

#pragma warning disable CA1031 // Intentional: Try-* boundary methods convert CRM failures to bool and log them.
public static class OrganizationServiceExtensions
{
    public static Task<bool> TryUpdateAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(entity);
        return TryUpdateCoreAsync(service, entity, cancellationToken);
    }

    private static async Task<bool> TryUpdateCoreAsync(IOrganizationServiceAsync2 service, Entity entity, CancellationToken cancellationToken)
    {
        try
        {
            await service.ExecuteAsync(new UpdateRequest { Target = entity }, cancellationToken);
            LogToConsole("updated", TraceEventType.Verbose);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            LogToConsole($"update failed {entity.Id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static Task<bool> TryDeleteAsync(this IOrganizationServiceAsync2 service, string name, Guid id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(service);
        return TryDeleteCoreAsync(service, name, id, cancellationToken);
    }

    private static async Task<bool> TryDeleteCoreAsync(IOrganizationServiceAsync2 service, string name, Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await service.ExecuteAsync(new DeleteRequest { Target = new EntityReference(name, id) }, cancellationToken);
            LogToConsole("deleted", TraceEventType.Verbose);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            LogToConsole($"delete failed {id:B}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static Task<bool> TryCreateAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(entity);
        return TryCreateCoreAsync(service, entity, cancellationToken);
    }

    private static async Task<bool> TryCreateCoreAsync(IOrganizationServiceAsync2 service, Entity entity, CancellationToken cancellationToken)
    {
        try
        {
            await service.ExecuteAsync(new CreateRequest { Target = entity }, cancellationToken);
            LogToConsole("created", TraceEventType.Verbose);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            LogToConsole($"create failed {entity.LogicalName}: {e.RootMessage()}", TraceEventType.Error);
            return false;
        }

        return true;
    }

    public static Task<(bool Succeeded, T? Entity)> TryRetrieveAsync<T>(
        this IOrganizationServiceAsync2 service,
        string entityName,
        Guid entityId,
        ColumnSet columns,
        CancellationToken cancellationToken = default)
        where T : Entity
    {
        ArgumentNullException.ThrowIfNull(service);
        return TryRetrieveCoreAsync<T>(service, entityName, entityId, columns, cancellationToken);
    }

    private static async Task<(bool Succeeded, T? Entity)> TryRetrieveCoreAsync<T>(
        IOrganizationServiceAsync2 service,
        string entityName,
        Guid entityId,
        ColumnSet columns,
        CancellationToken cancellationToken)
        where T : Entity
    {
        try
        {
            var entity = (await service.RetrieveAsync(entityName, entityId, columns, cancellationToken)).ToEntity<T>();
            LogToConsole("retrieved", TraceEventType.Verbose);
            return (true, entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            LogToConsole($"retrieve failed {entityName}({entityId:D}): {e.RootMessage()}", TraceEventType.Error);
            return (false, null);
        }
    }

    public static Task<(bool Succeeded, TO? Response)> TryExecuteAsync<TR, TO>(
        this IOrganizationServiceAsync2 service,
        TR request,
        CancellationToken cancellationToken = default)
        where TR : OrganizationRequest
        where TO : OrganizationResponse
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(request);
        return TryExecuteCoreAsync<TR, TO>(service, request, cancellationToken);
    }

    private static async Task<(bool Succeeded, TO? Response)> TryExecuteCoreAsync<TR, TO>(
        IOrganizationServiceAsync2 service,
        TR request,
        CancellationToken cancellationToken)
        where TR : OrganizationRequest
        where TO : OrganizationResponse
    {
        try
        {
            var response = (TO)await service.ExecuteAsync(request, cancellationToken);
            LogToConsole($"executed {request.RequestName}", TraceEventType.Verbose);
            return (true, response);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            LogToConsole($"execute failed {request.RequestName}: {e.RootMessage()}", TraceEventType.Error);
            return (false, null);
        }
    }

    private static void LogToConsole(string message, TraceEventType type) =>
        AnsiConsole.Console.MarkupLine($"[underline red]{type}:[/]  {message}");
}
#pragma warning restore CA1031
