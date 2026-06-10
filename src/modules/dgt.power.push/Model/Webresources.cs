// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using Spectre.Console;

namespace dgt.power.push.Model;

internal sealed record Webresources(
    int Type,
    string Name,
    string DisplayName,
    string? Content,
    string? Hash,
    WebresourceState? State = null,
    Guid? XrmId = null)
{
    // Should be matching to WebResource.Options.WebResourceType
    public WebresourceState? State { get; set; } = State;
    public Guid? XrmId { get; set; } = XrmId;

    public Webresources(int type, string name, WebresourceState webresourceState, Guid xrmId) : this(type, name, name, null, null, webresourceState, xrmId) { }

    public static Webresources FromFile(string localFilePath, string localFolderPath, int type, string solutionPrefix, IDictionary<string, string>? fileMapping = null, IAnsiConsole? console = null)
    {
        var content = File.ReadAllBytes(localFilePath);
        var hash = Convert.ToHexString(SHA256.HashData(content));
        var relativePath = Path.GetRelativePath(localFolderPath, localFilePath).Replace('\\', '/');

        string name;
        string displayName;

        if (fileMapping is not null && fileMapping.TryGetValue(relativePath, out var mappedPath))
        {
            name = mappedPath;
            displayName = Path.GetFileName(mappedPath);

            (console ?? AnsiConsole.Console).MarkupLine($" - [yellow]Applying mapping[/]: [blue]{relativePath}[/] {Emoji.Known.RightArrow} [green]{name}[/]");
        }
        else if (relativePath.StartsWith($"{solutionPrefix}_/", StringComparison.InvariantCulture))
        {
            name = relativePath;
            displayName = Path.GetFileName(localFilePath);
        }
        else
        {
            name = $"{solutionPrefix}_/{relativePath}";
            displayName = Path.GetFileName(localFilePath);
        }

        return new Webresources(type, name, displayName, Convert.ToBase64String(content), hash);
    }
}
