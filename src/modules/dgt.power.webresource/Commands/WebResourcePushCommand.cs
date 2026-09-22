// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.webresource.Execution;
using dgt.power.webresource.Local;
using dgt.power.webresource.Repositories;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;

namespace dgt.power.webresource.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebResourcePushCommand(ITracer tracer, Microsoft.Xrm.Sdk.IOrganizationService connection, IConfigResolver configResolver, IAnsiConsole console)
    : PowerLogic<WebResourcePushSettings>(tracer, connection, configResolver, console)
{
    protected override Task<bool> InvokeAsync(WebResourcePushSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        return InvokeCoreAsync(settings, cancellationToken);
    }

    private async Task<bool> InvokeCoreAsync(WebResourcePushSettings settings, CancellationToken cancellationToken)
    {
        Tracer.Start(this);
        try
        {
            if (settings.DryRun)
            {
                Console.MarkupLine("[yellow]Dry run - no changes will be written to Dataverse[/]");
            }

            var mapping = settings.MappingFile is null ? null : WebResourceMappingFile.Load(settings.MappingFile);
            var local = WebResourceDiscovery.Discover(settings.Target, settings.PublisherPrefix, mapping?.Mappings, settings.Name);
            if (local.Count == 0)
            {
                Console.MarkupLine($"[yellow]No supported webresources found in '{Markup.Escape(settings.Target)}'[/]");
                return Tracer.End(this, true);
            }

            var service = (IOrganizationServiceAsync2)Connection;
            var executor = new WebResourcePushExecutor(new WebResourceRepository(service), new SolutionRepository(service), Console);
            await executor.ExecuteAsync(local, new WebResourcePushOptions(settings.Solution, settings.Publish, settings.DeleteObsolete, settings.DryRun), cancellationToken);

            return Tracer.End(this, true);
        }
        catch
        {
            Tracer.End(this, false);
            throw;
        }
    }

}
