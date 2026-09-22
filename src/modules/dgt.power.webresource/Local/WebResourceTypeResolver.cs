// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;

namespace dgt.power.webresource.Local;

public static class WebResourceTypeResolver
{
    private static readonly Dictionary<string, int> s_types =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [".html"] = WebResource.Options.WebResourceType.WebpageHTML,
            [".css"] = WebResource.Options.WebResourceType.StyleSheetCSS,
            [".js"] = WebResource.Options.WebResourceType.ScriptJScript,
            [".xml"] = WebResource.Options.WebResourceType.DataXML,
            [".png"] = WebResource.Options.WebResourceType.PNGFormat,
            [".jpg"] = WebResource.Options.WebResourceType.JPGFormat,
            [".gif"] = WebResource.Options.WebResourceType.GIFFormat,
            [".xap"] = WebResource.Options.WebResourceType.SilverlightXAP,
            [".xsl"] = WebResource.Options.WebResourceType.StyleSheetXSL,
            [".ico"] = WebResource.Options.WebResourceType.ICOFormat,
            [".svg"] = WebResource.Options.WebResourceType.VectorFormatSVG,
            [".resx"] = WebResource.Options.WebResourceType.StringRESX
        };

    public static bool TryResolve(string path, out int type)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        return s_types.TryGetValue(Path.GetExtension(path), out type);
    }
}
