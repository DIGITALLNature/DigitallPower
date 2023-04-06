using System.Globalization;
using System.Security.Cryptography;
using NuGet.Packaging;
using Spectre.Console;

namespace dgt.power.push.Model;

internal record Webresources
{
    // Should be matching to WebResource.Options.WebResourceType
    public int Type { get; }
    public string Name { get; }
    public string DisplayName { get; }

    public WebresourceState State { get; set; }

    public Webresources(int type, string path, string folderPath, string solutionPrefix)
    {
        var content = File.ReadAllBytes(path);

        Type = type;
        Name = $"{solutionPrefix}_/{Path.GetRelativePath(folderPath, path)}";
        DisplayName =  Path.GetFileName(path);
        Content = Convert.ToBase64String(content);
        Hash = Convert.ToHexString(SHA256.HashData(content));
    }

    public string Content { get; }
    public string Hash { get; }
    public Guid XrmId { get; set; }
}
