// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.Telemetry;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

public class VersionCheckInterceptor(
    IsolatedStorageFile isolatedStorageFile,
    PackageMetadataResource packageMetadataClient,
    IAnsiConsole console)
    : ICommandInterceptor
{
    private const string FileName = "last-updated.json";
    private const int CheckBarrierInDays = 3;
    private readonly IAnsiConsole _console = console;

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (TelemetryConfig.IsCi)
        {
            _console.MarkupLine("[grey]Build agent detected - abort check for new version.[/]");
            return;
        }

        var localPackageData = isolatedStorageFile.FileExists(FileName) ? ReadOrCreateLastUpdateCheck() : new LastUpdateCheck();

        var today = DateTime.Today;
        var daysSinceLastCheck = (today - localPackageData.LastUpdateCheckOn.Date).TotalDays;
        if (daysSinceLastCheck > CheckBarrierInDays)
        {
            CheckForNewVersion();
            localPackageData.LastUpdateCheckOn = today;
            WriteLastUpdateCheck(localPackageData);
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
            _console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"Theres a new version [green]({remoteVersion})[/] available");
            _console.MarkupLine("[yellow]Consider upgrading dgt.power by running:[/]");
            _console.MarkupLine("[grey]dotnet tool update -g dgt.power[/]");
        }
    }

    private void WriteLastUpdateCheck(LastUpdateCheck lastUpdate)
    {
        using var storageStream = isolatedStorageFile.OpenFile(FileName, FileMode.Create);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(lastUpdate);
        storageStream.Write(bytes, 0, bytes.Length);
    }

    private LastUpdateCheck ReadOrCreateLastUpdateCheck()
    {
        using var storageStream = isolatedStorageFile.OpenFile(FileName, FileMode.Open);
        using var memoryStream = new MemoryStream();
        storageStream.CopyTo(memoryStream);
        return JsonSerializer.Deserialize<LastUpdateCheck>(memoryStream.ToArray()) ?? new LastUpdateCheck();
    }

    [SuppressMessage(
        "Usage",
        "VSTHRD002:Avoid problematic synchronous waits",
        Justification = "Spectre's ICommandInterceptor is synchronous. Version check runs once in CLI startup and has no async hook.")]
    private static T RunSynchronously<T>(Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();

    private sealed class LastUpdateCheck
    {
        [JsonPropertyName("update-last-checked-on")]
        public DateTime LastUpdateCheckOn { get; set; } = DateTime.MinValue;
    }
}
