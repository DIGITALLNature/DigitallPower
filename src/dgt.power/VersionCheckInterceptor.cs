// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.IO.IsolatedStorage;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using NuGet.Common;
using NuGet.Protocol.Core.Types;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

public class VersionCheckInterceptor : ICommandInterceptor
{
    private readonly IsolatedStorageFile _isolatedStorageFile;
    private readonly PackageMetadataResource _packageMetadataClient;
    public const string FileName = "last-updated.json";
    public const int CheckBarrierInDays = 3;

    public VersionCheckInterceptor(IsolatedStorageFile isolatedStorageFile, PackageMetadataResource packageMetadataClient)
    {
        _isolatedStorageFile = isolatedStorageFile;
        _packageMetadataClient = packageMetadataClient;
    }

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (CheckForBuildAgent())
        {
            AnsiConsole.MarkupLine("[grey]Build agent detected - abort check for new version.[/]");
            return;
        }

        var localPackageData = _isolatedStorageFile.FileExists(FileName) ? ReadOrCreateLastUpdateCheck() : new LastUpdateCheck();

        var today = DateTime.Today;
        var daysSinceLastCheck = (today - localPackageData.LastUpdateCheckOn.Date).TotalDays;
        if (daysSinceLastCheck > CheckBarrierInDays)
        {
            CheckForNewVersion();
            localPackageData.LastUpdateCheckOn = today;
            WriteLastUpdateCheck(localPackageData);
        }
    }

    private static bool CheckForBuildAgent()
    {
        var isAgent = false;

        // Azure DevOps
        isAgent |= Environment.GetEnvironmentVariable("BUILD_BUILDURI") != null;
        // GitHub
        isAgent |= Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;

        return isAgent;
    }

    private void CheckForNewVersion()
    {
        var packageMetadata = _packageMetadataClient.GetMetadataAsync(
            "dgt.power",
            false,
            false,
            new SourceCacheContext(),
            NullLogger.Instance,
            CancellationToken.None
        ).GetAwaiter().GetResult();
        var lastPackage = packageMetadata.OrderByDescending(package => package.Published).First();

        var localPackageVersion = Assembly.GetExecutingAssembly().GetName().Version;
        var remoteVersion = lastPackage.Identity.Version.Version;
        if (remoteVersion > localPackageVersion)
        {
            AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"Theres a new version [green]({remoteVersion})[/] available");
            AnsiConsole.MarkupLine("[yellow]Consider upgrading dgt.power by running:[/]");
            AnsiConsole.MarkupLine("[grey]dotnet tool update -g dgt.power[/]");
        }
    }

    private void WriteLastUpdateCheck(LastUpdateCheck lastUpdate)
    {
        using var storageStream = _isolatedStorageFile.OpenFile(FileName, FileMode.Create);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(lastUpdate);
        storageStream.Write(bytes, 0, bytes.Length);
    }

    private LastUpdateCheck ReadOrCreateLastUpdateCheck()
    {
        using var storageStream = _isolatedStorageFile.OpenFile(FileName, FileMode.Open);
        using var memoryStream = new MemoryStream();
        storageStream.CopyTo(memoryStream);
        return JsonSerializer.Deserialize<LastUpdateCheck>(memoryStream.ToArray()) ?? new LastUpdateCheck();
    }

    private sealed class LastUpdateCheck
    {
        [JsonPropertyName("update-last-checked-on")]
        public DateTime LastUpdateCheckOn { get; set; } = DateTime.MinValue;
    }
}
