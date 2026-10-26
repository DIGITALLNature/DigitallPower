// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.common.Extensions;

#pragma warning disable CA1031 // Intentional: Try-* boundary methods convert CRM failures to bool and log them.
public static class OrganizationServiceExtensions
{
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
