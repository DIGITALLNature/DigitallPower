// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.Telemetry;
using dgt.power.common.Storage;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

public class VersionCheckInterceptor(
    StateStore stateStore,
    PackageMetadataResource packageMetadataClient,
    IAnsiConsole console)
    : ICommandInterceptor
{
    private const int CheckBarrierInDays = 3;

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (TelemetryConfig.IsCi)
        {
            console.MarkupLine("[grey]Build agent detected - abort check for new version.[/]");
            return;
        }

        var today = DateTime.Today;
        var daysSinceLastCheck = (today - stateStore.LastVersionCheckOn.Date).TotalDays;
        if (daysSinceLastCheck > CheckBarrierInDays)
        {
            CheckForNewVersion();
            stateStore.SetLastVersionCheckOn(today);
        }
    }

    private void CheckForNewVersion()
    {
        using var sourceCache = new SourceCacheContext();
        var packageMetadata = RunSynchronously(packageMetadataClient.GetMetadataAsync(
            "dgt.power",
            false,
            false,
            sourceCache,
            NullLogger.Instance,
            CancellationToken.None
        ));
        var lastPackage = packageMetadata.OrderByDescending(package => package.Published).First();

        var localPackageVersion = typeof(VersionCheckInterceptor).Assembly.GetName().Version;
        var remoteVersion = lastPackage.Identity.Version.Version;
        if (remoteVersion > localPackageVersion)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"Theres a new version [green]({remoteVersion})[/] available");
            console.MarkupLine("[yellow]Consider upgrading dgt.power by running:[/]");
            console.MarkupLine("[grey]dotnet tool update -g dgt.power[/]");
        }
    }

    [SuppressMessage(
        "Usage",
        "VSTHRD002:Avoid problematic synchronous waits",
        Justification = "Spectre's ICommandInterceptor is synchronous. Version check runs once in CLI startup and has no async hook.")]
    private static T RunSynchronously<T>(Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();
}
