// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using Spectre.Console;

namespace dgt.power.push.Model;

internal record Webresources
{
    // Should be matching to WebResource.Options.WebResourceType
    public int Type { get; }
    public string Name { get; }
    public string DisplayName { get; }
    public string? Content { get; }
    public string? Hash { get; }
    public WebresourceState? State { get; set; }
    public Guid? XrmId { get; set; }

    public Webresources(int type, string name, string displayName, string? content, string? hash, WebresourceState? webresourceState = default, Guid? xrmId = default)
    {
        Type = type;
        Name = name;
        DisplayName = displayName;
        Content = content;
        Hash = hash;
        State = webresourceState;
        XrmId = xrmId;
    }

    public Webresources(int type, string name, WebresourceState webresourceState, Guid xrmId) : this(type, name, name, null, null, webresourceState, xrmId) { }

    public static Webresources FromFile(string localFilePath, string localFolderPath, int type, string solutionPrefix, IDictionary<string, string> fileMapping = default)
    {
        var content = File.ReadAllBytes(localFilePath);
        var hash = Convert.ToHexString(SHA256.HashData(content));
        var relativePath = Path.GetRelativePath(localFolderPath, localFilePath).Replace('\\', '/');

        string name;
        string displayName;

        if (fileMapping?.ContainsKey(relativePath) == true)
        {
            name = fileMapping[relativePath];
            displayName = Path.GetFileName(fileMapping[relativePath]);

            AnsiConsole.MarkupLine($" - [yellow]Applying mapping[/]: [blue]{relativePath}[/] {Emoji.Known.RightArrow} [green]{name}[/]");
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
